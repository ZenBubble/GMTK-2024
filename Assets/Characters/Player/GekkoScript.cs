using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GekkoScript : MonoBehaviour
{
	public SavedPlayerData Data;
	
    private Rigidbody2D rigidBody;

    private Vector3 originalScale;
	private Animator anim;
    [SerializeField] private ContactFilter2D contactFilter;

    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }

    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnGroundTime { get; private set; }

    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }
    
    //Set all of these up in the inspector
    [Header("Checks")] 
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        rigidBody.mass = Data.initialPlayerMass;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
	    Run(1);
    }

    private void Update()
    {
		
	    #region TIMERS
	    LastOnGroundTime -= Time.deltaTime;

	    LastPressedJumpTime -= Time.deltaTime;
	    #endregion

	    #region INPUT HANDLER
	    _moveInput.x = Input.GetAxisRaw("Horizontal");
	    _moveInput.y = Input.GetAxisRaw("Vertical");

	    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) 
	        || Input.GetKeyDown(KeyCode.LeftArrow))
	    {
		    rigidBody.mass = Math.Max(rigidBody.mass - Data.runMassConsumption, Data.minPlayerMass);
			anim.SetFloat("xVelocity", 1);
	    }
	    if (_moveInput.x != 0) {
		    CheckDirectionToFace(_moveInput.x > 0);
			anim.SetFloat("xVelocity", 0); // set animation to idle
		}

	    if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
	    {
		    OnJumpInput();
	    }

	    if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
	    {
		    OnJumpUpInput();
	    }
	    #endregion
	    
		#region COLLISION CHECKS
		if (!IsJumping)
		{
			//Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, groundLayer) && !IsJumping) //checks if set box overlaps with ground
			{
				LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }

			//Right Wall Check
			if ((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, groundLayer) && !IsFacingRight)) ;

			//Right Wall Check
			if ((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, groundLayer) && IsFacingRight));
			
		}
		#endregion

		#region JUMP CHECKS
		if (IsJumping && rigidBody.velocity.y < 0)
		{
			IsJumping = false;
		}
		

		if (LastOnGroundTime > 0 && !IsJumping)
        {
			_isJumpCut = false;

			if(!IsJumping)
				_isJumpFalling = false;
		}

		//Jump
		if (CanJump() && LastPressedJumpTime > 0)
		{
			IsJumping = true;
			_isJumpCut = false;
			_isJumpFalling = false;
			Jump();
		}
		#endregion	    
	    
    }
    
    private void Run(float lerpAmount)
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = _moveInput.x * Data.runMaxSpeed;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(rigidBody.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		//Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
		if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(rigidBody.velocity.y) < Data.jumpHangTimeThreshold)
		{
			accelRate *= Data.jumpHangAccelerationMult;
			targetSpeed *= Data.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if(Data.doConserveMomentum && Mathf.Abs(rigidBody.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rigidBody.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
			accelRate = 0; 
		}
		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - rigidBody.velocity.x;
		//Calculate force along x-axis to apply to thr player
		float movement = speedDif * accelRate;
		
		//Convert this to a vector and apply to rigidbody
		rigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*	
		 * For those interested here is what AddForce() will do
		 * rigidBody.velocity = new Vector2(rigidBody.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / rigidBody.mass, rigidBody.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}
    
	private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    
    
	#region INPUT CALLBACKS
	//Methods which whandle input detected in Update()
	public void OnJumpInput()
	{
		LastPressedJumpTime = Data.jumpInputBufferTime;
	}

	public void OnJumpUpInput()
	{
		if (CanJumpCut())
			_isJumpCut = true;
	}
	#endregion    
    private void Jump()
    {
	    //Ensures we can't call Jump multiple times from one press
	    LastPressedJumpTime = 0;
	    LastOnGroundTime = 0;

	    #region Perform Jump
	    //We increase the force applied if we are falling
	    //This means we'll always feel like we jump the same amount 
	    //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
	    float force = Data.jumpForce;
	    if (rigidBody.velocity.y < 0)
		    force -= rigidBody.velocity.y;

	    rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
	    rigidBody.mass = Math.Max(rigidBody.mass - Data.jumpMassConsumption, Data.minPlayerMass);
	    #endregion
    }
    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
	    if (isMovingRight != IsFacingRight)
		    Turn();
    }

    private bool CanJump()
    {
	    return LastOnGroundTime > 0 && !IsJumping;
    }

    private bool CanJumpCut()
    {
	    return IsJumping && rigidBody.velocity.y > 0;
    }
    #endregion

}
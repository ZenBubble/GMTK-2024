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
	private Boolean jumping;
	private Boolean isFacingRight;
    [SerializeField] private ContactFilter2D contactFilter;


    //Jump
    private Vector2 _moveInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        rigidBody.mass = Data.initialPlayerMass;
		isFacingRight = (originalScale.x > 0);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
	    Run(1);
    }

    private void Update()
    {
	    #region INPUT HANDLER
	    _moveInput.x = Input.GetAxisRaw("Horizontal");
	    _moveInput.y = Input.GetAxisRaw("Vertical");

	    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) 
	        || Input.GetKeyDown(KeyCode.LeftArrow))
	    {
		    // rigidBody.mass = Math.Max(rigidBody.mass - Data.runMassConsumption, Data.minPlayerMass);
	    }
	    if (_moveInput.x != 0) {
		    CheckDirectionToFace(_moveInput.x > 0);
		}

	    if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
	    {
			jumping = true;
	    }

	    if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
	    {
			jumping = false;
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
		accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		#endregion

		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if(Data.doConserveMomentum && Mathf.Abs(rigidBody.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rigidBody.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
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

		if (movement > 0 || movement < 0) {
			anim.SetFloat("xVelocity", 1);
		} else {
			anim.SetFloat("xVelocity", 0);
		}

		if (!isGrounded()) {
			anim.SetBool("isAirborne", true);
		} else {
			anim.SetBool("isAirborne", false);
		}
		
		//Convert this to a vector and apply to rigidbody
		rigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*	
		 * For those interested here is what AddForce() will do
		 * rigidBody.velocity = new Vector2(rigidBody.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / rigidBody.mass, rigidBody.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/

		if (jumping && isGrounded())
		{
			Jump();
		}
	}
    
	private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;
	}
    
    
	#region INPUT CALLBACKS
	//Methods which whandle input detected in Update()
	#endregion    
    private void Jump()
    {
	    #region Perform Jump
	    //We increase the force applied if we are falling
	    //This means we'll always feel like we jump the same amount 
	    //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
	    float force = Data.jumpForce;
	    if (rigidBody.velocity.y < 0)
		    force -= rigidBody.velocity.y;

	    rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
	    //rigidBody.mass = Math.Max(rigidBody.mass - Data.jumpMassConsumption, Data.minPlayerMass);
	    #endregion
    }
    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
	    if (isMovingRight != isFacingRight)
		{
			isFacingRight = isMovingRight;
            Turn();
        }
    }

	private Boolean isGrounded()
	{
        return rigidBody.IsTouching(contactFilter);
    }
    #endregion

}
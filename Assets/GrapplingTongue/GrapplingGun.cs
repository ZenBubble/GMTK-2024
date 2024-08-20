using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasePrefabs;
using Unity.VisualScripting;

/// <summary>
/// Grabs onto defined layers or all surfaces. Can lasso enemies with both spring joints and rb2Ds. 
/// </summary>
public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")] public GrapplingTongue grappleRope;

    [Header("Main Camera:")] public Camera m_camera;

    [Header("Transform Ref:")] public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")] public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")] [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")] [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [Header("Grappalable objects")]
    [SerializeField] private LayerMask layerMask;

    private float defaultGravityScale;
    private GameObject connectedObject;

    private Boolean shouldLaunch;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")] [SerializeField]
    private bool launchToPoint = true;

    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")] [SerializeField]
    private bool autoConfigureDistance = false;

    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    private LineRenderer ropeLineRenderer;
    [SerializeField] private float timeToDisableRope = 0.5f;
    private float disableTimer = 0f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        connectedObject = null;
        defaultGravityScale = m_rigidbody.gravityScale;
        ropeLineRenderer = grappleRope.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetGrapplePoint();
            audioManager.PlaySFX(audioManager.chameleonSlurp);
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            if (grappleRope.enabled)
            {
                if (connectedObject)
                {
                    grapplePoint = connectedObject.transform.position;
                }
                else if (!shouldLaunch)
                {
                    int idx = ropeLineRenderer.positionCount - 1;
                    Vector3 pos = ropeLineRenderer.GetPosition(idx);
                    if (new Vector2(pos.x, pos.y).Equals(grapplePoint)) {
                        disableTimer += Time.deltaTime;
                        if (disableTimer > timeToDisableRope)
                        {
                            grappleRope.enabled = false;
                            disableTimer = 0;
                        }
                    }
                    
                }
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            disableGrapple();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (Math.Sign(gunHolder.localScale.x) == -1)
        {
            angle += 180;
        }
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward),
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        disableTimer = 0;
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, maxDistnace, 
                ~LayerMask.GetMask("Player"));
            if (_hit)
            {
                if (_hit.transform.gameObject.tag == "Lever")
                {
                    _hit.transform.gameObject.GetComponent<LeverController>().Toggle();
                }
                if ((layerMask & (1 << _hit.collider.gameObject.layer)) != 0) {
                    // hit a grappalable object
                    if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                    {
                        if (_hit.transform.gameObject.HasComponent<SpringJoint2D>()
                            && _hit.transform.gameObject.HasComponent<Rigidbody2D>())
                        {
                            connectedObject = _hit.transform.gameObject;
                            if (connectedObject.tag == "Enemy")
                            {
                                connectedObject.GetComponent<Enemy>().setGrab(true);
                            }
                        }
                        grapplePoint = _hit.point;
                        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                        grappleRope.enabled = true;
                        shouldLaunch = true;
                        return;
                    }
                } 
                else
                {
                    // hit an object but not a grappalable one
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    shouldLaunch = false;
                    return;
                }
                
            }
        }
        // hit nothing
        grapplePoint = firePoint.position + 
                       (Vector3)((Vector2)m_camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)firePoint.position).normalized * maxDistnace;
        grappleDistanceVector = grapplePoint -(Vector2)gunPivot.position;
        grappleRope.enabled = true;
        shouldLaunch = false;
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            if (!shouldLaunch)
            {
                return;
            }
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;
                    Vector2 distanceVector = firePoint.position - gunHolder.position;
                    if (connectedObject)
                    {
                        SpringJoint2D otherSpring = connectedObject.GetComponent<SpringJoint2D>();
                        Rigidbody2D otherRb = connectedObject.GetComponent<Rigidbody2D>();
                        
                        otherSpring.connectedAnchor = gunHolder.transform.position;
                        otherSpring.distance = distanceVector.magnitude;
                        otherSpring.frequency = launchSpeed / otherRb.mass;
                        otherSpring.enabled = true;
                    }

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed / m_rigidbody.mass;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    public GameObject getConnectedObject()
    {
        return connectedObject;
    }

    public void disableGrapple()
    {
        disableTimer = 0;
        if (connectedObject)
        {
            connectedObject.GetComponent<SpringJoint2D>().enabled = false;
            if (connectedObject.tag == "Enemy")
            {
                connectedObject.GetComponent<Enemy>().setGrab(false);
            }
        }

        connectedObject = null;
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = defaultGravityScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}
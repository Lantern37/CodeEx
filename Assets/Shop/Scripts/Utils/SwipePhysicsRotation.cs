using UnityEngine;

public class SwipePhysicsRotation : MonoBehaviour 
{
    private const float Deadzone = 100f;

    
    public float speedTorque;
    public GameObject objectSwipePrefab;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    private bool isDraging = false;

    public bool activeCubeTouched = false;

    private Rigidbody rigidbody;
    
    private Quaternion _currentRotationPoint;

    public bool CanSwipe
    {
        get;
        set;
    }

    private void Awake ()
    {
        _currentRotationPoint = transform.rotation;
        CanSwipe = true;
        
        rigidbody = objectSwipePrefab.AddComponent<Rigidbody> ();
        rigidbody.useGravity = false;
        rigidbody.centerOfMass = new Vector3 (0, 0, 0);
        rigidbody.angularDrag = 2f;
        rigidbody.drag = 10;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update ()
    {
        // VibrationSelect();
        
        if (!activeCubeTouched ) 
        {
            tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

            #region Stand Alone

            if (Input.GetMouseButtonDown (0)) {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            } else if (Input.GetMouseButtonUp (0)) {
                Reset();
            }
            #endregion

            #region Mobile Inputs

            if (Input.touches.Length != 0) {
                if (Input.touches[0].phase == TouchPhase.Began) {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[0].position;
                } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
                    Reset();
                }
            }
            #endregion

            swipeDelta = Vector2.zero;
            if(isDraging)
            { 
                if (Input.touches.Length > 0) 
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }
                else if (Input.GetMouseButton (0)) 
                {
                    swipeDelta = (Vector2) Input.mousePosition - startTouch;
                }
            }

            if (swipeDelta.magnitude > Deadzone) 
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                

                Vector3 torque = new Vector3 (0, x, 0);

                if (Mathf.Abs (x) > Mathf.Abs (y)) 
                {
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                } 
                else 
                {
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }

                rigidbody.AddTorque (-torque * speedTorque, ForceMode.Force);

                Reset();
            }
        } 
        else 
        {
            rigidbody.angularVelocity = Vector3.zero;
        }
    }


    
    private void VibrationSelect()
    {
        float angle = Quaternion.Angle(transform.rotation, _currentRotationPoint);
        if (angle < 30f)    
        {
            return;
        }

        _currentRotationPoint = transform.rotation;
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;  
        isDraging = false;
    }

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
}
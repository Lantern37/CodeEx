// using MoreMountains.NiceVibrations;

using DG.Tweening;
using UnityEngine;

public class SwipeTweenRotation : MonoBehaviour 
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float speedTorque;
    public LayerMask touchInputMask;


    private const float Deadzone = 100f;
    private Vector2 swipeDelta, startTouch;
    private Quaternion _currentRotationPoint;
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Camera _camera;

    public bool ActiveCubeTouched { get; set; }
    public bool RoomTouched { get; set; }
    private void Awake ()
    {
        _camera = Camera.main;

        _currentRotationPoint = transform.rotation;
        ActiveCubeTouched = false;
        RoomTouched = false;
        // _rigidbody = objectSwipePrefab.AddComponent<Rigidbody> ();
        // _rigidbody.useGravity = false;
        // _rigidbody.centerOfMass = new Vector3 (0, 0, 0);
        // _rigidbody.angularDrag = 2f;
        // _rigidbody.drag = 10;
        // _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update ()
    {
            // VibrationSelect();
            tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

            #region Stand Alone

            if (Input.GetMouseButtonDown (0)) {
                Debug.Log("IGetMouseButtonDown");
                var _ray = _camera.ScreenPointToRay (Input.mousePosition);
                RaycastHit _hit;
                if (Physics.Raycast (_ray, out _hit, 40f, touchInputMask)) 
                {
                    Debug.Log("touched");
               
                    StartSwiping();
                }
                
            } else if (Input.GetMouseButtonUp (0)) {
                Debug.Log("GetMouseButtonUp");
            
                StopSwiping();

            }
            #endregion

            #region Mobile Inputs

            if (Input.touches.Length != 0) 
            {
                if (Input.touches[0].phase == TouchPhase.Began) {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[0].position;
                } else if (Input.touches[0].phase == TouchPhase.Ended 
                    || Input.touches[0].phase == TouchPhase.Canceled) 
                {
                    StopSwiping();
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
                Debug.Log("swipe " + swipeDelta.magnitude);
                
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                


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

                // _rigidbody.AddTorque (-torque * speedTorque, ForceMode.Force);

                Vector3 torque = new Vector3 (0, 1, 0);
                if (swipeLeft)
                {
                    transform.DORotate(-torque, speedTorque);
                }
                else if (swipeRight)
                {
                    transform.DORotate(torque, speedTorque);

                }

                // StopSwiping();
            }
    }

    public void StartSwiping()
    {
        tap = true;
        isDraging = true;
        startTouch = Input.mousePosition;

    }
  

    public void StopSwiping()
    {
        startTouch = swipeDelta = Vector2.zero;  
        isDraging = false;
    }

    
    private void VibrationSelect()
    {
        float angle = Quaternion.Angle(transform.rotation, _currentRotationPoint);
        if (angle < 30f)    
        {
            return;
        }

        _currentRotationPoint = transform.rotation;
        
        // MMVibrationManager.Haptic(HapticTypes.Selection);
    }
    // public bool Tap { get { return tap; } }
    // public Vector2 SwipeDelta { get { return swipeDelta; } }
    // public bool SwipeLeft { get { return swipeLeft; } }
    // public bool SwipeRight { get { return swipeRight; } }
    // public bool SwipeUp { get { return swipeUp; } }
    // public bool SwipeDown { get { return swipeDown; } }
}
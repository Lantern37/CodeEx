using UnityEngine;

public class LookAtCameraY : MonoBehaviour
{
   [SerializeField] private float _speed = 1f;
   private Transform _transform;
   private Transform _camera;

   private void Awake()
   {
      _transform = transform;
      _camera = Camera.main.transform;
   }

   private void Update()
   {
      Quaternion toTargetRot = Quaternion.LookRotation(_camera.position - _transform.position);
      var x = _transform.rotation.x;
      var y = toTargetRot.eulerAngles.y;
      var z = _transform.rotation.z;
      
      var newRotation = Quaternion.Euler(x,y,z);

      transform.rotation = Quaternion.Slerp(_transform.rotation, newRotation, Time.deltaTime * _speed);
   }
}

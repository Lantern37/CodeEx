using Assets.Scripts.Carousel;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class TestCreateItemsAround : MonoBehaviour
    {
        //[SerializeField] private CreateItemsAround _createItems;
        //private CircleRotator _circleRotator = new CircleRotator();

        [SerializeField] KeyCode _keyCode = KeyCode.F;

        [SerializeField] private int _count;

        [SerializeField] private Transform _point;

        [SerializeField] private float _offset;

        [SerializeField] private float _rotationSpeed;

        [SerializeField] private int _index;

        public void Update()
        {
            // if (Input.GetKeyDown(_keyCode))
            // {
            //     _createItems.CreateItemsAroundPoint(_count, _point, _offset);
            //     _circleRotator.Init(_point, _createItems.AngleStep);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.J))
            // {
            //     _createItems.DestroyCircle();
            // }
            //
            // if (Input.GetKey(KeyCode.RightArrow))
            // {
            //     _circleRotator.Rotate(-Time.deltaTime*_rotationSpeed);
            // }
            //
            // if (Input.GetKey(KeyCode.LeftArrow))
            // {
            //     _circleRotator.Rotate(Time.deltaTime*_rotationSpeed);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     _createItems.AddItemAtIndex(_point,_index);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     _circleRotator.StopRotation();
            // }
            //
            // _circleRotator?.Update();
            // if (Input.GetKeyDown(KeyCode.X))
            // {
            //     _createItems.AddItemLeft(_point);
            // }
        }
    }
}
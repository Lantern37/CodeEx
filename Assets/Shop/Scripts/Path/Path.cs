using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using Shop.Core;
using UnityEngine;


namespace Shop
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private PathBehaviour m_PathBehavior;
        [SerializeField] private SplineComputer m_Spline;
        [SerializeField] private Transform m_SenterPointControl;
        [SerializeField] private float m_DistanceBetweenItems = 0.25f;
        private List<ItemPathParent> m_CurrentLable = new List<ItemPathParent>();

        public List<ItemPathParent> CurrentLable => m_CurrentLable;

        private float m_PathLength;

        // public float PathLength => m_PathLength;

        public void SetNewLable(List<ItemPathParent> lable)
        {
            m_CurrentLable = lable;
            for (int i = 0; i < m_CurrentLable.Count; i++)
            {
                m_CurrentLable[i].InitSplineBehaviour(m_PathBehavior);
                m_CurrentLable[i].ConnectToSpline(m_Spline, 0.5f);
                m_CurrentLable[i].PathIndex = i;
                m_CurrentLable[i].SetRenderActive(false);
                m_CurrentLable[i].Positioner.SetDistance(0f);
                m_CurrentLable[i].SplinePosition = 0;
            }

            m_CurrentLable[0].IsLocalHead = true;

            m_PathBehavior.CurrentLable = m_CurrentLable;
            Debug.Log("m_pathCollection[0] " + m_CurrentLable[0].name);

            DOVirtual.DelayedCall(1, () => { m_PathBehavior.ShowLable(); });
        }

        public void Init()
        {
            var splineMesh = m_Spline.gameObject.GetComponent<SplineMesh>();
            splineMesh.clipFrom = 0;
            splineMesh.clipTo = 0;
            // var meshRenderer = m_Spline.gameObject.GetComponent<MeshRenderer>();
            // var tileObj = param.tile;
            var tileMesh = m_Spline.GetComponent<MeshFilter>().sharedMesh;
            var channel = splineMesh.AddChannel(tileMesh, "Channel 1");
            channel.type = SplineMesh.Channel.Type.Extrude;
            channel.count = 20;
            channel.spacing = 0.4d;
            SplineMeshTween(splineMesh, 2);

            //Triggers
            TriggerEvents events = m_Spline.gameObject.AddComponent<TriggerEvents>();

            m_Spline.triggerGroups = new TriggerGroup[1];
            m_Spline.triggerGroups[0] = new TriggerGroup();
            m_Spline.triggerGroups[0].triggers = new SplineTrigger[1];

            //Normal Speed Trigger
            m_Spline.triggerGroups[0].triggers[0] = new SplineTrigger(SplineTrigger.Type.Forward);
            m_Spline.triggerGroups[0].triggers[0].position = 1;
            m_Spline.triggerGroups[0].triggers[0].AddListener(new UnityEngine.Events.UnityAction(events.TriggerEvent));

            SetMiddlePointToCenter(m_SenterPointControl);
            ShopManager.Instance.Init(this);
            DOVirtual.DelayedCall(1, () => m_PathBehavior.Init(m_Spline, m_DistanceBetweenItems));
        }

        void TriggersOff()
        {
            foreach (var trigger in m_Spline.triggerGroups[0].triggers)
            {
                trigger.enabled = false;
            }
        }

        void TriggersOn()
        {
            foreach (var trigger in m_Spline.triggerGroups[0].triggers)
            {
                trigger.enabled = true;
            }
        }

        void RelodeLable()
        {
            if (m_CurrentLable.Count > 0)
            {

            }
        }

        private void SplineMeshTween(SplineMesh mesh, float duration)
        {
            DOTween.To(
                () => mesh.clipTo,
                x => mesh.clipTo = x,
                1,
                duration
            );
        }

        void SetMiddlePointToCenter(Transform middleTransform)
        {
            Vector3 centerWorldPosition = m_Spline.EvaluatePosition(0.5f);
            middleTransform.position = centerWorldPosition;
        }

        #region PathMovements for Debug

        private bool m_PathIsMoving;
        private bool m_PointIsMoving;
        [SerializeField] private float _speedMovePath = 0.01f;
        [SerializeField] private PathPointControl m_PathStartPointControl;
        [SerializeField] private PathPointControl m_PathEndPointControl;
        [SerializeField] private PathPointControl m_PathMiddlePointControl;

        public void MovePathForward()
        {
            m_PathIsMoving = true;
            var direction = Vector3.forward;
            StartCoroutine(MovePath(direction, _speedMovePath));
        }


        public void MovePathBackward()
        {
            m_PathIsMoving = true;
            var direction = Vector3.back;
            StartCoroutine(MovePath(direction, _speedMovePath));
        }

        public void MovePathStop()
        {
            m_PathIsMoving = false;

        }

        IEnumerator MovePath(Vector3 direction, float speed)
        {
            while (m_PathIsMoving)
            {
                transform.localPosition += direction * speed;

                string value = transform.localPosition.z.ToString("0.00");
                ShopManager.Instance.UpdateTextSplineZ(value);
                yield return null;
            }
        }

        public void MovePointX(bool smaller)
        {
            m_PointIsMoving = true;
            StartCoroutine(MovePointX(smaller, _speedMovePath));
            // StartCoroutine(MovePointX(2, !smaller, _speedMovePath));
        }

        public void MoveMiddleZ(bool forward)
        {
            m_PointIsMoving = true;
            StartCoroutine(MoveMiddlePointZ(forward, _speedMovePath));
        }
        public void StopMovePoint()
        {
            m_PointIsMoving = false;
        }
        public void StopMovePointX()
        {
            m_PointIsMoving = false;
        }


      
        IEnumerator MovePointX(bool increment, float speed)
        {
            if (!increment)
            {
                speed *= -1;
            }

            while (m_PointIsMoving)
            {
                m_PathStartPointControl.SetX(speed);
                m_PathEndPointControl.SetX(speed * -1);
                var pathLength = m_Spline.CalculateLength();
                m_PathBehavior.SplineLength = pathLength;
                string length = pathLength.ToString("0.00");
                ShopManager.Instance.UpdateLength(length);

                yield return null;
            }
        }
        IEnumerator MoveMiddlePointZ(bool forward, float speed)
        {
            if (!forward)
            {
                speed *= -1;
            }

            var pointPosition = m_Spline.GetPointPosition(1, SplineComputer.Space.World);
            while (m_PointIsMoving)
            {
                pointPosition.z += speed;
                Debug.Log("Move Point Z middle");

                string value = m_PathMiddlePointControl.GetZ(speed);
                ShopManager.Instance.UpdateTextMiddleZ(value);

                
               
                yield return null;
            }
            
            var pathLength = m_Spline.CalculateLength();
            m_PathBehavior.SplineLength = pathLength;
        }

        //Distance between items
        public void ChangeDistanseBetweenItems(bool increase)
        {
            m_PointIsMoving = true;
            StartCoroutine(ChangeDistance(increase));
        }
        IEnumerator ChangeDistance(bool increase)
        {
            var delta = 0.001f;
            if (!increase)
            {
                delta *= -1;
            }
            while (m_PointIsMoving)
            {
                m_PathBehavior.DistanceBetweenItems += delta;
                if (m_PathBehavior.DistanceBetweenItems < 0.2f)
                {
                    yield  break;
                }
                Debug.Log("Move Point Z middle");

                string value = m_PathBehavior.DistanceBetweenItems.ToString();
                ShopManager.Instance.UpdateTextDistance(value);

                yield return null;
            }
            
        }

        #endregion

        
    }
}

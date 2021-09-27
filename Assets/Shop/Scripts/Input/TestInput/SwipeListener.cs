using DG.Tweening;
using UnityEngine;

public class SwipeListener : MonoBehaviour
{
    private TestSwipeDetection m_SwipeDetection;
    private float m_SwipePower = 50;

    private void Awake()
    {
        m_SwipeDetection = FindObjectOfType<TestSwipeDetection>();
    }

    private void OnEnable()
    {
        m_SwipeDetection.ShortSwipeEvent += Swipe;
    }

    private void Swipe(SwipeSide side, float power)
    {
        var rotationEuler = transform.rotation.eulerAngles;
        if (side == SwipeSide.Up)
        {
            rotationEuler.x += power * m_SwipePower;
        }
        if (side == SwipeSide.Down)
        {
            rotationEuler.x -= power * m_SwipePower;
        }
        if (side == SwipeSide.Left)
        {
            rotationEuler.y += power * m_SwipePower;
        }
        if (side == SwipeSide.Right)
        {
            rotationEuler.y -= power * m_SwipePower;
        }

        transform.DOLocalRotate(rotationEuler, 1);
    }
}

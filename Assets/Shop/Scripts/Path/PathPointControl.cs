using Dreamteck.Splines;
using UnityEngine;

public class PathPointControl : MonoBehaviour
{
  [SerializeField] private int m_PointIndex;
  [SerializeField] private SplineComputer m_Spline;

  private void Start()
  {
    SetPointPosition();
  }

  public void SetPointPosition()
  {
      m_Spline.SetPointPosition(m_PointIndex, transform.position, SplineComputer.Space.World);
      Debug.Log("SetPointPosition " + m_PointIndex + " " );
  }

  // public void SetLocalPositionX(float delta)
  // {
  //     Debug.Log("SetLocalPositionX " + m_PointIndex + " " + delta);
  //
  //     if (delta <= 0) return;
  //     var position = transform.localPosition;
  //     position.x += delta;
  //     transform.localPosition = position;
  //     SetPointPosition();
  // }

  public string GetX(float delta)
  {
      Debug.Log("SetLocalPositionX " + m_PointIndex + " " + delta);

      var position = transform.localPosition;
      position.x += delta;
      transform.localPosition = position;
      SetPointPosition();
      string value = position.x.ToString("0.00");
      return value;
  }
  
  public void SetX(float delta)
  {
      Debug.Log("SetLocalPositionX " + m_PointIndex + " " + delta);

      var position = transform.localPosition;
      position.x += delta;
      transform.localPosition = position;
      SetPointPosition();
  }
  
  public string GetZ(float delta)
  {
      Debug.Log("SetLocalPositionX " + m_PointIndex + " " + delta);

      var position = transform.localPosition;
      position.z += delta;
      transform.localPosition = position;
      SetPointPosition();
      string value = position.z.ToString("0.00");
      return value;
  }

}

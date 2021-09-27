using TMPro;
using UnityEngine;

public class DebugUIPathController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PathLength;
    [SerializeField] private TextMeshProUGUI m_MiddlePointZ;
    [SerializeField] private TextMeshProUGUI m_SplinePozitionZ;
    [SerializeField] private TextMeshProUGUI m_Distance;

   


    public void SetPathLengthText(string lenth)
    {
        m_PathLength.text = lenth;
    }
    
    public void SetMiddleZText(string middleZ)
    {
        m_MiddlePointZ.text = middleZ; 
    }

    public void SetSplineZText(string splineZ)
    {
        m_SplinePozitionZ.text = splineZ;
    } 
    public void SetDistanceText(string splineZ)
    {
        m_Distance.text = splineZ;
    }

}

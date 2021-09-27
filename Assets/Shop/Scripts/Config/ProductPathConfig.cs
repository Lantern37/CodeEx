using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ProductPathConfig", menuName = "Engenious/Core/Configs/NetworkConfig")]

public class ProductPathConfig : ScriptableObject
{
   public float m_CureLabelDistance;
   public float m_BloomLabelDistance;
   public float m_PlugLabelDistance;
   public float m_AlmoraLabelDistance;

   public float m_PathDepth;
   public float m_PathHighOffset;


}

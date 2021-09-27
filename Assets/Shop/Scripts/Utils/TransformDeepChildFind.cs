
//Two ways to find a child deep


using UnityEngine;

namespace ARTower.Core.Utils
{
	
public static class TransformDeepChildFind
{

	//Breadth search
		public static Transform FindDeepChild2(this Transform aParent, string aName)
		{
			var result = aParent.Find (aName);
			if (result != null)
				return result;
	
			foreach(Transform child in aParent)
			{
				result = child.FindDeepChild (aName);
				if (result != null)
					return result;
			}
			return null;
	
		}

	//Deep search
	public static Transform FindDeepChild (this Transform aParent, string aName)
	{




		foreach (Transform child in aParent) {
			if (child.name == aName)
				return child;

			var result = child.FindDeepChild (aName);


			if (result != null)
				return result;
		}
		return null;

	}

}

}

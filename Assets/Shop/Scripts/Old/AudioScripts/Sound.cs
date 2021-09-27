using UnityEngine;

namespace Shop
{
	
	[System.Serializable]
	public class Sound 
	{
		public string name;
		public AudioClip clip;

		[Range(0,1)]
		public float volume;
	
		[Range(0,12)]
		public float pitch = 1;
		public bool loop;
	}
}
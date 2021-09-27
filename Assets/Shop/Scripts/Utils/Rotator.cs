using UnityEngine;

public class Rotator : MonoBehaviour
{
	[Range (-10, 10)]
	public float speedX;
	[Range (-10, 10)]
	public float speedY;
	[Range (-10, 10)]
	public float speedZ;


	private void Update()
	{
        transform.Rotate (speedX, speedY, speedZ);
	}
}

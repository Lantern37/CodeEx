using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LookAtCamera : MonoBehaviour 
{
	private static readonly int Rotating = Animator.StringToHash("Rotating");
	
	[Range (0, 180)] 
	[SerializeField] private float SpeedNormal = 0.9f;
	
	[Range (0, 45)] 
	[SerializeField] private float limitRotation = 0.04f;

	private GameObject _targetCamera;
	private GameObject _normalTargetCamera;
	

	private void Start () 
	{
		_normalTargetCamera = GameObject.FindGameObjectWithTag ("DebugCamera");
		_targetCamera = _normalTargetCamera;
	}

	private void Update () 
	{
			if (_targetCamera == null)
			{
				return;
			}
			
			Quaternion rotationToTarget = Quaternion.LookRotation (_targetCamera.transform.position - transform.position);
			rotationToTarget.x = 0;
			rotationToTarget.z = 0;

			if (rotationToTarget.y - transform.rotation.y >= limitRotation 
			    || rotationToTarget.y - transform.rotation.y <= -limitRotation) 
			{
				StartCoroutine (nameof(ObjRotation), rotationToTarget);
			} 
			else 
			{
				StopCoroutine (nameof(ObjRotation));
			}
		
		
	}

	// public void ChangeTargetCamera()
	// {
	// 	_targetCamera = Tools.isInZumaRoom 
	// 		? _zumaTarget 
	// 		: _normalTargetCamera;
	// }

	IEnumerator ObjRotation(Quaternion rotation)
	{
		float speed;
		speed =  SpeedNormal;
		
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * speed);

		yield return null;
	}


}
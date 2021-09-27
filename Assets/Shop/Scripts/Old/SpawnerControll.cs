using System;
using DG.Tweening;
using Shop;
using Shop.Core;
using Shop.Core.Utils;
using UniRx.Async;
using UnityEngine;

public class SpawnerControll : SingletonT<SpawnerControll>
{
	protected event Action LableChoosen;
	
	public GameObject spawnObjPrefab;
	public GameObject arPointParent;
	private GameObject _spawnedObject;
	private LableType _currentLableType;
	// public GameObject shadowPlanePrefab;
	// private GameObject shadowPlane;
	// public GameObject GetShadowPlane => shadowPlane;

	private GameObject arParent;

	public GameObject GetARParentObj()
	{
		return arParent;
	} 

	public GameObject GetSpawnedObj()
	{
		return _spawnedObject;
	}

	
	private async void Start () 
	{
		arParent = GameObject.FindWithTag ("ARParent");
		if (arParent == null) {
			arParent = Instantiate (arPointParent);
			transform.parent = arParent.transform;
		}
		_spawnedObject = Instantiate (spawnObjPrefab, transform.position, transform.rotation);
		_spawnedObject.transform.parent = arParent.transform;

		// DOVirtual.DelayedCall(2, () => AudioSystem.Instance.PlayOneShot(AudioClips.CanIHelp));
		// DOVirtual.DelayedCall(4, () => Game.UI.Instance.QuestionsPanel(true));
		
		
		DOVirtual.DelayedCall(4, () => Shop.UI.Instance.SpeechPanel(true));
		// DOVirtual.DelayedCall(4, () => SpawnManager.Instance.EndSpawning());
		
		// AudioSystem.Instance.PlayOneShot(AudioClips.Hello);
		
		// SpeechManager.Instance.AssistantSpeak("Hello man");
		var showRoom = _spawnedObject.GetComponentInChildren<ShowRoom>();
		showRoom.ActiveFX(true);
		await WaitForLabel();
		_currentLableType = ShopManager.Instance.LableType;
			SetLable(_currentLableType);
			showRoom.ActiveFX(false);
	}

	public UniTask WaitForLabel()
	{
		return new UniTask(() =>
		{
			var completionSource = new UniTaskCompletionSource();
			LableChoosen += () => completionSource.TrySetResult();

			return completionSource.Task;
		}); 
	}
    
	public void OnLableChoosen()
	{
		LableChoosen?.Invoke();
	}

	public void SetLable(LableType lableType)
	{

		var showRoom = _spawnedObject.GetComponentInChildren<ShowRoom>();
		if (showRoom != null)
		{
			showRoom.ShowCurrentLableItems(lableType);
		Debug.Log("ShowCurrentLableItems " + lableType);
		}
		
	}
	void DestroyARPlanes () {
		GameObject[] arPlanes = GameObject.FindGameObjectsWithTag ("ARKitPlane");
		foreach (var item in arPlanes) {
			Destroy (item);
		}
	}

	void OnDestroy ()
	{
		Destroy (_spawnedObject);
	}

	// void InstantiateShadow () {
	// 	shadowPlane = Instantiate (shadowPlanePrefab, gameObject.transform.position, gameObject.transform.rotation);
	// 	shadowPlane.transform.parent = GetSpawnedObj().transform;
	// 	StartCoroutine(ShadowScale());
	// }


}
using System.Collections;
using UnityEngine;

public class EasyTrigger : MonoBehaviour {

	private SteamVR_TrackedController device;
	public GameObject Prefab;
	public Transform playerTransform;
	void Start()
	{
		device = GetComponent<SteamVR_TrackedController>();
		device.TriggerClicked += Trigger;
	}

	void Trigger (object sender, ClickedEventArgs e)
	{
		Debug.Log("Trigger has been pressed");
		GameObject obj=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
	}
}

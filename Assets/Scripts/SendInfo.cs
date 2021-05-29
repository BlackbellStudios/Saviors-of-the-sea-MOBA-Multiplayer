using UnityEngine;
using System.Collections;

public class SendInfo : MonoBehaviour {
	public Camera Camera;
	
	void Start () {
		Camera.GetComponentInChildren<Camera> ();
	}
	
	
	void Update () {
		if (GetComponent<NetworkView>().isMine) {

		}
	}
}

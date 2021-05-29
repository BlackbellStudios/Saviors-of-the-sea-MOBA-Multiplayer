using UnityEngine;
using System.Collections;

public class CameraAjuste : MonoBehaviour {
	public GameObject PlayerThisMachine;
	public Camera CamemaThisMachine;
	public string MyPlayerName;

	// Use this for initialization
	void Start () {
		MyPlayerName = PhotonNetwork.player.name;
		PlayerThisMachine = GameObject.Find (MyPlayerName);
		CamemaThisMachine = GameObject.Find (MyPlayerName).GetComponent<Player_Moba>().MyCam;
		CamemaThisMachine.depth = 2;


	}
	
	// Update is called once per frame
	void Update () {
	}
}

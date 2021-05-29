using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class Control_Intantiate : Photon.MonoBehaviour
{

	//Data stuff/Networking
	private PhotonView pv;
	private PhotonPlayer[] players;
	public GameObject SpawnPoint;
	public GameObject[] PlayerToInstantiate;   // set in inspector
	public int Team = 0;
	public int Order = 0;

	// Use this for initialization
	void Start ()
	{
		pv = GetComponent<PhotonView> ();

		if(PhotonNetwork.isMasterClient){
			SpawnPoint = GameObject.Find ("Team_0_Play_0");
		}else{
			SpawnPoint = GameObject.Find ("Team_1_Play_1");
		}

		players = PhotonNetwork.playerList;
		foreach (GameObject player in this.PlayerToInstantiate) {
			Debug.Log("Foi Instanciado o: " + player.name);

//			for (int i = 0; i < players.Length; i++) {
			Debug.Log (SpawnPoint.name);
//				//GameObject.Find ("Play" + i + "_Level").GetComponent<Text> ().text = NameTeam;
//
//				Debug.Log ("Instantiating: " + players [i].name);
//			}
			Vector3 spawnPos = SpawnPoint.transform.position;

			GameObject playerInst = PhotonNetwork.Instantiate (player.name, spawnPos, Quaternion.identity, 0);
			playerInst.name = PhotonNetwork.player.name;

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

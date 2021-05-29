using UnityEngine;
using System.Collections;

public class ConnectionGUI : MonoBehaviour {

	private string remoteIP = "127.0.0.1";
	private int RemotePort = 25000;
	private int listenport = 25000;
	private bool useNAT = false;
	//private string yourIP = "";
	//private string yourPort = "";
	private string ipaddress = "";
	private string port = "";






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		void OnGUI () {
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				if (GUI.Button (new Rect (10,10,100,30), "Connect"))
				{
					Network.useNat = useNAT;
					Network.Connect (remoteIP, RemotePort);
				}
				if (GUI.Button (new Rect (10,50,100,30),"Start Server"))
				{
					Network.useNat = useNAT;
					Network.InitializeServer (32, listenport);
					GameObject[] Players =
						GameObject.FindObjectsOfType(typeof(GameObject)) as
							GameObject[];
					foreach(GameObject go in Players)
					{
						go.SendMessage ("OnNetworkLoadedLevel",
						                SendMessageOptions.DontRequireReceiver);
					}
				}
				remoteIP = GUI.TextField (new Rect (120,10,100,20), remoteIP);
				RemotePort = int.Parse (GUI.TextField (new Rect (230,10,40,20),
				                                       RemotePort.ToString ()));
			}
			else
			{
				ipaddress = Network.player.ipAddress;
				port = Network.player.port.ToString ();
				GUI.Label (new Rect (140,20,250,40), "IP Endereço " + ipaddress + ":"
				           + port);
				if (GUI.Button (new Rect (10,10,100,50), "Disconnect"))
				{
					Network.Disconnect (200);
				}
			}
		}

		void OnConnectedToServer () {
			GameObject[] Players = GameObject.FindObjectsOfType(typeof(GameObject))	as GameObject[];
			foreach(GameObject go in Players)
			{
				go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
			Debug.LogWarning("teste");
			}
		}


	void OnDisconnectedFromServer(NetworkDisconnection info){

		//GameObject[] Players = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
		GameObject[] Players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i =0; i < Players.Length; i++) {
			Destroy(Players[i]);		
		}
		//foreach (GameObject go in Players){
	//		if(go.gameObject.tag == "Player"){
	//			Destroy (go.gameObject);
	//		}
	//	}
	}

}
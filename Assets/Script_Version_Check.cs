using UnityEngine;
using System.Collections;


public class Script_Version_Check : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		while (!TryConnect()) {
			Debug.Log("Conectado: " + TryConnect());
		}
		Debug.Log ("Conectado ao Servidor");
		//PhotonNetwork.ConnectUsingSettings ("V1.0");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool TryConnect(){
		try {
			PhotonNetwork.ConnectUsingSettings ("V1.0");
		}
		catch (UnityException error){
			Debug.Log (error);
			return (false);
		}
		return (true);
	}

	public void Iniciar(){
		Application.LoadLevel (1);
	}
}

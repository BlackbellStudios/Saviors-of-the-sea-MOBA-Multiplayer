using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Script_Login : MonoBehaviour {
	public InputField User;
	public InputField Password;
	public Text Aviso;

	Hashtable h;
	//public string Usuario;


	// Use this for initialization
	void Start () {
		h = new Hashtable(10);
		h.Add("Ship",0);
		PhotonNetwork.player.SetCustomProperties(h);
		Debug.Log(PhotonNetwork.player.customProperties);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Logar(){
		if (User.text == "") {
			Aviso.gameObject.SetActive(true);

		}
		else{
			PhotonNetwork.player.name = User.text;
			Application.LoadLevel (2);

		}
			
	}
}

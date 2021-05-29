using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class Script_Room : Photon.MonoBehaviour
{
	//Teams Controller
	public int redTeamPlayers = 0;
	public int blueTeamPlayers = 0;
	private int localBluePlayers = 0;
	private int localRedPlayers = 0;
	//matchmaking stuff
	private RoomInfo[] rooms;
	private PhotonPlayer[] players;
	//Temp Strings
	public string tempCreateName = "";
	public string tempCreatePassword = "";
	//Data stuff/Networking
	private PhotonView pv;

	private Rect screenRect = new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400);
	public GUISkin skin;
	public Color defaultColor = Color.white;

	//Canvas Criaçao sala!
	public bool Btn_CSAtiva = false;
	public InputField Field_TempCreateName;
	public InputField Field_TempCreatePassword;
	public Toggle IsV;
	public Button Btn_Create;
	public Text AvisoErro;
	//public bool Test = false;

	//Texture Backgroun
	public RawImage Backgound;
	public Texture Room_Selection;
	public Texture Room_Time;

	public string Play1_Name;

	//Canvas Screens
	public GameObject Room_Players;
	public Button BtnIniciaJogo;

	public int PlayersNum  = 0 ;
	public string NameTeam = "";


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		//Escreve
		if (stream.isWriting == true) {
			stream.SendNext (PlayersNum);
			stream.SendNext (NameTeam);
		//Recebe Valores
		} else {
			PlayersNum = (int)stream.ReceiveNext ();
			NameTeam =(string)stream.ReceiveNext();
		}
	}
		
	// Use this for initialization
	void Start ()
	{
		Backgound.texture = Room_Selection;
		pv = GetComponent<PhotonView> ();
		PhotonNetwork.JoinLobby ();
		Debug.Log ("Estou no Lobby: " + PhotonNetwork.JoinLobby());
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	// Update is called once per frame
	public void Btn_CriarSala ()
	{
		Btn_CSAtiva = !Btn_CSAtiva;
	}

	public void Btn_CriandoSala ()
	{
		if (Field_TempCreateName.text == "") {
			AvisoErro.gameObject.SetActive (true);
		} else {
			//Linhas de codigo Para definir os parametros e criar a nova sala.
			RoomOptions roomOptions = new RoomOptions () {isOpen = true, isVisible = !IsV.isOn, maxPlayers = 6 };
			PhotonNetwork.CreateRoom (tempCreateName, roomOptions, TypedLobby.Default);
			Debug.Log ("Sala? " + PhotonNetwork.room);
			//Desabilita os Canvas Para a Cena da Sala. Habilita o ultimo canvas para a Cena da Sala
			GameObject.Find ("Screen_RoomCreate").gameObject.SetActive (false);
			CleanScreenToRoom ();
			InvokeRepeating("InsideRoomMaster",2f,1.5f);

		}
	}

	void Update ()
	{

		if (PhotonNetwork.inRoom && PhotonNetwork.isMasterClient) {
			localRedPlayers = redTeamPlayers;
			localBluePlayers = blueTeamPlayers;
			pv.RPC ("updateTeamValues", PhotonTargets.All);
			//Debug.Log(PhotonNetwork.player.customProperties);
		}

		if (Btn_CSAtiva) {
			tempCreateName = Field_TempCreateName.text;
			tempCreatePassword = Field_TempCreatePassword.text;
		}
	
		// Atualiza Telas
		rooms = PhotonNetwork.GetRoomList ();
		if (!PhotonNetwork.inRoom) {
			//GUILayout.Window (0, screenRect, MatchMakingGUI, "Games");
		}
		if (PhotonNetwork.inRoom) {
			//GUILayout.Window (1, screenRect, LobbyGUI, "Lobby");
			//InsideRoom ();
			//Debug.Log(PhotonNetwork.player.customProperties);
			Backgound.texture = Room_Time;
			if (PhotonNetwork.player.isMasterClient) {
				BtnIniciaJogo.gameObject.SetActive (true);
			}
		}
	}

	public void StartParty ()
	{
		// IniciaPartida
		PhotonNetwork.LoadLevel (3);
	}


	void OnGUI ()
	{
		GUI.skin = skin;
		rooms = PhotonNetwork.GetRoomList ();
		if (!PhotonNetwork.inRoom) {
			GUILayout.Window (0, screenRect, MatchMakingGUI, "Games");
		}
		if (PhotonNetwork.inRoom) {
			Debug.Log("Estou na Sala");
			GUILayout.Window (1, new Rect(0,0,0,0), LobbyGUI, "Lobby");

		}
		
	}
	void MatchMakingGUI (int id)
	{
		GUI.skin = skin;
		GUILayout.Label ("Create Game");

		GUILayout.Space (10);
		foreach (RoomInfo roomz in rooms) {
			if (GUILayout.Button (roomz.name + " |" + roomz.playerCount + "/" + roomz.maxPlayers + "|")) {
				PhotonNetwork.JoinRoom (roomz.name);
				CleanScreenToRoom ();
				//OnJoinedRoom ();

			}
		}
		GUI.DragWindow (); //At the end for dragability
	}
	void CleanScreenToRoom ()
	{
		//Desabilita os Canvas Para a Cena da Sala. Habilita o ultimo canvas para a Cena da Sala
		GameObject.Find ("Btn_IniciarCriacao").gameObject.SetActive (false);
		Room_Players.gameObject.SetActive (true);
	}

	void LobbyGUI (int id)
	{
		players = PhotonNetwork.playerList;
		Backgound.texture = Room_Time;

		GUILayout.BeginScrollView (new Vector2 (0, 10));
		foreach (PhotonPlayer player in players) {
			int? teamNumb = player.customProperties ["Team"] as int?; //This is how we read the object's value
			if (teamNumb == 0) {
				GUI.contentColor = Color.red;
			}
			if (teamNumb == 1) {
				GUI.contentColor = Color.blue;
			}
			if (player.name == "DaftDev") {
				GUI.backgroundColor = Color.green;
			}
			GUILayout.Button ("|" + player.name + "|");
			GUI.color = defaultColor;
			GUI.contentColor = defaultColor;
			GUI.backgroundColor = defaultColor;
		}
		GUILayout.EndScrollView ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Main Menu")) {
			int? teamNumb = PhotonNetwork.player.customProperties ["Team"] as int?;
			if (teamNumb == 0) {
				pv.RPC ("leaveTeam", PhotonTargets.All, 0); //Notify users we left a team.
			}
			if (teamNumb == 1) {
				pv.RPC ("leaveTeam", PhotonTargets.All, 1); //Notify users we left a team.    
			}
			PhotonNetwork.LeaveRoom ();
			Application.LoadLevel (2);
		}
		if (PhotonNetwork.isMasterClient) {
			if (GUILayout.Button ("Start")) {
				//TODO add a start function
			}
		}
		GUILayout.EndHorizontal ();
	}

	void OnJoinedRoom ()
	{
//		players = PhotonNetwork.playerList;
//		foreach (PhotonPlayer player in players) {
//		
//					//players = PhotonNetwork.playerList;
//						int i = 0;
//						
//						for (i= 0; i < players.Length; i++) {
//						int? Pos = players [i].customProperties ["Order"] as int?;
//							PlayersNum = i;
//							int teamChoice = 0;
//							
//							if (i == 0 || i == 2 || i == 4) {
//								NameTeam = "A";
//								teamChoice = 0;
//							} else {
//								teamChoice = 1;
//								NameTeam = "B";
//							}
//							
//							pv.RPC ("joinTeam", PhotonTargets.AllBufferedViaServer, teamChoice); //Notify everyone that we joined a team
//							players [i].customProperties ["Team"] = teamChoice;
//				players[i].customProperties["Order"] = i;
//							Debug.Log ("OnJoin palyer are: "+i+ NameTeam);
//				if(PhotonNetwork.isMasterClient){
//					RoomPlayersAtualize(i , NameTeam);
//				}		
//					
//						}
//				}
		}

	void InsideRoomMaster ()
	{
		players = PhotonNetwork.playerList;
		foreach (PhotonPlayer player in players) {
			
			//players = PhotonNetwork.playerList;
			int i = 0;
			PlayersNum = i;
			for (i= 0; i < players.Length; i++) {
				int teamChoice = 0;
				if (i == 0 || i == 2 || i == 4) {
					NameTeam = "A";
					teamChoice = 0;
				} else {
					teamChoice = 1;
					NameTeam = "B";
				}
				players [i].customProperties ["Team"] = teamChoice;
				players [i].customProperties["Order"] = i;

				int? Order = players [i].customProperties ["Order"] as int?;
				int? Team = players [i].customProperties ["Team"] as int?;
				int TempOrder = (int)Order;
				int TempTeam = (int)Team;
				RoomPlayersAtualize(TempOrder , TempTeam);
				Debug.Log ("Atualize palyer: "+players[i].name +TempOrder+ NameTeam);

			}
		}
	}

	void RoomPlayersAtualize(int NumOrder, int NumTeam){
	foreach (PhotonPlayer player in players) {
			for (int i = 0; i < players.Length; i++) {
				
				if (i == 0 || i == 2 || i == 4) {
					NameTeam = "A";
				} else {
					NameTeam = "B";
				}

			GameObject.Find ("Play" + i + "_Name").GetComponent<Text> ().text = players [i].name;
				GameObject.Find ("Play" + i + "_Level").GetComponent<Text> ().text = NameTeam;
				Debug.Log(players[i].name + " Info: " +players[i].customProperties);
				//pv.RPC("RoomInfo", PhotonTargets.All,  i , TempTeamName);
		}
	}
	}

	[PunRPC]
	void joinTeam (int team)
	{

		switch (team) {
		case 0:
			redTeamPlayers++;
			break;
		case 1:
			blueTeamPlayers++;
			break;
		}
	}

	[PunRPC]
	void leaveTeam (int team)
	{
		switch (team) {
		case 0:
			redTeamPlayers--;
			break;
		case 1:
			blueTeamPlayers--;
			break;
		}
	}
	[PunRPC]
	void RoomInfo(int i, string NameTeam){
		GameObject.Find ("Play" + i + "_Name").GetComponent<Text> ().text = players [i].name;
		GameObject.Find ("Play" + i + "_Level").GetComponent<Text> ().text = NameTeam;
		Debug.Log ("Fez: " + i);

	}
	[PunRPC]
	void updateTeamValues ()
	{
		redTeamPlayers = localRedPlayers;
		blueTeamPlayers = localBluePlayers;
	}
	[PunRPC]
	void OnLeftRoom ()
	{
		
	}
}

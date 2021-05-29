using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject TeamA_minionS;
	public GameObject TeamA_minionM;
	public GameObject TeamA_minionC;
	public GameObject TeamB_minionS;
	public GameObject TeamB_minionM;
	public GameObject TeamB_minionC;
	public GameObject minionS;
	public GameObject minionM;
	public GameObject minionC;
	public GameObject Target_TopA;
	public GameObject Target_TopB;
	public GameObject Target_BotA;
	public GameObject Target_BotB;
	public int i=0;
	public GameObject SpawnPoint;
	public GameObject Target;
	public PhotonView Photon;

	public enum SpawnChoices {OnTA = 0, OnTB = 1, OnBA = 2, OnBB = 3};
	SpawnChoices SpawnChoice;

	// Use this for initialization
	void Start () {
		Photon = this.GetComponent<PhotonView> ();
		if (PhotonNetwork.isMasterClient == true) {
			Photon.RPC ("DefineSpawn", PhotonTargets.All);
			SpawnTime ();
		}




	
//		if (this.gameObject.name == TopA.name ){
//			SpawnChoice = SpawnChoices.OnTA;
//			print ("Top A - Superman");
//		}
//
//		else if (this.gameObject.name == TopB.name){
//			SpawnChoice = SpawnChoices.OnTB;
//			print ("Top B - Batman");
//		}
//
//		else if (this.gameObject.name == BotA.name){
//			SpawnChoice = SpawnChoices.OnBA;
//			print ("Bot A - Robin");
//		}
//
//		else if (this.gameObject.name == BotB.name){
//			SpawnChoice = SpawnChoices.OnBB;
//			print ("Bot B - Jimmy");
//		}
//
//		else 
//		{
//			print ("Fudeu Geral Na Bagaça");
//		}


//		switch(SpawnChoice){
//		case SpawnChoices.OnTA:
//			this.SpawnPoint = TopA ;
//			minionS = TeamA_minionS;
//			minionC = TeamA_minionC;
//			minionM = TeamA_minionM;
//			SpawnTime ();
//			break;
//		case SpawnChoices.OnTB:
//			this.SpawnPoint = TopB;
//			minionS = TeamB_minionS;
//			minionC = TeamB_minionC;
//			minionM = TeamB_minionM;
//			SpawnTime ();
//			break;
//		case SpawnChoices.OnBA:
//			this.SpawnPoint = BotA;
//			minionS = TeamA_minionS;
//			minionC = TeamA_minionC;
//			minionM = TeamA_minionM;
//			SpawnTime ();
//			break;
//		case SpawnChoices.OnBB:
//			this.SpawnPoint = BotB;
//			minionS = TeamB_minionS;
//			minionC = TeamB_minionC;
//			minionM = TeamB_minionM;
//			SpawnTime ();
//			break;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SpawnMinionsS1()
	{
		i++;
		//GameObject minion_S = (GameObject)Instantiate(minionS);
		Vector3 SpawnOn = SpawnPoint.transform.position;
		GameObject minion_S = PhotonNetwork.Instantiate(minionS.name, SpawnOn,Quaternion.identity,0);

		//minion_S.GetComponent<Minion_BehaviorABot>().spawn_point = SpawnPoint;
		//minion_S.GetComponent<Minion_BehaviorABot>().target_pos = Target;
		//minion_S.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_S.GetComponent<Minion_Behavior>().target_pos = Target;

		if (SpawnPoint.tag == "WaypointA") 
		{
			if(SpawnPoint.name == "SpawnPointTopA")
			{
					minion_S.name = "MinionTopA"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotA")
			{
					minion_S.name = "MinionBotA"+i;
			
			}
		}

		if (SpawnPoint.tag == "WaypointB") 
		{
			if(SpawnPoint.name == "SpawnPointTopB")
			{
					minion_S.name = "MinionTopB"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotB")
			{
					minion_S.name = "MinionBotB"+i;
			}
		}

	}
	
	void SpawnMinionsS2()
	{
		//GameObject minion_S = (GameObject)Instantiate(minionS);
		Vector3 SpawnOn = SpawnPoint.transform.position;
		GameObject minion_S = PhotonNetwork.Instantiate(minionS.name, SpawnOn,Quaternion.identity,0);
		//minion_S.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_S.GetComponent<Minion_Behavior>().target_pos = Target;


		/*
		i++;
		GameObject minion_S = (GameObject)Instantiate(minionS);
		minion_S.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_S.GetComponent<Minion_Behavior>().target_pos = Target;
		
		if (SpawnPoint.tag == "WaypointA") 
		{
			if(SpawnPoint.name == "SpawnPointTopA")
			{
				minion_S.name = "MinionTopA"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotA")
			{
				minion_S.name = "MinionBotA"+i;
				
			}
		}
		
		if (SpawnPoint.tag == "WaypointB") 
		{
			if(SpawnPoint.name == "SpawnPointTopB")
			{
				minion_S.name = "MinionTopB"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotB")
			{
				minion_S.name = "MinionBotB"+i;
			}
		}
		*/

	}
	
	void SpawnMinionsS3()
	{
		GameObject minion_S = (GameObject)Instantiate(minionS);
		minion_S.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_S.GetComponent<Minion_Behavior>().target_pos = Target;

		/*
		i++;
		GameObject minion_S = (GameObject)Instantiate(minionS);
		minion_S.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_S.GetComponent<Minion_Behavior>().target_pos = Target;
		
		if (SpawnPoint.tag == "WaypointA") 
		{
			if(SpawnPoint.name == "SpawnPointTopA")
			{
				minion_S.name = "MinionTopA"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotA")
			{
				minion_S.name = "MinionBotA"+i;
				
			}
		}
		
		if (SpawnPoint.tag == "WaypointB") 
		{
			if(SpawnPoint.name == "SpawnPointTopB")
			{
				minion_S.name = "MinionTopB"+i;
			}
			if(SpawnPoint.name == "SpawnPointBotB")
			{
				minion_S.name = "MinionBotB"+i;
			}
		}
		*/
	}
	
	void SpawnMinionsM()
	{
		GameObject minion_M = (GameObject)Instantiate(minionM);
		minion_M.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_M.GetComponent<Minion_Behavior>().target_pos = Target;
	}
	
	
	void SpawnMinionCatapult()
	{
		GameObject minion_C = (GameObject)Instantiate(minionC);
		minion_C.GetComponent<Minion_Behavior>().spawn_point = SpawnPoint;
		minion_C.GetComponent<Minion_Behavior>().target_pos = Target;
	}
	
	void SpawnTime()
	{
		if(PhotonNetwork.isMasterClient){
			InvokeRepeating("SpawnMinionsS1",5.0F,30.0F);
			InvokeRepeating("SpawnMinionsS1",6.0F,31.0F);
			InvokeRepeating("SpawnMinionsS1",7.0F,32.0F);
			//InvokeRepeating("SpawnMinionsS2",6.0F,31.0F);
			//InvokeRepeating("SpawnMinionsS3",7.0F,32.0F);

			//InvokeRepeating("SpawnMinionsM",3.0F,13.0F);
			//InvokeRepeating("SpawnMinionCatapult",30.0F,30.0F);
				
		}
		}
	[PunRPC]
	void DefineSpawn(){
		SpawnPoint = GameObject.Find (this.gameObject.name);
		print ("Define Spawn");
		if(SpawnPoint.tag == "WaypointA"){
			if(SpawnPoint.name == "SpawnPointTopA" || this.SpawnPoint.name == "PointB7"){
				Target = GameObject.Find ("PointB7");
			}
			if(SpawnPoint.name == "SpawnPointBotA"){
				Target = GameObject.Find ("PointT7");
			}
			minionS = TeamA_minionS;
			minionC = TeamA_minionC;
			minionM = TeamA_minionM;
			print ("Team DC");
		}
		
		if(SpawnPoint.tag == "WaypointB"){
			if(SpawnPoint.name == "SpawnPointTopB"){
				Target = GameObject.Find ("PointT1");
			}
			if(SpawnPoint.name == "SpawnPointBotB"){
				Target = GameObject.Find ("PointB1");
			}
			minionS = TeamB_minionS;
			minionC = TeamB_minionC;
			minionM = TeamB_minionM;
			print ("Team Marvel");
		}
	}
}

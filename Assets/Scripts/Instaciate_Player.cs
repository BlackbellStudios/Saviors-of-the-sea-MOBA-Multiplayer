using UnityEngine;
using System.Collections;

public class Instaciate_Player : MonoBehaviour
{

		public Transform PlayerTransform;
		public Camera Camera;
	public RecievedMovement ThisPlayer;



		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnNetworkLoadedLevel ()
		{
		Instantiate (PlayerTransform, transform.position, transform.rotation);

			GameObject[] Players = GameObject.FindObjectsOfType (typeof(GameObject))	as GameObject[];
			foreach (GameObject go in Players) {
				if (go.gameObject.tag == "Player")
					go.GetComponentInChildren<Camera> ().depth = 2;
			}
		//ThisPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<RecievedMovement> ();
			
		}

		void OnPlayerDisconnected (NetworkPlayer player)
		{
				Debug.Log ("Clean up after player " + player);
				Network.RemoveRPCs (player);
				Network.DestroyPlayerObjects (player);
		}


		


	}
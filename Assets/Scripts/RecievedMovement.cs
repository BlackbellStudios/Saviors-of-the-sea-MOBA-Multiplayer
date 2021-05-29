using UnityEngine;
using System.Collections;

public class RecievedMovement : MonoBehaviour {
	
	
	Vector3 newposition;
	public float speed;
	public float walkRange;
	public int PlayerID;
	public GameObject graphics;
	public Camera Camera;
	
	
	void Start () {
		PlayerID = GetComponent<NetworkView>().GetInstanceID ();
		newposition = this.transform.position;
		//Camera.GetComponentInChildren<Camera> ();
	}
	
	
	void Update () {
		bool RMB = Input.GetMouseButtonDown (1);
		if (RMB) {
			
			RaycastHit hit;
			Ray ray = Camera.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit) && hit.transform.tag == "Ground") {
				//this.GetComponent<NetworkView> ().RPC ("RecievedMove", RPCMode.All, hit.point);
				GetComponent<NetworkView>().RPC("RecievedMove", RPCMode.All,  hit.point);
				
			}
			
		}

			if (Vector3.Distance (newposition, this.transform.position) > walkRange) {
				this.transform.position = Vector3.MoveTowards (this.transform.position, newposition, speed * Time.deltaTime);
				Quaternion transRot = Quaternion.LookRotation (newposition - this.transform.position, Vector3.up);
				graphics.transform.rotation = Quaternion.Slerp (transRot, graphics.transform.rotation, 0.7f);
			}
		
	}	
	
	[PunRPC]
	public void RecievedMove(Vector3 movePos){
		newposition = movePos;
	}
	
}

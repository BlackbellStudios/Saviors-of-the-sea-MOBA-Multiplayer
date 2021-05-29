using UnityEngine;
using System.Collections;

public class TiroTorreBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Translate(0,0,50);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "mapa")
		{
			Destroy(this.gameObject);
		}
	}
}

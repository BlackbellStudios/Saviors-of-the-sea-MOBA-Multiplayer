using UnityEngine;
using System.Collections;

public class NexusBehavior : MonoBehaviour {
	public int vidaMaxima = 800;   		
	public int vidaAtual = 800;	   		
	public GameObject healthBar;
	public float calc_Vida;
	public bool atacado = false;

	// Use this for initialization

	/*
	void OnPhotonServerView(PhotonStream stream, PhotonMessageInfo info)
	{
		//Escreve
		if(stream.isWriting ==true)
		{
			stream.SendNext(PlayersNum);
			stream.SendNext(NameTeam);
		}
		//Recebe Valores
		else
		{
			PlayersNum = (int)stream.ReceiveNext();
			NameTeam = (string)stream.ReceiveNext();
		}
	}
*/

	void Start () {
		vidaAtual = vidaMaxima;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (this.gameObject.tag == "TeamA" && col.gameObject.tag == "TeamB") 
		{
			print ("Torre atacada A");
			atacado = true;
			atacado = col.gameObject.GetComponent<Minion_Behavior>().attack;
			if(atacado == true)
			{
				this.vidaAtual -= 50;
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			}
		}
		if (this.gameObject.tag == "TeamB" && col.gameObject.tag == "TeamA") 
		{
			print ("Torre atacada B");
			atacado = true;
			atacado = col.gameObject.GetComponent<Minion_Behavior>().attack;
			if(atacado == true)
			{
				print ("Torre atacada B");
				this.vidaAtual -= 50;
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			}
		}
	}

	void AtualizarVida(float Minhavida)  //Pegando a escala da vida
	{
		this.healthBar.transform.localScale = new Vector3 (Minhavida, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}

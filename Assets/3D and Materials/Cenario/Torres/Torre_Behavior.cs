using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Torre_Behavior : MonoBehaviour {
	public List <Transform> Enemies;


	public int vidaMaxima = 300;   		//Vida Maxima do Minion
	public int vidaAtual = 300;	   		//Vida Atual do Minion
	public GameObject healthBar;
	public float calc_Vida;


	public int dano = 10;		   		//Dano Causado pelo Minion
	public float alcanceATK = 5.0f;		//Alcance do Ataque do Minion
	public float delayATK = 0.0f;		//Coldown do ataque do Minion
	public float nextATK = 0.0f;		//
	public bool attack = false;
	//public GameObject target;
	public float distance;
	public int InimigoVida;

	public GameObject tiroTorre;
	public GameObject tiroTorreOrigin;

	public Transform bestTarget = null;
	public Transform SelectedTarget;

	public int ind;
	public bool spawn_Bool;

	public bool atacado = false;
	public bool atacadoHeroi = false;
	void Start () 
	{
		SelectedTarget = null;
		Enemies = new List<Transform>();
		vidaAtual = vidaMaxima;
		//AddEnemiesToList();
		DelayTiro();
	}
	
	public void AddEnemiesToList()
	{
		if(this.gameObject.tag =="TeamA")
		{
			GameObject[] ItemsInList = GameObject.FindGameObjectsWithTag("TeamB");
		
			foreach(GameObject _Enemy in ItemsInList)
			{
				AddTarget(_Enemy.transform);
			}

		}

		if(this.gameObject.tag =="TeamB")
		{
			GameObject[] ItemsInList = GameObject.FindGameObjectsWithTag("TeamA");
			
			foreach(GameObject _Enemy in ItemsInList)
			{
				AddTarget(_Enemy.transform);
			}
		}
	}
	
	public void AddTarget(Transform enemy)
	{
		Enemies.Add(enemy);
	}
	
	public void DistanceToTarget()
	{
		Enemies.Sort(delegate( Transform t1, Transform t2){ 
			return Vector3.Distance(t1.position,this.transform.position).CompareTo(Vector3.Distance(t2.position,this.transform.position)); 
		});
		bestTarget = Enemies[0];

	}
	
	public void TargetedEnemy() 
	{
		if(SelectedTarget == null && spawn_Bool == true)
		{
			SelectedTarget = bestTarget;

		}
		this.distance = Vector3.Distance (this.gameObject.transform.position,bestTarget.transform.position);

		if(this.distance <=3000)
		{
			DistanceToTarget ();
			if(delayATK == 0 )
			{
				this.tiroTorreOrigin.transform.LookAt(bestTarget.transform.position);
				Instantiate(tiroTorre);
				delayATK = 5.0f;
				this.tiroTorre.transform.position = tiroTorreOrigin.transform.position;
				this.tiroTorre.transform.LookAt(bestTarget.transform.position);
			}	
		}
		
	}
	
	void Update () 
	{
		//Apagando objetos que forem nulos na lista
		if (bestTarget == null) 
		{
			ind = Enemies.IndexOf(bestTarget);
			Enemies.RemoveAt(ind);
		}
		if(this.vidaAtual <= 0)
		{
			Destroy(this.gameObject);
			
		}


		Enemies.Sort(delegate( Transform t1, Transform t2){ 
			return Vector3.Distance(t1.transform.position,this.transform.position).CompareTo(Vector3.Distance(t2.transform.position,this.transform.position)); 
		});
		bestTarget = Enemies[0];


		TargetedEnemy();
		//float dist = Vector3.Distance(SelectedTarget.transform.position,transform.position);
	}

	void AddList()
	{
		Enemies.Clear();
		Enemies = new List<Transform>();

		AddEnemiesToList();
		DistanceToTarget ();
		//Apagando objetos que forem nulos na lista
		if (bestTarget == null) 
		{
			ind = Enemies.IndexOf(bestTarget);
			Enemies.RemoveAt(ind);
		}
	}
	
	void DelayATK()
	{
		delayATK= 0f;
		//attack=true;
	}
	void DelayTiro()
	{
		InvokeRepeating("DelayATK",5.0F,5.0F);
		InvokeRepeating ("AddList", 6.0f, 34.0f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (this.gameObject.tag == "TeamA" && col.gameObject.tag == "TeamB") 
		{
			print ("Torre atacada A");
			//atacado = true;
			atacado = col.gameObject.GetComponent<Minion_Behavior>().attack;
			if(atacado == true)
			{

				this.vidaAtual -= 10;
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			}

			atacadoHeroi = col.gameObject.GetComponent<Player_Moba>().atacou;
			if(atacadoHeroi ==true)
			{
				this.vidaAtual -= 25;
				print ("Torre atacada A" +vidaAtual);
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			}
		}

		if (this.gameObject.tag == "TeamB" && col.gameObject.tag == "TeamA") 
		{
			print ("Torre atacada B");
			//atacado = true;
			atacado = col.gameObject.GetComponent<Minion_Behavior>().attack;
			//atacadoHeroi = col.gameObject.GetComponent<Player_Moba>().atacou;
			if(atacado == true)
			{

				this.vidaAtual -= 10;
				print ("Torre atacada B" +vidaAtual);
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			}

			//verifica se a variavel de ataque do heroi inimigo esta verdadeira e pega seu valor

			//if(atacadoHeroi ==true)
			//{
				this.vidaAtual -= 25;
				print ("Torre atacada B" +vidaAtual);
				this.calc_Vida = vidaAtual / vidaMaxima;
				AtualizarVida(this.calc_Vida);
			//}
		}
	}

	void AtualizarVida(float Minhavida)  //Pegando a escala da vida
	{
		this.healthBar.transform.localScale = new Vector3 (Minhavida, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}


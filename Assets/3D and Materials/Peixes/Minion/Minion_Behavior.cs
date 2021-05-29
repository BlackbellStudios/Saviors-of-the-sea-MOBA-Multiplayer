using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Minion_Behavior : MonoBehaviour {

	public Animator minion;

	public GameObject target_pos;
	public GameObject spawn_point;

	//Variaveis de ataque
	public float vidaMaxima = 100f;   		//Vida Maxima do Minion
	public float vidaAtual = 0f;	   		//Vida Atual do Minion
	public GameObject healthBar;
	public float calc_Vida;

	public int dano = 10;		   		//Dano Causado pelo Minion
	public float alcanceATK = 5.0f;		//Alcance do Ataque do Minion
	public float delayATK = 0.0f;		//Coldown do ataque do Minion	
	public bool attack = false;
	public GameObject target;
	public float distance;
	public int InimigoVida;

	public GameObject TopA;
	public GameObject TopB;
	public GameObject BotA;
	public GameObject BotB;
	
	public enum Caminhos {TopA = 0, TopB = 1, BotA = 2, BotB = 3};

	//Variaveis pra localizar alvo do ataque
	public List <Transform> EnemiesM;
	//public Transform SelectedTarget;
	public Transform bestTarget = null;


	public int ind;
	public int indNull;
	public int i;
	// Use this for initialization
	Caminhos Caminho;
	void Start () {
		minion = GetComponentInChildren<Animator>();

		//SelectedTarget = null;
		EnemiesM = new List<Transform>();
		Delay ();

		vidaAtual = vidaMaxima;
		//this.transform.position = spawn_point.transform.position;
		AddEnemiesToList ();
		EnemiesM.Sort(delegate( Transform t1, Transform t2){ 
			return Vector3.Distance(t1.transform.position,this.transform.position).CompareTo(Vector3.Distance(t2.transform.position,this.transform.position)); 
		});
		bestTarget = EnemiesM[0];



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
		EnemiesM.Add(enemy);
	}
	
	public void DistanceToTarget()
	{
		//Distribuir qual inimigo esta mais perto
		EnemiesM.Sort(delegate( Transform t1, Transform t2){ 
			return Vector3.Distance(t1.position,this.transform.position).CompareTo(Vector3.Distance(t2.position,this.transform.position)); 
		});
		bestTarget = EnemiesM[0];
	}
	
	public void TargetedEnemy() 
	{
		if(bestTarget == null)
		{
			ind = EnemiesM.IndexOf(bestTarget);
			EnemiesM.RemoveAt(ind);
			//DistanceToTarget();
			//SelectedTarget = Enemies[0];
		}
		this.distance = Vector3.Distance (this.gameObject.transform.position,bestTarget.transform.position);	
	}

	void Update () {
		if(EnemiesM.Contains(null)){
			indNull = EnemiesM.IndexOf (null);
			EnemiesM.RemoveAt (indNull);
			DistanceToTarget();
		}
		//Apagando objetos que forem nulos na lista
		if (bestTarget == null) 
		{
			ind = EnemiesM.IndexOf(bestTarget);
			EnemiesM.RemoveAt(ind);
		}

		//Removendo Objeto ao morrer
		if(this.vidaAtual <= 0)
		{
			Destroy(gameObject);

		}
	//	if(this.vidaAtual >0){
			EnemiesM.Sort(delegate( Transform t1, Transform t2){ 
				return Vector3.Distance(t1.transform.position,this.transform.position).CompareTo(Vector3.Distance(t2.transform.position,this.transform.position)); 
			});
			bestTarget = EnemiesM[0];

			ind = EnemiesM.IndexOf(bestTarget); // pegar o index do best target

			TargetedEnemy();

			if(bestTarget == null || bestTarget != null && distance > 1500)
			{
				this.transform.LookAt (target_pos.transform.position);
				this.transform.Translate (0, 0, 400 * Time.deltaTime);
			}

			if(bestTarget != null && distance < 1500)
			{
				this.transform.LookAt(bestTarget.transform.position);
				this.transform.Translate(0,0,200 * Time.deltaTime);
				if(distance < 600)
				{
					AttackMove();
				}
			}
	//	}
	}

	void AddList()
	{
		EnemiesM.Clear();
		EnemiesM = new List<Transform>();

		AddEnemiesToList();
	}

	void DelayATK()
	{
		delayATK= 0f;
		//attack=true;
		
	}
	void Delay()
	{
		InvokeRepeating("DelayATK",1.0F,2.0F);
		InvokeRepeating("AddList",35.0f,35.0f);
	}

	void OnCollisionEnter(Collision colisao)
	{
		if (colisao.gameObject.tag == "WaypointB" && this.gameObject.tag == "TeamA") 
		{
			target_pos =colisao.gameObject.GetComponent<Waypoints>().destinoA;		
		}

		if (colisao.gameObject.tag == "WaypointB" & this.gameObject.tag == "TeamB" ) 
		{
			target_pos =colisao.gameObject.GetComponent<Waypoints>().destinoB;		
		}
		
		if (colisao.gameObject.tag == "TeamB" && this.gameObject.tag=="TeamA" && attack ==true) 
		{
			AttackMove();
			print ("Atacou");
			this.calc_Vida = vidaAtual / vidaMaxima;
			AtualizarVida(this.calc_Vida);
		}

		
		if (colisao.gameObject.tag == "TeamA" && this.gameObject.tag=="TeamB" && attack ==true) 
		{
			AttackMove();
			print ("Atacou");
			this.calc_Vida = vidaAtual / vidaMaxima;
			AtualizarVida(this.calc_Vida);
		}

		if (colisao.gameObject.tag == "tiroTorre") 
		{

			this.vidaAtual -= 10;
			this.calc_Vida = vidaAtual / vidaMaxima;
			AtualizarVida(this.calc_Vida);
		
		}
	}
	
	void AtualizarVida(float Minhavida)  //Pegando a escala da vida
	{
		this.healthBar.transform.localScale = new Vector3 (Minhavida, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	public void AttackMove()
	{
		if(this.distance <=1000)
		{
			attack=true;
			this.transform.Translate(0,0,0);

			this.transform.LookAt(bestTarget.transform.position);
			//DistanceToTarget ();
			if(delayATK == 0 )
			{
				this.minion.SetTrigger("atq");
				this.vidaAtual -= 10;
				delayATK = 5.0f;
			}
			
		}
	}
}

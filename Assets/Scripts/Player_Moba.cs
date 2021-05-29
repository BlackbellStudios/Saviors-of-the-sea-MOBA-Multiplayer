using UnityEngine;
using System.Collections;

public class Player_Moba : MonoBehaviour
{
  public Animator heroi_anim;
  public UnityEngine.AI.NavMeshAgent agent;
  public Vector3 TargetDestination;
  public PhotonView Photon;
  public Camera MyCam;
  public int MyTeam;

  //Variaveis de ataque
  public float vidaMaxima = 100f;       //Vida Maxima do Minion
  public float vidaAtual = 0f;        //Vida Atual do Minion
  public GameObject healthBar;
  public float calc_Vida;

  public int dano = 25;
  public float distance;
  public Transform bestTarget = null;
  public GameObject alvo;
  public float delayATK = 0.0f;
  public bool atacou = false;

  // Use this for initialization
  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    //Your code here..
  }

  void Start()
  {
    heroi_anim = GetComponentInChildren<Animator>();
    int? Team = PhotonNetwork.player.customProperties["Team"] as int?;
    MyTeam = (int)Team;
    if (MyTeam == 0)
    {
      this.gameObject.tag = "TeamA";
    }
    if (MyTeam == 1)
    {
      this.gameObject.tag = "TeamB";
    }


    agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    //agent = this.gameObject.GetComponentInChildren<NavMeshAgent> ();
    Photon = this.GetComponent<PhotonView>();
    if (Photon.isMine)
    {
      MyCam.depth = 2;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Photon.isMine)
    {
      if (Input.GetMouseButtonDown(1))
      {
        //Move ();
        Photon.RPC("Move", PhotonTargets.All);
      }

      if (Input.GetMouseButtonDown(1))
      {
        Photon.RPC("Attack", PhotonTargets.All);

      }
    }
  }
  void OnCollisionEnter(Collision colisao)
  {
    /*
		if (colisao.gameObject.tag == "TeamA" && this.gameObject.tag=="TeamB" && attack ==true) 
		{
			AttackMove();
			print ("Atacou");
			this.calc_Vida = vidaAtual / vidaMaxima;
			AtualizarVida(this.calc_Vida);
		}
		*/
    if (colisao.gameObject.tag == "tiroTorre")
    {
      this.vidaAtual -= 10;
      this.calc_Vida = vidaAtual / vidaMaxima;
      AtualizarVida(this.calc_Vida);
    }
  }

  void AtualizarVida(float Minhavida)  //Pegando a escala da vida
  {
    this.healthBar.transform.localScale = new Vector3(Minhavida, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
  }

  public void AttackMove()
  {
    if (this.distance <= 1000)
    {
      this.transform.Translate(0, 0, 0);
      this.transform.LookAt(bestTarget.transform.position);
    }
  }

  [PunRPC]
  void Move()
  {
    Ray ray = MyCam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
      TargetDestination = hit.point;
      agent.SetDestination(TargetDestination);
    }
  }

  [PunRPC]
  void Attack()
  {
    Ray ray;
    RaycastHit hit;
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit))
    {

      if (hit.transform.tag == "TeamA" && this.gameObject.tag == "TeamB")
      {
        print("Inimigo detectado TeamA");
        bestTarget = hit.transform;
        this.transform.LookAt(bestTarget);
        this.transform.Translate(10 * Time.deltaTime, 0, 10 * Time.deltaTime);

        if (this.distance <= 500)
        {
          //Delay();
          ///if(delayATK == 0)
          //{
          heroi_anim.SetTrigger("atq");
          //}
        }
        if (this.distance > 500)
        {
          //CancelInvoke(Delay);
        }
      }

      if (hit.transform.tag == "TeamB" && this.gameObject.tag == "TeamA")
      {
        print("Inimigo detectado TeamB");
        bestTarget = hit.transform;
        this.transform.LookAt(bestTarget);
        this.transform.Translate(10 * Time.deltaTime, 0, 10 * Time.deltaTime);

        if (this.distance <= 500)
        {
          Delay();
          if (delayATK == 0)
          {
            atacou = true;
            heroi_anim.SetTrigger("atq");
            delayATK = 5.0f;

          }
        }
        if (this.distance > 500)
        {
          //CancelInvoke(Delay);
        }
      }
    }
    //bestTarget = 
    this.distance = Vector3.Distance(this.gameObject.transform.position, bestTarget.transform.position);
  }

  void DelayATK()
  {
    delayATK = 0f;
  }
  void Delay()
  {
    InvokeRepeating("DelayATK", 1.0F, 2.0F);
  }
}

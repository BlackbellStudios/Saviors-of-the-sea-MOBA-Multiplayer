using UnityEngine;
using System.Collections;

public class AudiosBG : MonoBehaviour {
	public AudioClip audio1;
	public AudioClip audio2;
	public AudioClip audio3;
	public AudioSource audioBG;

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
		AudioSource audioBG = GetComponent<AudioSource>();
		DelayAudio();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlayAudio1()
	{
		audioBG.PlayOneShot(audio1,0.7f);

	}

	public void PlayAudio2()
	{
		audioBG.PlayOneShot(audio2,0.7f);
		
	}

	public void PlayAudio3()
	{
		audioBG.PlayOneShot(audio3,1.0f);
	}

	public void DelayAudio()
	{
		InvokeRepeating("PlayAudio1",2.0f,30.0f);
		InvokeRepeating("PlayAudio2",2.0f,18.0f);
		InvokeRepeating("PlayAudio3",5.0f,180.0f);
	}
}

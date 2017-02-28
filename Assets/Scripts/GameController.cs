using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class GameController : MonoBehaviour {
	public int marketTimer;
	public int marketTimerMax;
	public float timercount;
	public static int score;
	public static int coints;
	public Text timerUI;
	public Text scoreUI;
	public Text cointsUI;

	public bool enchente;
	public bool enchenteTut;
	public bool seca;
	public bool secaTut;
	public bool lixo;
	public bool lixoTut;
	public GameObject[] tutorialImages;
	public GameObject[] uiAvisos;


	public float desastresCooldown;

	void Start () {

		//LOLSDK.Init ("com.ticjoy.jogodaplantinha");

		LOLSDK.Instance.StopSound ("Menu_e_zerada.mp3");
		LOLSDK.Instance.PlaySound("Gameplay.mp3", true, true);

		//CAPTURA DE ELEMENTOS UI
		timerUI = GameObject.Find ("TimeText").GetComponent<Text> ();
		scoreUI = GameObject.Find ("ScoreText").GetComponent<Text> ();
		cointsUI = GameObject.Find ("CashText").GetComponent<Text> ();

		//PARAMETROS INICIAIS
		this.marketTimer = marketTimerMax;
		coints = 15;
		score = 0;

	}

	void Update () {
		//CONTROLE TEMPO DO MERCADO
		timercount += Time.deltaTime;
		if (timercount > 1) {
			timercount = 0;
			marketTimer -= 1;
		}
		if (marketTimer <= 0) {
			marketTimer = marketTimerMax;
		}

		if (this.marketTimer == 0) {
			LOLSDK.Instance.CompleteGame();
		}

		//ATUALIZA UI
		timerUI.text = marketTimer.ToString ();
		scoreUI.text = score.ToString ();
		cointsUI.text = coints.ToString ();



		desastresCooldown += Time.deltaTime;
		if (timercount < 500 && desastresCooldown>45) {
			TriggerDesastres ();
			desastresCooldown = 0;
		}

		if (timercount < 500 && desastresCooldown>15) {
			seca = false;
			enchente = false;
			lixo = false;
			uiAvisos [0].SetActive (false);
			uiAvisos [1].SetActive (false);
			uiAvisos [2].SetActive (false);
		}
	}

	public void UnPause(){
		Time.timeScale = 1;
	}

	public void TriggerDesastres(){
		switch (Random.Range(1,4)) {
		case 1:
			enchente = true;
			if (enchenteTut == false) {
				tutorialImages [0].SetActive (true);
				uiAvisos [0].SetActive (true);
				LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				Time.timeScale = 0;
				enchenteTut = true;
			}
			break;
		case 2:
			seca = true;
			if (secaTut == false) {
				tutorialImages [1].SetActive (true);
				uiAvisos [1].SetActive (true);
				LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				Time.timeScale = 0;
				secaTut = true;
			}
			break;
		case 3:
			lixo = true;
			if (lixoTut == false) {
				tutorialImages [2].SetActive (true);
				uiAvisos [2].SetActive (true);
				LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				Time.timeScale = 0;
				lixoTut = true;
			}
			break;
		default:
			break;
		}
	}

}

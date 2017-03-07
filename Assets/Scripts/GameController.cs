using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
	public int marketTimer;
	public int marketTimerMax;
	public float timercount;
	public static int score;
	public static int scorefinal;
	public static int coints;
	public Text timerUI;
	public Text scoreUI;
	public Text cointsUI;
	public Text scoreFinal;

	public bool enchente;
	public bool enchenteTut;
	public bool seca;
	public bool secaTut;
	public bool lixo;
	public bool lixoTut;
	public GameObject[] tutorialImages;
	public GameObject[] uiAvisos;

	public Vector3 novaPosicao;

	public GameObject tela_win;
	public GameObject tela_loser;

	public float desastresCooldown;

	void Start () {

		//LOLSDK.Init ("com.ticjoy.jogodaplantinha");

		LOLSDK.Instance.StopSound ("Menu_e_zerada.mp3");
		LOLSDK.Instance.PlaySound("Gameplay.mp3", true, true);
		//ESTAGIO PLANTAS


		//CAPTURA DE ELEMENTOS UI
		timerUI = GameObject.Find ("TimeText").GetComponent<Text> ();
		scoreUI = GameObject.Find ("ScoreText").GetComponent<Text> ();
		cointsUI = GameObject.Find ("CashText").GetComponent<Text> ();

		//PARAMETROS INICIAIS
		this.marketTimer = marketTimerMax;
		coints = 20;
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
			LOLSDK.Instance.CompleteGame ();
		}

		//ATUALIZA UI
		timerUI.text = marketTimer.ToString ();
		scoreUI.text = score.ToString ();
		cointsUI.text = coints.ToString ();


		desastresCooldown += Time.deltaTime;
		if (timercount < 500 && desastresCooldown > 45) {
			TriggerDesastres ();
			desastresCooldown = 0;
		}

		if (timercount < 500 && desastresCooldown > 15) {
			seca = false;
			enchente = false;
			lixo = false;
			uiAvisos [0].SetActive (false);
			uiAvisos [1].SetActive (false);
			uiAvisos [2].SetActive (false);
		}
		if (marketTimer < 2) {
			if (score > 100) {
				tela_win.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);

				scoreFinal = GameObject.Find ("scoreFinal").GetComponent<Text> ();
				scoreFinal.text = score.ToString ();
			} else {
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
		}
		if (score > 200) {
			tela_win.SetActive(true);
			Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
			Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);
			scoreFinal = GameObject.Find ("scoreFinal").GetComponent<Text> ();
			scoreFinal.text = score.ToString ();
		}
		if (marketTimer < 140) {
			if (score < 90) {
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
		}
		if (marketTimer < 240) {
			if (score < 70) {//70
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
		}
		if (marketTimer < 330) {
			if (score < 50) {//70
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
		}
		if (marketTimer < 440) {
			if (score < 30) {//40
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
		}
		//APENAS TESTES
		if (marketTimer < 490) {
			if (score < 10) {//40
				tela_loser.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
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
	public void VoltarmenuStartClicked(){
		SceneManager.LoadScene ("voltar_menu");

	}
	public void reiniciarStartClicked(){
		SceneManager.LoadScene ("Jogo");

	}

}

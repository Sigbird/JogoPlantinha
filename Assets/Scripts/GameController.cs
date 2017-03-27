﻿using System.Collections;
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
	public static int progress;
	public static int maxProgress;
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
	public bool testador;
	public bool FUllMILHO;
	public bool FUllGIRASOL;
	public bool FUllTOMATE;
	public bool FUllVIOLA;
	public bool jaPASSOU2;

	public GameObject[] tutorialImages;
	public GameObject[] uiAvisos;

	public Vector3 novaPosicao;

	public GameObject tela_win;
	public GameObject tela_loser;
	public GameObject telahelp;
	public GameObject tela_win_full;
	public GameObject tela_win_fulltime;
	public GameObject tela_loser_fulltime;
	public GameObject chekMILHO;
	public GameObject chekGIRASOL;
	public GameObject chekTOMATE;
	public GameObject chekVIOLA;


	public float desastresCooldown;

	void Start () {
		//TELAS DE TURTORIAIS
		telas();
		GameController.maxProgress = 19;
		LOLSDK.Instance.SubmitProgress(0, 0, 19);

		LOLSDK.Instance.StopSound ("Menu_e_zerada.mp3");
		LOLSDK.Instance.PlaySound("Gameplay.mp3", true, true);
		//ESTAGIO PLANTAS

		//CAPTURA DE ELEMENTOS UI
		timerUI = GameObject.Find ("TimeText").GetComponent<Text> ();
		scoreUI = GameObject.Find ("ScoreText").GetComponent<Text> ();
		cointsUI = GameObject.Find ("CashText").GetComponent<Text> ();

		//PARAMETROS INICIAIS
		this.marketTimer = marketTimerMax;
		coints = 15;
		score = 0;

		telahelp.SetActive (false);
		tela_win_full.SetActive(false);
		tela_win_fulltime.SetActive(false);
		tela_loser_fulltime.SetActive (false);

		Button helpButton = GameObject.Find ("helpButton").GetComponent<Button> ();
		helpButton.onClick.AddListener (helpButtonStartClicked);

		FUllMILHO = false;
		FUllGIRASOL = false;
		FUllTOMATE = false;
		FUllVIOLA = false;
		jaPASSOU2 = true;

		chekMILHO.SetActive (false);
		chekGIRASOL.SetActive (false);
		chekTOMATE.SetActive (false);
		chekVIOLA.SetActive (false);
	}

	public void telas(){
		if ((PlayerPrefs.GetInt ("turtoriais")) == 1) {
			testador = true;
		} else {
			testador = false;
		}			
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
		if (this.marketTimer < 2 & jaPASSOU2 == true) {
			jaPASSOU2 = false;

			//ACABOU O TEMPO E CONSEGUIU FAZER OS 4 TIPOS DE PLANTAS
			if (FUllMILHO == true & FUllGIRASOL == true & FUllTOMATE == true & FUllVIOLA == true) {
				tela_win_full.SetActive(true);
				scoreFinal = GameObject.Find ("scoreFinal").GetComponent<Text> ();
				scoreFinal.text = score.ToString ();
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
				Time.timeScale = 0;
			//ACABOU O TEMPO E NÃO CONSEGUIU FAZER OS 4 TIPOS DE PLANTAS
			} else {
				tela_loser_fulltime.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);

				Button reiniciar = GameObject.Find ("reiniciar").GetComponent<Button> ();
				reiniciar.onClick.AddListener (reiniciarStartClicked);
			}
			LOLSDK.Instance.CompleteGame ();
			 
		}

	}

	public void UnPause(){
		Time.timeScale = 1;
	}

	public void TriggerDesastres(){
		Destroy (GameObject.FindGameObjectWithTag ("draggable"));
		switch (Random.Range(1,4)) {
		case 1:
			enchente = true;
			if (enchenteTut == false) {
				tutorialImages [0].SetActive (testador);
				uiAvisos [0].SetActive (true);
				if (testador) {
					LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				}
				if (testador == true) {
					Time.timeScale = 0;
					enchenteTut = true;

				} else {
					Time.timeScale = 1;
				}
				//enchenteTut = true;
			}
			break;
		case 2:
			seca = true;
			if (secaTut == false) {
				tutorialImages [1].SetActive (testador);
				uiAvisos [1].SetActive (true);
				if (testador) {
					LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				}
				if (testador == true) {
					Time.timeScale = 0;
					secaTut = true;
				} else {
					Time.timeScale = 1;
				}
				//secaTut = true;
			}
			break;
		case 3:
			lixo = true;
			if (lixoTut == false) {
				tutorialImages [2].SetActive (testador);
				uiAvisos [2].SetActive (true);
				if (testador) {
					LOLSDK.Instance.PlaySound("Telas_educativa.mp3", false, false);
				}
				if (testador == true) {
					Time.timeScale = 0;
					lixoTut = true;
				} else {
					Time.timeScale = 1;
				}
				//lixoTut = true;
			}
			break;
		default:
			break;
		}
	}

	public static void UpdateProgress(){
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);
	}

	public void VoltarmenuStartClicked(){
		LOLSDK.Instance.StopSound ("Gameplay.mp3");


		SceneManager.LoadScene ("voltar_menu");

	}
	public void reiniciarStartClicked(){
		StartCoroutine (reiniciarfase());
	}

	IEnumerator reiniciarfase(){
		LOLSDK.Instance.StopSound ("Gameplay.mp3");
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("Jogo");
	}

	public void helpButtonStartClicked(){
		telahelp.SetActive (true);
		Button vjButton = GameObject.Find ("vjButton").GetComponent<Button> ();
		vjButton.onClick.AddListener (vjButtonStartClicked);
		Time.timeScale = 0;
	}
	public void vjButtonStartClicked(){
		telahelp.SetActive (false);
		UnPause ();
	}

	public void voltarProJOGOStartClicked(){
		tela_win_full.SetActive (false);
		UnPause ();
	}
}

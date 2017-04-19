using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
	
	public PlantController Plant;
	public PlantController Plant2;
	public PlantController Plant3;
	public PlantController Plant4;

	public int marketTimer;
	public int marketTimerMax;
	public float timercount;
	public float minutecount;
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
	public bool pisca;

	public GameObject[] tutorialImages;
	public GameObject[] uiAvisos;

	public Vector3 novaPosicao;

	public GameObject telahelp;
	public GameObject tela_win_full;
	public GameObject tela_loser_fulltime;
	public GameObject chekMILHO;
	public GameObject chekGIRASOL;
	public GameObject chekTOMATE;
	public GameObject chekVIOLA;

	public GameObject seta_pisca;
	public GameObject seta_piscaDerrota;

	public float desastresCooldown;

	void Start () {
		UnPause ();
		StartCoroutine (implementProgress ());
		//TELAS DE TURTORIAIS
		telas();
		GameController.maxProgress = 10;
		LOLSDK.Instance.SubmitProgress(0, 0, 10);

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
		tela_loser_fulltime.SetActive (false);

		Button helpButton = GameObject.Find ("helpButton").GetComponent<Button> ();
		helpButton.onClick.AddListener (helpButtonStartClicked);

		FUllMILHO = false;
		FUllGIRASOL = false;
		FUllTOMATE = false;
		FUllVIOLA = false;
		jaPASSOU2 = true;
		pisca = false;

		chekMILHO.SetActive (false);
		chekGIRASOL.SetActive (false);
		chekTOMATE.SetActive (false);
		chekVIOLA.SetActive (false);
		seta_pisca.SetActive (false);
		seta_piscaDerrota.SetActive (false);
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
		minutecount += Time.deltaTime;
		if (timercount > 1) {
			timercount = 0;
			marketTimer -= 1;
		}
		if (marketTimer <= 0) {
			marketTimer = marketTimerMax;
		}

		if (minutecount >= 60) {
			minutecount = 0;
			//GameController.progress += 1;
			//LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);
			//Debug.Log ("progresso"+progress);
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

				pisca = true;
				Plant.jogando = false;
				Plant2.jogando = false;
				Plant3.jogando = false;
				Plant4.jogando = false;

				StartCoroutine (piscando ());
				tela_win_full.SetActive(true);
				scoreFinal = GameObject.Find ("scoreFinal").GetComponent<Text> ();
				scoreFinal.text = score.ToString ();
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);
				//Time.timeScale = 0;
			//ACABOU O TEMPO E NÃO CONSEGUIU FAZER OS 4 TIPOS DE PLANTAS
			} else {
				pisca = true;
				Plant.jogando = false;
				Plant2.jogando = false;
				Plant3.jogando = false;
				Plant4.jogando = false;

				StartCoroutine (piscandoDerrota ());
				tela_loser_fulltime.SetActive(true);
				Button Voltarmenu = GameObject.Find ("Voltarmenu").GetComponent<Button> ();
				Voltarmenu.onClick.AddListener (VoltarmenuStartClicked);
				Button VoltarmenuOver = GameObject.Find ("VoltarmenuOver").GetComponent<Button> ();
				VoltarmenuOver.onClick.AddListener (VoltarmenuOverStartClicked);
				//Time.timeScale = 0;

			}
			 
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
			}
			break;
		default:
			break;
		}
	}
		

	public void VoltarmenuStartClicked(){
		LOLSDK.Instance.CompleteGame ();
		LOLSDK.Instance.StopSound ("Gameplay.mp3");
		pisca = false;
	}
	public void VoltarmenuOverStartClicked(){
		LOLSDK.Instance.StopSound ("Gameplay.mp3");
		SceneManager.LoadScene ("voltar_menu");
		pisca = false;

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



	IEnumerator implementProgress(){
		yield return new WaitForSeconds (62);
		GameController.progress = 1;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 2;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 3;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 4;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 5;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 6;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 7;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 8;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 9;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

		yield return new WaitForSeconds (60);
		GameController.progress = 10;
		LOLSDK.Instance.SubmitProgress(GameController.score, GameController.progress, GameController.maxProgress);

	}



	IEnumerator piscando(){
		yield return new WaitForSeconds (0.3f);
		if (pisca) {
			seta_pisca.SetActive (true);
			StartCoroutine (piscandoFalso ());
		}
	}
	IEnumerator piscandoFalso(){
		yield return new WaitForSeconds (0.3f);
		if (pisca) {
			seta_pisca.SetActive (false);
			StartCoroutine (piscando ());
		}
	}


	IEnumerator piscandoDerrota(){
		yield return new WaitForSeconds (0.3f);
		if (pisca) {
			seta_piscaDerrota.SetActive (true);
			StartCoroutine (piscandoFalsoDerrota ());
		}
	}
	IEnumerator piscandoFalsoDerrota(){
		yield return new WaitForSeconds (0.3f);
		if (pisca) {
			seta_piscaDerrota.SetActive (false);
			StartCoroutine (piscandoDerrota ());
		}
	}
}


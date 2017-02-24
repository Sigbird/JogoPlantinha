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


	void Start () {

		LOLSDK.Init ("com.ticjoy.jogodaplantinha");

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

		LOLSDK.Instance.SubmitProgress(0, score, 100);
	}
}

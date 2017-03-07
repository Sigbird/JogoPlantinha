using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using LoLSDK;
using UnityEngine.SceneManagement;

public class voltar_menu : MonoBehaviour {
	public AudioClip clip;

	void Start () {
		//LOLSDK.Init ("com.ticjoy.jogodaplantinha");
		//LOLSDK.Instance.PlaySound("Menu_e_zerada.mp3", true, true);
		Button playButton = GameObject.Find ("playButton").GetComponent<Button> ();
		playButton.onClick.AddListener (playButtonStartClicked);
	}

	public void playButtonStartClicked(){
		SceneManager.LoadScene ("intro");

	}
}

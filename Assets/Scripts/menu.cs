using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LoLSDK;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
	public AudioClip clip;
	public Toggle myToggle;
	// Use this for initialization
	void Start () {
		LOLSDK.Init ("com.ticjoy.jogodaplantinha");
		LOLSDK.Instance.PlaySound("Menu_e_zerada.mp3", true, true);
		Button playButton = GameObject.Find ("playButton").GetComponent<Button> ();
		playButton.onClick.AddListener (playButtonStartClicked);
//		this.GetComponent<AudioSource> ().clip = clip;
//		this.GetComponent<AudioSource> ().loop = true;
//		this.GetComponent<AudioSource> ().Play();

	}
	void Update () {
		if(myToggle.isOn) {
			PlayerPrefs.SetInt("turtoriais", 1);
		} else {
			PlayerPrefs.SetInt("turtoriais", 0);

		}
	}


		
	public void playButtonStartClicked(){
		if ((PlayerPrefs.GetInt ("turtoriais")) == 1) {
			SceneManager.LoadScene ("intro");
		} else {
			SceneManager.LoadScene ("Jogo");

		}

	}
}

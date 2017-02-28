using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transicao_telas : MonoBehaviour {
	public int intro;
	public GameObject intro01;
	public GameObject intro02;
	public GameObject intro03;
	public GameObject intro04;
	public GameObject botao01;
	public Transform botao;
	public Vector3 novaPosicao;
	public Vector3 Posicao2;

	// Use this for initialization
	void Start () {
		intro01.SetActive(true);
		intro02.SetActive(false);
		intro03.SetActive(false);
		intro04.SetActive(false);
		botao01.SetActive(true);
		//posicao do botao 
		novaPosicao = transform.position;
		novaPosicao.x = -2;
		novaPosicao.y = -2.8f;
		novaPosicao.z = 0;
		//posicao do botao 
		Posicao2 = transform.position;
		Posicao2.x = 0.35f;
		Posicao2.y = -2.8f;
		Posicao2.z = 0;

		intro = 1;

		Button intro2Button = GameObject.Find ("intro2Button").GetComponent<Button> ();
		intro2Button.onClick.AddListener (intro2ButtonStartClicked);
	}
		
	public void intro2ButtonStartClicked(){
		switch (intro) {
		case 1:
			intro = 2;
			intro01.SetActive(false);
			intro02.SetActive(true);
			break;
		case 2:
			intro = 3;
			botao01.transform.position = novaPosicao;
			intro02.SetActive(false);
			intro03.SetActive(true);
			break;
		case 3:
			intro = 4;
			botao01.transform.position = Posicao2;
			intro03.SetActive(false);
			intro04.SetActive(true);
			break;
		case 4:
			SceneManager.LoadScene ("Jogo");
			break;
		default:
			Debug.Log ("Tipo fora do Switch");
			break;
		}
	}
	public void intro3StartClicked(){
		Debug.Log ("teste");

	}

}

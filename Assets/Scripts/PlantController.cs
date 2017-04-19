using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PlantController : MonoBehaviour {

	public GameController Controller;
	public Scrollbar Barra;	
	public Image ScrollbarHandle;
	public GameObject Bloqueio;
	public Image PlantSprite;
	public GameObject Canvas;
	public GameObject FlyingCoin;

	public Sprite[] MilhosSaudaveis;
	public Sprite[] MilhosSecos;
	public Sprite[] MilhosEncharcados;

	public Sprite[] GirassoisSaudaveis;
	public Sprite[] GirassoisSecos;
	public Sprite[] GirassoisEncharcados;

	public Sprite[] TomatesSaudaveis;
	public Sprite[] TomatesSecos;
	public Sprite[] TomatesEncharcados;

	public Sprite[] ViolasSaudaveis;
	public Sprite[] ViolasSecas;
	public Sprite[] ViolasEncharcados;

	public GameObject RegadorIco;
	public GameObject TelhadoIco;
	public GameObject AduboIco;


	public float agua;
	public float aguaMin;
	public float aguaMax;
	public float adubo;
	public float aduboMin;
	public float sol;
	public float solMax;
	public int valor;

	public int tipo;
	public int estagio;
	public int crescimento;
	public int chances;

	public bool completa;
	public bool tipoFUllMILHO;
	public bool tipoFUllGIRASOL;
	public bool tipoFUllTOMATE;
	public bool tipoFUllVIOLA;
	public bool jogando;

	//public int progress;

	// Use this for initialization
	void Start () {
		jogando = true;
		Controller = GameObject.Find ("GameController").GetComponent<GameController> ();
		this.estagio = 0;
		this.ScrollbarHandle.color = Color.green;
		tipoFUllMILHO = false;
		tipoFUllGIRASOL = false;
		tipoFUllTOMATE = false;
		tipoFUllVIOLA = false;

	}
	
	void Update () {
		
	}

	public void Grow(){

		switch (tipo) {
		case 1:
			//ATRIBUTOS MILHO
			this.agua = 15;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 500;
			this.adubo = 15;
			this.valor = 30;
			break;
		case 2:
			//ATRIBUTOS GIRASSOL
			this.agua = 15;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 15;
			this.adubo = 30;
			this.valor = 40;
			break;
		case 3:
			//ATRIBUTOS TOMATE
			this.agua = 15;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 30;
			this.adubo = 30;
			this.valor = 60;
			break;
		case 4:
			//ATRIBUTOS VIOLAS
			this.agua = 30;
			this.aguaMax = 45;
			this.sol = 0;
			this.solMax = 15;
			this.adubo = 45;
			this.valor = 70;
			break;
		default:
			break;
		}
		this.chances = 2;
		this.estagio++;
		PlantUpdate ();
	}

	public void PlantUpdate(){
		completa = false;

		if (this.crescimento <= 5) {
		} else {
			//PLANTA COMPLETA
			if (estagio >= 6) {
				GameController.coints += this.valor;
				GameController.score += 40;
				StartCoroutine (FlyCoinsFull());

				if (this.valor == 30 & tipoFUllMILHO == false) {
					//GameController.progress += 1;
					Controller.FUllMILHO = true;
					//LOLSDK.Instance.SubmitProgress (0, this.progress, 10);
					Controller.chekMILHO.SetActive (true);
				}
				if (this.valor == 40 & tipoFUllGIRASOL == false) {
					//GameController.progress += 2;
					Controller.FUllGIRASOL = true;
					//LOLSDK.Instance.SubmitProgress (0, this.progress, 10);
					Controller.chekGIRASOL.SetActive (true);
				}
				if (this.valor == 60 & tipoFUllTOMATE == false) {
					//GameController.progress += 3;
					Controller.FUllTOMATE = true;
					//LOLSDK.Instance.SubmitProgress (0, this.progress, 10);
					Controller.chekTOMATE.SetActive (true);
				}

				if (this.valor == 70 & tipoFUllVIOLA == false) {
					//GameController.progress += 4;
					Controller.FUllVIOLA = true;
					//LOLSDK.Instance.SubmitProgress (0, this.progress, 10);
					Controller.chekVIOLA.SetActive (true);
				}
			} else {
				GameController.coints += this.valor/2;
				GameController.score += 20;
				StartCoroutine (FlyCoinsLess());
			}
			LOLSDK.Instance.PlaySound("Planta_fica_completa.mp3", false, false);
			//GameController.UpdateProgress();
		} 

		this.Barra.size = (float)this.crescimento / 7f;

		if (completa == false) {
			if (this.agua <= aguaMin || this.adubo <= aduboMin || this.sol >= solMax) { // Sofre Efeito de SECA ou SEM NUTRIENTES
				switch (tipo) {
				case 1:
					PlantSprite.sprite = MilhosSecos [estagio];
					break;
				case 2:
					PlantSprite.sprite = GirassoisSecos [estagio];
					break;
				case 3:
					PlantSprite.sprite = TomatesSecos [estagio];
					break;
				case 4:
					PlantSprite.sprite = ViolasSecas [estagio];
					break;
				default:
					break;
				}
				this.crescimento++;
				if(this.estagio>1)
				this.estagio--;
//				chances--;
				ScrollbarHandle.color = Color.red;
			} else if (this.agua >= aguaMax) { 	// MUITA AGUA
				switch (tipo) {
				case 1:
					PlantSprite.sprite = MilhosEncharcados [estagio];
					break;
				case 2:
					PlantSprite.sprite = GirassoisEncharcados [estagio];
					break;
				case 3:
					PlantSprite.sprite = TomatesEncharcados [estagio];
					break;
				case 4:
					PlantSprite.sprite = ViolasEncharcados [estagio];
					break;
				default:
					break;
				}
				this.crescimento++;
				if(this.estagio>1)
				this.estagio --;
//				chances--;
				ScrollbarHandle.color = Color.red;
			} else {   // PLANTA SAUDAVEL
				switch (tipo) {
				case 1:
					PlantSprite.sprite = MilhosSaudaveis [estagio];
					break;
				case 2:
					PlantSprite.sprite = GirassoisSaudaveis [estagio];
					break;
				case 3:
					PlantSprite.sprite = TomatesSaudaveis [estagio];
					break;
				case 4:
					PlantSprite.sprite = ViolasSaudaveis [estagio];
					break;
				default:
					break;
				}
				this.crescimento++;
				this.estagio ++;
				ScrollbarHandle.color = Color.green;
			}
	
		}


		if (completa == false) { // INCREMENTO EM CADA TEMPO
			if (Controller.seca == false) {
				sol += 15;
			} else {
				sol += 30;
				agua -= 15;
			}

			if (Controller.enchente == false) {
				agua -= 15;
			}

			if (Controller.lixo == false) {
				adubo -= 15;
			}else{
				adubo -= 30;
				}

			CheckPlantStatus ();
			StartCoroutine (Timer ());
		} 
	}

	public void CheckPlantStatus(){
		RegadorIco.SetActive (true);
		StartCoroutine (IconUpdateAgua());

		AduboIco.SetActive (true);
		StartCoroutine (IconUpdateAdubo());

		TelhadoIco.SetActive (true);
		StartCoroutine (IconUpdateTelhado());

	}

	public void UpdatePlantStatus(){
		RegadorIco.SetActive (true);
		if (this.agua <= aguaMin) {
			RegadorIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			RegadorIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}

		AduboIco.SetActive (true);
		if (this.adubo <= aduboMin) {
			AduboIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			AduboIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}

		TelhadoIco.SetActive (true);
		if (this.sol >= solMax) {
			TelhadoIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			TelhadoIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}
	}

	IEnumerator IconUpdateAgua(){
		yield return new WaitForSeconds (Random.Range (0.2f, 4f));
		if (this.agua <= aguaMin) {
			RegadorIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			RegadorIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}
	}
		
	IEnumerator IconUpdateAdubo(){
		yield return new WaitForSeconds(Random.Range(0.2f,4f));
		if (this.adubo <= aduboMin) {
			AduboIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			AduboIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}
	}

	IEnumerator IconUpdateTelhado(){
		yield return new WaitForSeconds(Random.Range(0.1f,4f));
		if (this.sol >= solMax) {
			TelhadoIco.GetComponent<Image> ().color = new Color32 (130, 130, 130, 130);
		} else {
			TelhadoIco.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}
		
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds(Random.Range(14,19));
		//yield return new WaitForSeconds(Random.Range(11,15));
		PlantUpdate();
	}

	IEnumerator FlyCoinsFull(){
		if (jogando) {
			completa = true;
			RegadorIco.SetActive (false);
			AduboIco.SetActive (false);
			TelhadoIco.SetActive (false);
			ScrollbarHandle.color = Color.green;

			yield return new WaitForSeconds (1f);
			this.estagio = 0;
			this.crescimento = 0;
			this.Barra.size = (float)this.crescimento / 7f;
			PlantSprite.sprite = MilhosSecos [estagio];

			Instantiate (FlyingCoin, this.transform.position, Quaternion.identity).transform.SetParent (Canvas.transform);
			yield return new WaitForSeconds (0.1f);
			Instantiate (FlyingCoin, this.transform.position, Quaternion.identity).transform.SetParent (Canvas.transform);
			yield return new WaitForSeconds (0.1f);
			Instantiate (FlyingCoin, this.transform.position, Quaternion.identity).transform.SetParent (Canvas.transform);
		}
	}

	IEnumerator FlyCoinsLess(){
		if (jogando) {
			completa = true;
			RegadorIco.SetActive (false);
			AduboIco.SetActive (false);
			TelhadoIco.SetActive (false);
			ScrollbarHandle.color = Color.green;

			yield return new WaitForSeconds (2f);
			this.estagio = 0;
			this.crescimento = 0;
			this.Barra.size = (float)this.crescimento / 7f;
			PlantSprite.sprite = MilhosSecos [estagio];

			Instantiate (FlyingCoin, this.transform.position, Quaternion.identity).transform.SetParent (Canvas.transform);
		}
	}
}

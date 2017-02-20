using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour {

	public Scrollbar Barra;	
	public Image ScrollbarHandle;
	public GameObject Bloqueio;
	public Image PlantSprite;

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
	public int chances;

	public bool completa;

	// Use this for initialization
	void Start () {
		this.estagio = 0;
		this.ScrollbarHandle.color = Color.green;
	}
	
	// Update is called once per frame
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
			this.valor = 20;
			break;
		case 2:
			//ATRIBUTOS GIRASSOL
			this.agua = 15;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 500;
			this.adubo = 30;
			this.valor = 25;
			break;
		case 3:
			//ATRIBUTOS TOMATE
			this.agua = 15;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 500;
			this.adubo = 30;
			this.valor = 40;
			break;
		case 4:
			//ATRIBUTOS VIOLAS
			this.agua = 30;
			this.aguaMax = 30;
			this.sol = 0;
			this.solMax = 500;
			this.adubo = 45;
			this.valor = 45;
			break;
		default:
			Debug.Log ("Tipo fora do Switch");
			break;
		}
		this.chances = 2;
		this.estagio++;
		PlantUpdate ();
	}

	public void PlantUpdate(){
		completa = false;

		if (this.estagio <= 5 && chances > 0) {
			
		} else if (chances > 0) {
			//PLANTA COMPLETA
			completa = true;
			this.estagio = 0;
			PlantSprite.sprite = MilhosSecos [estagio];
			GameController.coints += this.valor;
		} else {
			completa = true;
			Bloqueio.SetActive (true);
		}

		this.Barra.size = (float)this.estagio / 7f;

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
					Debug.Log ("Tipo fora do Switch");
					break;
				}
				this.estagio--;
				chances--;
				ScrollbarHandle.color = Color.red;
			} else if (this.agua >= aguaMax) { 							// MUITA AGUA
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
					Debug.Log ("Tipo fora do Switch");
					break;
				}
				this.estagio --;
				chances--;
				ScrollbarHandle.color = Color.red;
			} else {   													// PLANTA SAUDAVEL
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
					Debug.Log ("Tipo fora do Switch");
					break;
				}
				this.estagio ++;
				ScrollbarHandle.color = Color.green;
			}
	
		}


		if (completa == false) { // INCREMENTO EM CADA TEMPO
			sol += 15;
			agua -= 15;
			adubo -= 15;
			StartCoroutine (Timer ());
		} 
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds(5);
		PlantUpdate();
	}
}

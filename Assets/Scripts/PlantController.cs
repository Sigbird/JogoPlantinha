using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour {

	public Scrollbar Barra;	

	public Image PlantSprite;

	public Sprite[] MilhosSaudaveis;
	public Sprite[] MilhosSecos;

	public Sprite[] GirassoisSaudaveis;
	public Sprite[] GirassoisSecos;

	public Sprite[] TomatesSaudaveis;
	public Sprite[] TomatesSecos;

	public Sprite[] ViolasSaudaveis;
	public Sprite[] ViolasSecas;

	public float agua;
	public float aguaMin;
	public float adubo;
	public float aduboMin;
	public float sol;
	public float solMin;

	public int tipo;
	public int estagio;

	public bool completa;

	// Use this for initialization
	void Start () {
		this.estagio = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Grow(){
	
		switch (tipo) {
		case 1:
			//ATRIBUTOS MILHO
			this.agua = 15;
			this.sol = 120;
			this.adubo = 15;
			break;
		case 2:
			//ATRIBUTOS GIRASSOL
			this.agua = 15;
			this.sol = 15;
			this.adubo = 30;
			break;
		case 3:
			//ATRIBUTOS TOMATE
			this.agua = 15;
			this.sol = 30;
			this.adubo = 30;
			break;
		case 4:
			//ATRIBUTOS VIOLAS
			this.agua = 30;
			this.sol = 15;
			this.adubo = 45;
			break;
		default:
			Debug.Log ("Tipo fora do Switch");
			break;
		}
		PlantUpdate ();
	}

	public void PlantUpdate(){
		completa = false;
		this.estagio += 1;
		this.Barra.size = (float)this.estagio / 7f;


		if (this.agua <= aguaMin || this.adubo <= aduboMin || this.sol <= solMin) { // Sofre Efeito
			switch (tipo) {
			case 1:
				PlantSprite.sprite = MilhosSecos[estagio];
				break;
			case 2:
				PlantSprite.sprite = GirassoisSecos[estagio];
				break;
			case 3:
				PlantSprite.sprite = TomatesSecos[estagio];
				break;
			case 4:
				PlantSprite.sprite = ViolasSecas[estagio];
				break;
			default:
				Debug.Log ("Tipo fora do Switch");
				break;
			}
		} else { // Saudavel
			switch (tipo) {
			case 1:
				PlantSprite.sprite = MilhosSaudaveis[estagio];
				break;
			case 2:
				PlantSprite.sprite = GirassoisSaudaveis[estagio];
				break;
			case 3:
				PlantSprite.sprite = TomatesSaudaveis[estagio];
				break;
			case 4:
				PlantSprite.sprite = ViolasSaudaveis[estagio];
				break;
			default:
				Debug.Log ("Tipo fora do Switch");
				break;
			}
		}
		if (this.estagio < 7) {
			sol -= 15;
			agua -= 15;
			adubo -= 15;
			StartCoroutine (Timer ());
		} else {
		//PLANTA COMPLETA
			completa = true;
		}
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds(15);
		PlantUpdate();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		this.estagio = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlantUpdate(){
		this.estagio += 1;
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
		StartCoroutine (Timer ());
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds(10);
		PlantUpdate();
	}
}

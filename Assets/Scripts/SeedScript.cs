using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {

	public GameObject origin;

	public string Nome;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other) {
		if (this.Nome == "Semente") {
			if (origin.GetComponent<SeedButton>().released) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio <= 0) {
					GameController.coints -= origin.GetComponent<SeedButton> ().seedCost;
					other.GetComponent<PlantController> ().tipo = origin.GetComponent<SeedButton> ().tipo;
					other.GetComponent<PlantController> ().Grow ();
					other.GetComponent<PlantController> ().CheckPlantStatus ();
				}
			}
		}
		if (this.Nome == "Regador") {
			if (origin.GetComponent<ToolButton>().released) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio >= 0) {
					other.GetComponent<PlantController> ().agua += 15;
					other.GetComponent<PlantController> ().CheckPlantStatus ();
				}
			}
		}
		if (this.Nome == "Telha") {
			if (origin.GetComponent<ToolButton>().released) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio >= 0) {
					other.GetComponent<PlantController> ().sol += 15;
					other.GetComponent<PlantController> ().CheckPlantStatus ();
				}
			}
		}
		if (this.Nome == "Fertilizante") {
			if (origin.GetComponent<ToolButton>().released) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio >= 0) {
					other.GetComponent<PlantController> ().adubo += 15;
					other.GetComponent<PlantController> ().CheckPlantStatus ();
				}
			}
		}
		if (this.Nome == "Pa") {
			if (origin.GetComponent<ToolButton>().released) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio >= 0) {
					other.GetComponent<PlantController> ().adubo += 15;
				}
			}
		}
	}

}

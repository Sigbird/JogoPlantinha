using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {

	public GameObject origin;

	public string Nome;

	public float destroyCount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (origin.GetComponent<SeedButton> () != null )
		if(origin.GetComponent<SeedButton>().released) {
			destroyCount += Time.deltaTime;
			if (destroyCount >= 0.2f) {
				Destroy (this.gameObject);
			}
		}

		if (origin.GetComponent<ToolButton> () != null )
		if(origin.GetComponent<ToolButton>().released) {
			destroyCount += Time.deltaTime;
			if (destroyCount >= 0.2f) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (this.Nome == "Semente") {
			if (origin.GetComponent<SeedButton>().released && other.GetComponent<PlantController>() != null) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio <= 0) {
					GameController.coints -= origin.GetComponent<SeedButton> ().seedCost;
					other.GetComponent<PlantController> ().tipo = origin.GetComponent<SeedButton> ().tipo;
					other.GetComponent<PlantController> ().Grow ();
					other.GetComponent<PlantController> ().UpdatePlantStatus ();
					origin.GetComponent<SeedButton> ().StartTutorial ();
				}
			}
			//Debug.Log (other.name);
		}
		if (this.Nome == "Regador") {
			if (origin.GetComponent<ToolButton>().released && other.GetComponent<PlantController>() != null) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio > 0) {
					other.GetComponent<PlantController> ().agua += 15;
					other.GetComponent<PlantController> ().UpdatePlantStatus ();
					origin.GetComponent<ToolButton> ().StartTutorial ();
				}
			}
		}
		if (this.Nome == "Telha") {
			if (origin.GetComponent<ToolButton>().released && other.GetComponent<PlantController>() != null) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio > 0) {
					other.GetComponent<PlantController> ().sol -= 15;
					other.GetComponent<PlantController> ().UpdatePlantStatus ();
					origin.GetComponent<ToolButton> ().StartTutorial ();
				}
			}
		}
		if (this.Nome == "Fertilizante") {
			if (origin.GetComponent<ToolButton>().released && other.GetComponent<PlantController>() != null) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio > 0) {
					other.GetComponent<PlantController> ().adubo += 15;
					other.GetComponent<PlantController> ().UpdatePlantStatus ();
					origin.GetComponent<ToolButton> ().StartTutorial ();
				}
			}
		}
		if (this.Nome == "Pa") {
			if (origin.GetComponent<ToolButton>().released && other.GetComponent<PlantController>() != null) {
				Destroy (this.gameObject);
				if (other.GetComponent<PlantController> ().estagio >= 0) {
					other.GetComponent<PlantController> ().adubo += 15;
				}
			}
		}
			
	}

}

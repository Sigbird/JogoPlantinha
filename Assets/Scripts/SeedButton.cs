using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField] 
	Vector2 startPoint;
	[SerializeField]
	Vector2 endPoint;
	[SerializeField] 
	public bool drag;

	public GameObject seed;
	public bool released;
	public bool seedCreated;
	public int seedCost;
	public GameObject draggable;

	public int tipo;

	// Use this for initialization
	void Start () {
		this.seedCreated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (draggable != null)
			draggable.transform.position = Vector2.MoveTowards (draggable.transform.position, endPoint, 50);
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		startPoint = eventData.pressPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		drag = true;
		endPoint = eventData.position;

		this.released = false;
		if (seedCost <= GameController.coints) {
			if (seedCreated == false) {
				seedCreated = true;
				draggable = (GameObject)Instantiate (seed, this.transform.position, Quaternion.identity);
				draggable.transform.SetParent (GameObject.Find ("Canvas").transform);
				draggable.GetComponent<SeedScript> ().origin = this.gameObject;
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		drag = false;

//		if (draggable != null)
//			Destroy (draggable.gameObject);
		this.released = true;
		seedCreated = false;
	}

}

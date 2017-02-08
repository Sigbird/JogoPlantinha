﻿using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField] 
	Vector2 startPoint;
	[SerializeField]
	Vector2 endPoint;
	[SerializeField] 
	public bool drag;

	public GameObject tool;
	public bool released;
	public bool toolCreated;
	public int toolCost;
	public GameObject draggable;


	// Use this for initialization
	void Start () {
		this.toolCreated = false;
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
		if (toolCost <= GameObject.Find ("GameController").GetComponent<GameController> ().coints) {
			if (toolCreated == false) {
				toolCreated = true;
				draggable = (GameObject)Instantiate (tool, this.transform.position, Quaternion.identity);
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
		toolCreated = false;
	}

}
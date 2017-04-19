using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Cash");
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position,target.transform.position)>0.5f){
		this.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime*1000);
		}else{
			Destroy(this.gameObject);
		}
	}
}

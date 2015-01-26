using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Car - Decent Skid") {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}

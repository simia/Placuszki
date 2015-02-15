using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

	public float LifeTime = 5.0f;

	void Awake() {
		Destroy (gameObject, LifeTime);
	}

	void OnCollisionEnter(Collision c) {
		Destroy (transform.gameObject);
	}
}

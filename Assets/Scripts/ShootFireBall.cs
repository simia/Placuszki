using UnityEngine;
using System.Collections;

public class ShootFireBall : MonoBehaviour {

	public float ShootDelay = 1;
	public Transform FireBallPrefab;

	private float LastShootTime;
	// Update is called once per frame
	void Update () {
		if (Time.time - this.LastShootTime >= this.ShootDelay 
		    	&& Input.GetKeyDown(KeyCode.Space)) {

			CarController controller = GetComponent<CarController>();
			Transform fBall = Instantiate(this.FireBallPrefab, transform.Find("ShootPoint").transform.position, Quaternion.identity) as Transform;
			fBall.rigidbody.AddForce(transform.forward * (300 + controller.Speed() * 10));
			LastShootTime = Time.fixedTime;
		}
	}
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarController : MonoBehaviour {

	public GenericInput input = new GenericInput();
	public string PlayerName = "Placuszek";
	public float idealRPM = 500f;
	public float maxRPM = 1000f;

	public Transform centerOfGravity;

	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;

	public float turnRadius = 6f;
	public float torque = 25f;
	public float brakeTorque = 100f;

	public float AntiRoll = 20000.0f;

	public enum DriveMode { Front, Rear, All };
	public DriveMode driveMode = DriveMode.Rear;
	
	int nextCheckpointId;

	public Text speedText;

	void Start() {
		rigidbody.centerOfMass = centerOfGravity.localPosition;
		nextCheckpointId = 1;
	}

	public float Speed() {
		return wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f;
	}

	public float Rpm() {
		return wheelRL.rpm;
	}
	
	public int GetNextCheckpoint() {
		return nextCheckpointId;
	}
	
	public void NextCheckpointIncr() {
		++nextCheckpointId;
	}
	
	public void NextCheckpointReset() {
		nextCheckpointId = 1;
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.K)) {
			input.keyboardMode = !input.keyboardMode;
			Debug.Log("keyboard: " + input.keyboardMode);
		}
	}

	void FixedUpdate () {
		if(speedText!=null)
			speedText.text = "Speed: " + Speed().ToString("f0") + " km/h";

		//Debug.Log ("Speed: " + (wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f) + "km/h    RPM: " + wheelRL.rpm);

		float scaledTorque = input.Vertical * torque;

		if(wheelRL.rpm < idealRPM)
			scaledTorque = Mathf.Lerp(scaledTorque/10f, scaledTorque, wheelRL.rpm / idealRPM );
		else 
			scaledTorque = Mathf.Lerp(scaledTorque, 0,  (wheelRL.rpm-idealRPM) / (maxRPM-idealRPM) );

		DoRollBar(wheelFR, wheelFL);
		DoRollBar(wheelRR, wheelRL);

		wheelFR.steerAngle = input.Horizontal * turnRadius;
		wheelFL.steerAngle = input.Horizontal * turnRadius;

		wheelFR.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
		wheelFL.motorTorque = driveMode==DriveMode.Rear  ? 0 : scaledTorque;
		wheelRR.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;
		wheelRL.motorTorque = driveMode==DriveMode.Front ? 0 : scaledTorque;

		if(Input.GetButton("Fire1")) {
			wheelFR.brakeTorque = brakeTorque;
			wheelFL.brakeTorque = brakeTorque;
			wheelRR.brakeTorque = brakeTorque;
			wheelRL.brakeTorque = brakeTorque;
		}
		else {
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
			wheelRR.brakeTorque = 0;
			wheelRL.brakeTorque = 0;
		}
	}


	void DoRollBar(WheelCollider WheelL, WheelCollider WheelR) {
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;
		
		bool groundedL = WheelL.GetGroundHit(out hit);
		if (groundedL)
			travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
		
		bool groundedR = WheelR.GetGroundHit(out hit);
		if (groundedR)
			travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
		
		float antiRollForce = (travelL - travelR) * AntiRoll;
		
		if (groundedL)
			rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
			                             WheelL.transform.position); 
		if (groundedR)
			rigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce,
			                             WheelR.transform.position); 
	}

	// on fallout respawn in the center of map
	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Fallout") {
			Respawn();
		}
	}
	
	public void Respawn() {
		Vector3 vec = Terrain.activeTerrain.collider.bounds.center;
		vec.y += 5;
		this.transform.position = vec;
		
		rigidbody.AddForceAtPosition(float.MaxValue * Vector3.down, this.transform.position);
	}
}

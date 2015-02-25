using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckpointController : MonoBehaviour {
	private const float HitDecay = 1.0f;

	public int Id = 0;

	private Sprite[] numberSprites;
	private List<int> digits = new List<int>();
	private float LastHitTime = 0;
	private bool IsHit = false; 
	
	private Color initColor;

	// Use this for initialization
	void Start () {
		DrawId();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0,20*Time.deltaTime,0);
		
		if(IsHit) {
			if( Time.time - LastHitTime > HitDecay ) {
				this.renderer.material.color = initColor;
				IsHit = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider c) {
		CarController ctrl = c.transform.parent.gameObject.GetComponent<CarController> ();
		
		if (!ctrl)
			return;
		
		if (ctrl.GetNextCheckpoint() == this.Id) {	
			LastHitTime = Time.time;	
			this.renderer.material.color = new Color(1.0f, 0, 0, 0.5f);
			IsHit = true;
			ctrl.NextCheckpointIncr();

			((ScoreManager)this.transform.GetComponentInParent<ScoreManager>()).SetNextCheckpointLabel(ctrl.GetNextCheckpoint());
			
			UpdateTime(ctrl);
		}
	}
	
	void UpdateTime(CarController cc) {
		ScoreManager SM = this.transform.GetComponentInParent<ScoreManager>();
		
		if(!SM) {
			Debug.Log ("Can't get ScoreManager from " + this + ". Hit by " + cc.PlayerName);
			return;
		}
		
		SM.UpdatePlayerScore(cc.PlayerName, cc.GetNextCheckpoint() - 1);
	}
	
	void DrawId() {
		// store initial color
		initColor = this.renderer.material.color;
		
		// load all sprites
		numberSprites = Resources.LoadAll<Sprite> ("Numbers");
		
		int tmpId = Id;
		do {
			digits.Insert (0, tmpId % 10);
			tmpId /= 10;
		} while(tmpId != 0);
	
		List<Transform> dT = new List<Transform>();
		
		// set cloner digit
		var idDispObj = this.transform.GetChild (0);
		idDispObj.GetComponent<SpriteRenderer> ().sprite = numberSprites [digits[0]];
		dT.Add(idDispObj);
		
		// clone
		for(int i=1;i<digits.Count;++i) {
			Transform digit = Instantiate (idDispObj) as Transform;
			digit.SetParent (this.transform);
			digit.GetComponent<SpriteRenderer> ().sprite = numberSprites [digits[i]];
			dT.Add(digit);
		}
		
		
		// arrange all digits over checkpoint
		float singleWidth = idDispObj.renderer.bounds.size.x * idDispObj.localScale.x * 0.5f;
		float startPosX = -(singleWidth / 2.0f) * (digits.Count - 1);
		Vector3 digitPos = idDispObj.localPosition;
		
		foreach(Transform t in dT) {
			digitPos.x = startPosX;
			t.localPosition = digitPos;
			t.localScale = idDispObj.localScale;
			startPosX += singleWidth;
		}
	}
}

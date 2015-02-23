using UnityEngine;
using System.Collections.Generic;

public class CheckpointController : MonoBehaviour {
	public int Id = 0;

	private Sprite[] numberSprites;
	private List<int> digits = new List<int>();

	void Awake() {
		numberSprites = Resources.LoadAll<Sprite> ("Numbers");

		int tmpId = Id;
		do {
			digits.Insert (0, tmpId % 10);
			tmpId /= 10;
		} while(tmpId != 0);
	}

	// Use this for initialization
	void Start () {
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
		float singleWidth = idDispObj.renderer.bounds.size.x;
		float totalWidth = digits.Count * singleWidth;
		float startPos = this.renderer.bounds.center.x - totalWidth / 2.0f;
		
		Vector3 lastPos = new Vector3(startPos, idDispObj.position.y, idDispObj.position.z);
		foreach(Transform t in dT) {
			t.position = lastPos;
			t.localScale = idDispObj.localScale;
			lastPos.x += singleWidth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0,20*Time.deltaTime,0);
	}
}

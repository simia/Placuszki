using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
	public int numOfCheckpoints = 10;
	public float minDistance = 100;
	public Text nextCheckpoint;
	public GameObject ScorePanel;
	
	private float StartTime;
	private int changeCounter, lastChange;
	private bool isEndOfRace;
	
	public struct PlayerData {
		public int LastChckPt;
		public float RaceTime;
		
		public PlayerData(int lastChckPt, float raceTime) {
			LastChckPt = lastChckPt;
			RaceTime = raceTime;
		}
	}
	
	Dictionary<string, PlayerData> ScoreTable = new Dictionary<string, PlayerData>();

	// Use this for initialization
	void Start () {
		ScoreTable.Clear();
		generateCheckpoints();
		StartTime = Time.time;
		changeCounter = 0;
		lastChange = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!ScorePanel)
			return;
	
		if(isEndOfRace) {
			if(!ScorePanel.activeSelf)
				ScorePanel.SetActive(true);
			
			if(Input.GetKeyDown(KeyCode.Return)) {
				ReloadRace();	
			}
			
		} else if(Input.GetKeyDown(KeyCode.Tab)) {
			ScorePanel.SetActive(!ScorePanel.activeSelf);
		}
		
		if(changeCounter == lastChange)
			return;
			
		lastChange = changeCounter;
		
		DrawOnUI();
	}
	
	void generateCheckpoints() {
		Vector3 maxBounds = Terrain.activeTerrain.collider.bounds.max;
		Vector3 minBounds = Terrain.activeTerrain.collider.bounds.min;
	
		GameObject go = Resources.Load<GameObject>("Checkpoint");
		
		for(int i=1;i<=numOfCheckpoints;++i) {
			GameObject chck = Instantiate(go) as GameObject;
			chck.transform.SetParent(this.transform);
			chck.transform.position = new Vector3(Random.Range(minBounds.x, maxBounds.x), 4, Random.Range(minBounds.z, maxBounds.z));
			((CheckpointController)chck.GetComponent<CheckpointController>()).Id = i;
		}	
	}
	
	public void UpdatePlayerScore(string playerName, int lastChckPt) {
		if(ScoreTable.ContainsKey(playerName)) {
			PlayerData pd = ScoreTable[playerName];
			pd.LastChckPt = lastChckPt;
			pd.RaceTime = Time.time - StartTime;
			ScoreTable[playerName] = pd;
		} else {
			ScoreTable.Add(playerName, new PlayerData(lastChckPt, Time.time - StartTime));
		}
		
		++changeCounter;
		
		// end condition
		if(lastChckPt == numOfCheckpoints) {
			isEndOfRace = true;
		}
	}
	
	void ReloadRace() {
		isEndOfRace = false;
		ScoreTable.Clear();
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
			CarController cc = g.GetComponent<CarController>();
			cc.NextCheckpointReset();
			cc.Respawn();
		}
		
		SetNextCheckpointLabel(1);
		StartTime = Time.time;
		ScorePanel.SetActive(false);
	}
		
	void DrawOnUI() {
		GameObject scoreLinePrefab = Resources.Load<GameObject>("ScoreLine");
		// clear all results
		Transform ScoreLines = ScorePanel.transform.GetChild(1);
		
		while(ScoreLines.childCount > 0) {
			Transform c = ScoreLines.GetChild(0);
			c.SetParent(null);
			Destroy(c.gameObject);
		}
		
		// generate scores
		foreach(string key in ScoreTable.Keys) {
			PlayerData pd = ScoreTable[key];
			Debug.Log(string.Format("Name: {0}, last checkpoint: {1}, time: {2}", key, pd.LastChckPt, pd.RaceTime));
			GameObject go = Instantiate(scoreLinePrefab) as GameObject;
			go.transform.SetParent(ScoreLines);
			
			go.transform.GetChild(0).GetComponent<Text>().text = key;
			go.transform.GetChild(1).GetComponent<Text>().text = pd.LastChckPt.ToString();
			go.transform.GetChild(2).GetComponent<Text>().text = pd.RaceTime.ToString("f");
		}
	}
	
	public void SetNextCheckpointLabel(int id) {
		if(nextCheckpoint) {
			nextCheckpoint.text = "Next: " + id.ToString();
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : StartHost {

	public enum GameState { Init, Race, Score };

	private const float initPeriod = 10.0f;
	private float startTime;

	public GameState gameState;

	private static GameController m_Instance;
	public static GameController Instance { get { return m_Instance; } }

	public bool extendInit()
	{
		if (gameState == GameState.Init)
		{
			startTime = Time.time + initPeriod;
			Debug.Log("New player joined, race starts in " + initPeriod + " seconds");
			return true;
		}
		return false;
	}

	public void restartGame() {
		gameState = GameState.Init;
		extendInit();
	}

	public float startCountdown()
	{
		return startTime - Time.time;
	}

	void Awake()
	{
		m_Instance = this;
	}
	
	void OnDestroy()
	{
		m_Instance = null;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		gameState = GameState.Init;
		startTime = Time.time + initPeriod;
		Debug.Log("Race starts in " + initPeriod + " seconds");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Init && Time.time > startTime) {
			Debug.Log("GO GO GO");
			gameState = GameState.Race;
		}
	}
	
	
}

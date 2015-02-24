using UnityEngine;
using System.Collections;

public class GameController : StartHost {
	
	public GenericInput input = new GenericInput();

	private static GameController m_Instance;
	public static GameController Instance { get { return m_Instance; } }
	
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
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.K)) {
		    input.keyboardMode = !input.keyboardMode;
		}
	}
	
	
}

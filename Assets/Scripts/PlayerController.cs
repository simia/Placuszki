using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Object carPrefab;
	private List<Player> players = new List<Player>();

	private static PlayerController m_Instance;
	public static PlayerController Instance { get { return m_Instance; } }
	
	void Awake()
	{
		m_Instance = this;
	}
	
	void OnDestroy()
	{
		m_Instance = null;
	}

	public Player newPlayer(string name)
	{
		GameObject car = Instantiate (carPrefab) as GameObject;
		car.GetComponentInChildren<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		Vector3 maxBounds = Terrain.activeTerrain.collider.bounds.max;
		Vector3 minBounds = Terrain.activeTerrain.collider.bounds.min;
		car.transform.position = new Vector3(Random.Range(minBounds.x, maxBounds.x), 10.0f, Random.Range(minBounds.z, maxBounds.z));
		car.transform.Rotate (new Vector3 (0.0f, Random.Range (0.0f, 360.0f), 0.0f));
		car.GetComponent<CarController> ().PlayerName = name; //TODO
		Player player = new Player(name, car);
		players.Add(player);
		ScoreManager.Instance.UpdatePlayerScore (name, 0);
		return player;
	}

	public Player findPlayer(string id)
	{
		return players.Find(p => p.id == id);
	}
}

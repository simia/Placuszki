using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Object carPrefab;

	private static PlayerController m_Instance;
	public static PlayerController Instance { get { return m_Instance; } }

	private List<Player> players = new List<Player>();

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
		Player player = new Player(name, car);
		players.Add(player);
		return player;
	}

	public Player findPlayer(string id)
	{
		return players.Find(p => p.id == id);
	}
}

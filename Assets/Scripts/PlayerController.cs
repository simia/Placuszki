using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
	private List<Player> players = new List<Player>();

	public Player newPlayer(String name)
	{
		Player player = new Player(name);
		players.Add(player);
		return player;
	}

	public Player findPlayer(String id)
	{
		return players.Find(p => p.id == id);
	}
}

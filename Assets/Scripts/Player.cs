using System;
using UnityEngine;

public class Player
{
	public String name { get; set; }
	public String id { get; set; }

	public GameObject car;
	
	public Player(String name, GameObject car)
	{
		this.name = name;
		this.id = Guid.NewGuid().ToString();
		this.car = car;
	}
}

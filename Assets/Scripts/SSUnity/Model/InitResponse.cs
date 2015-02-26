using System.Collections.Generic;
using UnityEngine;

public class InitResponse
{
	public string id { get; set; }
	public Vector3 position { get; set; }
	public Quaternion rotation { get; set; }
	public List<Vector3> checkpoints { get; set; }
	public float[,] map { get; set; }
}

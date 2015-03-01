using System.Collections.Generic;
using UnityEngine;

public class InitResponse
{
	public string id { get; set; }
	public float[] position { get; set; }
	public float[] rotation { get; set; }
	public List<float[]> checkpoints { get; set; }
	public float[,] map { get; set; }
}

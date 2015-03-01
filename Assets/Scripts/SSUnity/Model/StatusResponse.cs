using UnityEngine;

public class StatusResponse
{
    public float acceleration { get; set; }
    public float steering { get; set; }
    public float speed { get; set; }
	public float[] position { get; set; }
	public float[] rotation { get; set; }
	public int nextCheckpoint { get; set; }
}

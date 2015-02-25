using UnityEngine;

public class StatusResponse
{
    public float acceleration { get; set; }
    public float steering { get; set; }
    public float speed { get; set; }
	public Vector3 position { get; set; }
	public Quaternion rotation { get; set; }
}

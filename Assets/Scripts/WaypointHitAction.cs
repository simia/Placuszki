using UnityEngine;
using System.Collections;

public class WaypointHitAction : MonoBehaviour {

	public int Id = 1;
	public Material EnteredMatPrefab;

	void OnTriggerEnter(Collider c) {
		print ("on triher");
		CarController ctrl = c.transform.parent.gameObject.GetComponent<CarController> ();
		
		if (!ctrl)
			return;

		if (ctrl.lastWaypointId + 1 == this.Id) {		
			renderer.material = EnteredMatPrefab;
			++ctrl.lastWaypointId;
		}
	}
}

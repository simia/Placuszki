using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StatusService : Service
{
    public object Get(StatusRequest request)
    {
		StatusResponse response = new StatusResponse();
		Player player = PlayerController.Instance.findPlayer(request.id);
		var car = Cache.Get<GameObject>(request.id);

        Exec.OnMain(() =>
        {
			if (car == null)
            {
				car = player.car;
				Cache.Set<GameObject>(request.id, car);
            }
			CarController controller = car.GetComponent<CarController>();

			response.acceleration = controller.input.Vertical;
			response.steering = controller.input.Horizontal;
			response.speed = controller.Speed();
			response.position = new float[] { car.transform.position.x, car.transform.position.y, car.transform.position.z };
			response.rotation = new float[] { car.transform.eulerAngles.x, car.transform.eulerAngles.y, car.transform.eulerAngles.z };
			response.nextCheckpoint = controller.GetNextCheckpoint() - 1;

        }, true);

		return response;
    }
}
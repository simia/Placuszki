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
			response.position = car.transform.position;
			response.rotation = car.transform.rotation;

        }, true);

		return response;
    }
}
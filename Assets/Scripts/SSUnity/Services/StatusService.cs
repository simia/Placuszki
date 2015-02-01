using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StatusService : Service
{
    public object Get(StatusModel request)
    {
		StatusResponse response = new StatusResponse();
        var car = Cache.Get<GameObject>("Car - Decent Skid");

        Exec.OnMain(() =>
        {
			if (car == null)
            {
				car = GameObject.Find("Car - Decent Skid");
				Cache.Set<GameObject>("Car - Decent Skid", car);
            }
			CarController controller = car.GetComponent<CarController>();

			response.acceleration = GameController.Instance.input.Vertical;
			response.steering = GameController.Instance.input.Horizontal;
			response.speed = controller.Speed();

        }, true);

		return response;
    }
}
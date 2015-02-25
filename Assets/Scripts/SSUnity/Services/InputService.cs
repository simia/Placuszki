using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputService : Service
{
    public object Get(InputRequest request)
    {
        Exec.OnMain(() =>
        {
			Player player = PlayerController.Instance.findPlayer(request.id);
			CarController car = player.car.GetComponent<CarController>();
			car.input.Vertical = request.vertical;
			car.input.Horizontal = request.horizontal;
        }, true);

		return true;
    }
}
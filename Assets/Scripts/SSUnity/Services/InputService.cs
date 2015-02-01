using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputService : Service
{
    public object Get(InputModel request)
    {
        Exec.OnMain(() =>
        {
			GameController.Instance.input.Vertical = request.vertical;
			GameController.Instance.input.Horizontal = request.horizontal;
        }, true);

		return true;
    }
}
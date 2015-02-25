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
			Player player = GameController.Instance.players.findPlayer(request.id);
			player.input.Vertical = request.vertical;
			player.input.Horizontal = request.horizontal;
        }, true);

		return true;
    }
}
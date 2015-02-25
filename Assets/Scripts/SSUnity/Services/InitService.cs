using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InitService : Service
{
    public object Get(InitRequest request)
    {
		InitResponse response = new InitResponse();

		Exec.OnMain(() =>
		{
			Player player = PlayerController.Instance.newPlayer(request.name);
			response.id = player.id;

			//TODO: map, checkpoints
		}, true);

		return response;
    }
}
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
			response.position = player.car.transform.position;
			response.rotation = player.car.transform.rotation;
			response.checkpoints = ScoreManager.Instance.checkpoints;
			response.map = MapManager.Instance.HeightsMap;

			//TODO: map
		}, true);

		return response;
    }
}
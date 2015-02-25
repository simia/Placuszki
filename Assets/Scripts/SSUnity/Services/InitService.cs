using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InitService : Service
{
    public object Get(InitRequest request)
    {
		Player player = GameController.Instance.players.newPlayer(request.name);

		InitResponse response = new InitResponse();
		response.id = player.id;

		return response;
    }
}
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
			if (GameController.Instance.extendInit())
			{
				Player player = PlayerController.Instance.newPlayer(request.name);
				response.id = player.id;
				response.position = new float[] { player.car.transform.position.x, player.car.transform.position.y, player.car.transform.position.z };
				response.rotation = new float[] { player.car.transform.eulerAngles.x, player.car.transform.eulerAngles.y, player.car.transform.eulerAngles.z };
				response.checkpoints = ScoreManager.Instance.checkpoints.ConvertAll(v => new float[] { v.x, v.y, v.z });
				response.map = MapManager.Instance.HeightsMap;
			}
		}, true);
		if (response.id != null)
			return response;
		return false;
    }

}

public class CountdownService : Service
{
	public object Get(TimeToStartRequest request)
	{
		float response = -1.0f;
		Exec.OnMain(() =>
		{
			response = GameController.Instance.startCountdown();
		}, true);
		return response;
	}
}

using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Route("/input/{id}/{vertical}/{horizontal}")]
public class InputRequest
{
	public string id { get; set; }
	public float vertical { get; set; }
	public float horizontal { get; set; }
}

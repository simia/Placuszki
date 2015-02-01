using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Route("/input/{vertical}/{horizontal}")]
public class InputModel
{
	public float vertical { get; set; }
	public float horizontal { get; set; }
}

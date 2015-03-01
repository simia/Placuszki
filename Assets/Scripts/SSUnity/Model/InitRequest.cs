using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Route("/init/{name}")]
public class InitRequest
{
	public string name { get; set; }
}

[Route("/init")]
public class TimeToStartRequest
{
}

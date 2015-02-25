using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Route("/status/{id}")]
public class StatusRequest
{
	public string id { get; set; }
}

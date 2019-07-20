using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Table.Controllers
{
	public class ChatController : ApiController
	{
		public Task Post() => Task.WhenAll(SessionContext.Current.Players.Select(async x => x.Outbox.Enqueue(await Request.Content.ReadAsStringAsync())));
	}
}
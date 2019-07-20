using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace Table.Controllers
{
	public class EventsController : ApiController
	{
		public IHttpActionResult Get()
		{
			if (HttpContext.Current.IsWebSocketRequest)
			{
				HttpContext.Current.AcceptWebSocketRequest(HandleWebSocketConnection);

				return StatusCode(HttpStatusCode.SwitchingProtocols);
			}

			return BadRequest();
		}

		private async Task HandleWebSocketConnection(AspNetWebSocketContext context)
		{
			var player = new Player();

			SessionContext.Current.Players.Add(player);

			while (true)
			{
				if (context.WebSocket.State != System.Net.WebSockets.WebSocketState.Open)
				{
					SessionContext.Current.Players.Remove(player);
					break;
				}

				while (player.Outbox.Count == 0)
				{
					await Task.Delay(100);
				}

				var data = Encoding.UTF8.GetBytes(player.Outbox.Dequeue());

				await context.WebSocket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
			}
		}
	}
}
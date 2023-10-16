using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoreChess.Serverless.Hubs
{
  public class ChatHub : Hub
  {
    public async Task SendMessage(string name)
    {
      await Clients.All.SendAsync("newMessage", name);
    }
  }

}
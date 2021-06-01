using DemoSignalR.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSignalR.HubConfig
{
    public class ChartHub : Hub
    {
        public async Task BroadcastChartData(List<ChartModel> data, string connectionId)
        => await Clients.Client(connectionId).SendAsync("broadcastchartdata", data);
        public string GetConnectionId() => Context.ConnectionId;

    }
}

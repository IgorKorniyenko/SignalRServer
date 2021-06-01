
using DemoSignalR.DataStorage;
using DemoSignalR.HubConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> _hub;
        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }


        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }


        [HttpGet("send/{idGrupo}/{mensaje}")]
        public IActionResult EnviarMensajeGym(int idGrupo, string mensaje)
        {
            _hub.Clients.Group("gym-" + idGrupo).SendAsync("groupmessage", mensaje);
            return Ok(new { Message = "Request Completed" });
        }
        //api/chart/subscribe/
        [HttpGet("subscribe/{idGrupo}/{connectionId}")]
        public async Task<IActionResult> Subscribe(int idGrupo, string connectionId)
        {
            await _hub.Groups.AddToGroupAsync(connectionId, "gym-" + idGrupo);
            return Ok(new { Message = "Request Completed" });
        }


    }
}

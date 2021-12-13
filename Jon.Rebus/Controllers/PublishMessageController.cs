using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using Rebus.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.RebusClient.Components.Models;

namespace Jon.Rebus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishMessageController : ControllerBase
    {
        private readonly IBus bus;

        public PublishMessageController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public IActionResult SendMessage(Messages Message)
        {
            bus.Publish(new Messages(Message.Message, Message.HiddenMessage));
            return Ok();
        }
    }
}

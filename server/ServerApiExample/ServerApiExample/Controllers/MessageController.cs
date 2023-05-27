using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using ServerApiExample.Modals;
using System.Text;
using System.Text.Json;

namespace ServerApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Post([FromBody] User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://imevoimu:HD9q6S5rbRn9ql25hatbMQU6m-5NsCdP@shark.rmq.cloudamqp.com/imevoimu");
            using IConnection connection = factory.CreateConnection();

            //Creating Channel 
            using IModel channel = connection.CreateModel();

            //Creating Queue
            //durable messages for queue storage false and true  ( durable )
            // more channels connection ( exclusive ) 
            // ending message after destroy channel ( auto delete ) 
            channel.QueueDeclare("messagequeue",false,false,false);
            string serializeData = JsonSerializer.Serialize(model);
            //message to queue (binary type )  we can convert binary to byte
            //exchange : behaviour
            //routing : queue type
            //body : binary format
            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("","messagequeue",body:data);

            return Ok();
        }
    }
}

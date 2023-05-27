// See https://aka.ms/new-console-template for more information
using EmailSenderConsumer;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerApiExample.Modals;
using System.Text;
using System.Text.Json;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://imevoimu:HD9q6S5rbRn9ql25hatbMQU6m-5NsCdP@shark.rmq.cloudamqp.com/imevoimu");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.QueueDeclare("messagequeue", false, false, false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("messagequeue", true, consumer);

consumer.Received += async (s, e) =>
{
    //HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:7279/messagehub")
    //.Build();


    //Email Operasyonları. 
    //e.Body.Span
    string serializeData = Encoding.UTF8.GetString(e.Body.Span);
    User user = JsonSerializer.Deserialize<User>(serializeData);


    EmailSender.Send(user.Email,user.Message);
    Console.WriteLine($"{user.Email} received");

    //await connectionSignalR.InvokeAsync("SendMessageAsync", "Mail gönderildi.");
};

Console.Read();
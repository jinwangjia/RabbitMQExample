using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMQ2
    {
        ConnectionFactory factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "root", Password = "root001" };
        public void Send()
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", 
                                        durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.ExchangeDeclare("wsExchange", ExchangeType.Direct);
                channel.QueueBind("hello", "wsExchange", ExchangeType.Direct, null);
                for (int i = 0; i < 10000; i++)
                {
                    string message = "7e128156000a0092b7b0435700010020f37e " + i;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish( "wsExchange",
                                   ExchangeType.Direct,
                                   null,
                                   body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }

        public void Receive()
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

    }
}

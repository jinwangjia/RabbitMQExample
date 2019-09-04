using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMQ1
    {
        ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "root", Password = "root001", VirtualHost = "/" };

        public void Send()
        {
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel im = conn.CreateModel())
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                    im.ExchangeDeclare("rabbitmq_route", ExchangeType.Direct);
                    im.QueueDeclare("rabbitmq_query", false, false, false, null);
                    im.QueueBind("rabbitmq_query", "rabbitmq_route", ExchangeType.Direct, null);
                    for (int i = 0; i < 10000; i++)
                    {
                        byte[] message = Encoding.UTF8.GetBytes("7e128156000a0092b7b0435700010020f37e");
                        im.BasicPublish("rabbitmq_route", ExchangeType.Direct, null, message);
                        //Console.WriteLine("send:" + i);
                    }
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                }
            }
        }

        public void Receive()
        {
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel im = conn.CreateModel())
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                    while (true)
                    {
                        BasicGetResult res = im.BasicGet("rabbitmq_query", true);
                        if (res != null)
                        {
                            //Console.WriteLine("receiver:" + UTF8Encoding.UTF8.GetString(res.Body));
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                            break;
                        }
                    }
                }
            }
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ
{
    /// <summary>
    /// 这个比较好用
    /// </summary>
    public class RabbitMQ3
    {
        //1.1.实例化连接工厂
        ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
        public void Send()
        {
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            {
                //3. 创建信道
                using (var channel = connection.CreateModel())
            {
                    //4. 申明队列
                    channel.QueueDeclare(queue: "hello3", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                    for (int i = 0; i < 10000; i++)
                    {
                        //5. 构建byte消息数据包
                        string message = "7e128156000a0092b7b0435700010020f37e";
                        var body = Encoding.UTF8.GetBytes(message);
                        //6. 发送数据包
                        channel.BasicPublish(exchange: "", routingKey: "hello3", basicProperties: null, body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                }
            }
        }

        public void Receive()
        {
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            {
                //3. 创建信道
                using (var channel = connection.CreateModel())
                {
                    //4. 申明队列
                    channel.QueueDeclare(queue: "hello3", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //5. 构造消费者实例
                    var consumer = new EventingBasicConsumer(channel);
                    //6. 绑定消息接收后的事件委托
                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body);
                        Console.WriteLine(" [x] Received {0}", message);
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff"));
                    };
                    //7. 启动消费者
                    channel.BasicConsume(queue: "hello3", autoAck: true, consumer: consumer);
                    Thread.Sleep(100000);
                }
            }
        }
    }
}

using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Lab.Send
{
    class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "rabbit-test",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    for (int i = 1; i <= 10; i++)
                    {
                        var message = $"Message {i}";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "rabbit-test",
                                             basicProperties: null,
                                             body: body);

                        Console.WriteLine(" [x] Sent {0}", message);
                        Thread.Sleep(1000);
                    }
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
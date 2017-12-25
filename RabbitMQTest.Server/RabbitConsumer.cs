using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQTest.Server
{
    internal class RabbitConsumer: IDisposable
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Passowrd = "guest";
        private static string exchangeName = "";
        private static string queueName = "MyQuece";
        private static bool IsDurable = true;


        private const string VirtualHost = "";
        private int port = 0;


        //public delegate void OnReceiveMessage(string message);

        private IModel model;

        public RabbitConsumer()
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                Password = Passowrd,
                UserName = UserName,
                HostName = HostName
            };

            if (!string.IsNullOrEmpty(VirtualHost))
            {
                connectionFactory.VirtualHost = VirtualHost;
            }

            if (port > 0)
            {
                connectionFactory.Port = port;
            }

            var connection = connectionFactory.CreateConnection();
            model = connection.CreateModel();
            model.BasicQos(0, 1, false);
        }

        public bool Enabled { get; set; }

        public  void Start()
        {
            var consumer = new QueueingBasicConsumer(model);
            model.BasicConsume(queueName, false, consumer);

            while (Enabled)
            {
                var deliveryArgs = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                var message = Encoding.Default.GetString(deliveryArgs.Body);

                Console.WriteLine("Message Recived : {0}", message);
                model.BasicAck(deliveryArgs.DeliveryTag, false);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
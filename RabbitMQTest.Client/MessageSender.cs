using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTest.Client
{
    public class MessageSender: IDisposable
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Passowrd = "guest";
        private static string exchangeName = "";
        private static string queueName = "MyQuece";
        private static bool IsDurable = true;


        private const string VirtualHost = "";
        private int port = 0;

        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel model;

        public MessageSender()
        {
            DisplaySettigs();
            SetupRabbitMq();
        }

        private void SetupRabbitMq()
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

            if(port > 0)
            {
                connectionFactory.Port = port;
            }

            var connection = connectionFactory.CreateConnection();
            model = connection.CreateModel();

            //model.QueueDeclare(queueName, true, false, false, null);

            //model.ExchangeDeclare(exchangeName, ExchangeType.Topic);


            //model.QueueBind(queueName, exchangeName, "TestKey");
        }


        public void Send(string message)
        {
            var properties = model.CreateBasicProperties();
            properties.SetPersistent(false);

            byte[] messageBuffer = Encoding.Default.GetBytes(message);

            // send message
            model.BasicPublish(exchangeName, queueName, properties, messageBuffer);


        }

       
        private void DisplaySettigs()
        {
            Console.WriteLine("Host: {0}", HostName);
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}

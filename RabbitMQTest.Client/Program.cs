using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQTest.Client
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Passowrd = "guest";
        private static string exchangeName= "MyExchange";
        private static string queueName = "MyQuece";

        static void Main(string[] args)
        {
            //CreateQueue(); ;
            //BasicPublish();
            SimpleOneWayMessage();
        }

        private static void SimpleOneWayMessage()
        {
            Console.WriteLine("");

            var messageCount = 0;
            var sender = new MessageSender();

            Console.WriteLine("Type message and press enter to sender message");

            while (true)
            {
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Q)
                {
                    break;
                }

                if(key.Key == ConsoleKey.Enter)
                {
                    var message = string.Format("Message: {0}", messageCount);
                    Console.WriteLine(string.Format("Sending - {0}", message));
                    sender.Send(message);
                    messageCount++;
                }
            }
            Console.ReadLine();
        }

        private static void CreateQueue()
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                Password = Passowrd,
                UserName = UserName,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.QueueDeclare(queueName, true, false, false, null);

            model.ExchangeDeclare(exchangeName, ExchangeType.Topic);


            model.QueueBind(queueName, exchangeName, "TestKey");
        }

        static void BasicPublish()
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                Password = Passowrd,
                UserName = UserName,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            var properties = model.CreateBasicProperties();
            properties.SetPersistent(false);

            byte[] messageBuffer = Encoding.Default.GetBytes("My simple message");

            // send message
            model.BasicPublish( "", queueName, properties, messageBuffer);
            

            Console.WriteLine("Message sent");
           
        }
    }
}


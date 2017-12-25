using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTest.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMq queue processor");
            Console.WriteLine();

            var queueProcessor = new RabbitConsumer() { Enabled = true};
            queueProcessor.Start();
            Console.ReadLine();

        }

    }
}

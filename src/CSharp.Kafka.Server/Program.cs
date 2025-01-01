using Confluent.Kafka;
namespace CSharp.Kafka.Server
{
    class KafkaServer
    {
        public static async Task Main(string[] args)
        {
            string? bootstrapServers = null;
            string? inputTopic = null;
            string? outputTopic = null;
            string? groupId = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-bootstrap-server":
                        bootstrapServers = args[++i];
                        break;
                    case "-inputtopic":
                        inputTopic = args[++i];
                        break;
                    case "-outputtopic":
                        outputTopic = args[++i];
                        break;
                    case "-groupid":
                        groupId = args[++i];
                        break;
                }
            }

            if (bootstrapServers == null || inputTopic == null || outputTopic == null || groupId == null)
            {
                Console.WriteLine("Usage: -bootstrap-server <server> -inputtopic <inputTopic> -outputtopic <outputTopic> -groupid <groupId>");
                return;
            }

            // Consumer Task 실행
            Task.Run(() => ListenForMessages(bootstrapServers, inputTopic, outputTopic, groupId));

            // Producer에서 메시지 입력받기
            await HandleUserInputAsync(bootstrapServers, outputTopic);
        }

        public static async Task HandleUserInputAsync(string bootstrapServers, string outputTopic)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            while (true)
            {
                Console.WriteLine("Enter message to send (or 'q' to quit):");
                string? message = Console.ReadLine();
                if (message.ToLower() == "q")
                {
                    break;
                }

                try
                {
                    var deliveryReport = await producer.ProduceAsync(outputTopic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Sent '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Send failed: {e.Error.Reason}");
                }
            }
        }

        public static void ListenForMessages(string bootstrapServers, string inputTopic, string outputTopic, string groupId)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(inputTopic);

            var producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    string message = consumeResult.Message.Value;

                    if (message.StartsWith("[RESULT]"))
                    {
                        Console.WriteLine($"Result received: {message}");
                    }
                    else
                    {
                        Console.WriteLine($"Message received: {message}");
                        string resultMessage = "[RESULT] " + message;

                        try
                        {
                            producer.Produce(outputTopic, new Message<Null, string> { Value = resultMessage });
                            Console.WriteLine($"Processed and sent: {resultMessage}");
                        }
                        catch (ProduceException<Null, string> e)
                        {
                            Console.WriteLine($"Send failed: {e.Error.Reason}");
                        }
                    }
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Consume error: {e.Error.Reason}");
            }
        }
    }
}

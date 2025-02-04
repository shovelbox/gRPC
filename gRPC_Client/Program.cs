using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using GrpcServer;
using Grpc.Core;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new ChatService.ChatServiceClient(channel);

            using var call = client.Chat();

            // 서버로 메시지 보내기
            var sendTask = Task.Run(async () =>
            {
                while (true)
                {
                    var message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message)) break;

                    await call.RequestStream.WriteAsync(new ChatMessage
                    {
                        User = "Client",
                        Message = message
                    });
                }
                await call.RequestStream.CompleteAsync();
            });

            // 서버로부터 메시지 받기
            var receiveTask = Task.Run(async () =>
            {
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"{response.User}: {response.Message}");
                }
            });

            await Task.WhenAll(sendTask, receiveTask);
        }
    }
}

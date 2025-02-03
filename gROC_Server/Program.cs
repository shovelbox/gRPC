using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcServer
{
    class Program
    {
        const int Port = 50051;

        static async Task Main(string[] args)
        {
            var server = new Server
            {
                Services = { ChatService.BindService(new ChatServiceImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine("gRPC 서버가 실행 중입니다. 종료하려면 Ctrl+C를 누르세요.");
            await Task.Delay(Timeout.Infinite);
        }
    }

    class ChatServiceImpl : ChatService.ChatServiceBase
    {
        public override async Task Chat(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;
                Console.WriteLine($"{message.User}: {message.Message}");

                // 클라이언트에게 응답 보내기
                await responseStream.WriteAsync(new ChatMessage
                {
                    User = "Server",
                    Message = $"Received: {message.Message}"
                });
            }
        }
    }
}
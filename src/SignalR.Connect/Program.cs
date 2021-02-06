using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalR.Connect
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //https://orders.api-dev.marketplace.xxx.com/api/client/subscribe
            //https://users.api-dev.marketplace.xxx.com/api/store/subscribe
            //for all
            var connection = new HubConnectionBuilder()
                .WithUrl("https://orders.api-dev.marketplace.xxx.com/api/my/subscribe")
                .Build();


            connection
                .StartAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error opening the connection:{0}",
                                          task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine($"Connected, ConnectionId { connection.ConnectionId }");
                    }

                }).Wait();

            //order

            //public static readonly string SIGNALR_CHANNEL_RELOAD_HOME_BROKER = "reloadHomeBroker";

            //public static readonly string SIGNALR_CHANNEL_UPDATE_PENDENCIES = "updatePendencies";

            //public static readonly string SIGNALR_CHANNEL_UPDATE_INDEXES = "updateIndex";

            //client - receiver
            connection.On("newOrder", (string value) =>
                {
                    Console.WriteLine($"Recebendo uma nova orderm: { value }");
                });

            connection.On("updateIndex", (string value) =>
            {
                Console.WriteLine($"Recebendo uma atualização de um índice: { value }");
            });

            connection.On("dealOrder", (string value) =>
            {
                Console.WriteLine($"Recebendo uma nova negociação realizada: { value }");
            });

            connection.On("privateOrderChanged", (string value) =>
            {
                Console.WriteLine($"Uma ordem privada foi alteradada { value }");
            });

            connection.On("certificateImported", (string value) =>
            {
                Console.WriteLine($"Um certificado foi importado { value }");
            });

            connection.On("removeOrder", (string value) =>
            {
                Console.WriteLine($"Uma ordem foi removida do homeBroker { value }");
            });

            connection.On("reloadHomeBroker", (string value) =>
            {
                Console.WriteLine($"Recarregar homeBroker ? { value }");
            });

            connection.On("updatePendencies", (string value) =>
            {
                Console.WriteLine($"Atualizar pendências do usuário: { value }");
            });

            Console.ReadKey();
        }
    }
}
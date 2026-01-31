using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggingClient
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/"); // ポート番号を確認

                Console.WriteLine("1. 通常リクエストを送信します...");
                await client.GetAsync("api/get-data");

                Console.WriteLine("2. 除外対象（ClientLog）を送信します...");
                var logData = new { message = "Hello from C# Client", level = "Info" };
                await client.PostAsJsonAsync("api/common/clientLog", logData);
            }
            Console.WriteLine("送信完了。VSの出力ウィンドウを確認してください。");
            Console.ReadLine();
        }
    }
}

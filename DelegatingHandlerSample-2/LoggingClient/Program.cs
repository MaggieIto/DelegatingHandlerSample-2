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
                var response = await client.GetAsync("api/get-data");
                var content = await response.Content.ReadAsStringAsync();
            }

            Console.WriteLine("送信完了。VSの出力ウィンドウを確認してください。");
            Console.ReadLine();
        }
    }
}

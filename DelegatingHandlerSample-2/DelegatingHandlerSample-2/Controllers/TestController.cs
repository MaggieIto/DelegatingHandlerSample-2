using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace DelegatingHandlerSample_2.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/get-data")]
        public IHttpActionResult GetData()
        {
            // コントローラー内部での計測開始
            var sw = Stopwatch.StartNew();

            // 約1MBのダミーテキストを生成
            var data = Enumerable.Range(0, 10000).Select(i => $"DataIndex_{i}: Some large text content... ").ToList();

            sw.Stop();

            // コントローラー内での処理時間を出力
            Debug.WriteLine($"[Controller] Logic Time: {sw.Elapsed.TotalMilliseconds} ms");

            return Ok(data);
        }
    }
}

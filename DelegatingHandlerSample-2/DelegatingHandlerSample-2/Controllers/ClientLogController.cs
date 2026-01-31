using System.Diagnostics;
using System.Web.Http;

namespace DelegatingHandlerSample_2.Controllers
{
    /// <summary>
    /// クライアントログ受信用コントローラー（ログ計測除外対象）
    /// </summary>
    [RoutePrefix("api/common")]
    public class ClientLogController : ApiController
    {
        /// <summary>
        /// クライアントからのログを受信（このAPIはログ計測されない）
        /// </summary>
        /// <param name="logData">ログデータ</param>
        /// <returns>レスポンス</returns>
        [HttpPost]
        [Route("clientLog")]
        public IHttpActionResult PostClientLog([FromBody] dynamic logData)
        {
            Debug.WriteLine("[ClientLogController] クライアントログを受信（このAPIはHttpRequestLogHandlerで除外されています）");
            Debug.WriteLine($"Log Data: {logData}");

            return Ok(new { message = "Client log received (not logged by HttpRequestLogHandler)" });
        }
    }
}

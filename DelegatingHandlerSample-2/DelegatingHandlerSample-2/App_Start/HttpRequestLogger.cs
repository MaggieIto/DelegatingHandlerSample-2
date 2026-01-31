using System;
using System.Diagnostics;
using System.Net.Http;

namespace DelegatingHandlerSample_2
{
    /// <summary>
    /// HTTPリクエストのログ出力を担当するサービスクラス
    /// </summary>
    public class HttpRequestLogger
    {
        /// <summary>
        /// HTTPリクエストのログ情報を作成
        /// </summary>
        /// <param name="request">HTTPリクエスト</param>
        /// <param name="response">HTTPレスポンス（nullの場合は例外発生時）</param>
        /// <param name="elapsed">処理時間</param>
        /// <returns>ログ情報</returns>
        public HttpRequestDump CreateHttpRequestDump(
            HttpRequestMessage request,
            HttpResponseMessage response,
            TimeSpan elapsed)
        {
            var dump = new HttpRequestDump
            {
                Method = request.Method.Method,
                LocalPath = request.RequestUri.LocalPath,
                Elapsed = elapsed,
                Timestamp = DateTime.UtcNow,
                UserAgent = request.Headers.UserAgent?.ToString(),
                StatusCode = response?.StatusCode != null ? (int)response.StatusCode : (int?)null,
                IsSuccess = response?.IsSuccessStatusCode ?? false
            };

            return dump;
        }

        /// <summary>
        /// ログをコンソールに出力
        /// </summary>
        /// <param name="dump">ログ情報</param>
        public void WriteLog(HttpRequestDump dump)
        {
            var statusText = dump.StatusCode.HasValue ? dump.StatusCode.Value.ToString() : "ERROR";
            var successMark = dump.IsSuccess ? "✓" : "✗";

            Debug.WriteLine("========================================");
            Debug.WriteLine($"[{successMark}] HTTP Request Log");
            Debug.WriteLine($"Timestamp   : {dump.Timestamp:yyyy-MM-dd HH:mm:ss.fff}");
            Debug.WriteLine($"Method      : {dump.Method}");
            Debug.WriteLine($"Path        : {dump.LocalPath}");
            Debug.WriteLine($"Status      : {statusText}");
            Debug.WriteLine($"Elapsed     : {dump.Elapsed.TotalMilliseconds:F2} ms");
            Debug.WriteLine($"User-Agent  : {dump.UserAgent ?? "(none)"}");
            Debug.WriteLine("========================================");
        }
    }
}
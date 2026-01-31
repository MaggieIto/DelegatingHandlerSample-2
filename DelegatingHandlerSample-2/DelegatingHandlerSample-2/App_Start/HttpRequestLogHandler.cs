using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatingHandlerSample_2
{
    /// <summary>
    /// HTTPリクエストのログ出力を行うDelegatingHandler
    /// パイプラインの最も外側に配置することで、全体の処理時間を計測する
    /// </summary>
    public class HttpRequestLogHandler : DelegatingHandler
    {
        private readonly HttpRequestLogger logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HttpRequestLogHandler()
        {
            logger = new HttpRequestLogger();
        }

        /// <summary>
        /// HTTPリクエストを処理し、ログを出力
        /// </summary>
        /// <param name="request">HTTPリクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>HTTPレスポンス</returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // 除外APIの場合は計測せずに後続処理へ
            if (IsIgnoreApi(request))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            // 1. パイプラインに入る直前で計測開始
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // 2. 後続のすべての処理（圧縮、フィルタ、アクション実行）を待機
                var response = await base.SendAsync(request, cancellationToken);

                stopwatch.Stop();
                LogRequest(request, response, stopwatch.Elapsed);
                return response;
            }
            catch (Exception)
            {
                stopwatch.Stop();
                LogRequest(request, null, stopwatch.Elapsed);
                throw;
            }
        }

        /// <summary>
        /// 除外対象のAPIかどうかを判定
        /// </summary>
        /// <param name="request">HTTPリクエスト</param>
        /// <returns>除外対象の場合true</returns>
        private bool IsIgnoreApi(HttpRequestMessage request)
        {
            var method = request.Method.Method.ToUpper();
            var localPath = request.RequestUri.LocalPath;

            // /api/common/clientLog は除外
            if (method == "POST" && localPath == "/api/common/clientLog")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// リクエストログを出力
        /// </summary>
        /// <param name="request">HTTPリクエスト</param>
        /// <param name="response">HTTPレスポンス（nullの場合は例外発生時）</param>
        /// <param name="elapsed">処理時間</param>
        private void LogRequest(HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
        {
            var dump = logger.CreateHttpRequestDump(request, response, elapsed);
            logger.WriteLog(dump);
        }
    }
}
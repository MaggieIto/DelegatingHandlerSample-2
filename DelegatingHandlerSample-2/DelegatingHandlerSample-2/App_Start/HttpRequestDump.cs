using System;

namespace DelegatingHandlerSample_2
{
    public class HttpRequestDump
    {
        /// <summary>
        /// HTTPメソッド（GET, POST等）
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// リクエストのローカルパス
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 処理時間
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// HTTPステータスコード
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// リクエストが成功したかどうか
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
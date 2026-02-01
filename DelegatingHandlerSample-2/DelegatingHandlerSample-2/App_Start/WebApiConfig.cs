using System.Web.Http;

namespace DelegatingHandlerSample_2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API の設定およびサービス

            // Web API ルート
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new LoggingAttribute());

            // 0番目：自作のログハンドラー
            // 1番目：圧縮ハンドラー
            config.MessageHandlers.Insert(0, new HttpRequestLogHandler());
            //config.MessageHandlers.Insert(1, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }
    }
}

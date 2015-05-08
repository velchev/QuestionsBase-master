namespace QuestionsBase.FunctionalUI.Tests
{
    using TestStack.Seleno.Configuration;

    public class BrowserHost
    {
        public static readonly SelenoHost Instance = new SelenoHost();
        public static readonly string RootUrl;

        //static BrowserHost()
        //{
        //    Instance.Run("QuestionsBase", 4454);
        //    RootUrl = Instance.Application.Browser.Url;
        //}

        static BrowserHost()
        {
            Instance.Run("QuestionsBase", 4454, config => config.WithRouteConfig(RouteConfig.RegisterRoutes));

            RootUrl = Instance.Application.Browser.Url;
        }
    }
}

namespace MinimalApi.Endpoints;

public class EndpointsConfigure
{

    private static void ConfigurePublicEndPoints(WebApplication app)
    {
        //app.MapAccountAuthEndpoints();
        app.MapHREndpoints();
        //app.MapTaskEndpoints();
    }

    public void Configure(WebApplication app)
    {
        ConfigurePublicEndPoints(app);
    }


}

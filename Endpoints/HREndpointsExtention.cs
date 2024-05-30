using MinimalApi.Endpoints.GroupExtensions;

namespace MinimalApi.Endpoints;

public static class HREndpointsExtention
{
    public static void MapHREndpoints(this WebApplication app)
    {
        //app.MapGroup("/api/MasterData").MapMasterDataHrGroup().RequireAuthorization().WithOpenApi();
        app.MapGroup("/api/Employee").MapHREmployeeGroup().AllowAnonymous().WithOpenApi();
    }
}

namespace DocumentsService.Application.Configurations;

public static class MiddlewareSetup
{
    public static void ConfigureMiddleware(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
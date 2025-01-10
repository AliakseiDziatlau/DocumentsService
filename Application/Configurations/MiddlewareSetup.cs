namespace DocumentsService.Application.Configurations;

public static class MiddlewareSetup
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
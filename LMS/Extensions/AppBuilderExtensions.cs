using LMS.EFCore.Data;
using LMS.Services.Contracts.Constants;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Extensions
{
    public static class AppBuilderExtensions
    {
        public static WebApplication BuildCoreApp(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LMSDBContext>();
                dbContext.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(errApp => errApp.Run(async ctx =>
            {
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await ctx.Response.WriteAsJsonAsync(new APIResponse
                {
                    ResponseCode = nameof(API_RESPONSE_CODE.ERROR),
                    Description = Messages.EndpointErrorMessage,
                    Param = "",
                    Time = DateTime.UtcNow
                });
            }));

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}

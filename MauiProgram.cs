using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using BudgetMate.Service;
using BudgetMate.Service.Interface;

namespace BudgetMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
               .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITranscations, TranscationService>();
            builder.Services.AddScoped<ITagsService, TagsService>();
            builder.Services.AddScoped<IDeptService, DebtService>();
            
            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}

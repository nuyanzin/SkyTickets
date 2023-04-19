using Microsoft.Extensions.FileProviders;
using System.Net.Http.Headers;

namespace SkyTickets.WebClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/app");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    if (ShouldNotBeCached(context.File))
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            NoStore = true,
                            NoCache = true
                        };
                    }
                    else
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            MaxAge = TimeSpan.FromDays(7)
                        };
                    }
                }
            });
        }

        private static readonly HashSet<string> NoCacheFiles =
            new(new[] { "index.html", "en.json", "ru.json", "config.json", "styles.css", "style.css" });

        public bool ShouldNotBeCached(IFileInfo fileInfo)
        {
            return NoCacheFiles.Contains(fileInfo.Name);
        }
    }
}

using IGraphics.Graphics.Creators;
using Microsoft.Extensions.DependencyInjection;
using RetroGraph.Services;

namespace RetroGraph
{
    public static class ModuleRetroGraph
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton(SeedScene.CreateAndPopulateScene());
            services.AddSingleton<LogicHiddenLineViewService>();
        }
    }
}
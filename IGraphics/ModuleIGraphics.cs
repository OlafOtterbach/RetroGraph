using IGraphics.Graphics.Graphics.Creators;
using IGraphics.LogicViewing;
using Microsoft.Extensions.DependencyInjection;

namespace IGraphics
{
    public static class ModuleIGraphics
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton(SeedScene.CreateAndPopulateScene());
            services.AddSingleton(typeof(ILogicView), typeof(LogicView));
        }
    }
}
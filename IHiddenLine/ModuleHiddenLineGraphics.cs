using Microsoft.Extensions.DependencyInjection;

namespace IHiddenLineGraphics
{
    public static class ModuleHiddenLineGraphics
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton(typeof(IHiddenLineService), typeof(HiddenLineService));
        }
    }
}
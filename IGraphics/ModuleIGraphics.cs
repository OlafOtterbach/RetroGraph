using IGraphics.LogicViewing;
using IGraphics.LogicViewing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGraphics
{
    public static class ModuleIGraphics
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<ILogicView,LogicView>();
            services.AddSingleton<IMoveSensorService, CylinderSensorService>();
            services.AddSingleton<IMoveSensorService, SphereSensorService>();
            services.AddSingleton<IMoveSensorService, PlaneSensorService>();
            services.AddSingleton<IMoveSensorService, LinearSensorService>();
            services.AddSingleton<IMoveSensorProcessor, MoveSensorProcessor>();
        }
    }
}
using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.LogicViewing.Services
{
    public class MoveSensorProcessor : IMoveSensorProcessor
    {
        private Scene _scene;
        private IMoveSensorService[] _moveSensorServices;

        public MoveSensorProcessor(IEnumerable<IMoveSensorService> moveServices, Scene scene)
        {
            _scene = scene;
            _moveSensorServices = moveServices.ToArray();
        }

        public bool Process(MoveEvent moveEvent)
        {
            var body = _scene.GetBody(moveEvent.SelectedBodyId);
            var sensor = body?.Sensor;
            var eventIsProcessed = false;

            var services = _moveSensorServices.Where(s => s.CanProcess(sensor));
            foreach (var sensorService in services)
            {
                sensorService.Process(sensor, moveEvent);
                eventIsProcessed = true;
            }

            return eventIsProcessed;
        }
    }
}
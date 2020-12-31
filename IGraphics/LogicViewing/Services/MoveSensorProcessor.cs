using IGraphics.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.LogicViewing.Services
{
    public class MoveSensorProcessor : IMoveSensorProcessor
    {
        private IMoveSensorService[] _moveSensorServices;

        public MoveSensorProcessor(IEnumerable<IMoveSensorService> moveServices)
        {
            _moveSensorServices = moveServices.ToArray();
        }

        public bool Process(MoveState moveState)
        {
            ISensor sensor = moveState.SelectedBody?.Sensor;

            var eventIsProcessed = false;

            var services = _moveSensorServices.Where(s => s.CanProcess(sensor));
            foreach (var sensorService in services)
            {
                sensorService.Process(sensor, moveState);
                eventIsProcessed = true;
            }

            return eventIsProcessed;
        }
    }
}
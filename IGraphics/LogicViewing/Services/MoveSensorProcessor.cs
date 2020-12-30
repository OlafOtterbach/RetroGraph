﻿using IGraphics.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGraphics.LogicViewing.Services
{
    public class MoveSensorProcessor : IMoveSensorProcessor
    {
        private IMoveSensorService[] _moveSensorServices;

        public MoveSensorProcessor(IEnumerable<IMoveSensorService> moveServices)
        {
            _moveSensorServices = moveServices.ToArray();
        }

        public bool Process(ISensor sensor, MoveState moveState)
        {
            var eventIsProcessed = false;

            var services = _moveSensorServices.Where(s => s.CanProcess(sensor));
            foreach(var sensorService in services)
            {
                sensorService.Process(sensor, moveState);
                eventIsProcessed = true;
            }

            return eventIsProcessed;
        }
    }
}

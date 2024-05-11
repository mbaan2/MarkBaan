using System.Timers;
using Timer = System.Timers.Timer;

namespace MarkBaan.Server.Services
{
    public class Reactor : IReactor
    {
        private readonly IValveControl _valveControl;
        private readonly IPressureSensor _pressureSensor;

        private readonly double MAX_RELEASE_PRESSURE = 0.8;
        private readonly double MAX_INCREASE_PRESSURE = 0.3;
        public decimal ReleasePressure { get; set; } = 0.85m;
        public decimal IncreasePressure { get; set; } = 0.25m;
        private readonly Timer adjustTimer = new Timer(1000);


        public Reactor(IValveControl valveControl, IPressureSensor pressureSensor)
        {
            _valveControl = valveControl;
            _pressureSensor = pressureSensor;
            adjustTimer.Elapsed += AdjustValve;
            adjustTimer.Enabled = true;
        }


        private void AdjustValve(object source, ElapsedEventArgs e)
        {
            if (_pressureSensor.Pressure <= IncreasePressure) _valveControl.Close();
            if (_pressureSensor.Pressure >= ReleasePressure) _valveControl.Open();
        }

        public bool CheckValueNewReleasePressure(double releasePressure)
        {
            if (releasePressure > MAX_RELEASE_PRESSURE) return false;
            return true;
        }

        public bool CheckValueNewIncreasePressure(double increasePressure)
        {
            if (increasePressure < MAX_INCREASE_PRESSURE) return false;
            return true;
        }
    }
}

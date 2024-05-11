using System.Timers;
using Timer = System.Timers.Timer;

namespace MarkBaan.Server.Services
{
    public class PressureSensor : IPressureSensor
    {
        private readonly IValveControl _valveControl;

        public decimal Pressure { get; set; } = 0.7m;
        private Timer pressureTimer = new Timer(1000);

        public PressureSensor(IValveControl valveControl) {
            _valveControl = valveControl;
            pressureTimer.Elapsed += AdjustPressure;
            pressureTimer.Enabled = true;
        }

        private void AdjustPressure(Object source, ElapsedEventArgs e)
        {
            string ValveStatus = _valveControl.ValveStatus;

            if (ValveStatus.Equals("Closed")) Pressure += 0.03m;
            if (ValveStatus.Equals("Open")) Pressure -= 0.06m;

            if (Pressure < 0) Pressure = 0;
            if (Pressure > 1) Pressure = 1;
        }

        public decimal GetValue()
        {
            return Pressure;
        }
    }
}

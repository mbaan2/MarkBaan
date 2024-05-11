using System.Timers;

namespace MarkBaan.Server.Services
{
    public interface IReactor
    {
        decimal ReleasePressure { get; set; }
        decimal IncreasePressure { get; set; }
        bool CheckValueNewReleasePressure(double releasePressure);
        bool CheckValueNewIncreasePressure(double increasePressure);
    }
}

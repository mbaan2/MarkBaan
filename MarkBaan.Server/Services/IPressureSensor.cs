namespace MarkBaan.Server.Services
{
    public interface IPressureSensor
    {
        decimal Pressure { get; set; }
        decimal GetValue();
    }
}

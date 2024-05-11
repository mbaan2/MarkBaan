namespace MarkBaan.Server.Services
{
    public interface IValveControl
    {
        string ValveStatus { get; set; }
        void Open();
        void Close();
    }
}

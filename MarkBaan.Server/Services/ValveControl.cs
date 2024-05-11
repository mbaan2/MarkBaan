namespace MarkBaan.Server.Services
{
    public class ValveControl : IValveControl
    {
        public string ValveStatus { get; set; } = "Closed";

        public void Open()
        {
            Thread.Sleep(2000);
            ValveStatus = "Open";
        }

        public void Close()
        {
            Thread.Sleep(2000);
            ValveStatus = "Closed";
        }

    }
}

namespace F3R4L.DevPack.EveIntel.Logger.Models
{
    public class NewIntelLogEventArgs : EventArgs
    {
        public IEnumerable<LogLine> LogLines { get; set; }
    }
}

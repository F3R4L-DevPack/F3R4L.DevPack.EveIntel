namespace F3R4L.DevPack.EveIntel.Logger.Models
{
    public class LogEventArgs : EventArgs
    {
        public DateTime LogTime { get; set; }
        public string LogEntry { get; set; }

        public LogEventArgs(DateTime logTime, string logEntry)
        {
            LogTime = logTime;
            LogEntry = logEntry;
        }

        public override string ToString()
        {
            return $"{LogTime:yyyy-MM-dd HH:mm:ss} {LogEntry}";
        }
    }
}
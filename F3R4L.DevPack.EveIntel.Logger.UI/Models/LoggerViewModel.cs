using F3R4L.DevPack.EveIntel.Logger.Models;
using NTextCat.Commons;
using System.Collections.ObjectModel;

namespace F3R4L.DevPack.EveIntel.Logger.UI.Models
{
    public class LoggerViewModel
    {
        //public ObservableProperty<string> ListenerName { get; set; }
        //public ObservableProperty<DateTime> FileCreationTime { get; set; }

        public ObservableCollection<LogLine> LogLines { get; set; } = new ObservableCollection<LogLine>();

    }
}

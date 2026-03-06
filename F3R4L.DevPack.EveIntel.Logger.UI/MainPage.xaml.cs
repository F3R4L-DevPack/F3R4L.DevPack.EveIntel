using F3R4L.DevPack.EveIntel.Logger.Models;
using F3R4L.DevPack.EveIntel.Logger.UI.Models;
using NTextCat.Commons;

namespace F3R4L.DevPack.EveIntel.Logger.UI
{
    public partial class MainPage : ContentPage
    {
        public LoggerViewModel ViewModel { get; set; } = new LoggerViewModel();

        private readonly IWorker _worker;
        int count = 0;

        public MainPage(IWorker worker)
        {
            InitializeComponent(); 

            BindingContext = ViewModel;

            _worker = worker;

            _worker.NewLogInfo += Worker_NewLogInfo;

            ViewModel.LogLines.Add(new LogLine
            {
                LogDateTime = DateTime.Now,
                SystemName = "Jita",
                UserName = "Test User",
                Message = "This is a test log line."
            });
        }

        private void Worker_NewLogInfo(object? sender, NewIntelLogEventArgs e)
        {
            e.LogLines.ForEach(logLine =>
            {
                ViewModel.LogLines.Add(logLine);
            });
            Body.ScrollToAsync(StopPoint, ScrollToPosition.End, true).RunSynchronously();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            _worker.ExecuteAsync(@"D:\My Documents\EVE\logs\Chatlogs\wc.Vale+Tr+Ge_20260302_115102_93902200.txt");
        }
    }
}

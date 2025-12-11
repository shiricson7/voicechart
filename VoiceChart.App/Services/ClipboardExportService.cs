using System.Windows;
using VoiceChart.Core.Export;

namespace VoiceChart.App.Services;

public class ClipboardExportService : IExportSink
{
    public Task ExportAsync(string text, CancellationToken cancellationToken = default)
    {
        // Clipboard operations must run on STA; WPF already runs in STA thread.
        Clipboard.SetText(text);
        return Task.CompletedTask;
    }
}

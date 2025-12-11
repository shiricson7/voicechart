namespace VoiceChart.Core.Export;

public interface IExportSink
{
    Task ExportAsync(string text, CancellationToken cancellationToken = default);
}

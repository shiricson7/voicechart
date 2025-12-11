using System.Windows;
using VoiceChart.App.ViewModels;
using VoiceChart.App.Services;
using VoiceChart.Core.Audio;
using VoiceChart.Core.Processing;
using VoiceChart.Core.STT;
using VoiceChart.Core.Storage;
using VoiceChart.Core.Workflow;

namespace VoiceChart;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        // Wire up stub services for now. Replace with real implementations later.
        var recorder = new StubAudioRecorder();
        var transcriber = new WhisperTranscriber();
        var processor = new StubNoteProcessor();
        var repository = new FileVisitRepository(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "visits"));
        var workflow = new VisitWorkflow(recorder, transcriber, processor, repository);
        var exportSink = new ClipboardExportService(); // Swap with AutoTypeService to auto-type into active control.

        _viewModel = new MainViewModel(workflow, exportSink);
        DataContext = _viewModel;
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        _viewModel.Dispose();
    }
}

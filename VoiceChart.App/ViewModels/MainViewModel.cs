using System.ComponentModel;
using System.Runtime.CompilerServices;
using VoiceChart.App.Commands;
using VoiceChart.Core.Export;
using VoiceChart.Core.Models;
using VoiceChart.Core.Workflow;

namespace VoiceChart.App.ViewModels;

public class MainViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly VisitWorkflow _workflow;
    private readonly IExportSink _exportSink;
    private string _status = "대기중";
    private string _patientName = string.Empty;
    private string _clinicianName = string.Empty;
    private SoapNote _soap = new();
    private string _summary = string.Empty;

    public MainViewModel(VisitWorkflow workflow, IExportSink exportSink)
    {
        _workflow = workflow;
        _exportSink = exportSink;
        StartCommand = new RelayCommand(() => _ = StartAsync(), () => !_workflow.IsRecording);
        CompleteCommand = new RelayCommand(() => _ = CompleteAsync(), () => _workflow.IsRecording);
        ExportCommand = new RelayCommand(() => _ = ExportAsync(), () => !string.IsNullOrWhiteSpace(Summary));
    }

    public RelayCommand StartCommand { get; }
    public RelayCommand CompleteCommand { get; }
    public RelayCommand ExportCommand { get; }

    public string PatientName
    {
        get => _patientName;
        set => SetField(ref _patientName, value);
    }

    public string ClinicianName
    {
        get => _clinicianName;
        set => SetField(ref _clinicianName, value);
    }

    public SoapNote Soap
    {
        get => _soap;
        set => SetField(ref _soap, value);
    }

    public string Summary
    {
        get => _summary;
        set => SetField(ref _summary, value);
    }

    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task StartAsync()
    {
        Status = "녹음 시작...";
        await _workflow.StartRecordingAsync();
        Status = "녹음중 (핫키로 중지)";
        RaiseCommandStates();
    }

    private async Task CompleteAsync()
    {
        Status = "처리중...";
        try
        {
            var visit = await _workflow.CompleteAsync(PatientName, ClinicianName);
            Soap = visit.Soap;
            Summary = visit.Summary;
            Status = "완료";
        }
        catch (Exception ex)
        {
            Status = $"오류: {ex.Message}";
        }

        RaiseCommandStates();
    }

    private async Task ExportAsync()
    {
        var builder = new System.Text.StringBuilder();
        builder.AppendLine("요약");
        builder.AppendLine(Summary);
        builder.AppendLine();
        builder.AppendLine("SOAP");
        builder.AppendLine($"S: {Soap.Subjective}");
        builder.AppendLine($"O: {Soap.Objective}");
        builder.AppendLine($"A: {Soap.Assessment}");
        builder.AppendLine($"P: {Soap.Plan}");

        await _exportSink.ExportAsync(builder.ToString());
        Status = "내보내기 완료";
    }

    private void RaiseCommandStates()
    {
        StartCommand.RaiseCanExecuteChanged();
        CompleteCommand.RaiseCanExecuteChanged();
        ExportCommand.RaiseCanExecuteChanged();
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    public void Dispose()
    {
    }
}

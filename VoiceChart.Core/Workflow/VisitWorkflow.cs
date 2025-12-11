using VoiceChart.Core.Audio;
using VoiceChart.Core.Models;
using VoiceChart.Core.Processing;
using VoiceChart.Core.STT;
using VoiceChart.Core.Storage;

namespace VoiceChart.Core.Workflow;

public class VisitWorkflow
{
    private readonly IAudioRecorder _recorder;
    private readonly ITranscriber _transcriber;
    private readonly INoteProcessor _processor;
    private readonly IVisitRepository _repository;

    public VisitWorkflow(IAudioRecorder recorder, ITranscriber transcriber, INoteProcessor processor, IVisitRepository repository)
    {
        _recorder = recorder;
        _transcriber = transcriber;
        _processor = processor;
        _repository = repository;
    }

    public bool IsRecording => _recorder.IsRecording;

    public Task<string> StartRecordingAsync(CancellationToken cancellationToken = default)
    {
        return _recorder.StartAsync(cancellationToken);
    }

    public async Task<Visit> CompleteAsync(string patientName, string clinicianName, CancellationToken cancellationToken = default)
    {
        var audioPath = await _recorder.StopAsync(cancellationToken);
        var transcription = await _transcriber.TranscribeAsync(audioPath, cancellationToken);
        var notes = await _processor.GenerateAsync(transcription, cancellationToken);

        var visit = new Visit
        {
            PatientName = patientName,
            ClinicianName = clinicianName,
            AudioPath = audioPath,
            Transcript = transcription.Transcript,
            Soap = notes.Soap,
            Summary = notes.Summary
        };

        await _repository.SaveAsync(visit, cancellationToken);
        return visit;
    }
}

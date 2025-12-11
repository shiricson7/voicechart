namespace VoiceChart.Core.Audio;

public interface IAudioRecorder
{
    Task<string> StartAsync(CancellationToken cancellationToken = default);
    Task<string> StopAsync(CancellationToken cancellationToken = default);
    bool IsRecording { get; }
}

public class StubAudioRecorder : IAudioRecorder
{
    private bool _isRecording;

    public bool IsRecording => _isRecording;

    public Task<string> StartAsync(CancellationToken cancellationToken = default)
    {
        _isRecording = true;
        // TODO: open microphone and record to wav file.
        return Task.FromResult("recordings\\pending.wav");
    }

    public Task<string> StopAsync(CancellationToken cancellationToken = default)
    {
        _isRecording = false;
        // TODO: finalize file and return final path.
        return Task.FromResult("recordings\\pending.wav");
    }
}

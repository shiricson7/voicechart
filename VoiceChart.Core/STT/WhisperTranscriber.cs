namespace VoiceChart.Core.STT;

public class WhisperTranscriber : ITranscriber
{
    public Task<TranscriptionResult> TranscribeAsync(string audioPath, CancellationToken cancellationToken = default)
    {
        // TODO: Invoke whisper.cpp or Azure Speech to text here.
        return Task.FromResult(new TranscriptionResult
        {
            Transcript = string.Empty,
            Segments = Array.Empty<Models.TranscriptSegment>()
        });
    }
}

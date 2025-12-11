using VoiceChart.Core.Models;

namespace VoiceChart.Core.STT;

public interface ITranscriber
{
    Task<TranscriptionResult> TranscribeAsync(string audioPath, CancellationToken cancellationToken = default);
}

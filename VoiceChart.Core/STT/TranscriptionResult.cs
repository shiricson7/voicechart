using VoiceChart.Core.Models;

namespace VoiceChart.Core.STT;

public class TranscriptionResult
{
    public string Transcript { get; set; } = string.Empty;
    public IReadOnlyList<TranscriptSegment> Segments { get; set; } = Array.Empty<TranscriptSegment>();
}

namespace VoiceChart.Core.Models;

public class TranscriptSegment
{
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    public string Speaker { get; set; } = "Unknown";
    public string Text { get; set; } = string.Empty;
}

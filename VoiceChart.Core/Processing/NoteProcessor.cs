using VoiceChart.Core.Models;
using VoiceChart.Core.STT;

namespace VoiceChart.Core.Processing;

public interface INoteProcessor
{
    Task<NoteProcessingResult> GenerateAsync(TranscriptionResult transcription, CancellationToken cancellationToken = default);
}

public class NoteProcessingResult
{
    public SoapNote Soap { get; set; } = new();
    public string Summary { get; set; } = string.Empty;
}

public class StubNoteProcessor : INoteProcessor
{
    public Task<NoteProcessingResult> GenerateAsync(TranscriptionResult transcription, CancellationToken cancellationToken = default)
    {
        // TODO: Call LLM with prompt to generate SOAP note and summary.
        return Task.FromResult(new NoteProcessingResult
        {
            Summary = transcription.Transcript,
            Soap = new SoapNote
            {
                Subjective = "TODO: subjective",
                Objective = "TODO: objective",
                Assessment = "TODO: assessment",
                Plan = "TODO: plan"
            }
        });
    }
}

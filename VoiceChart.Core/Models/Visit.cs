namespace VoiceChart.Core.Models;

public class Visit
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset RecordedAt { get; set; } = DateTimeOffset.Now;
    public string PatientName { get; set; } = string.Empty;
    public string ClinicianName { get; set; } = string.Empty;

    public string AudioPath { get; set; } = string.Empty;
    public string Transcript { get; set; } = string.Empty;
    public SoapNote Soap { get; set; } = new();
    public string Summary { get; set; } = string.Empty;
}

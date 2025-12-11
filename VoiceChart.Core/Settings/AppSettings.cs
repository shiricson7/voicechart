namespace VoiceChart.Core.Settings;

public class AppSettings
{
    public string PreferredMicrophone { get; set; } = string.Empty;
    public string SttEngine { get; set; } = "whisper-local";
    public string LlmEngine { get; set; } = "local-llm";
    public string HotkeyStartStop { get; set; } = "Ctrl+Shift+R";
    public string HotkeyExport { get; set; } = "Ctrl+Shift+E";
    public bool AutoTypeIntoActiveControl { get; set; } = false;
}

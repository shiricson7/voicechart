using System.Runtime.InteropServices;
using System.Text;
using VoiceChart.Core.Export;

namespace VoiceChart.App.Services;

public class AutoTypeService : IExportSink
{
    // TODO: refine key events and handle unicode properly.
    public Task ExportAsync(string text, CancellationToken cancellationToken = default)
    {
        SendKeystrokes(text);
        return Task.CompletedTask;
    }

    private void SendKeystrokes(string text)
    {
        foreach (var ch in text)
        {
            var input = new INPUT
            {
                type = 1, // INPUT_KEYBOARD
                U = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = ch,
                        dwFlags = KeyEventF.UNICODE
                    }
                }
            };

            var inputs = new[] { input };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            input.U.ki.dwFlags = KeyEventF.UNICODE | KeyEventF.KEYUP;
            SendInput(1, new[] { input }, Marshal.SizeOf(typeof(INPUT)));
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    private struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)] public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public KeyEventF dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [Flags]
    private enum KeyEventF : uint
    {
        KEYDOWN = 0x0000,
        KEYUP = 0x0002,
        UNICODE = 0x0004
    }
}

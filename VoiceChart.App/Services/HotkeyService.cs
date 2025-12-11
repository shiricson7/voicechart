using System.Windows;
using System.Windows.Interop;

namespace VoiceChart.App.Services;

public class HotkeyService : IDisposable
{
    // TODO: Wire up RegisterHotKey/UnregisterHotKey with desired key combinations.
    private HwndSource? _source;

    public void Initialize(Window window)
    {
        var handle = new WindowInteropHelper(window).EnsureHandle();
        _source = HwndSource.FromHwnd(handle);
        if (_source != null)
        {
            _source.AddHook(WndProc);
        }
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        // TODO: handle WM_HOTKEY and raise events.
        return IntPtr.Zero;
    }

    public void Dispose()
    {
        if (_source != null)
        {
            _source.RemoveHook(WndProc);
            _source = null;
        }
    }
}

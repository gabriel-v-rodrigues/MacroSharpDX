using System.Runtime.InteropServices;
using InputManager;

namespace MacroSharpDX
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "keybd_event", ExactSpelling = true, SetLastError = true)]
        public static extern void KEYB_Event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern Keys GetAsyncKeyState(Keys Tecla);

        [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetAsyncKeyState", ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyPress(int key);


        public Form1()
        {
            InitializeComponent();
        }
    }
}
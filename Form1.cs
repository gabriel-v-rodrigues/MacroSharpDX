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

        //To receive the keys from the keyboard
        [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetAsyncKeyState", ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyPress(int key);

        public static bool IsOn { get; set; }

        public Form1()
        {
            InitializeComponent();
            IsOn = false;
        }

        // Trying the method of macroing in task
        private void button1_Click(object sender, EventArgs e)
        {
            Task task1;
            if (IsOn == false) { 
                IsOn = true;
                task1 = Task.Run(() => MacroActivate(Keys.Space));
            }
            else { IsOn = false;}

        }

        public static async Task MacroActivate(Keys keys)
        {
            while (IsOn == true) { 
                Keyboard.KeyDown(keys);
                await Task.Delay(50);
                Keyboard.KeyUp(keys);
                await Task.Delay(70);
            }
        }
    }
}    
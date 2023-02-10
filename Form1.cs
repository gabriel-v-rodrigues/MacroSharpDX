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

        

        public Form1()
        {
            InitializeComponent();
            IsOn = false;
        }

        public static bool IsOn { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            starting();
        }

        private void starting() {
            Thread.Sleep(1000);
            if (IsOn == false)
            {
                label1.Text = "Ativo";
                IsOn = true;
                Thread thread = new Thread(() => MacroActivate(Keys.Escape));
                thread.Start();

            }
            else
            {
                IsOn = false;
                label1.Text = "Inativo";
            }
            
        }

        public void MacroActivate(Keys keys)
        {
            while (IsOn == true)
            {
                Mouse.PressButton(Mouse.MouseKeys.Left);
                Thread.Sleep(10);
                /*
                Keyboard.KeyDown(keys); 
                Thread.Sleep(10);
                Keyboard.KeyUp(keys);
                Thread.Sleep(10);
                */
                if (IsOn == false)
                {
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Pressing F2 will enable/disable the macro
            if (GetKeyPress(113) != 0) { starting();  }
        }
    }
}    
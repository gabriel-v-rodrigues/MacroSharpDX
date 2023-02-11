using System.Collections;
using System.Runtime.InteropServices;
using InputManager;

namespace MacroSharpDX
{

    public partial class MacroSharpDXMain : Form
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "keybd_event", ExactSpelling = true, SetLastError = true)]
        public static extern void KEYB_Event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern Keys GetAsyncKeyState(Keys Tecla);

        //To receive the keys from the keyboard
        [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetAsyncKeyState", ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyPress(int key);

        public MacroSharpDXMain()
        {
            InitializeComponent();
            IsOn = false;
            //Listing all keys in a array
            ArrayList keys = new ArrayList(Enum.GetValues(typeof(Keys)));
            //Dont allow these keys in the list
            ArrayList NotAllowedKeys = new ArrayList { Keys.LButton, Keys.RButton, };
            //Inserting the keys in the lists
            foreach (Keys k in keys)
            {
                if (!NotAllowedKeys.Contains(k)) { 
                    listKeys.Items.Add(k);
                    designedKeyList.Items.Add(k);
                }
            }
            //Template for choosing
            listKeys.SelectedIndex = listKeys.Items.IndexOf(Keys.F2);
            designedKeyList.SelectedIndex = listKeys.Items.IndexOf(Keys.Space);

        }

        public static bool IsOn { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            StartMacroThread();
        }

        private void StartMacroThread() {
            Thread.Sleep(1000);
            if (IsOn == false)
            {
                label1.Text = "Active";
                IsOn = true;
                string? actualkey = designedKeyList.SelectedItem.ToString();
                int delay = Convert.ToInt32(numericDelay.Value);
                //dont allow delay lower than 1
                if (delay <= 1) { delay = 2; }
                Thread thread = new Thread(() => MacroActivate(actualkey, delay));
                thread.Start();
            }
            else
            {
                IsOn = false;
                label1.Text = "Disabled";
            }
            
        }

        public static void MacroActivate(string actualkey, int delay)
        {
            
            Keys keyX;
            Enum.TryParse(actualkey, out keyX);
            while (IsOn == true)
            {
                Keyboard.KeyDown(keyX); 
                Thread.Sleep(delay);
                Keyboard.KeyUp(keyX);
                Thread.Sleep(delay);
                if (IsOn == false)
                {
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listKeys.SelectedIndex >= 0) {
                string? actualkey = listKeys.SelectedItem.ToString();
                //Pressing F2 will enable/disable the macro
                Keys keyX;
                Enum.TryParse(actualkey, out keyX);
                int key = Convert.ToInt32(keyX);
                if (GetKeyPress(key) != 0) { 
                    StartMacroThread();  
                }
            }
        }
    }
}    
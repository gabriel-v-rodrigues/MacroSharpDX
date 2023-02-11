using System.Collections;
using System.Runtime.InteropServices;
using InputManager;

namespace MacroSharpDX
{

    public partial class MacroSharpDXMain : Form
    {
        //Only for test and debug
        /*
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "keybd_event", ExactSpelling = true, SetLastError = true)]
        public static extern void KEYB_Event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern Keys GetAsyncKeyState(Keys Tecla);
        */

        //To receive the keys from the keyboard
        [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetAsyncKeyState", ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyPress(int key);

        //Start
        public MacroSharpDXMain()
        {
            InitializeComponent();
            IsOn = false;

            //Listing all keys in a array
            ArrayList keys = new ArrayList(Enum.GetValues(typeof(Keys)));
            //Dont allow these keys in the list
            ArrayList NotAllowedKeys = new ArrayList { Keys.LButton, Keys.RButton, };

            //Inserting the keys in the lists
            foreach (Keys k in keys) {
                if (!NotAllowedKeys.Contains(k)) { 
                    listKeys.Items.Add(k);
                    designedKeyList.Items.Add(k);
                }
            }
            //Template for first choosing
            listKeys.SelectedIndex = listKeys.Items.IndexOf(Keys.F2);
            designedKeyList.SelectedIndex = listKeys.Items.IndexOf(Keys.Space);

        }

        //Bool check if the macro should be on or off
        public static bool IsOn { get; set; }

        //Button on/off click function
        private void button1_Click(object sender, EventArgs e)
        {
            StartMacroThread();
        }

        //Function to start the macro thread, work everything here before sending it to the macro thread
        private void StartMacroThread() {
            //make it sleep for 1 sec, so it don't stutter
            Thread.Sleep(1000);
            
            if (IsOn == false) {
                //Setting values for the macro Thread and setting the status info
                label1.Text = "Active";
                IsOn = true;
                string? pressingKey = designedKeyList.SelectedItem.ToString();
                int delay = Convert.ToInt32(numericDelay.Value);

                //dont allow delay lower than 1 as it can crash the app
                if (delay <= 1) { delay = 2; }

                //Create and Set the infos in the macro thread and start it
                Thread thread = new Thread(() => MacroActivate(pressingKey, delay));
                thread.Start();
            }

            //Disabling the thread
            else {
                IsOn = false;
                label1.Text = "Disabled";
            }
            
        }

        //Function working in the new thread as a macro
        public static void MacroActivate(string pressingKey, int delay)
        {
            //Converting the string from the list to the respective key
            Keys keyPress;
            Enum.TryParse(pressingKey, out keyPress);
            
            //Looping for macro instance
            while (IsOn == true) {
                //Pressing the buttons for the macro
                Keyboard.KeyDown(keyPress); 
                Thread.Sleep(delay);
                Keyboard.KeyUp(keyPress);
                Thread.Sleep(delay);

                //check the bool to see if it need to disable
                if (IsOn == false) {
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Check if you have selected anything in the listkeys
            if (listKeys.SelectedIndex >= 0) {

                //Convert the string in the list to it's respective key
                string? actualkey = listKeys.SelectedItem.ToString();
                Keys keyX;
                Enum.TryParse(actualkey, out keyX);
                int key = Convert.ToInt32(keyX);

                //Check if the key you pressed is the same selected in the list
                if (GetKeyPress(key) != 0) { 
                    StartMacroThread();  
                }
            }
        }
    }
}    
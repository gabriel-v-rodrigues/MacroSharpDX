using System.Collections;
using System.Runtime.InteropServices;
using InputManager;

namespace MacroSharpDX
{
    public partial class MacroSharpDXMain : Form
    {
        //Start App initializing
        public MacroSharpDXMain()
        {
            InitializeComponent();
            FirstDefinitions();
        }

        //To receive the keys from the keyboard
        [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetAsyncKeyState", ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyPress(int key);

        //Bool check if the macro should be on or off
        public static bool IsOn { get; set; }

        //Settings in the start of the app
        private void FirstDefinitions()
        {
            //Starting the Bool
            IsOn = false;

            //Listing all keys in a array
            ArrayList keys = new ArrayList(Enum.GetValues(typeof(Keys)));

            //Dont allow these keys in the list / Add keys here to not show in the list
            ArrayList NotAllowedKeys = new ArrayList {/* Keys.LButton, Keys.RButton */ };

            //Inserting the keys in the lists, ignoring the not allowed keys
            foreach (Keys k in keys)
            {
                if (!NotAllowedKeys.Contains(k))
                {
                    listKeys.Items.Add(k);
                    designedKeyList.Items.Add(k);
                }
            }
            //Template for first choosing
            listKeys.SelectedIndex = listKeys.Items.IndexOf(Keys.F2);
            designedKeyList.SelectedIndex = listKeys.Items.IndexOf(Keys.Space);
        }

        //Function to start the macro thread, work everything here before sending it to the macro thread
        public void StartMacroThread()
        {
            //make it sleep for 1 sec, so it don't stutter
            Thread.Sleep(1000);

            //Enabling the thread
            if (IsOn == false)
            {
                //Setting values for the macro Thread and setting the status info
                label1.Text = "Active";
                label1.ForeColor = Color.Green;
                IsOn = true;
                string? pressingKey = designedKeyList.SelectedItem.ToString();
                Double delay = Convert.ToDouble(numericDelay.Value);

                //dont allow delay lower than 1 as it can crash the app
                if (delay <= 1) { delay = 2; }

                if (pressingKey != null)
                {
                    //Create and Set the infos in the macro thread and start it
                    Thread thread = new(() => MacroActivate(pressingKey, delay));
                    thread.Start();
                }
            }

            //Disabling the thread
            else
            {
                IsOn = false;
                label1.Text = "Disabled";
                label1.ForeColor = Color.Red;
            }

        }

        //Function working in the new thread as a macro (IMPORTANT: DONT USE IN THE MAIN THREAD)
        private static void MacroActivate(string pressingKey, double delay)
        {
            //Converting the string from the list to the respective key
            bool canParse = Enum.TryParse(pressingKey, out Keys keyPress);

            if (!canParse) return;

            bool isMouse = keyPress == Keys.LButton || keyPress == Keys.RButton;
            //Looping for macro instance
            while (IsOn == true)
            {
                PressButton(keyPress, delay, isMouse);
                //check the bool to see if it need to disable
                if (IsOn == false) { break; }
            }
        }

        //Pressing key command
        private static void PressButton(Keys key, double delay, bool isMouse)
        {
            TimeSpan timespan = TimeSpan.FromMilliseconds(delay);

            if (isMouse)
            {
                Mouse.MouseKeys mouseKey = key == Keys.LButton ? Mouse.MouseKeys.Left : Mouse.MouseKeys.Right;
                Mouse.PressButton(mouseKey); //Performing a left click
                Thread.Sleep(timespan);
                return;
            }

            Keyboard.KeyDown(key);
            Thread.Sleep(timespan);
            Keyboard.KeyUp(key);
            Thread.Sleep(timespan);
        }

        //Code that executes everytime a check for the pressed key
        private void Timer_Tick(object sender, EventArgs e)
        {
            //Check if you have selected anything in the listkeys
            if (listKeys.SelectedIndex >= 0)
            {
                //Convert the string in the list to it's respective key
                string? actualkey = listKeys.SelectedItem.ToString();
                bool canParse = Enum.TryParse(actualkey, out Keys keyX);

                if (!canParse) return;

                int key = Convert.ToInt32(keyX);

                //Check if the key you pressed is the same selected in the list
                if (GetKeyPress(key) != 0)
                {
                    StartMacroThread();
                }
            }
        }

        //Button on/off click function (i dont recommend using the button, only shortcut key)
        private void Toggle_Click(object sender, EventArgs e)
        {
            StartMacroThread();
        }
    }
}
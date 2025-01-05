namespace MacroSharpDX
{
    partial class MacroSharpDXMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            listKeys = new ComboBox();
            label3 = new Label();
            designedKeyList = new ComboBox();
            label4 = new Label();
            numericDelay = new NumericUpDown();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericDelay).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(131, 5);
            button1.Name = "button1";
            button1.Size = new Size(59, 23);
            button1.TabIndex = 0;
            button1.TabStop = false;
            button1.Text = "On/Off";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Toggle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(60, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 2;
            label1.Text = "Disabled";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Tick += Timer_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Status:";
            // 
            // listKeys
            // 
            listKeys.FormattingEnabled = true;
            listKeys.Location = new Point(38, 60);
            listKeys.Name = "listKeys";
            listKeys.Size = new Size(121, 23);
            listKeys.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 42);
            label3.Name = "label3";
            label3.Size = new Size(115, 15);
            label3.TabIndex = 5;
            label3.Text = "Shortcut to Activate:";
            // 
            // designedKeyList
            // 
            designedKeyList.FormattingEnabled = true;
            designedKeyList.Location = new Point(24, 116);
            designedKeyList.Name = "designedKeyList";
            designedKeyList.Size = new Size(157, 23);
            designedKeyList.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 98);
            label4.Name = "label4";
            label4.Size = new Size(105, 15);
            label4.TabIndex = 5;
            label4.Text = "Macro Key Repeat:";
            // 
            // numericDelay
            // 
            numericDelay.DecimalPlaces = 2;
            numericDelay.Location = new Point(68, 165);
            numericDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericDelay.Name = "numericDelay";
            numericDelay.Size = new Size(60, 23);
            numericDelay.TabIndex = 6;
            numericDelay.TextAlign = HorizontalAlignment.Center;
            numericDelay.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(78, 147);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 7;
            label5.Text = "Delay";
            // 
            // MacroSharpDXMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(202, 198);
            Controls.Add(label5);
            Controls.Add(numericDelay);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(designedKeyList);
            Controls.Add(listKeys);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MacroSharpDXMain";
            Text = "DXMacro";
            ((System.ComponentModel.ISupportInitialize)numericDelay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        public System.Windows.Forms.Timer timer1;
        private Label label2;
        private ComboBox listKeys;
        private Label label3;
        private ComboBox designedKeyList;
        private Label label4;
        private NumericUpDown numericDelay;
        private Label label5;
        public Button button1;
    }
}
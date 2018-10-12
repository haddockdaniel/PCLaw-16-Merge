namespace PCLaw_16_Merge
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.labelMatters = new System.Windows.Forms.Label();
            this.labelUsers = new System.Windows.Forms.Label();
            this.labelLawyers = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelRates = new System.Windows.Forms.Label();
            this.labelTypesOfLaw = new System.Windows.Forms.Label();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.labelClients = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(475, 226);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonOpenLog);
            this.tabPage1.Controls.Add(this.labelMatters);
            this.tabPage1.Controls.Add(this.labelUsers);
            this.tabPage1.Controls.Add(this.labelLawyers);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.labelRates);
            this.tabPage1.Controls.Add(this.labelTypesOfLaw);
            this.tabPage1.Controls.Add(this.progressBarTotal);
            this.tabPage1.Controls.Add(this.labelClients);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(467, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Process";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Location = new System.Drawing.Point(262, 162);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenLog.TabIndex = 29;
            this.buttonOpenLog.Text = "Open Log";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // labelMatters
            // 
            this.labelMatters.AutoSize = true;
            this.labelMatters.Location = new System.Drawing.Point(282, 84);
            this.labelMatters.Name = "labelMatters";
            this.labelMatters.Size = new System.Drawing.Size(42, 13);
            this.labelMatters.TabIndex = 30;
            this.labelMatters.Text = "Matters";
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Location = new System.Drawing.Point(123, 82);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(34, 13);
            this.labelUsers.TabIndex = 22;
            this.labelUsers.Text = "Users";
            // 
            // labelLawyers
            // 
            this.labelLawyers.AutoSize = true;
            this.labelLawyers.Location = new System.Drawing.Point(123, 8);
            this.labelLawyers.Name = "labelLawyers";
            this.labelLawyers.Size = new System.Drawing.Size(46, 13);
            this.labelLawyers.TabIndex = 20;
            this.labelLawyers.Text = "Lawyers";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelRates
            // 
            this.labelRates.AutoSize = true;
            this.labelRates.Location = new System.Drawing.Point(123, 46);
            this.labelRates.Name = "labelRates";
            this.labelRates.Size = new System.Drawing.Size(35, 13);
            this.labelRates.TabIndex = 24;
            this.labelRates.Text = "Rates";
            // 
            // labelTypesOfLaw
            // 
            this.labelTypesOfLaw.AutoSize = true;
            this.labelTypesOfLaw.Location = new System.Drawing.Point(282, 10);
            this.labelTypesOfLaw.Name = "labelTypesOfLaw";
            this.labelTypesOfLaw.Size = new System.Drawing.Size(67, 13);
            this.labelTypesOfLaw.TabIndex = 26;
            this.labelTypesOfLaw.Text = "TypesOfLaw";
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(24, 124);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(415, 23);
            this.progressBarTotal.TabIndex = 19;
            // 
            // labelClients
            // 
            this.labelClients.AutoSize = true;
            this.labelClients.Location = new System.Drawing.Point(282, 46);
            this.labelClients.Name = "labelClients";
            this.labelClients.Size = new System.Drawing.Size(38, 13);
            this.labelClients.TabIndex = 28;
            this.labelClients.Text = "Clients";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonCopy);
            this.tabPage2.Controls.Add(this.buttonClear);
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(467, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Error Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(358, 164);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(75, 23);
            this.buttonCopy.TabIndex = 2;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(67, 164);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(17, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(433, 142);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 274);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.Label labelMatters;
        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Label labelLawyers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelRates;
        private System.Windows.Forms.Label labelTypesOfLaw;
        private System.Windows.Forms.ProgressBar progressBarTotal;
        private System.Windows.Forms.Label labelClients;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
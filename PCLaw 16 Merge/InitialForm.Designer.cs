namespace PCLaw_16_Merge
{
    partial class InitialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialForm));
            this.buttonStart = new System.Windows.Forms.Button();
            this.checkBoxTypeofLaw = new System.Windows.Forms.CheckBox();
            this.checkBoxRespAtty = new System.Windows.Forms.CheckBox();
            this.textBoxPrimaryDB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSecondaryDB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(150, 222);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 17;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // checkBoxTypeofLaw
            // 
            this.checkBoxTypeofLaw.AutoSize = true;
            this.checkBoxTypeofLaw.Location = new System.Drawing.Point(131, 181);
            this.checkBoxTypeofLaw.Name = "checkBoxTypeofLaw";
            this.checkBoxTypeofLaw.Size = new System.Drawing.Size(145, 17);
            this.checkBoxTypeofLaw.TabIndex = 14;
            this.checkBoxTypeofLaw.Text = "Separate by Type of Law";
            this.checkBoxTypeofLaw.UseVisualStyleBackColor = true;
            this.checkBoxTypeofLaw.CheckedChanged += new System.EventHandler(this.checkBoxTypeofLaw_CheckedChanged);
            // 
            // checkBoxRespAtty
            // 
            this.checkBoxRespAtty.AutoSize = true;
            this.checkBoxRespAtty.Location = new System.Drawing.Point(131, 158);
            this.checkBoxRespAtty.Name = "checkBoxRespAtty";
            this.checkBoxRespAtty.Size = new System.Drawing.Size(132, 17);
            this.checkBoxRespAtty.TabIndex = 13;
            this.checkBoxRespAtty.Text = "Separate by Resp Atty";
            this.checkBoxRespAtty.UseVisualStyleBackColor = true;
            this.checkBoxRespAtty.CheckedChanged += new System.EventHandler(this.checkBoxRespAtty_CheckedChanged);
            // 
            // textBoxPrimaryDB
            // 
            this.textBoxPrimaryDB.Location = new System.Drawing.Point(131, 84);
            this.textBoxPrimaryDB.Name = "textBoxPrimaryDB";
            this.textBoxPrimaryDB.Size = new System.Drawing.Size(215, 20);
            this.textBoxPrimaryDB.TabIndex = 12;
            this.textBoxPrimaryDB.Text = "PCLAWDB_00000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Primary Database";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(131, 46);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(215, 20);
            this.textBoxServer.TabIndex = 10;
            this.textBoxServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "SQL Server";
            // 
            // textBoxSecondaryDB
            // 
            this.textBoxSecondaryDB.Location = new System.Drawing.Point(131, 120);
            this.textBoxSecondaryDB.Name = "textBoxSecondaryDB";
            this.textBoxSecondaryDB.Size = new System.Drawing.Size(215, 20);
            this.textBoxSecondaryDB.TabIndex = 19;
            this.textBoxSecondaryDB.Text = "PCLAWDB_79914";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Secondary Database";
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 262);
            this.Controls.Add(this.textBoxSecondaryDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.checkBoxTypeofLaw);
            this.Controls.Add(this.checkBoxRespAtty);
            this.Controls.Add(this.textBoxPrimaryDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InitialForm";
            this.Text = "InitialForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox checkBoxTypeofLaw;
        private System.Windows.Forms.CheckBox checkBoxRespAtty;
        private System.Windows.Forms.TextBox textBoxPrimaryDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSecondaryDB;
        private System.Windows.Forms.Label label3;
    }
}


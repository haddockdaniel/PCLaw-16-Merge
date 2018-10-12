namespace PCLaw_16_Merge
{
    partial class Selections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Selections));
            this.listViewLawyer = new System.Windows.Forms.ListView();
            this.buttonClearLawyers = new System.Windows.Forms.Button();
            this.buttonLawyerSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewLawyer
            // 
            this.listViewLawyer.Location = new System.Drawing.Point(30, 13);
            this.listViewLawyer.Name = "listViewLawyer";
            this.listViewLawyer.Size = new System.Drawing.Size(282, 223);
            this.listViewLawyer.TabIndex = 8;
            this.listViewLawyer.UseCompatibleStateImageBehavior = false;
            this.listViewLawyer.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewLawyer_ColumnClick);
            // 
            // buttonClearLawyers
            // 
            this.buttonClearLawyers.Location = new System.Drawing.Point(107, 256);
            this.buttonClearLawyers.Name = "buttonClearLawyers";
            this.buttonClearLawyers.Size = new System.Drawing.Size(111, 23);
            this.buttonClearLawyers.TabIndex = 7;
            this.buttonClearLawyers.Text = "Clear Selection";
            this.buttonClearLawyers.UseVisualStyleBackColor = true;
            this.buttonClearLawyers.Click += new System.EventHandler(this.buttonClearLawyers_Click);
            // 
            // buttonLawyerSelect
            // 
            this.buttonLawyerSelect.Location = new System.Drawing.Point(127, 309);
            this.buttonLawyerSelect.Name = "buttonLawyerSelect";
            this.buttonLawyerSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonLawyerSelect.TabIndex = 6;
            this.buttonLawyerSelect.Text = "Begin";
            this.buttonLawyerSelect.UseVisualStyleBackColor = true;
            this.buttonLawyerSelect.Click += new System.EventHandler(this.buttonLawyerSelect_Click);
            // 
            // Selections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 356);
            this.Controls.Add(this.listViewLawyer);
            this.Controls.Add(this.buttonClearLawyers);
            this.Controls.Add(this.buttonLawyerSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Selections";
            this.Text = "Selections";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewLawyer;
        private System.Windows.Forms.Button buttonClearLawyers;
        private System.Windows.Forms.Button buttonLawyerSelect;
    }
}
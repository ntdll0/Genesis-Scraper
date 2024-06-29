
namespace Genesis
{
    partial class Filter
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
            this.startswith = new AeroSuite.Controls.CueTextBox();
            this.captionLabel1 = new AeroSuite.Controls.CaptionLabel();
            this.captionLabel2 = new AeroSuite.Controls.CaptionLabel();
            this.endswith = new AeroSuite.Controls.CueTextBox();
            this.captionLabel3 = new AeroSuite.Controls.CaptionLabel();
            this.contains = new AeroSuite.Controls.CueTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startswith
            // 
            this.startswith.Cue = "";
            this.startswith.Location = new System.Drawing.Point(25, 40);
            this.startswith.Name = "startswith";
            this.startswith.Size = new System.Drawing.Size(264, 20);
            this.startswith.TabIndex = 2;
            this.startswith.TextChanged += new System.EventHandler(this.startswith_TextChanged);
            // 
            // captionLabel1
            // 
            this.captionLabel1.AutoSize = true;
            this.captionLabel1.Location = new System.Drawing.Point(21, 16);
            this.captionLabel1.Name = "captionLabel1";
            this.captionLabel1.Size = new System.Drawing.Size(86, 21);
            this.captionLabel1.TabIndex = 3;
            this.captionLabel1.Text = "Starts With";
            // 
            // captionLabel2
            // 
            this.captionLabel2.AutoSize = true;
            this.captionLabel2.Location = new System.Drawing.Point(21, 63);
            this.captionLabel2.Name = "captionLabel2";
            this.captionLabel2.Size = new System.Drawing.Size(80, 21);
            this.captionLabel2.TabIndex = 5;
            this.captionLabel2.Text = "Ends With";
            // 
            // endswith
            // 
            this.endswith.Cue = "";
            this.endswith.Location = new System.Drawing.Point(25, 87);
            this.endswith.Name = "endswith";
            this.endswith.Size = new System.Drawing.Size(264, 20);
            this.endswith.TabIndex = 4;
            this.endswith.TextChanged += new System.EventHandler(this.endswith_TextChanged);
            // 
            // captionLabel3
            // 
            this.captionLabel3.AutoSize = true;
            this.captionLabel3.Location = new System.Drawing.Point(21, 110);
            this.captionLabel3.Name = "captionLabel3";
            this.captionLabel3.Size = new System.Drawing.Size(71, 21);
            this.captionLabel3.TabIndex = 7;
            this.captionLabel3.Text = "Contains";
            // 
            // contains
            // 
            this.contains.Cue = "";
            this.contains.Location = new System.Drawing.Point(25, 134);
            this.contains.Name = "contains";
            this.contains.Size = new System.Drawing.Size(264, 20);
            this.contains.TabIndex = 6;
            this.contains.TextChanged += new System.EventHandler(this.contains_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.captionLabel3);
            this.groupBox1.Controls.Add(this.contains);
            this.groupBox1.Controls.Add(this.captionLabel2);
            this.groupBox1.Controls.Add(this.endswith);
            this.groupBox1.Controls.Add(this.captionLabel1);
            this.groupBox1.Controls.Add(this.startswith);
            this.groupBox1.ForeColor = System.Drawing.Color.Silver;
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 180);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters";
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(338, 204);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Filter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.Filter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private AeroSuite.Controls.CueTextBox startswith;
        private AeroSuite.Controls.CaptionLabel captionLabel1;
        private AeroSuite.Controls.CaptionLabel captionLabel2;
        private AeroSuite.Controls.CueTextBox endswith;
        private AeroSuite.Controls.CaptionLabel captionLabel3;
        private AeroSuite.Controls.CueTextBox contains;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
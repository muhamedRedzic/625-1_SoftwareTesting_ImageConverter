namespace CSTiffImageConverter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbImageFormats = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.dlgOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxResize = new System.Windows.Forms.GroupBox();
            this.radioButtonDontKeepRatio = new System.Windows.Forms.RadioButton();
            this.radioButtonKeepRatio = new System.Windows.Forms.RadioButton();
            this.radioButtonCrop = new System.Windows.Forms.RadioButton();
            this.radioButtonNoResize = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBoxResize.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbImageFormats);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnConvert);
            this.groupBox1.Location = new System.Drawing.Point(21, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox1.Size = new System.Drawing.Size(931, 326);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jpeg | Tiff | Bmp | Gif";
            // 
            // cbImageFormats
            // 
            this.cbImageFormats.FormattingEnabled = true;
            this.cbImageFormats.Items.AddRange(new object[] {
            ".jpeg",
            ".tiff",
            ".tiff (multipaged)",
            ".bmp",
            ".gif"});
            this.cbImageFormats.Location = new System.Drawing.Point(59, 238);
            this.cbImageFormats.Margin = new System.Windows.Forms.Padding(4);
            this.cbImageFormats.Name = "cbImageFormats";
            this.cbImageFormats.Size = new System.Drawing.Size(300, 39);
            this.cbImageFormats.TabIndex = 4;
            this.cbImageFormats.SelectedIndexChanged += new System.EventHandler(this.cbImageFormats_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(59, 47);
            this.textBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(808, 143);
            this.textBox1.TabIndex = 3;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // btnConvert
            // 
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(389, 238);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(477, 55);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert To Selected Format";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // dlgOpenFileDialog
            // 
            this.dlgOpenFileDialog.Filter = "Image files (.jpg, .jpeg, .tif)|*.jpg;*.jpeg;*.tif;*.tiff";
            this.dlgOpenFileDialog.Multiselect = true;
            this.dlgOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgOpenFileDialog_FileOk);
            // 
            // groupBoxResize
            // 
            this.groupBoxResize.Controls.Add(this.radioButtonDontKeepRatio);
            this.groupBoxResize.Controls.Add(this.radioButtonKeepRatio);
            this.groupBoxResize.Controls.Add(this.radioButtonCrop);
            this.groupBoxResize.Controls.Add(this.radioButtonNoResize);
            this.groupBoxResize.Location = new System.Drawing.Point(21, 352);
            this.groupBoxResize.Name = "groupBoxResize";
            this.groupBoxResize.Size = new System.Drawing.Size(931, 198);
            this.groupBoxResize.TabIndex = 7;
            this.groupBoxResize.TabStop = false;
            this.groupBoxResize.Text = "groupBox2";
            // 
            // radioButtonDontKeepRatio
            // 
            this.radioButtonDontKeepRatio.AutoSize = true;
            this.radioButtonDontKeepRatio.Location = new System.Drawing.Point(389, 116);
            this.radioButtonDontKeepRatio.Name = "radioButtonDontKeepRatio";
            this.radioButtonDontKeepRatio.Size = new System.Drawing.Size(249, 36);
            this.radioButtonDontKeepRatio.TabIndex = 3;
            this.radioButtonDontKeepRatio.TabStop = true;
            this.radioButtonDontKeepRatio.Text = "Don\'t keep ratio";
            this.radioButtonDontKeepRatio.UseVisualStyleBackColor = true;
            // 
            // radioButtonKeepRatio
            // 
            this.radioButtonKeepRatio.AutoSize = true;
            this.radioButtonKeepRatio.Location = new System.Drawing.Point(59, 117);
            this.radioButtonKeepRatio.Name = "radioButtonKeepRatio";
            this.radioButtonKeepRatio.Size = new System.Drawing.Size(182, 36);
            this.radioButtonKeepRatio.TabIndex = 2;
            this.radioButtonKeepRatio.TabStop = true;
            this.radioButtonKeepRatio.Text = "Keep ratio";
            this.radioButtonKeepRatio.UseVisualStyleBackColor = true;
            // 
            // radioButtonCrop
            // 
            this.radioButtonCrop.AutoSize = true;
            this.radioButtonCrop.Location = new System.Drawing.Point(389, 37);
            this.radioButtonCrop.Name = "radioButtonCrop";
            this.radioButtonCrop.Size = new System.Drawing.Size(113, 36);
            this.radioButtonCrop.TabIndex = 1;
            this.radioButtonCrop.TabStop = true;
            this.radioButtonCrop.Text = "Crop";
            this.radioButtonCrop.UseVisualStyleBackColor = true;
            // 
            // radioButtonNoResize
            // 
            this.radioButtonNoResize.AutoSize = true;
            this.radioButtonNoResize.Location = new System.Drawing.Point(59, 38);
            this.radioButtonNoResize.Name = "radioButtonNoResize";
            this.radioButtonNoResize.Size = new System.Drawing.Size(182, 36);
            this.radioButtonNoResize.TabIndex = 0;
            this.radioButtonNoResize.TabStop = true;
            this.radioButtonNoResize.Text = "No Resize";
            this.radioButtonNoResize.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 578);
            this.Controls.Add(this.groupBoxResize);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Image converter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxResize.ResumeLayout(false);
            this.groupBoxResize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.OpenFileDialog dlgOpenFileDialog;
        private System.Windows.Forms.ComboBox cbImageFormats;
        private System.Windows.Forms.GroupBox groupBoxResize;
        private System.Windows.Forms.RadioButton radioButtonDontKeepRatio;
        private System.Windows.Forms.RadioButton radioButtonKeepRatio;
        private System.Windows.Forms.RadioButton radioButtonCrop;
        private System.Windows.Forms.RadioButton radioButtonNoResize;

    }
}


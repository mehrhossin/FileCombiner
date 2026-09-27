//================================================================================
//Relative Path: Form1.Designer.cs
//================================================================================

namespace FileCombiner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        // ─── Declaration ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.RadioButton rdoInputFolder;
        private System.Windows.Forms.RadioButton rdoInputDrop;
        private System.Windows.Forms.Panel panelFolderMode;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFolderFiles;      // ← جدید

        private System.Windows.Forms.Label lblDropHere;
        private System.Windows.Forms.ListBox lstDroppedFiles;
        private System.Windows.Forms.Label lblDroppedCount;
        private System.Windows.Forms.Button btnClearDropList;

        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSearchSubfolders;
        private System.Windows.Forms.CheckBox chkTree;
        private System.Windows.Forms.CheckBox chkHaderSumury;
        private System.Windows.Forms.RadioButton rdoTxt;
        private System.Windows.Forms.RadioButton rdoMd;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.NumericUpDown numPartCount;
        private System.Windows.Forms.Label lblPartCount;
        private System.Windows.Forms.ComboBox cmbClipboardMode;
        private System.Windows.Forms.Label lblClipboardMode;

        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Button btnSaveTree;

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            rdoInputFolder = new RadioButton();
            rdoInputDrop = new RadioButton();
            panelFolderMode = new Panel();
            label1 = new Label();
            txtFolderPath = new TextBox();
            btnBrowseFolder = new Button();
            lstFolderFiles = new ListBox();
            lblDropHere = new Label();
            lstDroppedFiles = new ListBox();
            lblDroppedCount = new Label();
            btnClearDropList = new Button();
            groupBox2 = new GroupBox();
            label2 = new Label();
            txtExtension = new TextBox();
            chkSearchSubfolders = new CheckBox();
            chkTree = new CheckBox();
            chkHaderSumury = new CheckBox();
            lblFormat = new Label();
            rdoTxt = new RadioButton();
            rdoMd = new RadioButton();
            lblPartCount = new Label();
            numPartCount = new NumericUpDown();
            lblClipboardMode = new Label();
            cmbClipboardMode = new ComboBox();
            groupBox3 = new GroupBox();
            label3 = new Label();
            txtOutputPath = new TextBox();
            btnBrowseOutput = new Button();
            btnCombine = new Button();
            btnSaveTree = new Button();
            progressBar1 = new ProgressBar();
            txtLog = new TextBox();
            label4 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            saveFileDialog1 = new SaveFileDialog();
            groupBox1.SuspendLayout();
            panelFolderMode.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPartCount).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rdoInputFolder);
            groupBox1.Controls.Add(rdoInputDrop);
            groupBox1.Controls.Add(panelFolderMode);
            groupBox1.Controls.Add(lblDropHere);
            groupBox1.Controls.Add(lstDroppedFiles);
            groupBox1.Controls.Add(lblDroppedCount);
            groupBox1.Controls.Add(btnClearDropList);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(760, 290);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "مسیر ورودی";
            // 
            // rdoInputFolder
            // 
            rdoInputFolder.AutoSize = true;
            rdoInputFolder.Checked = true;
            rdoInputFolder.Location = new Point(15, 24);
            rdoInputFolder.Name = "rdoInputFolder";
            rdoInputFolder.Size = new Size(80, 19);
            rdoInputFolder.TabIndex = 0;
            rdoInputFolder.TabStop = true;
            rdoInputFolder.Text = "مسیر پوشه";
            rdoInputFolder.UseVisualStyleBackColor = true;
            rdoInputFolder.CheckedChanged += rdoInputMode_CheckedChanged;
            // 
            // rdoInputDrop
            // 
            rdoInputDrop.AutoSize = true;
            rdoInputDrop.Location = new Point(175, 24);
            rdoInputDrop.Name = "rdoInputDrop";
            rdoInputDrop.Size = new Size(140, 19);
            rdoInputDrop.TabIndex = 1;
            rdoInputDrop.Text = "کشیدن فایل‌های سورس";
            rdoInputDrop.UseVisualStyleBackColor = true;
            rdoInputDrop.CheckedChanged += rdoInputMode_CheckedChanged;
            // 
            // panelFolderMode
            // 
            panelFolderMode.Controls.Add(label1);
            panelFolderMode.Controls.Add(txtFolderPath);
            panelFolderMode.Controls.Add(btnBrowseFolder);
            panelFolderMode.Controls.Add(lstFolderFiles);
            panelFolderMode.Location = new Point(0, 48);
            panelFolderMode.Name = "panelFolderMode";
            panelFolderMode.Size = new Size(756, 235);
            panelFolderMode.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 10);
            label1.Name = "label1";
            label1.Size = new Size(224, 15);
            label1.TabIndex = 0;
            label1.Text = "پوشه‌ای که می‌خواهید فایل‌ها را از آن بخوانید:";
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(142, 30);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(598, 23);
            txtFolderPath.TabIndex = 2;
            // 
            // btnBrowseFolder
            // 
            btnBrowseFolder.Location = new Point(15, 28);
            btnBrowseFolder.Name = "btnBrowseFolder";
            btnBrowseFolder.Size = new Size(120, 27);
            btnBrowseFolder.TabIndex = 1;
            btnBrowseFolder.Text = "انتخاب پوشه...";
            btnBrowseFolder.UseVisualStyleBackColor = true;
            btnBrowseFolder.Click += btnBrowseFolder_Click;
            // 
            // lstFolderFiles
            // 
            lstFolderFiles.Font = new Font("Consolas", 8.5F);
            lstFolderFiles.FormattingEnabled = true;
            lstFolderFiles.HorizontalScrollbar = true;
            lstFolderFiles.IntegralHeight = false;
            lstFolderFiles.ItemHeight = 13;
            lstFolderFiles.Location = new Point(15, 62);
            lstFolderFiles.Name = "lstFolderFiles";
            lstFolderFiles.SelectionMode = SelectionMode.MultiExtended;
            lstFolderFiles.Size = new Size(725, 165);
            lstFolderFiles.TabIndex = 3;
            // 
            // lblDropHere
            // 
            lblDropHere.AllowDrop = true;
            lblDropHere.BackColor = Color.FromArgb(30, 30, 30);
            lblDropHere.BorderStyle = BorderStyle.FixedSingle;
            lblDropHere.Font = new Font("Segoe UI", 10F);
            lblDropHere.ForeColor = Color.FromArgb(100, 180, 255);
            lblDropHere.Location = new Point(15, 50);
            lblDropHere.Name = "lblDropHere";
            lblDropHere.Size = new Size(730, 55);
            lblDropHere.TabIndex = 7;
            lblDropHere.Text = "📂  فایل‌های سورس را اینجا بکش و ول کن";
            lblDropHere.TextAlign = ContentAlignment.MiddleCenter;
            lblDropHere.Visible = false;
            lblDropHere.DragDrop += lblDropHere_DragDrop;
            lblDropHere.DragEnter += lblDropHere_DragEnter;
            // 
            // lstDroppedFiles
            // 
            lstDroppedFiles.Font = new Font("Consolas", 8.5F);
            lstDroppedFiles.FormattingEnabled = true;
            lstDroppedFiles.HorizontalScrollbar = true;
            lstDroppedFiles.IntegralHeight = false;
            lstDroppedFiles.ItemHeight = 13;
            lstDroppedFiles.Location = new Point(15, 112);
            lstDroppedFiles.Name = "lstDroppedFiles";
            lstDroppedFiles.SelectionMode = SelectionMode.MultiExtended;
            lstDroppedFiles.Size = new Size(650, 155);
            lstDroppedFiles.TabIndex = 8;
            lstDroppedFiles.Visible = false;
            lstDroppedFiles.KeyDown += lstDroppedFiles_KeyDown;
            // 
            // lblDroppedCount
            // 
            lblDroppedCount.AutoSize = true;
            lblDroppedCount.Location = new Point(15, 272);
            lblDroppedCount.Name = "lblDroppedCount";
            lblDroppedCount.Size = new Size(94, 15);
            lblDroppedCount.TabIndex = 9;
            lblDroppedCount.Text = "۰ فایل اضافه شده";
            lblDroppedCount.Visible = false;
            // 
            // btnClearDropList
            // 
            btnClearDropList.Location = new Point(672, 112);
            btnClearDropList.Name = "btnClearDropList";
            btnClearDropList.Size = new Size(73, 75);
            btnClearDropList.TabIndex = 10;
            btnClearDropList.Text = "پاک کردن\nهمه";
            btnClearDropList.UseVisualStyleBackColor = true;
            btnClearDropList.Visible = false;
            btnClearDropList.Click += btnClearDropList_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtExtension);
            groupBox2.Controls.Add(chkSearchSubfolders);
            groupBox2.Controls.Add(chkTree);
            groupBox2.Controls.Add(chkHaderSumury);
            groupBox2.Controls.Add(lblFormat);
            groupBox2.Controls.Add(rdoTxt);
            groupBox2.Controls.Add(rdoMd);
            groupBox2.Controls.Add(lblPartCount);
            groupBox2.Controls.Add(numPartCount);
            groupBox2.Controls.Add(lblClipboardMode);
            groupBox2.Controls.Add(cmbClipboardMode);
            groupBox2.Location = new Point(12, 310);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(760, 115);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "تنظیمات";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 25);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 0;
            label2.Text = "پسوند(ها) — مثال: .cs;.cpp";
            // 
            // txtExtension
            // 
            txtExtension.Location = new Point(188, 22);
            txtExtension.Name = "txtExtension";
            txtExtension.Size = new Size(180, 23);
            txtExtension.TabIndex = 2;
            txtExtension.Text = ".cs";
            // 
            // chkSearchSubfolders
            // 
            chkSearchSubfolders.AutoSize = true;
            chkSearchSubfolders.Checked = true;
            chkSearchSubfolders.CheckState = CheckState.Checked;
            chkSearchSubfolders.Location = new Point(15, 58);
            chkSearchSubfolders.Name = "chkSearchSubfolders";
            chkSearchSubfolders.Size = new Size(130, 19);
            chkSearchSubfolders.TabIndex = 5;
            chkSearchSubfolders.Text = "جستجو در زیرپوشه‌ها";
            chkSearchSubfolders.UseVisualStyleBackColor = true;
            // 
            // chkTree
            // 
            chkTree.AutoSize = true;
            chkTree.Checked = true;
            chkTree.CheckState = CheckState.Checked;
            chkTree.Location = new Point(200, 58);
            chkTree.Name = "chkTree";
            chkTree.Size = new Size(125, 19);
            chkTree.TabIndex = 7;
            chkTree.Text = "نمایش درخت فایل‌ها";
            chkTree.UseVisualStyleBackColor = true;
            // 
            // chkHaderSumury
            // 
            chkHaderSumury.AutoSize = true;
            chkHaderSumury.Checked = true;
            chkHaderSumury.CheckState = CheckState.Checked;
            chkHaderSumury.Location = new Point(390, 58);
            chkHaderSumury.Name = "chkHaderSumury";
            chkHaderSumury.Size = new Size(132, 19);
            chkHaderSumury.TabIndex = 6;
            chkHaderSumury.Text = "نمایش هدر و نتایج آخر";
            chkHaderSumury.UseVisualStyleBackColor = true;
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(15, 88);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(75, 15);
            lblFormat.TabIndex = 8;
            lblFormat.Text = "فرمت خروجی:";
            // 
            // rdoTxt
            // 
            rdoTxt.AutoSize = true;
            rdoTxt.Checked = true;
            rdoTxt.Location = new Point(110, 86);
            rdoTxt.Name = "rdoTxt";
            rdoTxt.Size = new Size(73, 19);
            rdoTxt.TabIndex = 9;
            rdoTxt.TabStop = true;
            rdoTxt.Text = "Text (.txt)";
            rdoTxt.UseVisualStyleBackColor = true;
            // 
            // rdoMd
            // 
            rdoMd.AutoSize = true;
            rdoMd.Location = new Point(220, 86);
            rdoMd.Name = "rdoMd";
            rdoMd.Size = new Size(114, 19);
            rdoMd.TabIndex = 10;
            rdoMd.Text = "Markdown (.md)";
            rdoMd.UseVisualStyleBackColor = true;
            // 
            // lblPartCount
            // 
            lblPartCount.AutoSize = true;
            lblPartCount.Location = new Point(402, 88);
            lblPartCount.Name = "lblPartCount";
            lblPartCount.Size = new Size(164, 15);
            lblPartCount.TabIndex = 4;
            lblPartCount.Text = "تعداد قسمت‌های خروجی (1-10):";
            // 
            // numPartCount
            // 
            numPartCount.Location = new Point(581, 85);
            numPartCount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numPartCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPartCount.Name = "numPartCount";
            numPartCount.Size = new Size(60, 23);
            numPartCount.TabIndex = 3;
            numPartCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblClipboardMode
            // 
            lblClipboardMode.AutoSize = true;
            lblClipboardMode.Location = new Point(375, 25);
            lblClipboardMode.Name = "lblClipboardMode";
            lblClipboardMode.Size = new Size(90, 15);
            lblClipboardMode.TabIndex = 11;
            lblClipboardMode.Text = "حالت Clipboard:";
            // 
            // cmbClipboardMode
            // 
            cmbClipboardMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClipboardMode.FormattingEnabled = true;
            cmbClipboardMode.Location = new Point(465, 22);
            cmbClipboardMode.Name = "cmbClipboardMode";
            cmbClipboardMode.Size = new Size(164, 23);
            cmbClipboardMode.TabIndex = 11;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(txtOutputPath);
            groupBox3.Controls.Add(btnBrowseOutput);
            groupBox3.Location = new Point(12, 433);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(760, 65);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "فایل خروجی";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 28);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 0;
            label3.Text = "نام فایل خروجی:";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(121, 25);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(490, 23);
            txtOutputPath.TabIndex = 4;
            txtOutputPath.Text = "combined_output.txt";
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(618, 23);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(120, 27);
            btnBrowseOutput.TabIndex = 5;
            btnBrowseOutput.Text = "انتخاب مسیر...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // btnCombine
            // 
            btnCombine.BackColor = Color.FromArgb(0, 122, 204);
            btnCombine.FlatStyle = FlatStyle.Flat;
            btnCombine.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCombine.ForeColor = Color.White;
            btnCombine.Location = new Point(12, 506);
            btnCombine.Name = "btnCombine";
            btnCombine.Size = new Size(572, 40);
            btnCombine.TabIndex = 3;
            btnCombine.Text = "ترکیب فایل‌ها";
            btnCombine.UseVisualStyleBackColor = false;
            btnCombine.Click += btnCombine_Click;
            // 
            // btnSaveTree
            // 
            btnSaveTree.BackColor = Color.FromArgb(40, 167, 69);
            btnSaveTree.FlatStyle = FlatStyle.Flat;
            btnSaveTree.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSaveTree.ForeColor = Color.White;
            btnSaveTree.Location = new Point(590, 506);
            btnSaveTree.Name = "btnSaveTree";
            btnSaveTree.Size = new Size(182, 40);
            btnSaveTree.TabIndex = 11;
            btnSaveTree.Text = "💾 ذخیره درخت";
            btnSaveTree.UseVisualStyleBackColor = false;
            btnSaveTree.Click += btnSaveTree_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 554);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(760, 23);
            progressBar1.TabIndex = 4;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.Black;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.ForeColor = Color.Lime;
            txtLog.Location = new Point(12, 600);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(760, 158);
            txtLog.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 583);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 6;
            label4.Text = "گزارش:";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 771);
            Controls.Add(label4);
            Controls.Add(txtLog);
            Controls.Add(progressBar1);
            Controls.Add(btnSaveTree);
            Controls.Add(btnCombine);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(800, 810);
            Name = "Form1";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مهر (ترکیب‌کننده فایل‌ها) نسخه 1.7 ";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panelFolderMode.ResumeLayout(false);
            panelFolderMode.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPartCount).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion



    }
}

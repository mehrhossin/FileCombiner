//================================================================================
// Relative Path: FileSelectionForm.Designer.cs
//================================================================================

namespace FileCombiner
{
    partial class FileSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBoxFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelectedFiles;
        private System.Windows.Forms.Label lblTotalFiles;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalFiles = new System.Windows.Forms.Label();
            this.lblSelectedFiles = new System.Windows.Forms.Label();
            this.checkedListBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();

            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();

            // ── groupBox1 ────────────────────────────────────────────────────
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.groupBox1.Controls.Add(this.lblTotalFiles);
            this.groupBox1.Controls.Add(this.lblSelectedFiles);
            this.groupBox1.Controls.Add(this.checkedListBoxFiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = AppStrings.FsfGrpFiles;

            // ── lblTotalFiles ────────────────────────────────────────────────
            this.lblTotalFiles.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left);
            this.lblTotalFiles.AutoSize = true;
            this.lblTotalFiles.Location = new System.Drawing.Point(15, 375);
            this.lblTotalFiles.Name = "lblTotalFiles";
            this.lblTotalFiles.Size = new System.Drawing.Size(90, 15);
            this.lblTotalFiles.TabIndex = 2;
            this.lblTotalFiles.Text = AppStrings.FsfLblTotal(0);

            // ── lblSelectedFiles ─────────────────────────────────────────────
            this.lblSelectedFiles.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right);
            this.lblSelectedFiles.Location = new System.Drawing.Point(550, 375);
            this.lblSelectedFiles.Name = "lblSelectedFiles";
            this.lblSelectedFiles.Size = new System.Drawing.Size(200, 15);
            this.lblSelectedFiles.TabIndex = 1;
            this.lblSelectedFiles.Text = AppStrings.FsfLblSelected(0, "0 B");
            this.lblSelectedFiles.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // ── checkedListBoxFiles ──────────────────────────────────────────
            this.checkedListBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.checkedListBoxFiles.CheckOnClick = true;
            this.checkedListBoxFiles.FormattingEnabled = true;
            this.checkedListBoxFiles.Location = new System.Drawing.Point(15, 25);
            this.checkedListBoxFiles.Name = "checkedListBoxFiles";
            this.checkedListBoxFiles.Size = new System.Drawing.Size(730, 340);
            this.checkedListBoxFiles.TabIndex = 0;
            this.checkedListBoxFiles.ItemCheck +=
                new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxFiles_ItemCheck);

            // ── panel1 ───────────────────────────────────────────────────────
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnDeselectAll);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 486);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 75);
            this.panel1.TabIndex = 1;

            // ── btnCancel ────────────────────────────────────────────────────
            this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = AppStrings.FsfBtnCancel;
            this.btnCancel.UseVisualStyleBackColor = true;

            // ── btnOK ────────────────────────────────────────────────────────
            this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right);
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F,
                                          System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(652, 20);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 35);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = AppStrings.FsfBtnOK;
            this.btnOK.UseVisualStyleBackColor = false;

            // ── btnDeselectAll ───────────────────────────────────────────────
            this.btnDeselectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeselectAll.Location = new System.Drawing.Point(282, 20);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(120, 35);
            this.btnDeselectAll.TabIndex = 1;
            this.btnDeselectAll.Text = AppStrings.FsfBtnDeselectAll;
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click +=
                new System.EventHandler(this.btnDeselectAll_Click);

            // ── btnSelectAll ─────────────────────────────────────────────────
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAll.Location = new System.Drawing.Point(408, 20);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(120, 35);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = AppStrings.FsfBtnSelectAll;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click +=
                new System.EventHandler(this.btnSelectAll_Click);

            // ── txtSearch ────────────────────────────────────────────────────
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.txtSearch.Location = new System.Drawing.Point(15, 35);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(730, 23);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged +=
                new System.EventHandler(this.txtSearch_TextChanged);

            // ── label1 ───────────────────────────────────────────────────────
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = AppStrings.FsfLblSearch;

            // ── groupBox2 ────────────────────────────────────────────────────
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 62);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = AppStrings.FsfGrpFilter;

            // ── FileSelectionForm ────────────────────────────────────────────
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FileSelectionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = AppStrings.FsfTitle;

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}

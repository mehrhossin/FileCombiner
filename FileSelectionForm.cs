//================================================================================
// Relative Path: FileSelectionForm.cs
//================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCombiner
{
    public partial class FileSelectionForm : Form
    {
        public List<string> SelectedFiles { get; private set; }
        private List<FileItemInfo> allFiles;
        private string rootPath;

        // ─── ست نگهداری مسیرهای تیک‌خورده (مستقل از فیلتر سرچ) ─────────────
        private readonly HashSet<string> _checkedPaths = new HashSet<string>();

        public FileSelectionForm(string rootPath, List<string> files)
        {
            InitializeComponent();
            this.rootPath = rootPath;

            allFiles = files.Select(f => new FileItemInfo
            {
                FullPath = f,
                RelativePath = Path.GetRelativePath(rootPath, f),
                FileName = Path.GetFileName(f),
                Size = new FileInfo(f).Length
            }).OrderBy(f => f.RelativePath).ToList();

            this.Load += FileSelectionForm_Load;
        }

        private void FileSelectionForm_Load(object sender, EventArgs e)
        {
            ApplyStrings();
            LoadFiles(allFiles);
            RestoreSelectionState_FromFile();
            RestoreSelectionState();
            UpdateCounts();
        }

        // ─── اعمال رشته‌های زبان انتخابی روی UI ─────────────────────────────
        private void ApplyStrings()
        {
            this.Text = AppStrings.FsfTitle;
            groupBox1.Text = AppStrings.FsfGrpFiles;
            groupBox2.Text = AppStrings.FsfGrpFilter;
            label1.Text = AppStrings.FsfLblSearch;
            btnSelectAll.Text = AppStrings.FsfBtnSelectAll;
            btnDeselectAll.Text = AppStrings.FsfBtnDeselectAll;
            btnOK.Text = AppStrings.FsfBtnOK;
            btnCancel.Text = AppStrings.FsfBtnCancel;

            // RTL فقط برای فارسی
            bool isFa = AppStrings.Language == AppLanguage.Persian;
            this.RightToLeft = isFa ? RightToLeft.Yes : RightToLeft.No;
            this.RightToLeftLayout = isFa;
        }

        // ─── بارگذاری لیست (بدون دست زدن به _checkedPaths) ──────────────────
        private void LoadFiles(List<FileItemInfo> files)
        {
            checkedListBoxFiles.BeginUpdate();
            checkedListBoxFiles.Items.Clear();

            foreach (var file in files)
                checkedListBoxFiles.Items.Add(file, false);

            checkedListBoxFiles.EndUpdate();
            lblTotalFiles.Text = AppStrings.FsfLblTotal(files.Count);
        }

        // ─── اعمال _checkedPaths روی آیتم‌های نمایش‌داده‌شده ─────────────────
        private void RestoreSelectionState()
        {
            checkedListBoxFiles.BeginUpdate();
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
            {
                var item = (FileItemInfo)checkedListBoxFiles.Items[i];
                checkedListBoxFiles.SetItemChecked(i, _checkedPaths.Contains(item.RelativePath));
            }
            checkedListBoxFiles.EndUpdate();
        }

        private void RestoreSelectionState_FromFile()
        {
            try
            {
                if (!File.Exists(SettingsPath)) return;

                string json = File.ReadAllText(SettingsPath);
                var state = JsonSerializer.Deserialize<SelectionState>(json);

                // فقط اگر همان rootPath بود بازیابی کن
                if (state == null || state.RootPath != rootPath) return;

                var savedSet = new HashSet<string>(
                    state.CheckedRelativePaths ?? new List<string>());

                // فقط _checkedPaths را پر کن — UI را RestoreSelectionState انجام می‌دهد
                foreach (var fi in allFiles)
                {
                    if (savedSet.Contains(fi.RelativePath))
                        _checkedPaths.Add(fi.RelativePath);
                }
            }
            catch
            {
                // اگر فایل تنظیمات خراب بود، بدون خطا ادامه بده
            }
        }

        // ─── وقتی کاربر تیک می‌زند یا برمی‌دارد ─────────────────────────────
        private void checkedListBoxFiles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!this.IsHandleCreated) return;

            // ItemCheck قبل از تغییر واقعی فایر می‌شود → Timer کوتاه
            var timer = new System.Windows.Forms.Timer { Interval = 10 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                timer.Dispose();
                SyncCheckedPathsFromUI();
                UpdateCounts();
            };
            timer.Start();
        }

        // ─── همگام‌سازی _checkedPaths با UI ──────────────────────────────────
        private void SyncCheckedPathsFromUI()
        {
            var visiblePaths = new HashSet<string>(
                checkedListBoxFiles.Items.Cast<FileItemInfo>().Select(f => f.RelativePath));

            // ابتدا همه آیتم‌های visible را از ست حذف کن
            foreach (var p in visiblePaths.ToList())
                _checkedPaths.Remove(p);

            // سپس آنهایی که تیک دارند را اضافه کن
            foreach (FileItemInfo item in checkedListBoxFiles.CheckedItems)
                _checkedPaths.Add(item.RelativePath);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            checkedListBoxFiles.BeginUpdate();
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
                checkedListBoxFiles.SetItemChecked(i, true);
            checkedListBoxFiles.EndUpdate();

            foreach (FileItemInfo item in checkedListBoxFiles.Items)
                _checkedPaths.Add(item.RelativePath);

            UpdateCounts();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            checkedListBoxFiles.BeginUpdate();
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
                checkedListBoxFiles.SetItemChecked(i, false);
            checkedListBoxFiles.EndUpdate();

            foreach (FileItemInfo item in checkedListBoxFiles.Items)
                _checkedPaths.Remove(item.RelativePath);

            UpdateCounts();
        }

        private void UpdateCounts()
        {
            if (!this.IsHandleCreated) return;

            long totalSize = allFiles
                .Where(f => _checkedPaths.Contains(f.RelativePath))
                .Sum(f => f.Size);

            lblSelectedFiles.Text = AppStrings.FsfLblSelected(_checkedPaths.Count,
                                        FormatFileSize(totalSize));

            btnOK.Enabled = _checkedPaths.Count > 0;
        }

        // ─── سرچ — فقط نمایش را فیلتر می‌کند، تیک‌ها دست نمی‌خورند ──────────
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var filtered = string.IsNullOrEmpty(searchText)
                ? allFiles
                : allFiles.Where(f =>
                    f.FileName.ToLower().Contains(searchText) ||
                    f.RelativePath.ToLower().Contains(searchText)
                  ).ToList();

            LoadFiles(filtered);
            RestoreSelectionState();
            UpdateCounts();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SelectedFiles = allFiles
                    .Where(f => _checkedPaths.Contains(f.RelativePath))
                    .Select(f => f.FullPath)
                    .ToList();

                if (SelectedFiles.Count == 0)
                {
                    MessageBox.Show(AppStrings.FsfMsgSelectOne, AppStrings.WarningTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                SaveSelectionState();
            }

            base.OnFormClosing(e);
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
            return $"{len:0.##} {sizes[order]}";
        }

        // ─── مسیر فایل ذخیره تنظیمات ─────────────────────────────────────────
        private static string SettingsPath =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MehrCombiner",
                "selection_state.json"
            );

        private void SaveSelectionState()
        {
            try
            {
                var state = new SelectionState
                {
                    RootPath = rootPath,
                    CheckedRelativePaths = _checkedPaths.ToList()
                };

                string dir = Path.GetDirectoryName(SettingsPath)!;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string json = JsonSerializer.Serialize(state,
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);
            }
            catch { }
        }

        // ─── مدل ذخیره وضعیت ─────────────────────────────────────────────────
        public class SelectionState
        {
            public string RootPath { get; set; }
            public List<string> CheckedRelativePaths { get; set; }
        }

        // ─── اطلاعات هر فایل ─────────────────────────────────────────────────
        public class FileItemInfo
        {
            public string FullPath { get; set; }
            public string RelativePath { get; set; }
            public string FileName { get; set; }
            public long Size { get; set; }

            public override string ToString()
            {
                string[] sizes = { "B", "KB", "MB", "GB" };
                double len = Size;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
                return $"{RelativePath}  ({len:0.##} {sizes[order]})";
            }
        }
    }
}

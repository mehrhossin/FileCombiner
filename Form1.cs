using System.Text;

namespace FileCombiner
{
    // ─── فرمت خروجی ──────────────────────────────────────────────────────────
    public enum OutputFormat { Txt, Md }

    // ─── حالت کپی ────────────────────────────────────────────────────────────
    public enum ClipboardMode { CopyFile, CopyContent }

    public partial class Form1 : Form
    {
        // ─── فیلد نگهداری فایل‌های Drop شده ─────────────────────────────────
        private readonly List<string> _droppedFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
            rdoTxt.CheckedChanged += OutputFormat_Changed;
            rdoMd.CheckedChanged += OutputFormat_Changed;
            this.lblDropHere.AllowDrop = true;
            // ✅ پر کردن ComboBox و انتخاب پیش‌فرض
            cmbClipboardMode.Items.Clear();
            cmbClipboardMode.Items.Add("کپی فایل نهایی");   // index 0
            cmbClipboardMode.Items.Add("کپی محتوا");         // index 1
            cmbClipboardMode.SelectedIndex = 0;

            // حالت پیش‌فرض ورودی
            rdoInputFolder.Checked = true;
            UpdateInputModeUI();
        }

        // ─── پر کردن lstFolderFiles بعد از انتخاب پوشه ───────────────────────────
        private void RefreshFolderFileList()
        {
            lstFolderFiles.BeginUpdate();
            lstFolderFiles.Items.Clear();

            if (string.IsNullOrWhiteSpace(txtFolderPath.Text) ||
                !Directory.Exists(txtFolderPath.Text))
            {
                lstFolderFiles.EndUpdate();
                return;
            }

            var searchOption = chkSearchSubfolders.Checked
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

            var allFiles = Directory.GetFiles(txtFolderPath.Text, "*.*", searchOption)
                                    .OrderBy(f => f)
                                    .ToList();

            foreach (var f in allFiles)
            {
                string rel = Path.GetRelativePath(txtFolderPath.Text, f);
                lstFolderFiles.Items.Add(rel);
            }

            lstFolderFiles.EndUpdate();
            LogMessage($"📂 {allFiles.Count} فایل در پوشه یافت شد.");
        }

        // ─── حذف آیتم‌های انتخابی از lstDroppedFiles با کلید Delete ─────────────
        private void lstDroppedFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (lstDroppedFiles.SelectedIndices.Count == 0) return;

            // از آخر به اول حذف کن تا index‌ها جابجا نشن
            var indices = lstDroppedFiles.SelectedIndices
                                         .Cast<int>()
                                         .OrderByDescending(i => i)
                                         .ToList();

            lstDroppedFiles.BeginUpdate();
            foreach (int idx in indices)
            {
                _droppedFiles.RemoveAt(idx);
                lstDroppedFiles.Items.RemoveAt(idx);
            }
            lstDroppedFiles.EndUpdate();

            UpdateDroppedCount();
            LogMessage($"🗑 {indices.Count} فایل از لیست حذف شد. باقی‌مانده: {_droppedFiles.Count}");
        }


        // ─── تشخیص حالت کپی انتخابی ─────────────────────────────────────────
        private ClipboardMode CurrentClipboardMode =>
            cmbClipboardMode.SelectedIndex == 1
                ? ClipboardMode.CopyContent
                : ClipboardMode.CopyFile;

        private OutputFormat CurrentFormat => rdoMd.Checked ? OutputFormat.Md : OutputFormat.Txt;

        // ─── تغییر فرمت خروجی ────────────────────────────────────────────────
        private void OutputFormat_Changed(object sender, EventArgs e)
        {
            string current = txtOutputPath.Text;
            if (string.IsNullOrWhiteSpace(current)) return;

            string dir = Path.GetDirectoryName(current) ?? "";
            string name = Path.GetFileNameWithoutExtension(current);
            string newExt = rdoMd.Checked ? ".md" : ".txt";
            txtOutputPath.Text = string.IsNullOrWhiteSpace(dir)
                ? name + newExt
                : Path.Combine(dir, name + newExt);

            saveFileDialog1.DefaultExt = rdoMd.Checked ? "md" : "txt";
            saveFileDialog1.Filter = rdoMd.Checked
                ? "Markdown Files|*.md|All Files|*.*"
                : "Text Files|*.txt|All Files|*.*";
        }

        // ─── تغییر حالت ورودی (پوشه / Drop) ─────────────────────────────────
        private void rdoInputMode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputModeUI();
        }

        private void UpdateInputModeUI()
        {
            bool isFolderMode = rdoInputFolder.Checked;

            panelFolderMode.Visible = isFolderMode;
            lblDropHere.Visible = !isFolderMode;
            lstDroppedFiles.Visible = !isFolderMode;
            lblDroppedCount.Visible = !isFolderMode;
            btnClearDropList.Visible = !isFolderMode;

            // در حالت Drop جستجوی زیرپوشه معنی ندارد
            chkSearchSubfolders.Enabled = isFolderMode;
        }

        // ─── Drag & Drop روی lblDropHere ─────────────────────────────────────
        private void lblDropHere_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                lblDropHere.BackColor = System.Drawing.Color.FromArgb(20, 60, 100);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lblDropHere_DragDrop(object sender, DragEventArgs e)
        {
            lblDropHere.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);

            var dropped = (string[])e.Data!.GetData(DataFormats.FileDrop)!;

            // فقط فایل قبول می‌کنیم (نه پوشه) و تکراری نباشه
            var newFiles = dropped
                .Where(p => File.Exists(p))
                .Where(p => !_droppedFiles.Contains(p))
                .ToList();

            _droppedFiles.AddRange(newFiles);

            lstDroppedFiles.BeginUpdate();
            foreach (var f in newFiles)
                lstDroppedFiles.Items.Add(Path.GetFileName(f) + "  —  " + Path.GetDirectoryName(f));
            lstDroppedFiles.EndUpdate();

            UpdateDroppedCount();
            LogMessage($"✚ {newFiles.Count} فایل اضافه شد. جمع: {_droppedFiles.Count} فایل");
        }

        private void btnClearDropList_Click(object sender, EventArgs e)
        {
            _droppedFiles.Clear();
            lstDroppedFiles.Items.Clear();
            UpdateDroppedCount();
            LogMessage("🗑 لیست فایل‌های Drop شده پاک شد.");
        }

        private void UpdateDroppedCount()
        {
            lblDroppedCount.Text = $"{_droppedFiles.Count} فایل اضافه شده";
        }

        // ─── پیدا کردن مشترک‌ترین پوشه بین فایل‌های Drop شده ───────────────
        private string GetCommonRootPath(List<string> files)
        {
            if (files.Count == 0)
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (files.Count == 1)
                return Path.GetDirectoryName(files[0])!;

            var allParts = files
                .Select(f => Path.GetDirectoryName(f)!
                    .Split(Path.DirectorySeparatorChar))
                .ToList();

            var common = allParts[0].ToList();
            foreach (var parts in allParts.Skip(1))
            {
                int matchLen = 0;
                while (matchLen < common.Count
                       && matchLen < parts.Length
                       && common[matchLen] == parts[matchLen])
                    matchLen++;
                common = common.Take(matchLen).ToList();
            }

            if (common.Count == 0)
                return Path.GetDirectoryName(files[0])!;

            return string.Join(Path.DirectorySeparatorChar.ToString(), common);
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
                RefreshFolderFileList();   // ← لیست رو پر کن
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = txtOutputPath.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                txtOutputPath.Text = saveFileDialog1.FileName;
        }

        // ─── پارس چند پسوند ──────────────────────────────────────────────────
        private List<string> ParseExtensions(string input)
        {
            return input
                .Split(new char[] { ';', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ext =>
                {
                    ext = ext.Trim();
                    if (!ext.StartsWith(".")) ext = "." + ext;
                    return ext.ToLower();
                })
                .Distinct()
                .ToList();
        }

        private List<string> GetFilesByExtensions(string folderPath, List<string> extensions, SearchOption searchOption)
        {
            var result = new List<string>();
            foreach (var ext in extensions)
                result.AddRange(Directory.GetFiles(folderPath, "*" + ext, searchOption));
            return result.Distinct().OrderBy(f => f).ToList();
        }

        // ─── تشخیص زبان فایل برای code fence در MD ───────────────────────────
        private string GetLanguageHint(string filePath)
        {
            return Path.GetExtension(filePath).ToLower() switch
            {
                ".cs" => "csharp",
                ".cpp" => "cpp",
                ".c" => "c",
                ".h" => "cpp",
                ".py" => "python",
                ".js" => "javascript",
                ".ts" => "typescript",
                ".java" => "java",
                ".go" => "go",
                ".rs" => "rust",
                ".php" => "php",
                ".rb" => "ruby",
                ".swift" => "swift",
                ".kt" => "kotlin",
                ".xml" => "xml",
                ".json" => "json",
                ".yaml" => "yaml",
                ".yml" => "yaml",
                ".html" => "html",
                ".css" => "css",
                ".sql" => "sql",
                ".sh" => "bash",
                ".bat" => "batch",
                ".ps1" => "powershell",
                ".md" => "markdown",
                _ => ""
            };
        }

        // ─── کپی محتوا به Clipboard ──────────────────────────────────────────
        private void CopyToClipboardFromMemory(string content)
        {
            try
            {
                Action copyAction = () =>
                {
                    Clipboard.Clear();
                    var dataObj = new DataObject();
                    dataObj.SetData(DataFormats.UnicodeText, true, content);
                    dataObj.SetData(DataFormats.Text, true, content);
                    Clipboard.SetDataObject(dataObj, true);
                };

                if (this.InvokeRequired)
                    this.Invoke(copyAction);
                else
                    copyAction();

                LogMessage("📋 محتوا در Clipboard کپی شد!");
            }
            catch (Exception ex)
            {
                LogMessage($"  ⚠ خطا در کپی محتوا: {ex.Message}");
            }
        }

        // ─── کپی فایل به Clipboard (مثل راست‌کلیک → Copy) ───────────────────
        private void CopyFileToClipboard(string filePath)
        {
            try
            {
                Action copyAction = () =>
                {
                    var fileList = new System.Collections.Specialized.StringCollection();
                    fileList.Add(filePath);
                    Clipboard.Clear();
                    Clipboard.SetFileDropList(fileList);
                };

                if (this.InvokeRequired)
                    this.Invoke(copyAction);
                else
                    copyAction();

                LogMessage("📋 فایل در Clipboard کپی شد — می‌توانید Paste کنید!");
            }
            catch (Exception ex)
            {
                LogMessage($"  ⚠ خطا در کپی فایل: {ex.Message}");
            }
        }

        // ─── متد مرکزی کپی — بر اساس انتخاب ComboBox ────────────────────────
        private void HandleClipboard(string outputPath, string memoryContent)
        {
            if (CurrentClipboardMode == ClipboardMode.CopyFile)
                CopyFileToClipboard(outputPath);
            else
                CopyToClipboardFromMemory(memoryContent);
        }

        // ─── دکمه ترکیب ──────────────────────────────────────────────────────
        private void btnCombine_Click(object sender, EventArgs e)
        {
            // اعتبارسنجی مشترک
            if (string.IsNullOrWhiteSpace(txtExtension.Text))
            { MessageBox.Show("لطفاً حداقل یک پسوند فایل را وارد کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            { MessageBox.Show("لطفاً نام فایل خروجی را وارد کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // ─── جمع‌آوری فایل‌ها بر اساس حالت انتخابی ──────────────────────
            List<string> allFiles;
            string folderPath;
            var extensions = ParseExtensions(txtExtension.Text);
            string extensionDisplay = string.Join(", ", extensions);

            if (rdoInputFolder.Checked)
            {
                // حالت ۱: مسیر پوشه
                if (string.IsNullOrWhiteSpace(txtFolderPath.Text))
                { MessageBox.Show("لطفاً پوشه را انتخاب کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (!Directory.Exists(txtFolderPath.Text))
                { MessageBox.Show("پوشه مورد نظر یافت نشد!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                folderPath = txtFolderPath.Text;
                bool searchSubfolders = chkSearchSubfolders.Checked;
                SearchOption searchOption = searchSubfolders
                    ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                allFiles = GetFilesByExtensions(folderPath, extensions, searchOption);
            }
            else
            {
                // حالت ۲: فایل‌های Drop شده
                if (_droppedFiles.Count == 0)
                {
                    MessageBox.Show("لیست فایل‌های Drop شده خالی است!\nابتدا فایل‌ها را بکشید و رها کنید.",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                allFiles = _droppedFiles
                    .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                    .Distinct()
                    .OrderBy(f => f)
                    .ToList();

                if (allFiles.Count == 0)
                {
                    MessageBox.Show(
                        $"هیچ فایلی با پسوندهای [{extensionDisplay}] در لیست Drop شده یافت نشد!\n\nفایل‌های موجود در لیست: {_droppedFiles.Count} عدد",
                        "اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                folderPath = GetCommonRootPath(allFiles);
            }

            if (allFiles.Count == 0)
            {
                MessageBox.Show($"هیچ فایلی با پسوندهای [{extensionDisplay}] یافت نشد!",
                    "اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var selectionForm = new FileSelectionForm(folderPath, allFiles))
                {
                    if (selectionForm.ShowDialog() != DialogResult.OK) return;

                    var selectedFiles = selectionForm.SelectedFiles;
                    if (selectedFiles == null || selectedFiles.Count == 0) return;

                    btnCombine.Enabled = false;
                    btnSaveTree.Enabled = false;
                    txtLog.Clear();
                    progressBar1.Value = 0;

                    string outputPath = txtOutputPath.Text;
                    int partCount = (int)numPartCount.Value;
                    OutputFormat fmt = CurrentFormat;

                    if (!Path.IsPathRooted(outputPath))
                        outputPath = Path.Combine(folderPath, outputPath);

                    LogMessage("شروع عملیات...");
                    LogMessage($"حالت ورودی: {(rdoInputFolder.Checked ? "مسیر پوشه" : "فایل‌های Drop شده")}");
                    LogMessage($"پوشه پایه: {folderPath}");
                    LogMessage($"پسوندها: {extensionDisplay}");
                    LogMessage($"فرمت خروجی: {(fmt == OutputFormat.Md ? "Markdown (.md)" : "Text (.txt)")}");
                    LogMessage($"حالت Clipboard: {cmbClipboardMode.SelectedItem}");
                    LogMessage($"تعداد فایل‌های انتخاب شده: {selectedFiles.Count} از {allFiles.Count}");
                    LogMessage($"تعداد قسمت‌های خروجی: {partCount}");
                    LogMessage("");

                    if (partCount == 1)
                        CombineFiles(folderPath, extensionDisplay, outputPath, selectedFiles, fmt);
                    else
                        CombineFilesInParts(folderPath, extensionDisplay, outputPath, selectedFiles, partCount, fmt);

                    progressBar1.Value = 100;
                    LogMessage("");
                    LogMessage("✓ عملیات با موفقیت انجام شد!");

                    string clipMsg = CurrentClipboardMode == ClipboardMode.CopyFile
                        ? "📋 فایل نهایی در Clipboard کپی شد!"
                        : "📋 محتوای فایل در Clipboard کپی شد!";

                    MessageBox.Show(
                        $"فایل‌ها با موفقیت ترکیب شدند!\n\nتعداد: {selectedFiles.Count} فایل\nتعداد قسمت‌ها: {partCount}\nفرمت: {(fmt == OutputFormat.Md ? "Markdown" : "Text")}\n\n{clipMsg}",
                        "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("آیا می‌خواهید پوشه خروجی را باز کنید؟", "سوال",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(outputPath)!);

                    btnCombine.Enabled = true;
                    btnSaveTree.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogMessage($"\n✗ خطا: {ex.Message}");
                MessageBox.Show($"خطا در انجام عملیات:\n\n{ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCombine.Enabled = true;
                btnSaveTree.Enabled = true;
            }
        }

        // ─── ذخیره درخت فایل‌ها ──────────────────────────────────────────────
        private void btnSaveTree_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolderPath.Text))
            { MessageBox.Show("لطفاً ابتدا پوشه را انتخاب کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!Directory.Exists(txtFolderPath.Text))
            { MessageBox.Show("پوشه مورد نظر یافت نشد!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "ذخیره درخت فایل‌ها";
                sfd.Filter = "Text Files|*.txt|All Files|*.*";
                sfd.DefaultExt = "txt";
                sfd.FileName = "file_tree.txt";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    btnSaveTree.Enabled = false;
                    btnCombine.Enabled = false;
                    txtLog.Clear();

                    string folderPath = txtFolderPath.Text;
                    bool searchSubfolders = chkSearchSubfolders.Checked;
                    SearchOption searchOption = searchSubfolders
                        ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                    LogMessage("در حال ساخت درخت فایل‌ها...");
                    Application.DoEvents();

                    var allFiles = Directory.GetFiles(folderPath, "*.*", searchOption)
                                            .OrderBy(f => f).ToList();
                    LogMessage($"تعداد کل فایل‌ها: {allFiles.Count}");
                    Application.DoEvents();

                    using (var writer = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine("//================================================================================");
                        writer.WriteLine("//  FILE TREE REPORT");
                        writer.WriteLine("//================================================================================");
                        writer.WriteLine($"//  Generated : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                        writer.WriteLine($"//  Root      : {folderPath}");
                        writer.WriteLine($"//  Total     : {allFiles.Count} file(s)");
                        writer.WriteLine("//================================================================================");
                        writer.WriteLine();
                        WriteFullDirectoryTree(writer, folderPath, searchSubfolders);
                        writer.WriteLine();
                        writer.WriteLine("//================================================================================");
                        writer.WriteLine($"//  End of Tree — {allFiles.Count} file(s) found");
                        writer.WriteLine("//================================================================================");
                    }

                    LogMessage("✓ درخت فایل‌ها با موفقیت ذخیره شد!");
                    LogMessage($"مسیر: {sfd.FileName}");

                    MessageBox.Show(
                        $"درخت فایل‌ها با موفقیت ذخیره شد!\n\nتعداد فایل‌ها: {allFiles.Count}\nمسیر: {sfd.FileName}",
                        "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("آیا می‌خواهید فایل خروجی را باز کنید؟", "سوال",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        { FileName = sfd.FileName, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    LogMessage($"\n✗ خطا: {ex.Message}");
                    MessageBox.Show($"خطا:\n\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnSaveTree.Enabled = true;
                    btnCombine.Enabled = true;
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  COMBINE — یک فایل خروجی
        // ═══════════════════════════════════════════════════════════════════════
        private void CombineFiles(string folderPath, string extensionDisplay, string outputPath,
            List<string> selectedFiles, OutputFormat fmt)
        {
            LogMessage("در حال آماده‌سازی فایل‌ها...");
            Application.DoEvents();

            var files = selectedFiles.OrderBy(f => f).ToList();
            var folders = files.Select(f => Path.GetDirectoryName(f)!)
                               .Distinct().OrderBy(f => f).ToList();

            LogMessage($"تعداد {files.Count} فایل انتخاب شده — در {folders.Count} پوشه");
            LogMessage("");

            if (fmt == OutputFormat.Md)
                WriteMdFile(folderPath, extensionDisplay, outputPath, files, folders, 1, 1, true);
            else
                WriteTxtFile(folderPath, extensionDisplay, outputPath, files, folders, 1, 1, true);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  COMBINE — چند قسمت
        // ═══════════════════════════════════════════════════════════════════════
        private void CombineFilesInParts(string folderPath, string extensionDisplay, string outputPath,
            List<string> selectedFiles, int partCount, OutputFormat fmt)
        {
            LogMessage("در حال آماده‌سازی فایل‌ها...");
            Application.DoEvents();

            var files = selectedFiles.OrderBy(f => f).ToList();
            long totalSize = files.Sum(f => new FileInfo(f).Length);
            long targetSizePerPart = totalSize / partCount;

            LogMessage($"حجم کل: {FormatFileSize(totalSize)}");
            LogMessage($"حجم تقریبی هر قسمت: {FormatFileSize(targetSizePerPart)}");
            LogMessage("");

            var parts = new List<List<string>>();
            var currentPart = new List<string>();
            long currentSize = 0;

            foreach (var file in files)
            {
                long fileSize = new FileInfo(file).Length;
                if (currentSize + fileSize > targetSizePerPart
                    && currentPart.Count > 0
                    && parts.Count < partCount - 1)
                {
                    parts.Add(currentPart);
                    currentPart = new List<string>();
                    currentSize = 0;
                }
                currentPart.Add(file);
                currentSize += fileSize;
            }
            if (currentPart.Count > 0) parts.Add(currentPart);

            LogMessage($"فایل‌ها به {parts.Count} قسمت تقسیم شدند:");
            for (int i = 0; i < parts.Count; i++)
            {
                long ps = parts[i].Sum(f => new FileInfo(f).Length);
                LogMessage($"  قسمت {i + 1}: {parts[i].Count} فایل ({FormatFileSize(ps)})");
            }
            LogMessage("");

            string baseDir = Path.GetDirectoryName(outputPath)!;
            string baseName = Path.GetFileNameWithoutExtension(outputPath);
            string baseExt = Path.GetExtension(outputPath);

            for (int pi = 0; pi < parts.Count; pi++)
            {
                string partPath = Path.Combine(baseDir, $"{baseName}_{pi + 1}{baseExt}");
                LogMessage($"در حال ایجاد قسمت {pi + 1} از {parts.Count}...");

                var partFolders = parts[pi]
                    .Select(f => Path.GetDirectoryName(f)!)
                    .Distinct().OrderBy(f => f).ToList();

                if (fmt == OutputFormat.Md)
                    WriteMdFile(folderPath, extensionDisplay, partPath, parts[pi],
                        partFolders, pi + 1, parts.Count, pi == 0);
                else
                    WriteTxtFile(folderPath, extensionDisplay, partPath, parts[pi],
                        partFolders, pi + 1, parts.Count, pi == 0);

                progressBar1.Value = (int)((pi + 1) * 100.0 / parts.Count);
                Application.DoEvents();
                LogMessage($"✓ قسمت {pi + 1} با موفقیت ایجاد شد");
                LogMessage("");
            }

            LogMessage($"حجم کل فایل‌ها: {FormatFileSize(totalSize)}");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  نوشتن فایل TXT
        // ═══════════════════════════════════════════════════════════════════════
        private void WriteTxtFile(string folderPath, string extensionDisplay, string outputPath,
            List<string> files, List<string> folders,
            int partNumber, int totalParts, bool includeHeader)
        {
            long totalSize = 0;
            var memoryContent = new StringBuilder();

            using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                void W(string line) { writer.WriteLine(line); memoryContent.AppendLine(line); }
                void WRaw(string text) { writer.Write(text); memoryContent.Append(text); }

                if (includeHeader && chkHaderSumury.Checked)
                {
                    W("//################################################################################");
                    W("//#                          COMBINED FILES REPORT                               #");
                    W("//################################################################################");
                    W($"//Generated      : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    W($"//Source Folder  : {folderPath}");
                    W($"//File Extensions: {extensionDisplay}");
                    W($"//Total Files    : {files.Count}");
                    W($"//Total Folders  : {folders.Count}");
                    if (totalParts > 1) W($"//Part           : {partNumber} of {totalParts}");
                    W("//################################################################################");
                    W("");
                }
                else if (!includeHeader && chkHaderSumury.Checked && totalParts > 1)
                {
                    W("//#############################################################################");
                    W($"//# Part {partNumber} of {totalParts} - Continuation");
                    W("//#############################################################################");
                    W("");
                }

                if (includeHeader && chkTree.Checked)
                {
                    W("//================================================================================");
                    W("//📁 FOLDER & FILE STRUCTURE");
                    W("//================================================================================");
                    W("");
                    var treeSb = new StringBuilder();
                    using (var treeSw = new StringWriter(treeSb))
                        WriteFileStructure(treeSw, folderPath, files);
                    string treeText = treeSb.ToString();
                    writer.Write(treeText);
                    memoryContent.Append(treeText);
                    W("");
                }

                if (chkHaderSumury.Checked)
                {
                    W("//################################################################################");
                    W("//#                              FILE CONTENTS                                   #");
                    W("//################################################################################");
                    W("");
                }

                W("");

                for (int i = 0; i < files.Count; i++)
                {
                    string filePath = files[i];
                    string relativePath = Path.GetRelativePath(folderPath, filePath);
                    totalSize += new FileInfo(filePath).Length;

                    LogMessage($"[{i + 1}/{files.Count}] {relativePath}");
                    if (totalParts == 1)
                        progressBar1.Value = (int)((i + 1) * 100.0 / files.Count);
                    Application.DoEvents();

                    W("//================================================================================");
                    W($"//Relative Path: {relativePath}");
                    W("//================================================================================");
                    W("");

                    try
                    {
                        string content = File.ReadAllText(filePath, Encoding.UTF8);
                        WRaw(content);
                        if (!content.EndsWith("\n") && !content.EndsWith("\r\n"))
                            W("");
                    }
                    catch (Exception ex)
                    {
                        W($"[ERROR: Unable to read file - {ex.Message}]");
                        LogMessage($"  ⚠ خطا در خواندن: {ex.Message}");
                    }

                    W("");
                    W("");
                }

                if (includeHeader && chkHaderSumury.Checked)
                {
                    W("//################################################################################");
                    W("//#                                  SUMMARY                                     #");
                    W("//################################################################################");
                    W($"//Total Files Processed : {files.Count}");
                    W($"//Total Folders         : {folders.Count}");
                    W($"//Total Size            : {FormatFileSize(totalSize)}");
                    W($"//Extension Filter      : {extensionDisplay}");
                    W($"//Source Folder         : {folderPath}");
                    W($"//Output File           : {outputPath}");
                    W($"//Generated             : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    W("//################################################################################");
                    W("");
                    W("//List of all processed folders:");
                    W("//--------------------------------------------------------------------------------");
                    foreach (var folder in folders)
                    {
                        int fc = files.Count(f => Path.GetDirectoryName(f) == folder);
                        string relFolder = Path.GetRelativePath(folderPath, folder);
                        if (relFolder == ".") relFolder = "[Root]";
                        W($"  [{fc,3} files] {relFolder}");
                    }
                    W("//################################################################################");
                }

            } // ✅ writer بسته شد

            LogMessage($"حجم قسمت {partNumber}: {FormatFileSize(totalSize)}");

            if (partNumber == totalParts)
                HandleClipboard(outputPath, memoryContent.ToString());
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  نوشتن فایل MD
        // ═══════════════════════════════════════════════════════════════════════
        private void WriteMdFile(string folderPath, string extensionDisplay, string outputPath,
            List<string> files, List<string> folders,
            int partNumber, int totalParts, bool includeHeader)
        {
            long totalSize = 0;
            var memoryContent = new StringBuilder();

            using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                void W(string line) { writer.WriteLine(line); memoryContent.AppendLine(line); }
                void WRaw(string text) { writer.Write(text); memoryContent.Append(text); }

                if (includeHeader && chkHaderSumury.Checked)
                {
                    W("# Combined Files Report");
                    W("");
                    W("| Key | Value |");
                    W("|-----|-------|");
                    W($"| Generated | {DateTime.Now:yyyy-MM-dd HH:mm:ss} |");
                    W($"| Source Folder | `{folderPath}` |");
                    W($"| Extensions | `{extensionDisplay}` |");
                    W($"| Total Files | {files.Count} |");
                    W($"| Total Folders | {folders.Count} |");
                    if (totalParts > 1)
                        W($"| Part | {partNumber} of {totalParts} |");
                    W("");
                    W("---");
                    W("");
                }
                else if (!includeHeader && chkHaderSumury.Checked && totalParts > 1)
                {
                    W($"# Part {partNumber} of {totalParts} — Continuation");
                    W("");
                    W("---");
                    W("");
                }

                if (includeHeader && chkTree.Checked)
                {
                    W("## 📁 Folder & File Structure");
                    W("");
                    W("```");
                    var treeSb = new StringBuilder();
                    using (var treeSw = new StringWriter(treeSb))
                        WriteFileStructure(treeSw, folderPath, files);
                    string treeText = treeSb.ToString();
                    writer.Write(treeText);
                    memoryContent.Append(treeText);
                    W("```");
                    W("");
                    W("---");
                    W("");
                }

                if (chkHaderSumury.Checked)
                {
                    W("## 📄 File Contents");
                    W("");
                }

                for (int i = 0; i < files.Count; i++)
                {
                    string filePath = files[i];
                    string relativePath = Path.GetRelativePath(folderPath, filePath);
                    totalSize += new FileInfo(filePath).Length;

                    LogMessage($"[{i + 1}/{files.Count}] {relativePath}");
                    if (totalParts == 1)
                        progressBar1.Value = (int)((i + 1) * 100.0 / files.Count);
                    Application.DoEvents();

                    W($"* `{relativePath}`");
                    W("");

                    string lang = GetLanguageHint(filePath);
                    W($"```{lang}");

                    try
                    {
                        string content = File.ReadAllText(filePath, Encoding.UTF8);
                        WRaw(content);
                        if (!content.EndsWith("\n") && !content.EndsWith("\r\n"))
                            W("");
                    }
                    catch (Exception ex)
                    {
                        W($"ERROR: Unable to read file — {ex.Message}");
                        LogMessage($"  ⚠ خطا در خواندن: {ex.Message}");
                    }

                    W("```");
                    W("");
                }

                if (includeHeader && chkHaderSumury.Checked)
                {
                    W("---");
                    W("");
                    W("## 📊 Summary");
                    W("");
                    W("| Key | Value |");
                    W("|-----|-------|");
                    W($"| Total Files Processed | {files.Count} |");
                    W($"| Total Folders | {folders.Count} |");
                    W($"| Total Size | {FormatFileSize(totalSize)} |");
                    W($"| Extension Filter | `{extensionDisplay}` |");
                    W($"| Source Folder | `{folderPath}` |");
                    W($"| Output File | `{outputPath}` |");
                    W($"| Generated | {DateTime.Now:yyyy-MM-dd HH:mm:ss} |");
                    W("");
                    W("### Folders processed");
                    W("");
                    foreach (var folder in folders)
                    {
                        int fc = files.Count(f => Path.GetDirectoryName(f) == folder);
                        string relFolder = Path.GetRelativePath(folderPath, folder);
                        if (relFolder == ".") relFolder = "[Root]";
                        W($"- `{relFolder}` — {fc} file(s)");
                    }
                    W("");
                }

            } // ✅ writer بسته شد

            LogMessage($"حجم قسمت {partNumber}: {FormatFileSize(totalSize)}");

            if (partNumber == totalParts)
                HandleClipboard(outputPath, memoryContent.ToString());
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  درخت فایل‌ها — TextWriter قبول می‌کنه (StreamWriter و StringWriter)
        // ═══════════════════════════════════════════════════════════════════════
        private void WriteFullDirectoryTree(TextWriter writer, string rootPath, bool recursive)
        {
            writer.WriteLine($"📁 {Path.GetFileName(rootPath)}/  (Root)");
            WriteDirectoryNode(writer, rootPath, rootPath, "", recursive);
        }

        private void WriteDirectoryNode(TextWriter writer, string dirPath, string rootPath,
            string indent, bool recursive)
        {
            var subDirs = Directory.GetDirectories(dirPath).OrderBy(d => d).ToArray();
            var files = Directory.GetFiles(dirPath).OrderBy(f => f).ToArray();
            int totalItems = (recursive ? subDirs.Length : 0) + files.Length;
            int index = 0;

            if (recursive)
            {
                foreach (var sub in subDirs)
                {
                    index++;
                    bool isLast = index == totalItems;
                    string prefix = isLast ? "└── " : "├── ";
                    string childIndent = indent + (isLast ? "    " : "│   ");
                    writer.WriteLine($"{indent}{prefix}📁 {Path.GetFileName(sub)}/");
                    WriteDirectoryNode(writer, sub, rootPath, childIndent, recursive);
                }
            }

            foreach (var file in files)
            {
                index++;
                bool isLast = index == totalItems;
                string prefix = isLast ? "└── " : "├── ";
                var fi = new FileInfo(file);
                writer.WriteLine($"{indent}{prefix}📄 {fi.Name}  [{FormatFileSize(fi.Length)}]");
            }
        }

        private void WriteFileStructure(TextWriter writer, string rootPath, List<string> files)
        {
            var tree = BuildFolderTree(rootPath, files);
            writer.WriteLine($"📁 {Path.GetFileName(rootPath)} (Root)");
            writer.WriteLine($"   Relative Path: .");
            writer.WriteLine();
            WriteTreeNode(writer, tree, "", true, rootPath);
        }

        private FolderNode BuildFolderTree(string rootPath, List<string> files)
        {
            var root = new FolderNode { Path = rootPath, Name = Path.GetFileName(rootPath) };

            foreach (var file in files)
            {
                var relativePath = Path.GetRelativePath(rootPath, file);
                var parts = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                var currentNode = root;

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    var folderName = parts[i];
                    var existingFolder = currentNode.SubFolders.FirstOrDefault(f => f.Name == folderName);
                    if (existingFolder == null)
                    {
                        existingFolder = new FolderNode
                        {
                            Name = folderName,
                            Path = System.IO.Path.Combine(currentNode.Path, folderName)
                        };
                        currentNode.SubFolders.Add(existingFolder);
                    }
                    currentNode = existingFolder;
                }

                currentNode.Files.Add(new FileNode
                {
                    Name = Path.GetFileName(file),
                    FullPath = file,
                    RelativePath = relativePath
                });
            }

            return root;
        }

        private void WriteTreeNode(TextWriter writer, FolderNode node, string indent,
            bool isRoot, string rootPath)
        {
            if (!isRoot)
            {
                var relativePath = Path.GetRelativePath(rootPath, node.Path);
                writer.WriteLine($"{indent}📁 {node.Name}");
                writer.WriteLine($"{indent}   Relative Path: {relativePath}");
                if (node.Files.Count > 0 || node.SubFolders.Count > 0)
                    writer.WriteLine($"{indent}   │");
            }

            var allItems = new List<object>();
            allItems.AddRange(node.SubFolders.Cast<object>());
            allItems.AddRange(node.Files.Cast<object>());

            for (int i = 0; i < allItems.Count; i++)
            {
                bool isLast = i == allItems.Count - 1;
                string prefix = isLast ? "└── " : "├── ";
                string childIndent = indent + (isLast ? "    " : "│   ");

                if (allItems[i] is FolderNode folder)
                {
                    writer.Write($"{indent}{prefix}");
                    WriteTreeNode(writer, folder, childIndent, false, rootPath);
                }
                else if (allItems[i] is FileNode file)
                {
                    var fi = new FileInfo(file.FullPath);
                    writer.WriteLine($"{indent}{prefix}📄 {file.Name} ({FormatFileSize(fi.Length)})");
                }
            }

            if (!isRoot && (node.Files.Count > 0 || node.SubFolders.Count > 0))
                writer.WriteLine();
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
            return $"{len:0.##} {sizes[order]}";
        }

        private void LogMessage(string message)
        {
            if (txtLog.Text.Length > 0) txtLog.AppendText(Environment.NewLine);
            txtLog.AppendText(message);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
    }

    public class FolderNode
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<FolderNode> SubFolders { get; set; } = new();
        public List<FileNode> Files { get; set; } = new();
    }

    public class FileNode
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
    }
}

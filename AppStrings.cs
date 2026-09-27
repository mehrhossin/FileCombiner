//================================================================================
// Relative Path: AppStrings.cs
//================================================================================

namespace FileCombiner
{
    public enum AppLanguage { Persian, English }

    public static class AppStrings
    {
        public static AppLanguage Language { get; set; } = AppLanguage.Persian;

        private static string P(string fa, string en)
            => Language == AppLanguage.Persian ? fa : en;

        // ── Form1 ─────────────────────────────────────────────────────────────
        public static string AppTitle => P("مهر (ترکیب‌کننده فایل‌ها) نسخه 1.7", "Mehr (File Combiner) v1.7");

        // GroupBoxes
        public static string GrpInput => P("مسیر ورودی", "Input Source");
        public static string GrpSettings => P("تنظیمات", "Settings");
        public static string GrpOutput => P("فایل خروجی", "Output File");

        // Radio buttons — input mode
        public static string RdoFolder => P("مسیر پوشه", "Folder Path");
        public static string RdoDrop => P("کشیدن فایل‌های سورس", "Drag & Drop Source Files");

        // Labels & buttons — folder panel
        public static string LblFolderPrompt => P("پوشه‌ای که می‌خواهید فایل‌ها را از آن بخوانید:", "Folder to read files from:");
        public static string BtnBrowseFolder => P("انتخاب پوشه...", "Browse Folder...");

        // Drop panel
        public static string LblDropHere => P("📂  فایل‌های سورس را اینجا بکش و ول کن", "📂  Drag & drop source files here");
        public static string BtnClearDrop => P("پاک کردن\nهمه", "Clear\nAll");

        // Settings group
        public static string LblExtension => P("پسوند(ها) — مثال: .cs;.cpp", "Extension(s) — e.g. .cs;.cpp");
        public static string ChkSubfolders => P("جستجو در زیرپوشه‌ها", "Search Subfolders");
        public static string ChkTree => P("نمایش درخت فایل‌ها", "Show File Tree");
        public static string ChkHeaderSummary => P("نمایش هدر و نتایج آخر", "Show Header & Summary");
        public static string LblFormat => P("فرمت خروجی:", "Output Format:");
        public static string LblPartCount => P("تعداد قسمت‌های خروجی (1-10):", "Output Parts Count (1-10):");
        public static string LblClipboardMode => P("حالت Clipboard:", "Clipboard Mode:");

        // Clipboard combo items
        public static string CmbCopyFile => P("کپی فایل نهایی", "Copy Output File");
        public static string CmbCopyContent => P("کپی محتوا", "Copy Content");

        // Output group
        public static string LblOutputName => P("نام فایل خروجی:", "Output File Name:");
        public static string BtnBrowseOutput => P("انتخاب مسیر...", "Browse...");

        // Main buttons
        public static string BtnCombine => P("ترکیب فایل‌ها", "Combine Files");
        public static string BtnSaveTree => P("💾 ذخیره درخت", "💾 Save Tree");

        // Log label
        public static string LblLog => P("گزارش:", "Log:");

        // ── Runtime messages ──────────────────────────────────────────────────
        public static string MsgNoExtension => P("لطفاً حداقل یک پسوند فایل را وارد کنید!",
                                                     "Please enter at least one file extension!");
        public static string MsgNoOutputPath => P("لطفاً نام فایل خروجی را وارد کنید!",
                                                     "Please enter an output file name!");
        public static string MsgNoFolder => P("لطفاً پوشه را انتخاب کنید!",
                                                     "Please select a folder!");
        public static string MsgFolderNotFound => P("پوشه مورد نظر یافت نشد!",
                                                     "The selected folder was not found!");
        public static string MsgDropEmpty => P("لیست فایل‌های Drop شده خالی است!\nابتدا فایل‌ها را بکشید و رها کنید.",
                                                     "The drop list is empty!\nPlease drag and drop files first.");
        public static string MsgNoMatchingFiles(string exts) =>
            P($"هیچ فایلی با پسوندهای [{exts}] یافت نشد!",
              $"No files found with extensions [{exts}]!");
        public static string MsgNoMatchingDropFiles(string exts, int total) =>
            P($"هیچ فایلی با پسوندهای [{exts}] در لیست Drop شده یافت نشد!\n\nفایل‌های موجود در لیست: {total} عدد",
              $"No files with extensions [{exts}] found in the drop list!\n\nFiles currently in list: {total}");

        public static string MsgSuccess(int count, int parts, string fmt, string clipMsg) =>
            P($"فایل‌ها با موفقیت ترکیب شدند!\n\nتعداد: {count} فایل\nتعداد قسمت‌ها: {parts}\nفرمت: {fmt}\n\n{clipMsg}",
              $"Files combined successfully!\n\nCount: {count} files\nParts: {parts}\nFormat: {fmt}\n\n{clipMsg}");

        public static string MsgOpenOutputFolder =>
            P("آیا می‌خواهید پوشه خروجی را باز کنید؟",
              "Would you like to open the output folder?");
        public static string MsgOpenOutputFile =>
            P("آیا می‌خواهید فایل خروجی را باز کنید؟",
              "Would you like to open the output file?");

        public static string MsgTreeSaved(int count, string path) =>
            P($"درخت فایل‌ها با موفقیت ذخیره شد!\n\nتعداد فایل‌ها: {count}\nمسیر: {path}",
              $"File tree saved successfully!\n\nFile count: {count}\nPath: {path}");

        public static string MsgClipFile => P("📋 فایل نهایی در Clipboard کپی شد!", "📋 Output file copied to Clipboard!");
        public static string MsgClipContent => P("📋 محتوای فایل در Clipboard کپی شد!", "📋 File content copied to Clipboard!");

        public static string MsgClipFileErr(string msg) => P($"  ⚠ خطا در کپی فایل: {msg}", $"  ⚠ Error copying file: {msg}");
        public static string MsgClipContentErr(string msg) => P($"  ⚠ خطا در کپی محتوا: {msg}", $"  ⚠ Error copying content: {msg}");

        // Log messages
        public static string LogStart => P("شروع عملیات...", "Starting operation...");
        public static string LogInputMode(bool isFolder) =>
            P($"حالت ورودی: {(isFolder ? "مسیر پوشه" : "فایل‌های Drop شده")}",
              $"Input mode: {(isFolder ? "Folder Path" : "Dropped Files")}");
        public static string LogBaseFolder(string p) => P($"پوشه پایه: {p}", $"Base folder: {p}");
        public static string LogExtensions(string e) => P($"پسوندها: {e}", $"Extensions: {e}");
        public static string LogFormat(string f) => P($"فرمت خروجی: {f}", $"Output format: {f}");
        public static string LogClipMode(string m) => P($"حالت Clipboard: {m}", $"Clipboard mode: {m}");
        public static string LogSelectedFiles(int s, int t) =>
            P($"تعداد فایل‌های انتخاب شده: {s} از {t}",
              $"Selected files: {s} of {t}");
        public static string LogParts(int n) => P($"تعداد قسمت‌های خروجی: {n}", $"Output parts: {n}");
        public static string LogFolderScanned(int n) => P($"📂 {n} فایل در پوشه یافت شد.", $"📂 {n} file(s) found in folder.");
        public static string LogPreparing => P("در حال آماده‌سازی فایل‌ها...", "Preparing files...");
        public static string LogFilesInFolders(int f, int d) =>
            P($"تعداد {f} فایل انتخاب شده — در {d} پوشه",
              $"{f} file(s) selected — in {d} folder(s)");
        public static string LogTotalSize(string s) => P($"حجم کل: {s}", $"Total size: {s}");
        public static string LogPartSize(string s) => P($"حجم تقریبی هر قسمت: {s}", $"Approx. size per part: {s}");
        public static string LogSplitResult(int n) => P($"فایل‌ها به {n} قسمت تقسیم شدند:", $"Files split into {n} part(s):");
        public static string LogPartInfo(int i, int c, string s) =>
            P($"  قسمت {i}: {c} فایل ({s})",
              $"  Part {i}: {c} file(s) ({s})");
        public static string LogCreatingPart(int i, int t) =>
            P($"در حال ایجاد قسمت {i} از {t}...",
              $"Creating part {i} of {t}...");
        public static string LogPartDone(int i) => P($"✓ قسمت {i} با موفقیت ایجاد شد", $"✓ Part {i} created successfully");
        public static string LogTotalSizeFinal(string s) => P($"حجم کل فایل‌ها: {s}", $"Total files size: {s}");
        public static string LogPartSizeFinal(int i, string s) =>
            P($"حجم قسمت {i}: {s}",
              $"Part {i} size: {s}");
        public static string LogDone => P("✓ عملیات با موفقیت انجام شد!", "✓ Operation completed successfully!");
        public static string LogReadError(string m) => P($"  ⚠ خطا در خواندن: {m}", $"  ⚠ Read error: {m}");
        public static string LogError(string m) => P($"\n✗ خطا: {m}", $"\n✗ Error: {m}");
        public static string LogDeletedFiles(int d, int r) =>
            P($"🗑 {d} فایل از لیست حذف شد. باقی‌مانده: {r}",
              $"🗑 {d} file(s) removed from list. Remaining: {r}");
        public static string LogDropAdded(int n, int t) =>
            P($"✚ {n} فایل اضافه شد. جمع: {t} فایل",
              $"✚ {n} file(s) added. Total: {t} file(s)");
        public static string LogDropCleared => P("🗑 لیست فایل‌های Drop شده پاک شد.", "🗑 Drop list cleared.");
        public static string LogBuildingTree => P("در حال ساخت درخت فایل‌ها...", "Building file tree...");
        public static string LogTotalFiles(int n) => P($"تعداد کل فایل‌ها: {n}", $"Total files: {n}");
        public static string LogTreeSaved => P("✓ درخت فایل‌ها با موفقیت ذخیره شد!", "✓ File tree saved successfully!");
        public static string LogTreePath(string p) => P($"مسیر: {p}", $"Path: {p}");

        // Error dialog messages
        public static string ErrTitle => P("خطا", "Error");
        public static string InfoTitle => P("اطلاعات", "Information");
        public static string SuccessTitle => P("موفق", "Success");
        public static string QuestionTitle => P("سوال", "Question");
        public static string WarningTitle => P("هشدار", "Warning");
        public static string ErrOperation(string m) =>
            P($"خطا در انجام عملیات:\n\n{m}",
              $"Operation failed:\n\n{m}");

        // SaveTree dialog
        public static string SaveTreeTitle => P("ذخیره درخت فایل‌ها", "Save File Tree");
        public static string SaveTreeFileName => P("file_tree.txt", "file_tree.txt");

        // ── FileSelectionForm ─────────────────────────────────────────────────
        public static string FsfTitle => P("انتخاب فایل‌ها", "Select Files");
        public static string FsfGrpFiles => P("لیست فایل‌ها", "File List");
        public static string FsfGrpFilter => P("فیلتر", "Filter");
        public static string FsfLblSearch => P("جستجو (نام فایل یا مسیر):", "Search (file name or path):");
        public static string FsfBtnSelectAll => P("انتخاب همه", "Select All");
        public static string FsfBtnDeselectAll => P("حذف انتخاب همه", "Deselect All");
        public static string FsfBtnOK => P("ادامه", "Continue");
        public static string FsfBtnCancel => P("انصراف", "Cancel");
        public static string FsfLblTotal(int n) => P($"تعداد کل: {n} فایل", $"Total: {n} file(s)");
        public static string FsfLblSelected(int n, string s) =>
            P($"انتخاب شده: {n} فایل ({s})",
              $"Selected: {n} file(s) ({s})");
        public static string FsfMsgSelectOne => P("لطفاً حداقل یک فایل را انتخاب کنید!", "Please select at least one file!");

        // Drop count label
        public static string LblDropCount(int n) => P($"{n} فایل اضافه شده", $"{n} file(s) added");
    }
}

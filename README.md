# 📦 Mehr File Combiner — v1.7

A lightweight Windows desktop tool that merges multiple source files into a
single (or multi-part) output file — perfect for feeding large codebases into
AI assistants like ChatGPT or Claude.

---

## ✨ Features

- 📁 Two input modes: **Folder Path** or **Drag & Drop**
- 🌳 Optional file-tree structure in output
- 📄 Output formats: **TXT** or **Markdown (.md)**
- ✂️ Split output into up to **10 parts**
- 📋 Clipboard support: copy file or copy content
- 🔍 File selection with live search & persistent state
- 🌐 **Bilingual UI**: Persian (فارسی) / English toggle

---

## 🖥️ Requirements

- Windows 10 / 11
- .NET 6.0 or later (Windows Desktop Runtime)

---

## 🚀 Getting Started

1. Run `FileCombiner.exe`
2. Choose your **input mode** (top of the window):
   - **Folder Path** — browse to a folder; files are listed automatically
   - **Drag & Drop** — drag source files directly onto the drop zone
3. Set your **file extension(s)** in the Settings box  
   *(e.g. `.cs` or `.cs;.cpp;.py`)*
4. Choose your **output file** name/path
5. Click **Combine Files** → select which files to include → click **Continue**
6. The combined file is saved and optionally copied to clipboard

---

## ⚙️ Settings Reference

| Setting | Description |
|---|---|
| **Extension(s)** | Filter files by extension. Separate multiple with `;` |
| **Search Subfolders** | Include files in nested subfolders *(Folder mode only)* |
| **Show File Tree** | Prepend a directory tree to the output |
| **Show Header & Summary** | Add metadata header and summary footer |
| **Output Format** | `Text (.txt)` or `Markdown (.md)` |
| **Output Parts** | Split output into 1–10 equal-size parts |
| **Clipboard Mode** | `Copy Output File` — copies the file itself (like right-click → Copy) |
| | `Copy Content` — copies the text content to clipboard |

---

## 🌐 Language Toggle

Click the **`English`** / **`فارسی`** button (top-right action row) to switch
the entire UI language instantly. The setting applies to both windows.

---

## 💾 Save File Tree

Click **💾 Save Tree** to export a standalone directory tree of the selected
folder — no file contents included.  
*(Useful for giving an AI a quick project overview.)*

---

## 📂 File Selection Window

When you click **Combine Files**, a selection window opens:

- **Check / uncheck** individual files
- Use the **search box** to filter by name or path
- **Select All** / **Deselect All** buttons
- Your selection is **saved automatically** per root folder and restored next time

---

## 📋 Clipboard Modes

| Mode | Behavior |
|---|---|
| **Copy Output File** | The output `.txt` / `.md` file is placed on the clipboard — paste it anywhere as a file |
| **Copy Content** | The full text content is placed on the clipboard — paste directly into a chat or editor |

---

## 📁 Output File Format (TXT example)
//################################################################################
//#                          COMBINED FILES REPORT                               #
//################################################################################
//Generated      : 2026-09-27 11:53:00
//Source Folder  : C:\MyProject
//File Extensions: .cs
//Total Files    : 5
//################################################################################

//================================================================================
//Relative Path: Form1.cs
//================================================================================

... file content ...


---

## 🗂️ Settings Storage

Selection state is saved to:
%APPDATA%\MehrCombiner\selection_state.json

This file is created automatically and stores the last checked files per root folder.

---

## 📝 Tips

- For **AI prompts**: use `Copy Content` mode + Markdown format for best results
- For **large projects**: set Parts to `3–5` to stay within AI context limits
- Use **Drag & Drop** mode when you only need specific files across different folders
- The **file tree** section helps AI understand your project structure at a glance

---

## 👤 Author

**Mehr File Combiner** — v1.7  
Built with ❤️ using C# / Windows Forms / .NET 6



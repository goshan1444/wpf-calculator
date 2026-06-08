# 🖩 Fluent WPF Calculator

A sleek, modern, and high-performance dark-themed desktop calculator built with **WPF** and **.NET 10** (C# 14). Designed with inspiration from Windows 11 Fluent Design, Acrylic surfaces, and smooth transitions.

---

## 📸 Screenshots

*(Add your screenshots here to showcase the beautiful UI!)*

<p align="center">
  <img src="https://via.placeholder.com/320x480.png?text=Fluent+WPF+Calculator+Placeholder" alt="Fluent WPF Calculator UI" width="320"/>
</p>

---

## ✨ Features

- **Windows 11 Fluent & Acrylic Style**:
  - Semi-transparent dark linear gradient background mimicking Acrylic materials.
  - Thin, elegant high-contrast gradient borders reflecting light at the top of the window.
  - Soft drop shadow effect for organic floating window depth.
  - 12px rounded window and button corners matching Windows 11 design guidelines.
- **Handcrafted Window Controls**:
  - Full support for standard window dragging by grabbing the customized title bar.
  - Minimalistic, modern Minimize and Close buttons.
  - Close button lights up red on hover, mirroring native OS behavior.
- **High-Fidelity Animations**:
  - Storyboard-powered smooth color transitions (`MouseEnter`/`MouseLeave`) for all buttons.
  - Immediate responsive tactile feedback on pressed states.
- **Robust Math Logic**:
  - Supports arithmetic operations: addition (`+`), subtraction (`-`), multiplication (`*`), and division (`/`).
  - Seamless support for floating-point calculations with cross-culture decimal separator parsing (handles both Russian `,` and Western `.` seamlessly depending on user's OS locale).
  - Safe division-by-zero handler (displays `"Ошибка"` instead of crashing the process).
  - Auto-scaling display: Uses `Viewbox` to dynamically scale font size down when typing long digits, avoiding layout overflow or truncation.
- **Advanced Desktop Input Support**:
  - Full keyboard control mapping including standard digits, numeric pad (NumPad), operators, Enter (`=`), Backspace (`⌫`), and Escape (`C`).
  - **Focus Immunity**: All buttons are non-focusable, preventing mouse clicks from stealing focus. This allows the user to click with the mouse and type with the keyboard in any sequence without losing active window focus!

---

## 🛠️ Tech Stack

- **Framework**: Windows Presentation Foundation (WPF)
- **Runtime**: .NET 10.0 (Windows)
- **Language**: C# 14
- **Style / Layout**: Declarative XAML, Custom ControlTemplates, Storyboards, and Triggers

---

## 🚀 How to Build and Run

### Prerequisites
Make sure you have the **.NET 10 SDK** installed on your Windows machine. You can verify this by running:
```bash
dotnet --version
```

### Run in Development Mode
To compile and launch the application directly from source:
```bash
dotnet run
```

### Build a Single-File Executable (`.exe`)
WPF and .NET 10 allow publishing the entire application as a single standalone executable.

#### Option 1: Framework-Dependent Standalone (Extremely Compact, ~180 KB)
This option creates a single `.exe` file that uses the .NET 10 runtime already installed on the user's machine.
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained false
```
The output file will be generated at:
`bin\Release\net10.0-windows\win-x64\publish\WpfCalculator.exe`

#### Option 2: Fully Self-Contained Single File (~140 MB)
This option packs the entire .NET 10 runtime together with the application. The executable can be run on *any* 64-bit Windows machine, even if they have absolutely no version of .NET installed.
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

## 📜 License

This project is open-source and available under the [MIT License](LICENSE). Feel free to customize and expand it!

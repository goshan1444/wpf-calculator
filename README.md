# 🖩 Fluent WPF Calculator

A sleek, modern, and high-performance desktop calculator built with **WPF** and **.NET 10** (C# 14). Designed with inspiration from Windows 11 Fluent Design, Acrylic surfaces, smooth transitions, and a customizable theme.

---

## 📸 Screenshots

*(Add your screenshots here to showcase the beautiful UI!)*

<p align="center">
  <img src="https://via.placeholder.com/380x550.png?text=Fluent+WPF+Calculator+Placeholder" alt="Fluent WPF Calculator UI" width="380"/>
</p>

---

## ✨ Features

- **Windows 11 Fluent & Acrylic Style**:
  - Semi-transparent linear gradient background mimicking Acrylic materials.
  - Thin, elegant high-contrast gradient borders reflecting light at the top of the window.
  - Soft drop shadow effect for organic floating window depth.
  - 12px rounded window and button corners matching Windows 11 design guidelines.
- **Theme Selection (Light & Dark)**:
  - Supports switching between **Dark Theme** (default) and **Light Theme** with a single click.
  - Theme toggle button (`☀️`/`🌙`) is integrated directly into the custom title bar.
  - Fully dynamic resources update all buttons, colors, gradients, display, and outline colors instantly.
- **Two Calculator Modes (Tabs)**:
  - **Обычный (Standard)**: Quick, clean tab for everyday operations (`+`, `-`, `*`, `/`).
  - **Расширенный (Scientific/Extended)**: Advanced tab containing extra buttons:
    - **Constants**: Pi (`π`), Euler's number (`e`).
    - **Binary Operators**: Power (`^`).
    - **Unary Scientific Functions**: Sine (`sin`), Cosine (`cos`), Tangent (`tan`), Natural Log (`ln`), Common Log (`log`), Square Root (`√`), Square (`x²`), Reciprocal (`1/x`), and Sign Toggle (`±`).
- **Handcrafted Window Controls**:
  - Full support for standard window dragging by grabbing the customized title bar.
  - Minimalistic, modern Minimize and Close buttons.
  - Close button lights up red on hover, mirroring native OS behavior.
- **High-Fidelity Animations & Triggers**:
  - Property trigger-powered smooth transitions (`MouseEnter`/`MouseLeave`) for all buttons.
  - Immediate responsive tactile feedback on pressed states.
- **Robust Math Logic**:
  - Seamless support for floating-point calculations with cross-culture decimal separator parsing (handles both Russian `,` and Western `.` seamlessly depending on user's OS locale).
  - Safe division-by-zero, negative root, and non-positive log input handlers (displays `"Ошибка"` instead of crashing the process).
  - Auto-scaling display: Uses `Viewbox` to dynamically scale font size down when typing long digits, avoiding layout overflow or truncation.
- **Advanced Desktop Input Support**:
  - Full keyboard control mapping including standard digits, numeric pad (NumPad), operators, Enter (`=`), Backspace (`⌫`), and Escape (`C`).
  - Keyboard shortcut `Shift + 6` mapped to exponentiation (`^`).
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

# WPF Calculator

A modern, dark-themed Windows calculator built with **WPF** and **.NET 10** (C# 14), inspired by Windows 11 Fluent Design guidelines.

---

## ✨ Features

- **Fluent UI**: Semi-transparent dark Acrylic-style background, soft drop shadow, and 12px rounded corners.
- **Custom Window Controls**: Borderless design with a customized title bar (supports Drag & Drop) and custom Minimize/Close buttons.
- **Smooth Animations**: Storyboard-based transitions for button hover and active click states.
- **Focus-Immune Input**: Buttons do not steal focus on click, allowing seamless simultaneous use of both mouse and keyboard/NumPad.
- **Robust Math Logic**:
  - Auto-scaling display (via `Viewbox`) to prevent text overflow.
  - Cross-culture decimal support (handles both `,` and `.` depending on system locale).
  - Division-by-zero protection (displays "Ошибка" instead of crashing).

---

## 🛠️ Tech Stack

- **Framework**: WPF (XAML)
- **Runtime**: .NET 10.0 (Windows)
- **Language**: C# 14

---

## 🚀 How to Build and Run

Make sure you have the **.NET 10 SDK** installed.

### Run in Development Mode
```bash
dotnet run
```

### Build a Standalone Executable (`.exe`)

#### Option 1: Lightweight (Uses system .NET 10, ~180 KB)
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained false
```
*Output: `bin\Release\net10.0-windows\win-x64\publish\WpfCalculator.exe`*

#### Option 2: Fully Self-Contained (Runs on any Win64 PC, ~140 MB)
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

## 📜 License

This project is licensed under the [MIT License](LICENSE).

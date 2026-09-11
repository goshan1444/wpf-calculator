using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfCalculator;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private double? operand1 = null;
    private string? pendingOperator = null;
    private bool isNewInput = false;
    private bool isDarkTheme = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void OnMinimizeClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void OnCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnThemeToggleClick(object sender, RoutedEventArgs e)
    {
        ApplyTheme(!isDarkTheme);
    }

    private void ApplyTheme(bool isDark)
    {
        isDarkTheme = isDark;

        var themeToggleIcon = this.FindName("ThemeToggleIcon") as TextBlock;
        if (themeToggleIcon != null)
        {
            themeToggleIcon.Text = isDark ? "☀️" : "🌙";
        }

        if (isDark)
        {
            this.Resources["WindowBackground"] = CreateLinearGradient(ColorFromString("#EA202020"), ColorFromString("#EA161616"));
            this.Resources["WindowBorder"] = CreateLinearGradient(ColorFromString("#40FFFFFF"), ColorFromString("#12FFFFFF"));
            this.Resources["TitleBarTextBrush"] = CreateSolidBrush("#99FFFFFF");
            this.Resources["TitleBarTextActiveBrush"] = CreateSolidBrush("#FFFFFF");
            this.Resources["DisplayTextBrush"] = CreateSolidBrush("#FFFFFF");
            this.Resources["FormulaTextBrush"] = CreateSolidBrush("#90FFFFFF");
            this.Resources["DisplayBackgroundBrush"] = CreateSolidBrush("#06FFFFFF");

            this.Resources["DigitButtonBackground"] = CreateSolidBrush("#2C2C2C");
            this.Resources["DigitButtonHoverBackground"] = CreateSolidBrush("#3C3C3C");
            this.Resources["DigitButtonPressedBackground"] = CreateSolidBrush("#1E1E1E");
            this.Resources["DigitButtonTextBrush"] = CreateSolidBrush("#FFFFFF");
            this.Resources["DigitButtonBorderBrush"] = CreateSolidBrush("#12FFFFFF");

            this.Resources["OperatorButtonBackground"] = CreateSolidBrush("#333333");
            this.Resources["OperatorButtonHoverBackground"] = CreateSolidBrush("#454545");
            this.Resources["OperatorButtonPressedBackground"] = CreateSolidBrush("#222222");
            this.Resources["OperatorButtonTextBrush"] = CreateSolidBrush("#FFFFFF");
            this.Resources["OperatorButtonBorderBrush"] = CreateSolidBrush("#18FFFFFF");

            this.Resources["TabHeaderSelectedBrush"] = CreateSolidBrush("#FFFFFF");
            this.Resources["TabHeaderUnselectedBrush"] = CreateSolidBrush("#99FFFFFF");
        }
        else
        {
            this.Resources["WindowBackground"] = CreateLinearGradient(ColorFromString("#EAF2F2F2"), ColorFromString("#EAE6E6E6"));
            this.Resources["WindowBorder"] = CreateLinearGradient(ColorFromString("#40000000"), ColorFromString("#12000000"));
            this.Resources["TitleBarTextBrush"] = CreateSolidBrush("#99000000");
            this.Resources["TitleBarTextActiveBrush"] = CreateSolidBrush("#000000");
            this.Resources["DisplayTextBrush"] = CreateSolidBrush("#000000");
            this.Resources["FormulaTextBrush"] = CreateSolidBrush("#90000000");
            this.Resources["DisplayBackgroundBrush"] = CreateSolidBrush("#06000000");

            this.Resources["DigitButtonBackground"] = CreateSolidBrush("#FFFFFF");
            this.Resources["DigitButtonHoverBackground"] = CreateSolidBrush("#F5F5F5");
            this.Resources["DigitButtonPressedBackground"] = CreateSolidBrush("#E5E5E5");
            this.Resources["DigitButtonTextBrush"] = CreateSolidBrush("#000000");
            this.Resources["DigitButtonBorderBrush"] = CreateSolidBrush("#12000000");

            this.Resources["OperatorButtonBackground"] = CreateSolidBrush("#F9F9F9");
            this.Resources["OperatorButtonHoverBackground"] = CreateSolidBrush("#F2F2F2");
            this.Resources["OperatorButtonPressedBackground"] = CreateSolidBrush("#E2E2E2");
            this.Resources["OperatorButtonTextBrush"] = CreateSolidBrush("#000000");
            this.Resources["OperatorButtonBorderBrush"] = CreateSolidBrush("#18000000");

            this.Resources["TabHeaderSelectedBrush"] = CreateSolidBrush("#000000");
            this.Resources["TabHeaderUnselectedBrush"] = CreateSolidBrush("#99000000");
        }
    }

    private Color ColorFromString(string hex)
    {
        return (Color)ColorConverter.ConvertFromString(hex);
    }

    private SolidColorBrush CreateSolidBrush(string hex)
    {
        var brush = new SolidColorBrush(ColorFromString(hex));
        brush.Freeze();
        return brush;
    }

    private LinearGradientBrush CreateLinearGradient(Color startColor, Color endColor)
    {
        var brush = new LinearGradientBrush(startColor, endColor, new Point(0, 0), new Point(0, 1));
        brush.Freeze();
        return brush;
    }

    private void OnDigitClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string digit = button.Content?.ToString() ?? "";
            AppendDigit(digit);
        }
    }

    private void AppendDigit(string digit)
    {
        if (DisplayText.Text == "Ошибка" || isNewInput || DisplayText.Text == "0")
        {
            DisplayText.Text = digit;
            isNewInput = false;
        }
        else
        {
            // Limit input to 16 characters to prevent layout issues and scientific notation overflow
            if (DisplayText.Text.Length < 16)
            {
                DisplayText.Text += digit;
            }
        }
    }

    private void OnDecimalClick(object sender, RoutedEventArgs e)
    {
        AppendDecimal();
    }

    private void AppendDecimal()
    {
        if (DisplayText.Text == "Ошибка" || isNewInput)
        {
            DisplayText.Text = "0,";
            isNewInput = false;
            return;
        }

        // Check if display already contains any decimal point (comma or dot)
        if (!DisplayText.Text.Contains(",") && !DisplayText.Text.Contains("."))
        {
            DisplayText.Text += ",";
        }
    }

    private void OnOperatorClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string op = button.Content?.ToString() ?? "";
            ApplyOperator(op);
        }
    }

    private void ApplyOperator(string op)
    {
        if (DisplayText.Text == "Ошибка") return;

        // If an operator is pressed right after another operator, just change the operator
        if (isNewInput && pendingOperator != null)
        {
            pendingOperator = op;
            FormulaText.Text = $"{FormatResult(operand1 ?? 0)} {pendingOperator}";
            return;
        }

        if (operand1 != null && !isNewInput)
        {
            Calculate();
        }
        else
        {
            operand1 = ParseNumber(DisplayText.Text);
        }

        pendingOperator = op;
        FormulaText.Text = $"{FormatResult(operand1 ?? 0)} {pendingOperator}";
        isNewInput = true;
    }

    private void OnEqualClick(object sender, RoutedEventArgs e)
    {
        Calculate();
    }

    private void OnClearClick(object sender, RoutedEventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        DisplayText.Text = "0";
        FormulaText.Text = "";
        operand1 = null;
        pendingOperator = null;
        isNewInput = false;
    }

    private void OnBackspaceClick(object sender, RoutedEventArgs e)
    {
        PerformBackspace();
    }

    private void PerformBackspace()
    {
        if (DisplayText.Text == "Ошибка" || isNewInput)
        {
            DisplayText.Text = "0";
            isNewInput = false;
            return;
        }

        if (DisplayText.Text.Length > 0)
        {
            DisplayText.Text = DisplayText.Text.Substring(0, DisplayText.Text.Length - 1);
            if (DisplayText.Text == "" || DisplayText.Text == "-")
            {
                DisplayText.Text = "0";
            }
        }
    }

    private void OnConstantClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string constant = button.Content?.ToString() ?? "";
            double val = constant == "π" ? Math.PI : Math.E;
            DisplayText.Text = FormatResult(val);
            isNewInput = true;
        }
    }

    private void OnUnaryOperatorClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string op = button.Content?.ToString() ?? "";
            ApplyUnaryOperator(op);
        }
    }

    private void ApplyUnaryOperator(string op)
    {
        if (DisplayText.Text == "Ошибка") return;

        double val = ParseNumber(DisplayText.Text);
        double result = 0;
        string formula = "";

        switch (op)
        {
            case "sin":
                result = Math.Sin(val * Math.PI / 180.0);
                formula = $"sin({FormatResult(val)})";
                break;
            case "cos":
                result = Math.Cos(val * Math.PI / 180.0);
                formula = $"cos({FormatResult(val)})";
                break;
            case "tan":
                result = Math.Tan(val * Math.PI / 180.0);
                formula = $"tan({FormatResult(val)})";
                break;
            case "ln":
                if (val <= 0)
                {
                    DisplayText.Text = "Ошибка";
                    isNewInput = true;
                    return;
                }
                result = Math.Log(val);
                formula = $"ln({FormatResult(val)})";
                break;
            case "log":
                if (val <= 0)
                {
                    DisplayText.Text = "Ошибка";
                    isNewInput = true;
                    return;
                }
                result = Math.Log10(val);
                formula = $"log({FormatResult(val)})";
                break;
            case "√":
                if (val < 0)
                {
                    DisplayText.Text = "Ошибка";
                    isNewInput = true;
                    return;
                }
                result = Math.Sqrt(val);
                formula = $"√({FormatResult(val)})";
                break;
            case "x²":
                result = Math.Pow(val, 2);
                formula = $"sqr({FormatResult(val)})";
                break;
            case "1/x":
                if (val == 0)
                {
                    DisplayText.Text = "Ошибка";
                    isNewInput = true;
                    return;
                }
                result = 1.0 / val;
                formula = $"1/({FormatResult(val)})";
                break;
            case "±":
                result = -val;
                DisplayText.Text = FormatResult(result);
                return;
        }

        DisplayText.Text = FormatResult(result);
        FormulaText.Text = formula;
        isNewInput = true;
    }

    private void Calculate()
    {
        if (string.IsNullOrEmpty(pendingOperator) || operand1 == null) return;

        double secondOperand = ParseNumber(DisplayText.Text);
        double firstOperand = operand1.Value;
        double result = 0;

        switch (pendingOperator)
        {
            case "+":
                result = firstOperand + secondOperand;
                break;
            case "-":
            case "−":
                result = firstOperand - secondOperand;
                break;
            case "*":
            case "×":
                result = firstOperand * secondOperand;
                break;
            case "/":
            case "∕":
            case "÷":
                if (secondOperand == 0)
                {
                    DisplayText.Text = "Ошибка";
                    FormulaText.Text = "";
                    operand1 = null;
                    pendingOperator = null;
                    isNewInput = true;
                    return;
                }
                result = firstOperand / secondOperand;
                break;
            case "^":
                result = Math.Pow(firstOperand, secondOperand);
                break;
        }

        DisplayText.Text = FormatResult(result);
        FormulaText.Text = $"{FormatResult(firstOperand)} {pendingOperator} {FormatResult(secondOperand)} =";
        operand1 = result;
        pendingOperator = null;
        isNewInput = true;
    }

    private double ParseNumber(string text)
    {
        string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        string sanitized = text.Replace(".", separator).Replace(",", separator);
        
        if (double.TryParse(sanitized, NumberStyles.Any, CultureInfo.CurrentCulture, out double result))
        {
            return result;
        }
        return 0;
    }

    private string FormatResult(double val)
    {
        if (double.IsNaN(val) || double.IsInfinity(val))
            return "Ошибка";

        // Limit to 12 significant digits to avoid float precision issues and maintain clean visual
        string formatted = val.ToString("G12", CultureInfo.CurrentCulture);
        string currentSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        
        // Ensure we always use comma for display to match the UI button content ","
        return formatted.Replace(currentSeparator, ",");
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        bool isShift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

        switch (e.Key)
        {
            // Digits (Top Row and Numpad)
            case Key.D0:
            case Key.NumPad0:
                AppendDigit("0");
                e.Handled = true;
                break;
            case Key.D1:
            case Key.NumPad1:
                AppendDigit("1");
                e.Handled = true;
                break;
            case Key.D2:
            case Key.NumPad2:
                AppendDigit("2");
                e.Handled = true;
                break;
            case Key.D3:
            case Key.NumPad3:
                AppendDigit("3");
                e.Handled = true;
                break;
            case Key.D4:
            case Key.NumPad4:
                AppendDigit("4");
                e.Handled = true;
                break;
            case Key.D5:
            case Key.NumPad5:
                AppendDigit("5");
                e.Handled = true;
                break;
            case Key.D6:
                if (isShift)
                    ApplyOperator("^"); // Shift + 6
                else
                    AppendDigit("6");
                e.Handled = true;
                break;
            case Key.NumPad6:
                AppendDigit("6");
                e.Handled = true;
                break;
            case Key.D7:
            case Key.NumPad7:
                AppendDigit("7");
                e.Handled = true;
                break;
            case Key.D8:
            case Key.NumPad8:
                if (isShift)
                    ApplyOperator("×"); // Shift + 8
                else
                    AppendDigit("8");
                e.Handled = true;
                break;
            case Key.D9:
            case Key.NumPad9:
                AppendDigit("9");
                e.Handled = true;
                break;

            // Operators (Numpad)
            case Key.Add:
                ApplyOperator("+");
                e.Handled = true;
                break;
            case Key.Subtract:
                ApplyOperator("−");
                e.Handled = true;
                break;
            case Key.Multiply:
                ApplyOperator("×");
                e.Handled = true;
                break;
            case Key.Divide:
                ApplyOperator("∕");
                e.Handled = true;
                break;

            // Operators (Standard keyboard)
            case Key.OemPlus:
                if (isShift)
                    ApplyOperator("+"); // Shift + '=' is '+'
                else
                    Calculate(); // '='
                e.Handled = true;
                break;
            case Key.OemMinus:
                ApplyOperator("−");
                e.Handled = true;
                break;

            // Division slash on US layout / dot on Russian layout
            case Key.OemQuestion:
                if (isShift)
                {
                    // In some Russian keyboard layouts Shift + OemQuestion might be ',' or '?'
                    // In US layout, Shift + OemQuestion is '?'
                }
                else
                {
                    // US layout: '/'
                    // Russian layout: '.' (decimal point)
                    // Let's decide based on whether Russian layout is likely active.
                    // We can check if the current input locale's separator is a comma.
                    // But to be safe, if we just handle Key.OemQuestion as division (US) and decimal (RU), 
                    // we can do a smart guess or just support both!
                    if (CultureInfo.CurrentCulture.Name.StartsWith("ru"))
                    {
                        AppendDecimal();
                    }
                    else
                    {
                        ApplyOperator("∕");
                    }
                    e.Handled = true;
                }
                break;

            // Decimal Separators
            case Key.Decimal:
            case Key.OemPeriod:
            case Key.OemComma:
                AppendDecimal();
                e.Handled = true;
                break;

            // Equal (Enter)
            case Key.Enter:
                Calculate();
                e.Handled = true;
                break;

            // Backspace
            case Key.Back:
                PerformBackspace();
                e.Handled = true;
                break;

            // Clear (Escape / C)
            case Key.Escape:
                ClearAll();
                e.Handled = true;
                break;
            case Key.C:
                ClearAll();
                e.Handled = true;
                break;
        }
    }
}

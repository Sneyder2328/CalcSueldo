using System;
using Gtk;

public partial class MainWindow : Gtk.Window {

    bool isSueldoBaseValid = false, isCantHijosValid = false;

    public MainWindow() : base(Gtk.WindowType.Toplevel) {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
        Application.Quit();
        a.RetVal = true;
    }

    protected void CalcularNuevoSueldo(object sender, EventArgs e) {
        double nuevoSueldo = Convert.ToDouble(sueldoBaseEntry.Text);
        int cantHijos = Convert.ToInt32(cantHijosEntry.Text);
        if (cantHijos == 1) {
            nuevoSueldo *= 1.2;
        }
        else if (cantHijos == 2) {
            nuevoSueldo *= 1.25;
        }
        else if (cantHijos > 2) {
            nuevoSueldo *= 1.35;
        }
        nuevoSueldoEntry.Text = nuevoSueldo.ToString();
    }

    protected void OnSueldoBaseChanged(object sender, EventArgs e) {
        string sueldoBaseValidadoStr = ValidateStringAsNumber(sueldoBaseEntry.Text, true);
        sueldoBaseEntry.Text = sueldoBaseValidadoStr;
        isSueldoBaseValid = sueldoBaseValidadoStr.Length != 0;
        CheckBtnSensitivity();
    }

    protected void OnCantHijosChanged(object sender, EventArgs e) {
        string cantHijosValidadaStr = ValidateStringAsNumber(cantHijosEntry.Text, false);
        cantHijosEntry.Text = cantHijosValidadaStr;
        isCantHijosValid = cantHijosValidadaStr.Length != 0;
        CheckBtnSensitivity();
    }

    protected string ValidateStringAsNumber(string originalStr, bool allowDecimals) {
        string strValidated = "";
        for (int index = 0; index < originalStr.Length; index++){
            if (char.IsNumber(originalStr[index]) || (allowDecimals && ValidateDecimalSeparator(originalStr, index))) {
                strValidated += originalStr[index];
            }
        }
        return strValidated;
    }

    protected bool ValidateDecimalSeparator(string originalStr, int index) {
        return originalStr[index] == '.' && originalStr.IndexOf('.') == index && index != 0;
    }

    protected void CheckBtnSensitivity() {
        calcularSueldoBtn.Sensitive = isSueldoBaseValid && isCantHijosValid;
    }
}

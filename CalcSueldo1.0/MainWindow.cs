using System;
using Gtk;

public partial class MainWindow : Gtk.Window {

    public MainWindow() : base(Gtk.WindowType.Toplevel) {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
        Application.Quit();
        a.RetVal = true;
    }

    protected void CalcularNuevoSueldo(object sender, EventArgs e) {
        double nuevoSueldo = Convert.ToDouble(sueldoBaseEntry.Text);
        int cantHijos = Convert.ToInt16(cantHijosEntry.Text);
        if (cantHijos == 1) {
            nuevoSueldo *= 1.2;
        } else if (cantHijos == 2) {
            nuevoSueldo *= 1.25;
        } else if (cantHijos > 2) {
            nuevoSueldo *= 1.35;
        }
        nuevoSueldoEntry.Text = nuevoSueldo.ToString();
    }

    protected void OnSueldoBaseChanged(object sender, EventArgs e) {
        ValidarNroDecimal(sueldoBaseEntry);
        ValidarBoton(calcularSueldoBtn, sueldoBaseEntry, cantHijosEntry);
    }

    protected void OnCantHijosChanged(object sender, EventArgs e) {
        ValidarNro(cantHijosEntry);
        ValidarBoton(calcularSueldoBtn, sueldoBaseEntry, cantHijosEntry);
    }

    public void ValidarNro(Entry ent) {
        string newStr = "";
        foreach (char c in ent.Text) {
            if (c >= '0' && c <= '9') 
                newStr += c;
        }
        ent.Text = newStr;
    }

    public void ValidarNroDecimal(Entry ent){
        string newStr = "";
        int cont = 0;
        foreach (char c in ent.Text) {
            if (c >= '0' && c <= '9'){
                newStr += c;
            } else if ((c == '.' || c == ',') && cont == 0 && newStr != "") {
                newStr += ',';
                cont++;
            }
        }
        ent.Text = newStr;
    }

    public void ValidarBoton(Button bton, params Entry[] entries){
        foreach (Entry entry in entries){
            if (string.IsNullOrEmpty(entry.Text)){
                bton.Sensitive = false;
                return;
            }
        }
        bton.Sensitive = true;
    }

    protected void OnSalirClicked(object sender, EventArgs e) {
        Application.Quit();
        this.Destroy();
    }

    protected void OnLimpiar(object sender, EventArgs e) {
        LimpiarEntries(sueldoBaseEntry, cantHijosEntry, nuevoSueldoEntry);
    }

    protected void LimpiarEntries(params Entry[] entries) {
        foreach(Entry entry in entries) {
            entry.Text = "";
        }
    }
}

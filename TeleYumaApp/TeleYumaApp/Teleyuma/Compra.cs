using System;
using System.Collections.Generic;
using System.Text;
using TeleYumaApp.Class;

namespace TeleYumaApp.Teleyuma
{
    public class Compra
    {
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public string LabelProducto
        {
            get
            {
                if (TipoProducto == "movil")
                    return "Número";
                else return "Usuario";
            }
        }
        public string LabelBono
        {
            get
            {
                if (string.IsNullOrEmpty(Bono))
                    return "";
                else return "Bono:";
            }
        }
        public string ProductoIcon
        {
            get
            {
                if (TipoProducto == "movil")
                    return "phone_blue";
                else return "wifi_green";
            }
        }
        public float Monto { get; set; }
        public float Precio { get; set; }
        public string Bono { get; set; }
        public EstadoCompra Estado { get; set; }

    }
}

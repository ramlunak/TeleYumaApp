
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleYumaApp.Class;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TeleYumaApp.App;

namespace TeleYumaApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetail : MasterDetailPage
	{
		public MasterDetail ()
		{
			InitializeComponent ();
            //Detail = new NavigationPage(new Inicio())
            //{
            //    BarBackgroundColor = Color.FromHex("#1648CA"),
            //    BarTextColor = Color.White
            //};
            Detail = new NavigationPage(new Pages.HomeTabbedPage())
            {
                BarBackgroundColor = Color.FromHex("#1648CA"),
                BarTextColor = Color.White
            };

        }

        private void lblCuenta_Tapped(object sender, EventArgs e)
        {
            //var cuenta = new PagesInicio.CrearCuenta();
            //cuenta.CargarDatos();
            //cuenta.Transaction = TipoTransaction.Edit;
            //this.Navigation.PushModalAsync(cuenta);
        }

        private void lblContactos_Tapped(object sender, EventArgs e)
        {
            //this.Navigation.PushModalAsync(_Global.Vistas.ListaContactos);
            //_Global.Vistas.ListaContactos.Transaction = TipoTransaction.New;
            //_Global.Vistas.ListaContactos.LlenarLista();
        }

        private void lblSalir_Tapped(object sender, EventArgs e)
        {
           
        }

        private void LlamarSoporte_Tapped(object sender, EventArgs e)
        {                       
            DependencyService.Get<ICallService>().Call("+"+lblNumeroSoporte.Text.Trim());
        }
    }
}

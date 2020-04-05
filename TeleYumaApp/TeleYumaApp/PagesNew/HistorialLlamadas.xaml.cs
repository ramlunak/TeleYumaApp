using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleYumaApp.Class;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeleYumaApp.PagesNew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialLlamadas : ContentPage
    {
        public HistorialLlamadas()
        {
            InitializeComponent();
            RecargarLista();
        }

        public async Task RecargarLista()
        {
            CargarXDR();
            await Task.Delay(5);
            RecargarLista();
        }

        public async void CargarXDR()
        {
            var GetAccountXDRListResponse = await _Global.CurrentAccount.GetAccountXDR(new GetAccountXDRListRequest { i_service = 3, from_date = _Global.GetDateFormat_YYMMDD(DateTime.Now), to_date = _Global.GetDateFormat_YYMMDD(DateTime.Now, "final") });
            listGistorial.ItemsSource = null;
            listGistorial.ItemsSource = GetAccountXDRListResponse.xdr_list;

        }

        private async void imgBuscar_Tapped(object sender, EventArgs e)
        {
            var desde = _Global.GetDateFormat_YYMMDD(pkrDesde.Date);
            var hasta = _Global.GetDateFormat_YYMMDD(pkrHasta.Date, "final");

            var GetAccountXDRListResponse = await _Global.CurrentAccount.GetAccountXDR(new GetAccountXDRListRequest { from_date = desde, to_date = hasta });

            listGistorial.ItemsSource = null;
            listGistorial.ItemsSource = GetAccountXDRListResponse.xdr_list;

        }

    }
}
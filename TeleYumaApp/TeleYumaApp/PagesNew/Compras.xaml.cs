﻿using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleYumaApp.Class;
using TeleYumaApp.Teleyuma;
using Xamarin.Forms;

namespace TeleYumaApp
{
    public partial class Compras : ContentPage
    {
        public Compras()
        {
            InitializeComponent();
            BindingContext = _Global.VM.VMCompras;
        }


        private void listGrupos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                _Global.VM.VMCompras.SelectedItem = (Compra)e.SelectedItem;
                _Global.VM.VMCompras.popupOpcionesVisible = true;
                
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ;
            try
            {
                _Global.TipoRecarga = "movil";
                this.Navigation.PushAsync(_Global.Vistas.Pagar);
                _Global.Vistas.Pagar.ActualizarInformacionMonto();
            }
            catch (Exception)
            {
                ;

            }
        }

        public async void PayPalPayment()
        {
            var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Monto de las Compras", new Decimal(_Global.ListaRecargas.TotalPagar), "USD"), new Decimal(0));
            if (result.Status == PayPalStatus.Cancelled)
            {
                await DisplayAlert("TeleYuma", "Pago Cancelado", "OK");
            }
            else if (result.Status == PayPalStatus.Error)
            {
                await DisplayAlert("TeleYuma", result.ErrorMessage, "OK");
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                await DisplayAlert("TeleYuma", "Gracias, su pago se realizó correctamente", "OK");

                //Poner dinero en la cuenta manual
                await _Global.CurrentAccount.MakeTransaction_ManualPayment(new Decimal(_Global.ListaRecargas.TotalPagar), "Recargas con PayPal");

                await RecargarMovil();

                //Regrasar a pagina HomePage
                var pages = Navigation.NavigationStack.ToList();
                foreach (var page in pages)
                {
                    if (page.GetType() != typeof(Pages.HomeTabbedPage))
                        Navigation.RemovePage(page);
                }
                //------------------------
            }
        }

        public async Task<bool> RecargarMovil()
        {
            decimal montoRecargaSinError = 0;
            if (_Global.ListaRecargas.Lista.Any())
            {
                // recargar
                foreach (var recarga in _Global.ListaRecargas.Lista)
                {
                    if (_Global.ModoPrueba)
                        await recarga.Simular();
                    else
                        await recarga.Recargar();
                }

                //monto de las recargas echas sin error               
                foreach (var recarga in _Global.ListaRecargas.Lista)
                {
                    if (recarga.topupResponse.error_code == "0")
                    {
                        await _Global.CurrentAccount.MakeTransaction_Manualcharge(Convert.ToDecimal(recarga.precio), "Recarga a " + recarga.numero);
                        montoRecargaSinError += recarga.TotalPagar;
                    }

                }

            }

            if (montoRecargaSinError > 0)
            {
                await DisplayAlert("TeleYuma", "El sistema completó la solicitud", "OK");
            }
            else
            {
                await DisplayAlert("TeleYuma", "El sistema completó la solicitud con errores", "OK");
            }

            return true;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            PayPalPayment();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TeleYumaApp.Class;
using System.Runtime.CompilerServices;

namespace TeleYumaApp.ViewModels
{
    public class VMTarjetaCredito : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<string> PickerData
        {
            get
            {
                var dataCard = new List<string>();
                dataCard.Add("MasterCard");
                dataCard.Add("American Express");
                dataCard.Add("Discover");
                dataCard.Add("VISA");              
                return dataCard;
            }
        }



        private string _cardNumber;

        public string cardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                cardNumber = value;
                OnPropertyChanged();
            }

        }



    }
}

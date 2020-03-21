using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeleYumaApp.rlkControles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class rlkButtonNet : ContentView
    {
        public rlkButtonNet()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
        }
          
    }
}
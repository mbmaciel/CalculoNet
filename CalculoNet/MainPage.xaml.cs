using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculoNet
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Função do botão de cálculo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnCalcular_OnClicked(object sender, EventArgs e)
        {
            var inicial = (Decimal.Parse(txtInicial.Text));
            var final = (Decimal.Parse(txtFinal.Text));
            var dividendo = (Decimal.Parse(txtDividendo.Text));

            FileDownloader File = new FileDownloader();
            string Url = String.Format("https://calculonetapi.azurewebsites.net/api/calculo/{0}/{1}/{2}", inicial, final, dividendo);
            await File.DownloadFile(Url);
            String Dados = File.GetFile;
            
            try
            {
                lblResultado.HeightRequest = 150;
                lblResultado.Text = String.Empty;
                lblResultado.Text = Dados;
            }
            catch (Exception)
            {
                await DisplayAlert("ERRO", "O cálculo não pode ser executado. Por favor preencha todos os campos.", "Ok");
            }
        }

        public class FileDownloader
        {
            public String GetFile { get; private set; }

            public async Task<bool> DownloadFile(string uri)
            {
                try
                {
                    var httpClient = new HttpClient(); // Error CS0246: The type or namespace name `HttpClient' could not be found. Are you missing an assembly reference? (CS0246)
                    this.GetFile = await httpClient.GetStringAsync(uri);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }

                return true;
            }
            
        }
        
        /// <summary>
        /// Limpa os campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            lblResultado.Text = String.Empty;
            txtDividendo.Text = String.Empty;
            txtInicial.Text = String.Empty;
            txtFinal.Text = String.Empty;
        }

    }
}

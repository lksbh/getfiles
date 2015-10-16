using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Net;
using iTextSharp.text.pdf.parser;



namespace getfiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ValorDaPagina;
        int ValorDoPDF;
        
            string valor1;
                string valor2 ;
        public MainWindow()
        {
            InitializeComponent();
            ValorDaPagina = 0;
            ValorDoPDF = 0;
            label1.Content = "!";

        }


        public void carega_valores() 
        {
             valor1 = "http://jornal.iof.mg.gov.br/xmlui/bitstream/handle/123456789/";
             valor2 = "http://jornal.iof.mg.gov.br/xmlui/bitstream/handle/123456789/153240/caderno1_2015-10-15%";
            ValorDaPagina = Convert.ToInt32(CAIXA1.Text.Substring( valor1.Length ,6));
            ValorDoPDF = Convert.ToInt32(CAIXA1.Text.Substring( valor2.Length ,3));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            carega_valores();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
           
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //
                // The user selected a folder and pressed the OK button.
                // We print the number of files found.
                //,

                //verifica e cria a treta da pasta
                if (!Directory.Exists(dialog.SelectedPath)) Directory.CreateDirectory(dialog.SelectedPath);

                //conta os arkivos num diretorio
                label1.Content = dialog.SelectedPath.ToString();
                string[] files = Directory.GetFiles(dialog.SelectedPath);
                System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                // "le a pagina da web ate agora ta uma bosta

                WebRequest req = WebRequest.Create("http://www.iof.mg.gov.br/index.php?/ultima-edicao.html");
                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string resposta = reader.ReadToEnd();
                
                reader.Close();
                res.Close();


                //escreve o arquivo txt diretao
                using (StreamWriter writer = new StreamWriter(dialog.SelectedPath + "\\TESTE.txt"))
                {
                    writer.Write("Lucas Souza .net ");
                    writer.Write(resposta);
                 
                }

                using (var client = new WebClient())
                {
                    //trocar o nome do arkivo pro nome da pagina vai ser util pra carai!!!
                    client.DownloadFile("http://jornal.iof.mg.gov.br/xmlui/bitstream/handle/123456789/" + ValorDaPagina + "/caderno1_2015-10-15%"+ValorDoPDF+".pdf", dialog.SelectedPath + "\\"+ valor2.Substring(valor1.Length+7)+ ".pdf");
                }


            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        public void Geratexto()
        {
            PdfTextExtractor.GetTextFromPage(new Pd);
        }
    }
}

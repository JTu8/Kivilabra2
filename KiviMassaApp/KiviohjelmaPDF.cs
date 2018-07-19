using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KiviMassaApp
{
    class KiviohjelmaPDF
    {
        public void SavePDF(Kiviohjelma kiviOhjelma)
        {
            Kiviohjelma _kivi = kiviOhjelma;

            //Avaa tallennus dialogin, joka tallentaa tiedoston oletuksena PDF-muodossa 
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PDF Document (*.pdf)|*.pdf";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    FileStream fs = (FileStream)fileDialog.OpenFile();

                    fs.Close();

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Virhe");
                }


            }

            //Ottaa syötetyn tiedoston nimen sekä polun ja antaa sen parametrina PDFCreation funktiolle
            string text = fileDialog.FileName;
            Console.WriteLine(text);

            PDFCreation(text, _kivi);

        }




        public void PDFCreation(string fileName, Kiviohjelma kiviohjelma)
        {
            GetValueFromTextBoxes(kiviohjelma);



            PdfDocument document = new PdfDocument();
            document.Info.Title = "Seulonnantulos";

            PdfPage page = document.AddPage();

            XGraphics graphics = XGraphics.FromPdfPage(page);

            //Fonttien määritykset
            XFont font = new XFont("Verdanna", 15, XFontStyle.Regular);
            XFont textFont = new XFont("Verdanna", 10, XFontStyle.Regular);
            XFont otsikkoFont = new XFont("Verdanna", 15, XFontStyle.Bold);
            XFont alaOtsikkoFont = new XFont("Verdanna", 7, XFontStyle.Regular);
            XFont osoiteFont = new XFont("Verdanna", 7, XFontStyle.Regular);

            //Piirtää tarvittavat kuvat, otsikot ja niiden arvot PDF-tiedostoon

            string filePath;
            filePath = ((ComboBoxItem)kiviohjelma.Kuvat.SelectedItem).Tag.ToString(); //Asettaa comboboxista valitun kuvan tagin mukaan
            Console.WriteLine(filePath);

            graphics.DrawImage(XImage.FromFile(filePath), 10, 0, 120, 30);
            graphics.DrawString(alaOtsikkoHolder, alaOtsikkoFont, XBrushes.Black, new XRect(50, 26, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(osoiteHolder, osoiteFont, XBrushes.Black, new XRect(50, 33, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(postiHolder, osoiteFont, XBrushes.Black, new XRect(50, 40, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(puhHolder, osoiteFont, XBrushes.Black, new XRect(50, 48, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawString("KIVIAINESTUTKIMUS", otsikkoFont, XBrushes.Black, new XRect(280, 50, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(seulontaHolder, textFont, XBrushes.Black, new XRect(450, 55, page.Width, page.Height), XStringFormats.TopLeft);


            string leima;
            leima = ((ComboBoxItem)kiviohjelma.leimat.SelectedItem).Tag.ToString();
            Console.WriteLine(leima);
            graphics.DrawString(leima, textFont, XBrushes.Black, new XRect(450, 40, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Työmaa", textFont, XBrushes.Black, new XRect(50, 80, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tyomaaHolder, textFont, XBrushes.Black, new XRect(100, 80, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Lajite", textFont, XBrushes.Black, new XRect(50, 95, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lajiteHolder, textFont, XBrushes.Black, new XRect(100, 95, page.Width, page.Height), XStringFormats.TopLeft);

            if (!String.IsNullOrEmpty(kiviohjelma.tbKosteuspros.Text))
            {
                graphics.DrawString("Kosteus %", textFont, XBrushes.Black, new XRect(50, 110, page.Width, page.Height), XStringFormats.TopLeft);
                graphics.DrawString(kiviohjelma.tbKosteuspros.Text.Trim(), textFont, XBrushes.Black, new XRect(110, 110, page.Width, page.Height), XStringFormats.TopLeft);
            }

            graphics.DrawString(lisatietoHolder, textFont, XBrushes.Black, new XRect(50, 120, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näyte no", textFont, XBrushes.Black, new XRect(350, 80, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(nayteHolder, textFont, XBrushes.Black, new XRect(450, 80, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Päiväys", textFont, XBrushes.Black, new XRect(350, 95, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(dateHolder, textFont, XBrushes.Black, new XRect(450, 95, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näytteen ottaja", textFont, XBrushes.Black, new XRect(350, 120, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(nayteOttajaHolder, textFont, XBrushes.Black, new XRect(450, 120, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seulojen arvot PDF-dokumenttiin
            graphics.DrawString("#mm", textFont, XBrushes.Black, new XRect(50, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula1.Text, textFont, XBrushes.Black, new XRect(50, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula2.Text, textFont, XBrushes.Black, new XRect(50, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula3.Text, textFont, XBrushes.Black, new XRect(50, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula4.Text, textFont, XBrushes.Black, new XRect(50, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula5.Text, textFont, XBrushes.Black, new XRect(50, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula6.Text, textFont, XBrushes.Black, new XRect(50, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula7.Text, textFont, XBrushes.Black, new XRect(50, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula8.Text, textFont, XBrushes.Black, new XRect(50, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula9.Text, textFont, XBrushes.Black, new XRect(50, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula10.Text, textFont, XBrushes.Black, new XRect(50, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula11.Text, textFont, XBrushes.Black, new XRect(50, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula12.Text, textFont, XBrushes.Black, new XRect(50, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula13.Text, textFont, XBrushes.Black, new XRect(50, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula14.Text, textFont, XBrushes.Black, new XRect(50, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula15.Text, textFont, XBrushes.Black, new XRect(50, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula16.Text, textFont, XBrushes.Black, new XRect(50, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula17.Text, textFont, XBrushes.Black, new XRect(50, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.Seula18.Text, textFont, XBrushes.Black, new XRect(50, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Pohja", textFont, XBrushes.Black, new XRect(50, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seuloille jäävien grammojen arvot PDF-dokumenttiin
            graphics.DrawString("Seulalle jäi g", textFont, XBrushes.Black, new XRect(150, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG0.Text, textFont, XBrushes.Black, new XRect(150, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG1.Text, textFont, XBrushes.Black, new XRect(150, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG2.Text, textFont, XBrushes.Black, new XRect(150, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG3.Text, textFont, XBrushes.Black, new XRect(150, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG4.Text, textFont, XBrushes.Black, new XRect(150, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG5.Text, textFont, XBrushes.Black, new XRect(150, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG6.Text, textFont, XBrushes.Black, new XRect(150, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG7.Text, textFont, XBrushes.Black, new XRect(150, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG8.Text, textFont, XBrushes.Black, new XRect(150, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG9.Text, textFont, XBrushes.Black, new XRect(150, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG10.Text, textFont, XBrushes.Black, new XRect(150, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG11.Text, textFont, XBrushes.Black, new XRect(150, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG12.Text, textFont, XBrushes.Black, new XRect(150, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG13.Text, textFont, XBrushes.Black, new XRect(150, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG14.Text, textFont, XBrushes.Black, new XRect(150, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG15.Text, textFont, XBrushes.Black, new XRect(150, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG16.Text, textFont, XBrushes.Black, new XRect(150, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG17.Text, textFont, XBrushes.Black, new XRect(150, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulaG18.Text, textFont, XBrushes.Black, new XRect(150, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seuloilla jäävien prosenttien arvot PDF-dokumenttiin
            graphics.DrawString("Seulalle jäi %", textFont, XBrushes.Black, new XRect(250, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros1.Text, textFont, XBrushes.Black, new XRect(250, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros2.Text, textFont, XBrushes.Black, new XRect(250, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros3.Text, textFont, XBrushes.Black, new XRect(250, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros4.Text, textFont, XBrushes.Black, new XRect(250, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros5.Text, textFont, XBrushes.Black, new XRect(250, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros6.Text, textFont, XBrushes.Black, new XRect(250, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros7.Text, textFont, XBrushes.Black, new XRect(250, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros8.Text, textFont, XBrushes.Black, new XRect(250, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros9.Text, textFont, XBrushes.Black, new XRect(250, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros10.Text, textFont, XBrushes.Black, new XRect(250, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros11.Text, textFont, XBrushes.Black, new XRect(250, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros12.Text, textFont, XBrushes.Black, new XRect(250, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros13.Text, textFont, XBrushes.Black, new XRect(250, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros14.Text, textFont, XBrushes.Black, new XRect(250, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros15.Text, textFont, XBrushes.Black, new XRect(250, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros16.Text, textFont, XBrushes.Black, new XRect(250, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros17.Text, textFont, XBrushes.Black, new XRect(250, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros18.Text, textFont, XBrushes.Black, new XRect(250, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.seulapros19.Text, textFont, XBrushes.Black, new XRect(250, 430, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää läpäisy % arvot PDF-dokumenttiin
            graphics.DrawString("Läpäisy %", textFont, XBrushes.Black, new XRect(350, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros1.Text, textFont, XBrushes.Black, new XRect(350, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros2.Text, textFont, XBrushes.Black, new XRect(350, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros3.Text, textFont, XBrushes.Black, new XRect(350, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros4.Text, textFont, XBrushes.Black, new XRect(350, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros5.Text, textFont, XBrushes.Black, new XRect(350, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros6.Text, textFont, XBrushes.Black, new XRect(350, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros7.Text, textFont, XBrushes.Black, new XRect(350, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros8.Text, textFont, XBrushes.Black, new XRect(350, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros9.Text, textFont, XBrushes.Black, new XRect(350, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros10.Text, textFont, XBrushes.Black, new XRect(350, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros11.Text, textFont, XBrushes.Black, new XRect(350, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros12.Text, textFont, XBrushes.Black, new XRect(350, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros13.Text, textFont, XBrushes.Black, new XRect(350, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros14.Text, textFont, XBrushes.Black, new XRect(350, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros15.Text, textFont, XBrushes.Black, new XRect(350, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros16.Text, textFont, XBrushes.Black, new XRect(350, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros17.Text, textFont, XBrushes.Black, new XRect(350, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviohjelma.lapaisypros18.Text, textFont, XBrushes.Black, new XRect(350, 415, page.Width, page.Height), XStringFormats.TopLeft);

            KayraImage ki = new KayraImage();
            MemoryStream kayrastream = ki.KayraKuva(kiviohjelma);
            XImage kayrakuva = XImage.FromStream(kayrastream);
            graphics.DrawImage(kayrakuva, 40, 450, 500, 300); //Piirtää käyrän kuvan PDF-dokumenttiin)
            //graphics.DrawImage(XImage.FromFile(@".\Asetukset\kuvat\kayrakuva.png"), 40, 450, 500, 300); //Piirtää käyrän kuvan PDF-dokumenttiin

            graphics.DrawString("Pvm _____._____._______", textFont, XBrushes.Black, new XRect(50, 755, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Tutkija______________________________", textFont, XBrushes.Black, new XRect(250, 755, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tutkijaHolder, textFont, XBrushes.Black, new XRect(320, 765, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawImage(XImage.FromFile(@".\Asetukset\kuvat\leima1.png"), 20, 800, 500, 49); //Piirtää leiman tiedoston alapäähän

            try
            {
                try // Avaa PDF-dokumentin tallennuksen jälkeen
                {
                    document.Save(fileName);

                    Process.Start(fileName);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Virhe"); //Jos tiedoston nimi on tyhjä heitetään siitä ilmoitus käyttäjälle
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Virhe");
            }

        }

        //Holderit tekstilaatikoille ja alasvetovalikoille
        string tyomaaHolder;
        string lajiteHolder;
        string seulontaHolder;
        string nayteHolder;
        string dateHolder;
        string nayteOttajaHolder;
        string lisatietoHolder;
        string alaOtsikkoHolder;
        string osoiteHolder;
        string postiHolder;
        string puhHolder;
        string tutkijaHolder;



        public void GetValueFromTextBoxes(Kiviohjelma kiviohjelma) //Funktio ottaa tekstilaatikoista arvot ja asettaa ne holder-muuttujille 
        {
            Kiviohjelma _kivi = kiviohjelma;

            tyomaaHolder = _kivi.tyomaa.Text.Trim();
            lajiteHolder = _kivi.lajite.Text.Trim();
            nayteHolder = _kivi.nayteNro.Text.Trim();
            dateHolder = _kivi.date.Text.Trim();
            nayteOttajaHolder = _kivi.naytteenOttaja.Text.Trim();
            lisatietoHolder = _kivi.lisatieto.Text.Trim();
            alaOtsikkoHolder = _kivi.alempiOtsikko.Text.Trim();
            osoiteHolder = _kivi.lahiosoite.Text.Trim();
            postiHolder = _kivi.osoite.Text.Trim();
            puhHolder = _kivi.puh.Text.Trim();
            tutkijaHolder = _kivi.tutkija.Text.Trim();





            if (_kivi.rbKuivaseulonta.IsChecked == true) //Tarkastaa kumpi radiobuttoneista on valittuna
            {
                seulontaHolder = _kivi.rbKuivaseulonta.Content.ToString().Trim();
            }
            else
            {
                seulontaHolder = _kivi.rbPesuseulonta.Content.ToString().Trim();
            }
        }


    }
}

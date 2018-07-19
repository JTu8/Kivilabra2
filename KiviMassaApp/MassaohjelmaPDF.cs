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
    class MassaohjelmaPDF
    {
        public void SavePDF(Massaohjelma massaOhjelma)
        {
            Massaohjelma _massa = massaOhjelma;

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

            PDFCreation(text, _massa);
        }

        public void PDFCreation(string fileName, Massaohjelma massaOhjelma)
        {
            GetValuesFromTextBoxes(massaOhjelma);

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

            //Piirtää tarvittavat otsikot ja niiden arvot PDF-tiedostoon

            string filePath;
            filePath = ((ComboBoxItem)massaOhjelma.Kuvat.SelectedItem).Tag.ToString(); //Asettaa comboboxista valitun kuvan tagin mukaan
            Console.WriteLine(filePath);

            graphics.DrawImage(XImage.FromFile(filePath), 10, 0, 120, 30);
            graphics.DrawString(alaOtsikkoHolder, alaOtsikkoFont, XBrushes.Black, new XRect(50, 27, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(lahiOsoiteHolder, osoiteFont, XBrushes.Black, new XRect(50, 33, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(osoiteHolder, osoiteFont, XBrushes.Black, new XRect(50, 40, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(puhHolder, osoiteFont, XBrushes.Black, new XRect(50, 48, page.Width, page.Height), XStringFormats.TopLeft);



            graphics.DrawString("PANK 4102", textFont, XBrushes.Black, new XRect(150, 30, page.Width, page.Height), XStringFormats.TopCenter);
            graphics.DrawString("P Ä Ä L L Y S T E T U T K I M U S", otsikkoFont, XBrushes.Black, new XRect(50, 60, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Urakka", textFont, XBrushes.Black, new XRect(50, 90, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(urakkaHolder, textFont, XBrushes.Black, new XRect(130, 90, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sek.asema", textFont, XBrushes.Black, new XRect(50, 105, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sekAsemaHolder, textFont, XBrushes.Black, new XRect(130, 105, page.Width, page.Height), XStringFormats.TopLeft);

            if (!String.IsNullOrEmpty(massaOhjelma.tbKosteusP.Text))
            {
                graphics.DrawString("Kosteus %", textFont, XBrushes.Black, new XRect(50, 120, page.Width, page.Height), XStringFormats.TopLeft);
                graphics.DrawString(massaOhjelma.tbKosteusP.Text.Trim(), textFont, XBrushes.Black, new XRect(130, 120, page.Width, page.Height), XStringFormats.TopLeft);
            }

            graphics.DrawString("Näytteen ottaja", textFont, XBrushes.Black, new XRect(340, 90, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(nayteHolder, textFont, XBrushes.Black, new XRect(426, 90, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Työkohde", textFont, XBrushes.Black, new XRect(340, 105, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tyoKohdeHolder, textFont, XBrushes.Black, new XRect(426, 105, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString(lisatietoHolder, textFont, XBrushes.Black, new XRect(426, 120, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää seulojen arvot PDF-dokumenttiin
            graphics.DrawString("#mm", textFont, XBrushes.Black, new XRect(55, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula1.Text, textFont, XBrushes.Black, new XRect(55, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula2.Text, textFont, XBrushes.Black, new XRect(55, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula3.Text, textFont, XBrushes.Black, new XRect(55, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula4.Text, textFont, XBrushes.Black, new XRect(55, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula5.Text, textFont, XBrushes.Black, new XRect(55, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula6.Text, textFont, XBrushes.Black, new XRect(55, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula7.Text, textFont, XBrushes.Black, new XRect(55, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula8.Text, textFont, XBrushes.Black, new XRect(55, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula9.Text, textFont, XBrushes.Black, new XRect(55, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula10.Text, textFont, XBrushes.Black, new XRect(55, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula11.Text, textFont, XBrushes.Black, new XRect(55, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula12.Text, textFont, XBrushes.Black, new XRect(55, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula13.Text, textFont, XBrushes.Black, new XRect(55, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula14.Text, textFont, XBrushes.Black, new XRect(55, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula15.Text, textFont, XBrushes.Black, new XRect(55, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.Seula16.Text, textFont, XBrushes.Black, new XRect(55, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Pohja", textFont, XBrushes.Black, new XRect(55, 400, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Seulalle jäi g", textFont, XBrushes.Black, new XRect(100, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG0.Text, textFont, XBrushes.Black, new XRect(100, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG1.Text, textFont, XBrushes.Black, new XRect(100, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG2.Text, textFont, XBrushes.Black, new XRect(100, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG3.Text, textFont, XBrushes.Black, new XRect(100, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG4.Text, textFont, XBrushes.Black, new XRect(100, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG5.Text, textFont, XBrushes.Black, new XRect(100, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG6.Text, textFont, XBrushes.Black, new XRect(100, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG7.Text, textFont, XBrushes.Black, new XRect(100, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG8.Text, textFont, XBrushes.Black, new XRect(100, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG9.Text, textFont, XBrushes.Black, new XRect(100, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG10.Text, textFont, XBrushes.Black, new XRect(100, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG11.Text, textFont, XBrushes.Black, new XRect(100, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG12.Text, textFont, XBrushes.Black, new XRect(100, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG13.Text, textFont, XBrushes.Black, new XRect(100, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG14.Text, textFont, XBrushes.Black, new XRect(100, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG15.Text, textFont, XBrushes.Black, new XRect(100, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulaG16.Text, textFont, XBrushes.Black, new XRect(100, 400, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Seulalle jäi %", textFont, XBrushes.Black, new XRect(180, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros1.Text, textFont, XBrushes.Black, new XRect(180, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros2.Text, textFont, XBrushes.Black, new XRect(180, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros3.Text, textFont, XBrushes.Black, new XRect(180, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros4.Text, textFont, XBrushes.Black, new XRect(180, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros5.Text, textFont, XBrushes.Black, new XRect(180, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros6.Text, textFont, XBrushes.Black, new XRect(180, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros7.Text, textFont, XBrushes.Black, new XRect(180, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros8.Text, textFont, XBrushes.Black, new XRect(180, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros9.Text, textFont, XBrushes.Black, new XRect(180, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros10.Text, textFont, XBrushes.Black, new XRect(180, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros11.Text, textFont, XBrushes.Black, new XRect(180, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros12.Text, textFont, XBrushes.Black, new XRect(180, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros13.Text, textFont, XBrushes.Black, new XRect(180, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros14.Text, textFont, XBrushes.Black, new XRect(180, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros15.Text, textFont, XBrushes.Black, new XRect(180, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros16.Text, textFont, XBrushes.Black, new XRect(180, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.seulapros17.Text, textFont, XBrushes.Black, new XRect(180, 400, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Läpäisy %", textFont, XBrushes.Black, new XRect(260, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros1.Text, textFont, XBrushes.Black, new XRect(260, 160, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros2.Text, textFont, XBrushes.Black, new XRect(260, 175, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros3.Text, textFont, XBrushes.Black, new XRect(260, 190, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros4.Text, textFont, XBrushes.Black, new XRect(260, 205, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros5.Text, textFont, XBrushes.Black, new XRect(260, 220, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros6.Text, textFont, XBrushes.Black, new XRect(260, 235, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros7.Text, textFont, XBrushes.Black, new XRect(260, 250, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros8.Text, textFont, XBrushes.Black, new XRect(260, 265, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros9.Text, textFont, XBrushes.Black, new XRect(260, 280, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros10.Text, textFont, XBrushes.Black, new XRect(260, 295, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros11.Text, textFont, XBrushes.Black, new XRect(260, 310, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros12.Text, textFont, XBrushes.Black, new XRect(260, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros13.Text, textFont, XBrushes.Black, new XRect(260, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros14.Text, textFont, XBrushes.Black, new XRect(260, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros15.Text, textFont, XBrushes.Black, new XRect(260, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(massaOhjelma.lapaisypros16.Text, textFont, XBrushes.Black, new XRect(260, 385, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää otsikkotietoja PDF-dokumenttiin
            graphics.DrawString("Päällyste", textFont, XBrushes.Black, new XRect(340, 150, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paallysteHolder, textFont, XBrushes.Black, new XRect(426, 150, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näyte no", textFont, XBrushes.Black, new XRect(340, 165, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(nayteNroHolder, textFont, XBrushes.Black, new XRect(426, 165, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Päiväys", textFont, XBrushes.Black, new XRect(340, 180, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paivaysHolder, textFont, XBrushes.Black, new XRect(426, 180, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Klo", textFont, XBrushes.Black, new XRect(340, 195, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kloHolder, textFont, XBrushes.Black, new XRect(426, 195, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Paalu/kaista", textFont, XBrushes.Black, new XRect(340, 210, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(paaluHolder, textFont, XBrushes.Black, new XRect(426, 210, page.Width, page.Height), XStringFormats.TopLeft);

            //Piirtää sideainepitoisuuksien määritykset PDF-dokumenttiin
            graphics.DrawString("SIDEAINEMÄÄRITYS", textFont, XBrushes.Black, new XRect(340, 230, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Näytteen paino", textFont, XBrushes.Black, new XRect(340, 245, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 245, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(naytePainoHolder, textFont, XBrushes.Black, new XRect(475, 245, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Kiviain. yht. paino", textFont, XBrushes.Black, new XRect(340, 260, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 260, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(kiviAineHolder, textFont, XBrushes.Black, new XRect(475, 260, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainemäärä", textFont, XBrushes.Black, new XRect(340, 275, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("g", textFont, XBrushes.Black, new XRect(450, 275, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideAineMHolder, textFont, XBrushes.Black, new XRect(475, 275, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainepitoisuus", textFont, XBrushes.Black, new XRect(340, 290, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 290, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideainePHolder, textFont, XBrushes.Black, new XRect(475, 290, page.Width, page.Height), XStringFormats.TopLeft);

            /*
            //Piirtää ohjearvojen määritykset PDF-dokumenttiin
            graphics.DrawString("OHJEARVOT", textFont, XBrushes.Black, new XRect(340, 310, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Sideainepitoisuus", textFont, XBrushes.Black, new XRect(340, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 325, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(sideAinePitHolder, textFont, XBrushes.Black, new XRect(475, 325, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Täytejauheen määrä", textFont, XBrushes.Black, new XRect(340, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("%", textFont, XBrushes.Black, new XRect(450, 340, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tayteJauheHolder, textFont, XBrushes.Black, new XRect(475, 340, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("12", textFont, XBrushes.Black, new XRect(415, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 355, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak12Holder, textFont, XBrushes.Black, new XRect(475, 355, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("8", textFont, XBrushes.Black, new XRect(415, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 370, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak8Holder, textFont, XBrushes.Black, new XRect(475, 370, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("4", textFont, XBrushes.Black, new XRect(415, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 385, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak4Holder, textFont, XBrushes.Black, new XRect(475, 385, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("2", textFont, XBrushes.Black, new XRect(415, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 400, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak2Holder, textFont, XBrushes.Black, new XRect(475, 400, page.Width, page.Height), XStringFormats.TopLeft);


            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("0.5", textFont, XBrushes.Black, new XRect(415, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 415, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak05Holder, textFont, XBrushes.Black, new XRect(475, 415, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawString("Rakeisuus", textFont, XBrushes.Black, new XRect(340, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("#", textFont, XBrushes.Black, new XRect(400, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("0.063", textFont, XBrushes.Black, new XRect(415, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("mm", textFont, XBrushes.Black, new XRect(450, 430, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(rak0063Holder, textFont, XBrushes.Black, new XRect(475, 430, page.Width, page.Height), XStringFormats.TopLeft);
            */

            KayraImage ki = new KayraImage();
            MemoryStream kayrastream = ki.KayraKuva(massaOhjelma);
            XImage kayrakuva = XImage.FromStream(kayrastream);
            graphics.DrawImage(kayrakuva, 40, 420, 500, 300); //Piirtää käyrän kuvan PDF-dokumenttiin)

            graphics.DrawString("Pvm _____._____._______", textFont, XBrushes.Black, new XRect(50, 755, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString("Tutkija______________________________", textFont, XBrushes.Black, new XRect(250, 755, page.Width, page.Height), XStringFormats.TopLeft);
            graphics.DrawString(tutkijaHolder, textFont, XBrushes.Black, new XRect(320, 765, page.Width, page.Height), XStringFormats.TopLeft);

            graphics.DrawImage(XImage.FromFile(@".\Asetukset\kuvat\leima1.png"), 20, 800, 500, 49); //Piirtää leiman tiedoston alapäähän

            try
            {
                try //Avaa PDF-dokumentin tallennuksen jälkeen
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

        string urakkaHolder;
        string sekAsemaHolder;
        string lisatietoHolder;
        string nayteHolder;
        string tyoKohdeHolder;
        string alaOtsikkoHolder;
        string lahiOsoiteHolder;
        string osoiteHolder;
        string puhHolder;

        string paallysteHolder;
        string nayteNroHolder;
        string paivaysHolder;
        string kloHolder;
        string paaluHolder;

        string naytePainoHolder;
        string kiviAineHolder;
        string sideAineMHolder;
        string sideainePHolder;

        string tutkijaHolder;

        /*
        string sideAinePitHolder;
        string tayteJauheHolder;
        string rak12Holder;
        string rak8Holder;
        string rak4Holder;
        string rak2Holder;
        string rak05Holder;
        string rak0063Holder;
        */


        public void GetValuesFromTextBoxes(Massaohjelma massaOhjelma)
        {

            Massaohjelma _massa = massaOhjelma;

            urakkaHolder = _massa.urakka.Text.Trim();
            sekAsemaHolder = _massa.sekoitusAsema.Text.Trim();
            lisatietoHolder = _massa.lisatietoja.Text.Trim();
            nayteHolder = _massa.naytteenOttaja.Text.Trim();
            tyoKohdeHolder = _massa.tyokohde.Text.Trim();
            alaOtsikkoHolder = _massa.alempiOtsikko.Text.Trim();
            lahiOsoiteHolder = _massa.lahiOsoite.Text.Trim();
            osoiteHolder = _massa.osoite.Text.Trim();
            puhHolder = _massa.puh.Text.Trim();

            paallysteHolder = _massa.paallyste.Text.Trim();
            nayteNroHolder = _massa.nayteNro.Text.Trim();
            paivaysHolder = _massa.paivays.Text.Trim();
            kloHolder = _massa.klo.Text.Trim();
            paaluHolder = _massa.paaluKaista.Text.Trim();

            naytePainoHolder = _massa.naytteenPaino.Text.Trim();
            kiviAineHolder = _massa.nayte.Text.Trim();
            sideAineMHolder = _massa.sideainemaara.Text.Trim();
            sideainePHolder = _massa.sideainepitoisuus.Text.Trim();

            tutkijaHolder = _massa.tutkija.Text.Trim();

            /*
            sideAinePitHolder = _massa.sideainepitoisuusOhjeArvo.Text.Trim();
            tayteJauheHolder = _massa.tayteJauheenMaara.Text.Trim();
            rak12Holder = _massa.rakeisuus12mm.Text.Trim();
            rak8Holder = _massa.rakeisuus8mm.Text.Trim();
            rak4Holder = _massa.rakeisuus4mm.Text.Trim();
            rak2Holder = _massa.rakeisuus2mm.Text.Trim();
            rak05Holder = _massa.rakeisuus05mm.Text.Trim();
            rak0063Holder = _massa.rakeisuus0065mm.Text.Trim();
            */
        }
    }
}

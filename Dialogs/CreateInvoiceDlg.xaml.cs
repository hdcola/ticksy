using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateInvoiceDlg : Window
    {
        public CreateInvoiceDlg()
        {
            InitializeComponent();
            var pdfStream = GeneratedPdfStream();

            string pdfPath = SavePdfToTempFile(pdfStream);

            PdfViewer.Source = new System.Uri(pdfPath);

        }

        private MemoryStream GeneratedPdfStream()
        {
            Document document = new Document();
            Section section = document.AddSection();

            // Add a title
            section.AddParagraph("Invoice")
                .Format.Font.Size = 18;
            section.AddParagraph($"Date: {System.DateTime.Now.ToString("yyyy-MM-dd")}")
                .Format.Font.Size = 12;

            // Add a table
            Table table = section.AddTable();
            table.Borders.Width = 0.75;

            // Add columns
            Column column1 = table.AddColumn("4cm");
            column1.Format.Alignment = ParagraphAlignment.Center;
            Column column2 = table.AddColumn("3cm");
            column2.Format.Alignment = ParagraphAlignment.Center;
            Column column3 = table.AddColumn("3cm");
            column3.Format.Alignment = ParagraphAlignment.Center;
            Column column4 = table.AddColumn("3cm");
            column4.Format.Alignment = ParagraphAlignment.Center;

            // Header row
            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Description");
            row.Cells[1].AddParagraph("Quantity");
            row.Cells[2].AddParagraph("Unit Price");
            row.Cells[3].AddParagraph("Total Price");

            // Add items in the table
            for (int i = 0; i < 10; i++)
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph($"Item {i + 1}");
                row.Cells[1].AddParagraph("1");
                row.Cells[2].AddParagraph("100");
                row.Cells[3].AddParagraph("100");
            }

            // Add a total row
            decimal totalAmount = 1000;
            var paragraph = section.AddParagraph($"Total Amount: {totalAmount.ToString("C")}");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Alignment = ParagraphAlignment.Right;

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();

            MemoryStream stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false); // Save the document to the stream
            stream.Seek(0, SeekOrigin.Begin); // Reset the stream position to the beginning

            return stream;
        }

        private string SavePdfToTempFile(MemoryStream stream)
        {
            string directory = Path.Combine(Path.GetTempPath(), "MyAppTemp");
            string tempPath = Path.Combine(directory, "preview.pdf");


            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }

            return tempPath;
        }
    }
}

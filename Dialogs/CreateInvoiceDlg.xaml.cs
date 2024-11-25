﻿using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ticksy.Helpers;
using Path = System.IO.Path;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateInvoiceDlg : Window
    {
        private Invoice _currentInvoice;
        public Invoice CurrentInvoice
        {
            get => _currentInvoice;
            set
            {
                if (_currentInvoice != value)
                {
                    _currentInvoice = value;
                };
            }
        }

        public CreateInvoiceDlg()
        {
            InitializeComponent();

            CurrentInvoice = new Invoice
            {
                InvoiceNumber = "INV-001",
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                BillTo = "John Doe",
                BillFrom = "Jane Doe",
                Items = new ObservableCollection<InvoiceItem>
                {
                    new InvoiceItem { Description = "Item 1", Quantity = 1, Price = 100 }
                }
            };

            DataContext = CurrentInvoice;
            updatePreview();
        }

        private void updatePreview()
        {
            var pdfStream = GeneratedPdfStream(CurrentInvoice);

            string pdfPath = SavePdfToTempFile(pdfStream);

            PdfViewer.Source = new Uri("about:blank");
            PdfViewer.Source = new System.Uri(pdfPath);

        }

        private MemoryStream GeneratedPdfStream(Invoice invoice)
        {
            Document document = new Document();
            Section section = document.AddSection();

            // add a title and some information
            section.AddParagraph("Invoice")
                .Format.Font.Size = 18;
            section.AddParagraph($"Invoice Number: {invoice.InvoiceNumber}")
                .Format.Font.Size = 12;
            section.AddParagraph($"Invoice Date: {invoice.InvoiceDate:yyyy-MM-dd}")
                .Format.Font.Size = 12;
            section.AddParagraph($"Due Date: {invoice.DueDate:yyyy-MM-dd}")
                .Format.Font.Size = 12;
            section.AddParagraph($"Bill From: {invoice.BillFrom}")
                .Format.Font.Size = 12;
            section.AddParagraph($"Bill To: {invoice.BillTo}")
                .Format.Font.Size = 12;
            section.AddParagraph(); // add a blank line

            // create a table
            Table table = section.AddTable();
            table.Borders.Width = 0.75;

            // add columns
            Column column1 = table.AddColumn("6cm");
            column1.Format.Alignment = ParagraphAlignment.Left;
            Column column2 = table.AddColumn("3cm");
            column2.Format.Alignment = ParagraphAlignment.Center;
            Column column3 = table.AddColumn("3cm");
            column3.Format.Alignment = ParagraphAlignment.Right;
            Column column4 = table.AddColumn("3cm");
            column4.Format.Alignment = ParagraphAlignment.Right;

            // add header row
            Row headerRow = table.AddRow();
            headerRow.Cells[0].AddParagraph("Description").Format.Font.Bold = true;
            headerRow.Cells[1].AddParagraph("Quantity").Format.Font.Bold = true;
            headerRow.Cells[2].AddParagraph("Unit Price").Format.Font.Bold = true;
            headerRow.Cells[3].AddParagraph("Total Price").Format.Font.Bold = true;

            // fill the table with items
            decimal totalAmount = 0;
            foreach (var item in invoice.Items)
            {
                Row row = table.AddRow();
                row.Cells[0].AddParagraph(item.Description);
                row.Cells[1].AddParagraph(item.Quantity.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString("C"));
                row.Cells[3].AddParagraph((item.Quantity * item.Price).ToString("C"));

                totalAmount += item.Quantity * item.Price;
            }

            // add a total row
            Row totalRow = table.AddRow();
            totalRow.Cells[0].MergeRight = 2; // merge cells
            totalRow.Cells[0].AddParagraph("Total Amount").Format.Font.Bold = true;
            totalRow.Cells[3].AddParagraph(totalAmount.ToString("C")).Format.Font.Bold = true;

            // render the document
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();

            // save the document to a stream
            MemoryStream stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false); // save the document to the stream
            stream.Seek(0, SeekOrigin.Begin); // reset the stream position to the beginning

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

        private void btnUpdatePreview_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{CurrentInvoice}");
            updatePreview();
        }
    }
}

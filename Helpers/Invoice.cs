using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticksy.Helpers
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BillTo { get; set; }
        public string BillFrom { get; set; }

        public ObservableCollection<InvoiceItem> Items { get; set; }

        public override string ToString()
        {
            return $"Invoice Number: {InvoiceNumber}, Invoice Date: {InvoiceDate}, Due Date: {DueDate}, Bill To: {BillTo}, Bill From: {BillFrom}, Items: {Items.Count}";
        }
    }

    public class InvoiceItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Description: {Description}, Quantity: {Quantity}, Price: {Price}";
        }
    }
}

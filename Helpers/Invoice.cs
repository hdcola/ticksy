using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ticksy.Helpers
{
    public class Invoice
    {
        public Invoice()
        {
            DeleteCommand = new RelayCommand<InvoiceItem>(DeleteItem);
        }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BillTo { get; set; }
        public string BillFrom { get; set; }

        public ObservableCollection<InvoiceItem> Items { get; set; }

        public ICommand DeleteCommand { get; }

        private void DeleteItem(InvoiceItem item)
        {
            if (item != null && Items.Contains(item))
            {
                Items.Remove(item);
            }

        }


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

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (parameter is T validParameter)
            {
                _execute(validParameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}

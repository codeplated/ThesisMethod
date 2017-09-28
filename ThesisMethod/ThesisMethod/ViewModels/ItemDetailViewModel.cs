using System.Diagnostics;
using ThesisMethod.Models;
using Xamarin.Forms;

namespace ThesisMethod.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        private static string TAG = "------------ItemDetailViewModel.cs ";
        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Text;
            Item = item;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
        

    }
    
}
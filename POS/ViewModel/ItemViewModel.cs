using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Models;
using POS.Services;
using Prism.Commands;
using POS.Views.DataEntry;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace POS.ViewModel
{
    public class ItemViewModel: INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Item> items { get; set; }
        public ObservableCollection<Unit> units { get; set; }
        public ObservableCollection<Category> categories { get; set; }
        public ItemDbService dbService { get; set; }
        public Item selected_item { get; set; }
        public Unit selected_unit { get; set; }
        public Category selected_category { get; set; }
        public DelegateCommand add_btn { get; set; }
        public DelegateCommand del_btn { get; set; }
        public DelegateCommand edit_btn { get; set; }
        public DelegateCommand search_box { get; set; }
        public string price { get; set; }
        public Item item { get; set; }
        public string search_item { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public ItemViewModel()
        {
            items = new ObservableCollection<Item>();
            dbService = new ItemDbService();
            items = dbService.GetItems();
            
            units = new UnitDbService().GetUnits();
            categories = new CategoryDbService().GetCategories();
            add_btn = new DelegateCommand(AddItem);
            del_btn = new DelegateCommand(DelItem);
            edit_btn = new DelegateCommand(EditItem);
            search_box = new DelegateCommand(SearchItem);

        }

        private void SearchItem()
        {
            dbService.SearchItem(search_item, items);
        }

        private void EditItem()
        {
            if (item != null)
            {
                ItemDataEntry itemDataEntry = new ItemDataEntry();
                selected_category = new Category();
                selected_unit = new Unit();
                selected_category.Name = item.Category;
                selected_unit.Name = item.Unit;
                price = item.Price.ToString();
                itemDataEntry.DataContext = this;
                itemDataEntry.ShowDialog();

                if (MessageBox.Show("Voulez-vous modifier ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (!string.IsNullOrEmpty(item.Description) && float.Parse(price) > 0 && selected_unit != null && !string.IsNullOrEmpty(item.Marque) && selected_category != null)
                    {
                        item.Unit = selected_unit.Name;
                        item.Category = selected_category.Name;
                        item.Price = float.Parse(price);
                        dbService.EditItem(item);
                        dbService.Refresh(items);
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir le champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        dbService.Refresh(items);
                    }


                }
                else
                {
                    //units.Clear();
                    dbService.Refresh(items);

                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelItem()
        {
            if (item != null)
            {
                if (MessageBox.Show("Voulez-vous supprimer ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dbService.DelItem(item);
                    items.Remove(item);
                    item = null;
                }

            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddItem()
        {
            item = new Item();
            ItemDataEntry itemDataEntry = new ItemDataEntry();
            //item.Price = null;
            itemDataEntry.DataContext = this;
            itemDataEntry.ShowDialog();
            try
            {
                if (!string.IsNullOrEmpty(item.Description) && itemDataEntry.validate == true && float.Parse(price) > 0 && selected_unit != null && !string.IsNullOrEmpty(item.Marque) && selected_category != null)
                {
                    item.Unit = selected_unit.Name;
                    item.Category = selected_category.Name;
                    item.Price = float.Parse(price);
                    dbService.AddItem(item);
                    items.Add(dbService.GetItem(item));
                    MessageBox.Show("Article ajouté avec succes !", "Good", MessageBoxButton.OK, MessageBoxImage.Information);
                    item = null;
                    price = null;

                }
                else
                {

                    // MessageBox.Show("Veuillez remplir tout les champs !", "Bad", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch(Exception ex)
            {

            }
            item = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}

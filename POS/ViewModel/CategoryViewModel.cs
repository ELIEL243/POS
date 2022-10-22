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

namespace POS.ViewModel
{
    public class CategoryViewModel
    {
        #region Properties
        public ObservableCollection<Category> categories { get; set; }
        public CategoryDbService dbService { get; set; }
        public Category selected_category { get; set; }
        public DelegateCommand add_btn { get; set; }
        public DelegateCommand del_btn { get; set; }
        public DelegateCommand edit_btn { get; set; }
        public Category category { get; set; }
        #endregion

        #region Constructor
        public CategoryViewModel()
        {
            categories = new ObservableCollection<Category>();
            dbService = new CategoryDbService();
            categories = dbService.GetCategories();
            add_btn = new DelegateCommand(AddCategory);
            del_btn = new DelegateCommand(DelCategory);
            edit_btn = new DelegateCommand(EditCategory);
        }
        #endregion

        #region Methodes
        private void EditCategory()
        {
            if (selected_category != null)
            {
                CategoryDataEntry unitDataEntry = new CategoryDataEntry();
                unitDataEntry.DataContext = selected_category;
                unitDataEntry.ShowDialog();

                if (MessageBox.Show("Voulez-vous modifier ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (!string.IsNullOrEmpty(selected_category.Name))
                    {
                        dbService.EditCategory(selected_category);
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir le champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        dbService.Refresh(categories);
                    }


                }
                else
                {
                    //units.Clear();
                    dbService.Refresh(categories);

                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelCategory()
        {
            if (selected_category != null)
            {
                if (MessageBox.Show("Voulez-vous supprimer ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dbService.DelCategory(selected_category);
                    categories.Remove(selected_category);
                }

            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddCategory()
        {
            category = new Category();
            CategoryDataEntry categoryDataEntry = new CategoryDataEntry();
            categoryDataEntry.DataContext = category;
            categoryDataEntry.ShowDialog();
            if (!string.IsNullOrEmpty(category.Name) && categoryDataEntry.validate == true)
            {
                dbService.AddCategory(category);
                categories.Add(dbService.GetCategory(category));
                MessageBox.Show("Categorie ajouté avec succes !", "Good", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {

                // MessageBox.Show("Veuillez remplir tout les champs !", "Bad", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
        #endregion
    }
}

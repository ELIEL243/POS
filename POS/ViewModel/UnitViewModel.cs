using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Models;
using POS.Services;
using Prism.Commands;
using POS.Views.DataEntry;
using System.Windows;

namespace POS.ViewModel
{
    public class UnitViewModel
    {
        #region Properties
        public ObservableCollection<Unit> units { get; set; }
        public UnitDbService dbService { get; set; }
        public Unit selected_unit { get; set; }
        public DelegateCommand add_btn { get; set; }
        public DelegateCommand del_btn { get; set; }
        public DelegateCommand edit_btn { get; set; }
        public Unit unit { get; set; }
        #endregion

        #region Constructor
        public UnitViewModel()
        {
            units = new ObservableCollection<Unit>();
            dbService = new UnitDbService();
            units = dbService.GetUnits();
            add_btn = new DelegateCommand(AddUnit);
            del_btn = new DelegateCommand(DelUnit);
            edit_btn = new DelegateCommand(EditUnit);
        }
        #endregion

        #region Methodes
        private void EditUnit()
        {
            if(selected_unit != null)
            {
                UnitDataEntry unitDataEntry = new UnitDataEntry();
                Unit prec = selected_unit;
                unitDataEntry.DataContext = selected_unit;
                unitDataEntry.ShowDialog();
                 
                if (MessageBox.Show("Voulez-vous modifier ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (!string.IsNullOrEmpty(selected_unit.Name))
                    {
                        dbService.EditUnit(selected_unit);
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir le champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        dbService.Refresh(units);
                    }
                    
                    
                }
                else
                {
                    //units.Clear();
                    dbService.Refresh(units);
                    
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelUnit()
        {
            if(selected_unit != null)
            {
                if (MessageBox.Show("Voulez-vous supprimer ?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dbService.DelUnit(selected_unit);
                    units.Remove(selected_unit);
                }
                
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un objet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddUnit()
        {
            unit = new Unit();
            UnitDataEntry unitDataEntry = new UnitDataEntry();
            unitDataEntry.DataContext = unit;
            unitDataEntry.ShowDialog();
            if (!string.IsNullOrEmpty(unit.Name) && unitDataEntry.validate == true)
            {
                dbService.AddUnit(unit);
                units.Add(dbService.GetUnit(unit));
                MessageBox.Show("Unité ajouté avec succes !", "Good", MessageBoxButton.OK, MessageBoxImage.Information);
                dbService = new UnitDbService();
                
            }
            else
            {

               // MessageBox.Show("Veuillez remplir tout les champs !", "Bad", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
        #endregion

    }
}

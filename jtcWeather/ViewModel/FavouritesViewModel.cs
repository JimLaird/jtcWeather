using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jtcWeather.ViewModel
{
    public partial class FavouritesViewModel : BaseViewModel
    {
        [ObservableProperty]
        string locationText;
        [ObservableProperty]
        string statusText;
        
        public ObservableCollection<Favourite> Favourites { get; set; }

        public FavouritesViewModel()
        {
            
            Favourites = new ObservableCollection<Favourite>();

            Task.Run(async() => await LoadAllAsync());

        }

        

        [ICommand]
        public async Task LoadAllAsync()
        {
            IsBusy = true;

            try
            {
                Favourites.Clear();
                var faveList = await App.DataRepo.GetAllAsync();

                foreach (var fave in faveList)
                {
                    Favourites.Add(fave);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        public async Task AddNewAsync()
        {
            StatusText = "";



            await App.DataRepo.AddAsync(new Favourite { Item = LocationText});

            StatusText = App.DataRepo.StatusMessage;

            await LoadAllAsync();
            
        }

        [ICommand]
        public async Task SelectedAsync(Favourite favourite)
        {
            if (favourite is null)
                return;

            

            await Shell.Current.GoToAsync($"..", true, new Dictionary<string, object>
            {
                {
                    "Favourite", favourite
                }
            });
        }

        [ICommand]
        public async Task RemoveAsync(Favourite favourite)
        {
            StatusText = "";

            var fave = favourite.Id;

            await App.DataRepo.DeleteAsync(fave);

            await LoadAllAsync();

            
        }
    }
}

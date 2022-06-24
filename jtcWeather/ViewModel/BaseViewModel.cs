

namespace jtcWeather.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        

        //  Base View Model Class From Which All Other View Models Will Inherit
        public BaseViewModel()
        {
            //            
        }

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !isBusy;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        bool isRefreshing;
    }
}

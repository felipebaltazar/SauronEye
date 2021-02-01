using System.Threading.Tasks;
using Saruman.ViewModels;
using Xamarin.Forms;

namespace Saruman.Interfaces.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentPageModel { get; }

        void MapPageViewModel<TPage, TViewModel>()
            where TPage : Page
            where TViewModel : BaseViewModel;
        Task NavigateFromAsync(bool modal = false);
        Task NavigateToAsync<T>(object parameter = null, bool modal = false) where T : BaseViewModel;
        void SetRootPage<T>(object parameter = null) where T : BaseViewModel;
    }
}
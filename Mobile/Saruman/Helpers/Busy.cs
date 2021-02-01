using System;
using Saruman.ViewModels;

namespace Saruman.Helpers
{
    public class Busy : IDisposable
    {
        private readonly BaseViewModel _viewmodel;

        public Busy(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.IsBusy = true;
        }

        public void Dispose()
        {
            _viewmodel.IsBusy = false;
        }
    }
}

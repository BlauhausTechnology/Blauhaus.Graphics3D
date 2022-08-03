using Blauhaus.MVVM.Abstractions.Contracts;
using Blauhaus.MVVM.Abstractions.ViewModels;
using Blauhaus.MVVM.Maui.Views;

namespace Blauhaus.Graphics3D.Maui.Skia.Pages
{
    public abstract class BaseUpdateContentPage<TViewModel> : BaseMauiContentPage<TViewModel> where TViewModel : IViewModel
    {
        private readonly Dictionary<Type, Action<object>> _handlers = new();

        protected BaseUpdateContentPage(TViewModel viewModel, bool isUpdatable = false) : base(viewModel)
        {
            if (isUpdatable)
            {
                SetBinding(UpdateProperty, new Binding(nameof(INotifyUpdates.Update), BindingMode.OneWay, new ActionConverter<object>(OnUpdated)));
            }
        }

        private void OnUpdated(object? update)
        {
            if (update != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                { 
                    foreach (var handler in _handlers.Where(handler => update.GetType() == handler.Key || handler.Key.IsInstanceOfType(update)))
                    {
                        handler.Value.Invoke(update);
                    }
                });
            }
        }
         
        protected void Subscribe<TUpdate>(Action<TUpdate> handler)
        {
            _handlers[typeof(TUpdate)] = obj => handler.Invoke((TUpdate) obj);

            if (BindingContext is INotifyUpdates notifyUpdatesViewModel)
            {
                if (notifyUpdatesViewModel.Update is TUpdate update)
                {
                    handler.Invoke(update);
                }
            }
        }

        public static readonly BindableProperty UpdateProperty = BindableProperty.Create(
            propertyName: nameof(Update),
            returnType: typeof(object),
            declaringType: typeof(BaseUpdateContentPage<TViewModel>),
            defaultValue: string.Empty);

        public object Update
        {
            get => GetValue(UpdateProperty);
            set => SetValue(UpdateProperty, value);
        }
    }
}
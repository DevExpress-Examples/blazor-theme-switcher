using System;
using System.Collections.Generic;

namespace BlazorAppThemes.ThemeSwitcher
{
    public class LinkService : IObservable<string>
    {

        string currentUrl = "css/bootstrap/bootstrap.min.css";
        List<IObserver<string>> observers = new List<IObserver<string>>();

        public void SetTheme(string newUrl) {
            if (currentUrl != newUrl) {
                currentUrl = newUrl;
                observers.ForEach(o => o.OnNext(currentUrl));
            }
        }
        public IDisposable Subscribe(IObserver<string> observer) {
            if (!observers.Contains(observer))
                observers.Add(observer);
            observer.OnNext(currentUrl);
            return new Unsubscriber(observers, observer);
        }

        class Unsubscriber : IDisposable
        {
            private List<IObserver<string>> _observers;
            private IObserver<string> _observer;

            public Unsubscriber(List<IObserver<string>> observers, IObserver<string> observer) {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose() {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}

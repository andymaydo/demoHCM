using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM
{
    public class AppState
    {
        public string SelectedUrl { get; set; }

        public event Action OnChange;

        public void SetUrl(string url)
        {
            SelectedUrl = url;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}

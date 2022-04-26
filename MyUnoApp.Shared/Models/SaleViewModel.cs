using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MyUnoApp.Models
{
    public class SaleViewModel : ObservableObject
    {
        private string? pageId = null;
        public string PageId => this.pageId ?? (this.pageId = Guid.NewGuid().ToString());

        public delegate void PageClosingDelegate(string pageId);
        public event PageClosingDelegate PageClosing;

        public void RaisePageClosingEvent()
        {
            if (this.PageClosing != null)
            {
                this.PageClosing.Invoke(this.PageId);
            }
        }
    }
}

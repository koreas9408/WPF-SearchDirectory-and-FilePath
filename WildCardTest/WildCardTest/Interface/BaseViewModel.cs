using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WildCardTest.Interface
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private static ILog log = LogManager.GetLogger(typeof(BaseViewModel));

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            log.Info("PropertyChanged has been called...");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

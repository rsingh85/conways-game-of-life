using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConwaysGameOfLife.Models
{
    public class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string memberName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(memberName));
            }
        }
    }
}

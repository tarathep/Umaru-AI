using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Umaru_AI //project name
{
  
    public class RosterListViewItem : ListViewItem 
    {
        public ObservableCollection<string> Resources = new ObservableCollection<string>();

        public RosterListViewItem(string text, int imageIndex, ListViewGroup group) : base(text, imageIndex, group)
        {
            Resources.CollectionChanged += ResourcesChanged;
        }

        void ResourcesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            string tt = "";
            foreach (string r in Resources)
            {
                if (tt.Length > 0)
                    tt += "\r\n";
                
                tt += r;
            }

            ToolTipText = tt;
        }
    }
}
using System.Windows;

namespace RiskManagement.UI.Views
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            logoImg.Audio = Properties.Resources.vivaldi;
        }
    }
}

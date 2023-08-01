using Avalonia.Controls;
using System.Linq;

namespace SimpleSnmpClient.Views
{
    public partial class SnmpMainView : UserControl
    {
        public SnmpMainView()
        {
            InitializeComponent();

            V3DiscoveryTimeout.KeyDown += V3DiscoveryTimeout_KeyDown;
            V3ResponseTimeout.KeyDown += V3ResponseTimeout_KeyDown;
            V2ResponseTimeout.KeyDown += V2ResponseTimeout_KeyDown;
        }

        private void V2ResponseTimeout_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            var input = e.Key.ToString();
            bool isDigitPresent = input.Any(c => char.IsDigit(c));
            if (!isDigitPresent)
            {
                e.Handled = true;
            }

        }

        private void V3ResponseTimeout_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            var input = e.Key.ToString();
            bool isDigitPresent = input.Any(c => char.IsDigit(c));
            if (!isDigitPresent)
            {
                e.Handled = true;
            }

        }

        private void V3DiscoveryTimeout_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            var input = e.Key.ToString();
            bool isDigitPresent = input.Any(c => char.IsDigit(c));
            if (!isDigitPresent)
            {
                e.Handled = true;
            }

        }
    }
}

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleSnmpClient.Core.Providers.Authentication;
using SimpleSnmpClient.Core.Providers.Requests;
using SimpleSnmpClient.Core.Services.Snmp.Agent;
using SimpleSnmpClient.ViewModels;
using SimpleSnmpClient.Views;
using Splat;
using System.Collections;
using System.Collections.Generic;

namespace SimpleSnmpClient
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {

            var auth = new AuthProviderFactory();
            var snmpRequest = new SnmpRequest(auth);

            Locator.CurrentMutable.RegisterConstant(auth, typeof(IAuthProviderFactory));
            Locator.CurrentMutable.RegisterConstant(snmpRequest, typeof(ISnmpRequest));
            Locator.CurrentMutable.RegisterConstant(new SnmpAgentService(snmpRequest), typeof(ISnmpAgentService));


            var snmpAgentService = Locator.Current.GetService<ISnmpAgentService>();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(snmpAgentService),
                };
            }

            
            base.OnFrameworkInitializationCompleted();
        }
    }
}

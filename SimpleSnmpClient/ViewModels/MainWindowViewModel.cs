using ReactiveUI;
using SimpleSnmpClient.Core.Services.Snmp.Agent;
using SimpleSnmpClient.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace SimpleSnmpClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public SnmpMainViewModel SnmpMainPanel { get; }

        public MainWindowViewModel(ISnmpAgentService snmpAgentService)
        {
            Content = SnmpMainPanel =  new SnmpMainViewModel(new List<SnmpPayLoad>(), snmpAgentService);
        }

        public void ShowSnmpOperation()
        {
            var vm = new SnmpOperationViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (SnmpPayLoad)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        SnmpMainPanel.Items.Add(model);
                    }
                    Content = SnmpMainPanel;
                });
            Content = vm;
        }
    }
}

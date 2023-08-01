using ReactiveUI;
using SimpleSnmpClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnmpClient.ViewModels
{
    public class SnmpOperationViewModel : ViewModelBase
    {
        private string? _oid;

        public string? Oid
        {
            get { return _oid; }
            set { this.RaiseAndSetIfChanged(ref _oid, value); }
        }

        private string? _value;

        public string? Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }


        public ReactiveCommand<Unit, SnmpPayLoad> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public SnmpOperationViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Oid,
                x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => new SnmpPayLoad { Oid = Oid, Value = Value },
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

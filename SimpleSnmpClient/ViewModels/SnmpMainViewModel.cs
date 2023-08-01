using Avalonia.Controls;
using Avalonia.Media;
using DynamicData;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.VisualBasic;
using ReactiveUI;
using SimpleSnmpClient.Core.DTO;
using SimpleSnmpClient.Core.Providers.Requests.RequestsParameters;
using SimpleSnmpClient.Core.Services.Snmp.Agent;
using SimpleSnmpClient.Data;
using SimpleSnmpClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleSnmpClient.ViewModels
{
    public class SnmpMainViewModel : ViewModelBase
    {
        private readonly ISnmpAgentService _snmpAgentService;

        private bool _getOperation = true;
        private RequestV2MessageParam _requestV2MessageParam;
        private RequestV3MessageParam _requestV3MessageParam;
        private bool _v2Checked;
        private bool _v3Checked = true;
        private bool _initializedList;
        private bool _anyLogs;
        private string _snmpDataType;

        private bool _profileStatusVisible;
        private string _profileStatusText;


        private string _oid = "1.3.6.1.4.1.9.2.1.2.0";
        private string _oidValue = "ExampleValue1";

        public ObservableCollection<SnmpPayLoad> Items { get; }
        public ObservableCollection<string> PrivProviders { get; }
        public ObservableCollection<string> AuthProviders { get; }
        public ObservableCollection<string> DataTypesProviders { get; }

        public RequestV2MessageParam SnmpAgentV2
        {
            get => _requestV2MessageParam;
            set => this.RaiseAndSetIfChanged(ref _requestV2MessageParam, value);
        }
        public RequestV3MessageParam SnmpAgentV3
        {
            get => _requestV3MessageParam;
            set => this.RaiseAndSetIfChanged(ref _requestV3MessageParam, value);
        }

        public bool GetOperation
        {
            get => _getOperation;
            set => this.RaiseAndSetIfChanged(ref _getOperation, value);
        }

        public bool V2Checked
        {
            get => _v2Checked;
            set => this.RaiseAndSetIfChanged(ref _v2Checked, value);
        }

        public bool V3Checked
        {
            get => _v3Checked;
            set => this.RaiseAndSetIfChanged(ref _v3Checked, value);
        }

        public string Oid
        {
            get => _oid;
            set => this.RaiseAndSetIfChanged(ref _oid, value);
        }

        public string OidValue
        {
            get => _oidValue;
            set => this.RaiseAndSetIfChanged(ref _oidValue, value);
        }

        public bool AnyLogs
        {
            get => _anyLogs;
            set => this.RaiseAndSetIfChanged(ref _anyLogs, value);
        }

        public string SnmpDataType
        {
            get => _snmpDataType;
            set => this.RaiseAndSetIfChanged(ref _snmpDataType, value);
        }

        public bool ProfileStatusVisible
        {
            get => _profileStatusVisible;
            set => this.RaiseAndSetIfChanged(ref _profileStatusVisible, value);
        }

        public string ProfileStatusText
        {
            get => _profileStatusText;
            set => this.RaiseAndSetIfChanged(ref _profileStatusText, value);
        }

        public ReactiveCommand<Unit, Unit> PerformAction { get; }
        public ReactiveCommand<Unit, Unit> ClearLogsActions { get; }

        public SnmpMainViewModel(IEnumerable<SnmpPayLoad> items, ISnmpAgentService snmpAgentService)
        {
            _snmpAgentService = snmpAgentService;
            string ipAddressTemp = "192.168.0.1"; int port = 161; int responseTimeout = 10000; int discoveryTimeout = 2000;
            string community = "public"; string authProvider = "MD5"; string privProvider = "DES";
             _requestV2MessageParam = new RequestV2MessageParam() { IpAddress = ipAddressTemp, Port = port, ResponseTimeout = responseTimeout,Community = community };
             _requestV3MessageParam = new RequestV3MessageParam() { IpAddress = ipAddressTemp, Port = port, ResponseTimeout = responseTimeout
                                                                   ,DiscoveryTimeout = discoveryTimeout,AuthProvider = authProvider, PrivProvider = privProvider};


            Items = new ObservableCollection<SnmpPayLoad>(items)
            {
                new SnmpPayLoad("Perform any Snmp Action", "")
            };
            _initializedList = true;

            PerformAction = ReactiveCommand.CreateFromTask(PerformSnmpOperation);
            ClearLogsActions = ReactiveCommand.Create(ClearLogs);

            PrivProviders = new ObservableCollection<string>(Providers.PRIVACYPROVIDER);
            AuthProviders = new ObservableCollection<string>(Providers.AUTHENTICATIONPROVIDERS);
            DataTypesProviders = new ObservableCollection<string>(Providers.DATATYPES);
        }


        async Task PerformSnmpOperation()
        {

            string operationPrefix = GetOperation ? "[GET]" : "[SET]";
            string oidWithPrefix = operationPrefix + ' ' + Oid;

            //Just remove initial text 
            if (_initializedList)
            {
                _initializedList = false;
                Items.Clear();
            }

            SnmpPayLoad initPayLoad = new SnmpPayLoad(oidWithPrefix, "Processing operation");
            Items.Add(initPayLoad);

            SnmpPayLoad snmpPayLoad;
            if (V2Checked)
            {
                try 
                {
                    var snmpPayLoadDto = await Task.Run(() => GetOperation ? _snmpAgentService.GetSnmpV2Request(_requestV2MessageParam, Oid) : _snmpAgentService.SetSnmpV2Request(_requestV2MessageParam, Oid, OidValue, SnmpDataType));
                    //ISnmpMessage meesage = GetOperation ? await _snmpAgentService.GetSnmpV2RequestAsync(_requestV2MessageParam, Oid)
                    //                                     : await _snmpAgentService.SetSnmpV2RequestAsync(_requestV2MessageParam, Oid, OidValue);
                    snmpPayLoad = Map(snmpPayLoadDto);
                }
                catch (Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    snmpPayLoad = new SnmpPayLoad(oidWithPrefix, ex.Message);
                }
                catch(Exception ex)
                {
                    snmpPayLoad = new SnmpPayLoad(oidWithPrefix, ex.Message);
                }
            }
            else
            {
                try
                {
                    var snmpPayLoadDto = await Task.Run(() => GetOperation ? _snmpAgentService.GetSnmpV3Request(_requestV3MessageParam, Oid) : _snmpAgentService.SetSnmpV3Request(_requestV3MessageParam, Oid, OidValue, SnmpDataType));
                    //ISnmpMessage meesage = GetOperation ? await _snmpAgentService.GetSnmpV3RequestAsync(_requestV3MessageParam, Oid)
                    //                                     : await _snmpAgentService.SetSnmpV3RequestAsync(_requestV3MessageParam, Oid, OidValue);
                    snmpPayLoad = Map(snmpPayLoadDto);
                }catch(Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
                {
                    snmpPayLoad = new SnmpPayLoad(oidWithPrefix, ex.Message);
                }
                catch (Exception ex)
                {
                    snmpPayLoad = new SnmpPayLoad(oidWithPrefix, ex.Message);
                }
            }

            snmpPayLoad.Oid = oidWithPrefix;
            Items.Replace(initPayLoad, snmpPayLoad);
            AnyLogs = true;
            //SnmpAgent agent = V2Checked ? SnmpAgentV2 : SnmpAgentV3;
//#
            //SnmpPayLoad initPayLoad = new SnmpPayLoad(Oid, "Processing operation");
            //Items.Add(initPayLoad);
            //SnmpPayLoad snmpPayLoad = await Task.Run(() => GetOperation ? agent.Get(Oid) : agent.Set(Oid, OidValue));
            //Items.Replace(initPayLoad, snmpPayLoad);
        }
        void ClearLogs()
        {
            Items.Clear();
            Items.Add(new SnmpPayLoad("Perform any Snmp Action", ""));
            _initializedList = true;
            AnyLogs = false;
        }

        void LoadProfile(string profileType)
        {
            //use this and create folder in this!
            string profilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + $"\\SnmpSimpleClient\\profile{profileType}.json";
            if(System.IO.File.Exists(profilePath))
            {
                _initializedList = false;
                string profileContent = File.ReadAllText(profilePath);
                SnmpProfileDto profileDto = JsonSerializer.Deserialize<SnmpProfileDto>(profileContent);
                SnmpAgentV2 = new RequestV2MessageParam(profileDto.V2IpAddress, profileDto.V2Port, profileDto.V2Community, profileDto.V2ResponseTimeout);
                SnmpAgentV3 = new RequestV3MessageParam(profileDto.V3IpAddress, profileDto.V3Port, profileDto.V3AuthPassword, profileDto.V3AuthProvider, profileDto.V3PrivPassword
                                                        , profileDto.V3PrivProvider, profileDto.V3UserName, profileDto.V3Context, profileDto.V3DiscoveryTimeout, profileDto.V3ResponseTimeout);
                Oid = profileDto.GetOid;
                OidValue = profileDto.SetValue;
                if(profileDto.Logs.Count() > 0)
                {
                    Items.Clear();
                    foreach (SnmpPayLoadDto item in profileDto.Logs)
                    {
                        var mappedItem = Map(item);
                        Items.Add(mappedItem);
                    }
                }

                ProfileStatusVisible = true;
                ProfileStatusText = $"Profile {profileType} loaded.";
            }
        }

        void SaveProfile(string profileType)
        {
            //use this and create folder in this!
            string configFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\SnmpSimpleClient";
            System.IO.Directory.CreateDirectory(configFolderPath);

            var profileDto = new SnmpProfileDto()
            {
                V2IpAddress = SnmpAgentV2.IpAddress,
                V2Port = SnmpAgentV2.Port,
                V2Community = SnmpAgentV2.Community,
                V2ResponseTimeout = SnmpAgentV2.ResponseTimeout,
                V3IpAddress = SnmpAgentV3.IpAddress,
                V3Port = SnmpAgentV3.Port,
                V3UserName = SnmpAgentV3.Username,
                V3Context = SnmpAgentV3.Context,
                V3AuthPassword = SnmpAgentV3.AuthPassword,
                V3AuthProvider = SnmpAgentV3.AuthProvider,
                V3PrivPassword = SnmpAgentV3.PrivPassword,
                V3PrivProvider = SnmpAgentV3.PrivProvider,
                V3DiscoveryTimeout = SnmpAgentV3.DiscoveryTimeout,
                V3ResponseTimeout = SnmpAgentV3.ResponseTimeout,
                GetOid = Oid,
                SetOid = Oid,
                SetValue = OidValue,
                Logs = Items.Select(a => MapDto(a)).ToList()
            };

            Console.WriteLine("dasd");
            
            string json = JsonSerializer.Serialize(profileDto);
            File.WriteAllText(configFolderPath + $"\\profile{profileType}.json", json);

            ProfileStatusVisible = true;
            ProfileStatusText = $"Profile {profileType} saved.";
        }

        private SnmpPayLoad Map(SnmpPayLoadDto snmpPayLoadDto)
        {
            return new SnmpPayLoad(snmpPayLoadDto.Oid, snmpPayLoadDto.Value);
        }

        private SnmpPayLoadDto MapDto(SnmpPayLoad snmpPayLoad)
        {
            return new SnmpPayLoadDto(snmpPayLoad.Oid, snmpPayLoad.Value);
        }
    }
}

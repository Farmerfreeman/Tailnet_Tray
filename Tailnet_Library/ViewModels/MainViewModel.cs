using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tailnet_Library.Models;
using Tailnet_Library.Services.Interfaces;


namespace Tailnet_Library.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ITailscaleService _tailscaleService;
        
        [ObservableProperty]
        private string connectionState = "Unknown";

        public ObservableCollection<TailscalePeer> Devices { get; } = new();
        
        public MainViewModel(ITailscaleService tailscaleService)
        {
            _tailscaleService = tailscaleService;
        }

        [RelayCommand]
        public async Task RefreshStatusAsync()
        {
            try
            {
                TailscaleStatus status = await _tailscaleService.GetStatusAsync();
                connectionState = status.backendState;
                Devices.Clear();
                if (status.peers != null)
                {
                    foreach (var peer in status.peers.Values)
                    {
                        Devices.Add(peer);
                    }
                }
            }
            catch (Exception ex)
            {
                ConnectionState = $"Error: {ex.Message}";
            }
        }
    }
}

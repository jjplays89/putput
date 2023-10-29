using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;

public class NetworkConnect : MonoBehaviour
{
    public string joinCode;
    public int maxConnection = 20;
    public UnityTransport transport;


    private async void Awake()
    {
        // Initialyzes player before starting the sign in
        await UnityService.Initializing.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }
    //Creating Relay
    public async void Create()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        //Join code
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.StartHost();
    }
    //Joing Relay
    public async void Join()
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

        NetworkManager.Singleton.StartClient();
    }
}

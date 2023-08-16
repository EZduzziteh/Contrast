using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class TestRelay : MonoBehaviour
{

    [SerializeField]
    [Tooltip("This is the max amount of players, not including the host. EX) set value to 1 for Host and 1 other player(2 players)")]
    int maxAmountOfPlayers = 1;

    [SerializeField]
    TMP_InputField joinCodeInputField;

    [SerializeField]
    TextMeshPro joinCodeDisplay;


    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();


    }

    private void Update()
    {
        Debug.Log(joinCodeInputField.text);
    }


    public void ConnectToRelay()
    {
        JoinRelay(joinCodeInputField.text);
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);
            string JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(JoinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData
            (
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();
            joinCodeDisplay.text = "Join Code: "+JoinCode;
        }catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void JoinRelay(string joinCode)
    {

        try
        {
            Debug.Log("Relay Connecting with code: " + joinCode);
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData
            (
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();

        }catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
     
}

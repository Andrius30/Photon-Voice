using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class NetworkManger : MonoBehaviourPunCallbacks
{
    public static NetworkManger Instance;

    [SerializeField] TextMeshProUGUI debugText;
    [SerializeField] int maxPlayers = 2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Log.InitLogger(debugText);
    }
    void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        Log.log($"Connected to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Log.log($"Joined lobby");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Log.SetColor(Color.red);
        Log.log($"Join random room failed");
        Log.ResetColor();
        RoomOptions opt = new RoomOptions();
        opt.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.CreateRoom("Test_Room", opt);
    }
    public override void OnJoinedRoom()
    {
        Log.log($"Joined random room successfully {PhotonNetwork.CurrentRoom.Name}.");
        Log.SetColor(Color.yellow);
        Log.log($" Spawn player!");
        Log.ResetColor();
        SpawnCharacter.onStartGameSpawnPlayer?.Invoke();
    }

    #region Photon Properties
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

    }
    #endregion
}

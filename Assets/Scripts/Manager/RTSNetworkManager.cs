using Mirror;
using UnityEngine;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject _unitSpawnerPrefab;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        var unitSpawner = Instantiate(_unitSpawnerPrefab, conn.identity.transform.position,
            conn.identity.transform.rotation);
        NetworkServer.Spawn(unitSpawner, conn);
    }
}
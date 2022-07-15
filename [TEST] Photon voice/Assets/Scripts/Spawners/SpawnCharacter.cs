using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCharacter : MonoBehaviour
{
    public static Action onStartGameSpawnPlayer;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<Transform> spawnPositionTransform;

    GameObject localPlayer;



    void SpawnPlayer()
    {
        if (localPlayer == null)
        {
            int rand = Random.Range(0, spawnPositionTransform.Count);
            localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPositionTransform[rand].position, Quaternion.identity);
        }
    }

    void OnEnable()
    {
        onStartGameSpawnPlayer += SpawnPlayer;
    }
    void OnDisable()
    {
        onStartGameSpawnPlayer -= SpawnPlayer;
    }
}

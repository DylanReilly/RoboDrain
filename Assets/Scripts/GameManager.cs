using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public enum Winner { Survivor, Hunter}

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public bool gameEnded = false;  // has the game ended?

    [Header("Players")]
    public string survivorPrefabLocation;
    public string hunterPrefabLocation;
    public Transform[] spawnPoints;
    //public PlayerController[] players;
    private int playersInGame;
    private List<int> pickedSpawnIndexTop;
    private List<int> pickedSpawnIndexBot;
    public float energyPool = 1000f;
    public int survivorLives = 3;




    // instance
    public static GameManager instance;

    private void Awake()
    {
        //instance
        instance = this;

    }

    private void Start()
    {
        pickedSpawnIndexTop = new List<int>();
        pickedSpawnIndexBot = new List<int>();
        //players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    private void Update()
    {

        //pingUI.text = "Cloud Region: " + PhotonNetwork.CloudRegion +
        //            "\n Ping: " + PhotonNetwork.GetPing().ToString() + "ms";
    }




    [PunRPC]
    void ImInGame()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    void SpawnPlayer()
    {
        string playerPrefab = survivorPrefabLocation;
        int index = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            playerPrefab = hunterPrefabLocation;
            index = 1;
        }

        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab, spawnPoints[index].position, Quaternion.identity);
        //PlayerController playerScript = playerObject.GetComponent<PlayerController>();
        //playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);


    }

    [PunRPC]
    void DestroyTarget(string targetName)
    {
        GameObject target = GameObject.Find(targetName);
        if (target)
        {
            target.SetActive(false);
        }
        else
        {
            print("target not found");
        }
    }

    public void depletesEnergy(float energyTaken)
    {
        this.energyPool -= energyTaken;
        if(this.energyPool <= 0)
        {
            gameOver(Winner.Survivor);
        }
    }

    public void onSurvivorDestroyed()
    {
        survivorLives--;
        if(survivorLives == 0)
        {
            gameOver(Winner.Hunter);
        }
    }

    public void gameOver(Winner winner)
    {
        switch (winner)
        {
            case Winner.Hunter:
                break;
            case Winner.Survivor:
                break;
        }

        this.gameEnded = true;
    }

//    public PlayerController GetPlayer(int playerId)
//    {
//        return players.First(x => x.id == playerId);
//    }

//    public PlayerController GetPlayer(GameObject playerObj)
//    {
//        return players.First(x => x.gameObject == playerObj);
//    }

//    public PlayerController GetPlayer(string nickname)
//    {
//        return players.First(x => x.transform.name == nickname);
//    }
}

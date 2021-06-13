using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using TMPro;

public enum Winner { Survivor, Hunter}

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public bool gameEnded = false;  // has the game ended?

    [Header("Players")]
    public string survivorPrefabLocation;
    public string hunterPrefabLocation;
    public Transform[] spawnPoints;
    public float energyPool = 500f;
    public TextMeshProUGUI energyLeft;
    public TextMeshProUGUI survivorLiveLeft;
    public TextMeshProUGUI winScreen;
    //public PlayerController[] players;
    private int playersInGame;
    private List<int> pickedSpawnIndexTop;
    private List<int> pickedSpawnIndexBot;
    
    private int survivorLives = 1;




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

        energyLeft.text = "Hunter Energy Left: " + energyPool.ToString();
        survivorLiveLeft.text = "Survivor Live: " + survivorLives.ToString();
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

    [PunRPC]
    public void depletesEnergy(float energyTaken)
    {
        this.energyPool -= energyTaken;
        if(this.energyPool <= 0)
        {
            photonView.RPC("gameOver", RpcTarget.All,Winner.Survivor);
        }
    }

    [PunRPC]
    public void onSurvivorDestroyed()
    {
        survivorLives--;
        if(survivorLives == 0)
        {
            photonView.RPC("gameOver", RpcTarget.All, Winner.Hunter);
        }
    }

    [PunRPC]
    public void gameOver(Winner winner)
    {
        switch (winner)
        {
            case Winner.Hunter:
                winScreen.text = "Hunter Wins!!";
                print("Hunter Wins!");
                
                break;
            case Winner.Survivor:
                winScreen.text = "Survivor Wins!!";
                print("Survivor Wins!");
                break;
        }

        this.gameEnded = true;
        Application.Quit();
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

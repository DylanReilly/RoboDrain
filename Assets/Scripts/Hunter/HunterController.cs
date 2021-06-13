using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class HunterController : MonoBehaviourPunCallbacks
{

    public SurvivorController survivor;
    public GameObject camera;
    public ThirdPersonMovement moveController;

    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            camera.GetComponent<CinemachineFreeLook>().Priority = 11;
            camera.SetActive(false);
            moveController.enabled = false;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(survivor != null)
        {
            if (survivor.GetComponent<SurvivorController>().killAble)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    survivor.photonView.RPC("destroy", Photon.Pun.RpcTarget.All);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChargePoint : MonoBehaviourPunCallbacks
{

    public bool isActive;
    public float energyAvailable;
    public bool successCharge;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        successCharge = false;
        energyAvailable = 50f;
    }

    // Update is called once per frame
    void Update()
    {

        if (successCharge && isActive) 
        {
            object[] param = { energyAvailable };
            GameManager.instance.photonView.RPC("depletesEnergy", RpcTarget.All, param);
            isActive = false;
        }
    }
}

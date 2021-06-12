using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePoint : MonoBehaviour
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
            GameManager.instance.depletesEnergy(energyAvailable);
            isActive = false;
        }
    }
}

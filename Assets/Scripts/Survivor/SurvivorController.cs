using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    // Start is called before the first frame update
    public float startEnergy;
    public float currentEnergy;
    public bool killAble;

    private bool onChargingPoint;
    private float currentSpeed;
    private ChargePoint targetChargingPoint;
    private float chargingTime = 5;

    void Start()
    {
        currentEnergy = startEnergy/2;
        killAble = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(survivorRB.velocity.magnitude);
        currentSpeed = this.gameObject.GetComponent<FirstPersonMovement>().currentSpeed;
        currentEnergy -= currentSpeed * 0.01f;



        if (onChargingPoint)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (this.targetChargingPoint.isActive)
                {
                    if (this.currentEnergy <= (this.startEnergy - this.targetChargingPoint.energyAvailable))
                    {
                        if (chargingTime > 0)
                        {
                            print("charging energy, keep hold E ");
                            chargingTime -= Time.deltaTime;
                        }
                        else
                        {
                            print("Charging Success!");
                            currentEnergy += this.targetChargingPoint.energyAvailable;
                            this.targetChargingPoint.successCharge = true;

                            if (currentEnergy > startEnergy)
                            {
                                currentEnergy = startEnergy;
                            }
                        }
                    }
                }   
            }
        }
    }

    public void destroy()
    {
        //run blow up animation
        this.gameObject.SetActive(false);
        GameManager.instance.onSurvivorDestroyed();
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "ChargePoint")
        {
            //if enter trigger volume for charge point
            this.onChargingPoint = true;
            this.targetChargingPoint = other.gameObject.GetComponent<ChargePoint>();
            print("collide with charge point");

        }
        else if(other.gameObject.tag == "HunterKillRange")
        {
            other.gameObject.transform.parent.GetComponent<HunterController>().survivor = this;
            this.killAble = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ChargePoint")
        {

            this.targetChargingPoint = null;
            this.onChargingPoint = false;

        }
        else if (other.gameObject.tag == "HunterKillRange")
        {
            other.gameObject.transform.parent.GetComponent<HunterController>().survivor = null;
            this.killAble = false;
        }
    }


}

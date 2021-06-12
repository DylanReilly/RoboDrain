using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody survivorRB;
    public float startEnergy;
    public float currentEnergy;
    private bool onChargingPoint;
    private float currentSpeed;

    void Start()
    {
        currentEnergy = startEnergy;

    }

    // Update is called once per frame
    void Update()
    {
        //print(survivorRB.velocity.magnitude);
        currentSpeed = this.gameObject.GetComponent<FirstPersonMovement>().currentSpeed;
        print(currentSpeed);
        currentEnergy -= currentSpeed * 0.01f;



        if (onChargingPoint)
        {
            if (Input.GetKey(KeyCode.E))
            {
                print("charging");
                if (this.currentEnergy <= this.startEnergy)
                {
                    print("currentEnergy: " + currentEnergy);
                    this.currentEnergy += 0.3f;
                    if (currentEnergy > startEnergy)
                    {
                        currentEnergy = startEnergy;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "ChargingPoint")
        {
            this.onChargingPoint = true;
            print("collide");

        }
        else
        {
            this.onChargingPoint = false;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{

    public SurvivorController survivor;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    survivor.destroy();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRotation : MonoBehaviour
{
    [SerializeField] List<ChargePoint> chargePoints = new List<ChargePoint>();
    [SerializeField] float maxCharge;
    [SerializeField] float currentCharge;
    [SerializeField] float baseRotationSpeed;

    private void Start()
    {
        currentCharge = 0f;
        maxCharge = 0f;

        foreach(ChargePoint chargePoint in chargePoints) 
        {
            currentCharge += chargePoint.energyAvailable;
        }
        maxCharge = currentCharge;
    }
    void Update()
    {
        float rotationMultiplier = currentCharge / maxCharge;
        transform.Rotate(0, 0, baseRotationSpeed * rotationMultiplier * Time.deltaTime);
    }
}

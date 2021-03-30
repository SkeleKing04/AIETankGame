using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPU : MonoBehaviour
{
    public float speedIncrease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankMovement tank = other.gameObject.GetComponent<tankMovement>();
            tank.IncreaseSpeed(speedIncrease);
            Destroy(gameObject);
        }
    }
}

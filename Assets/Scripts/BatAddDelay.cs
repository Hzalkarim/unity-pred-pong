using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAddDelay : MonoBehaviour
{
    public Rigidbody driver;
    private BatManager batManager;
    private float hitDelay;
    private int index;

    private void Start()
    {
        batManager = BatManager.instance;
        SmartBat[] bats = batManager.bats;
        for (int i = 0; i < bats.Length; i++)
        {
            if (bats[i].bat == driver)
            {
                index = i;
                Debug.Log("Find self: " + this.gameObject.name);
                break;
            }
        }

        hitDelay = batManager.bats[index].HitDelay;
    }

    private void OnCollisionEnter(Collision collision)
    {
        batManager.bats[index].HitDelay = hitDelay;
        Debug.Log("Hit delay : " + hitDelay);
    }
}

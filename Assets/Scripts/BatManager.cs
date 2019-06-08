using System.Linq;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    #region Singleton

    public static BatManager instance;

    #endregion

    public Transform disk;
    public ForceMode forceMode;
    public SmartBat[] bats;

    private int batCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        batCount = bats.Length;
        GetComponent<BatPlayerController>().onChangeBatCallBack += OnChangeBat;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < batCount; i++)
        {
            if (!bats[i].playerControlled && canChase(bats[i]) && shouldChase(bats[i], out float chaseForce)){
                bats[i].bat.AddForce(chaseForce, 0, 0, forceMode);
            }
            bats[i].HitDelay -= Time.deltaTime;
        }
    }

    private bool shouldChase(SmartBat bat, out float chaseForce)
    {
        float xPos = bat.bat.transform.position.x;
        if (xPos + bat.tolerance < disk.position.x)
        {
            chaseForce = bat.force;
            return true;
        }
        else if (xPos - bat.tolerance > disk.position.x)
        {
            chaseForce = -bat.force;
            return true;
        }
        else
        {
            chaseForce = 0;
            return false;
        }
    }

    private bool canChase(SmartBat bat)
    {
        bool inView = Mathf.Abs(bat.bat.transform.position.z - disk.position.z) <= bat.fieldOfView;
        bool notOnDelay = bat.HitDelay == 0;
        return inView && notOnDelay;
    }

    private void OnChangeBat(Rigidbody oldBat, Rigidbody newBat)
    {
        int count = bats.Length;
        for (int i = 0; i < count; i++)
        {
            if (bats[i].bat == oldBat)
            {
                bats[i].playerControlled = false;
                continue;
            }

            if (bats[i].bat == newBat)
                bats[i].playerControlled = true;
        }
    }
}

[System.Serializable]
public struct SmartBat
{
    public Rigidbody bat;
    public float force;
    public float tolerance;
    public float fieldOfView;
    public float hitDelay;
    public bool playerControlled;

    public float HitDelay
    {
        get
        {
            return hitDelay;
        }

        set
        {
            if (value < 0)
            {
                hitDelay = 0f;
            }
            else
            {
                hitDelay = value;
            }
        }
    }
}

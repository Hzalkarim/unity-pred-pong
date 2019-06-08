using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangeBat(Rigidbody oldBat, Rigidbody newBat);

public class BatPlayerController : MonoBehaviour
{
    public SmartBat player;
    public ForceMode forceMode;
    public bool backView = false;
    public bool invert = false;
    public event OnChangeBat onChangeBatCallBack;

    private Rigidbody oldBat;
    [SerializeField] private Rigidbody newBat;

    private void Start()
    {
        int count = BatManager.instance.bats.Length;
        for (int i = 0; i < count; i++)
        {
            if (BatManager.instance.bats[i].bat == player.bat)
            {
                BatManager.instance.bats[i].playerControlled = true;
                oldBat = player.bat;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeBatControlled();
            return;
        }

        float v = Input.GetAxis(backView ? "Horizontal" : "Vertical") * (invert ? -1 : 1) * player.force * Time.deltaTime;
        if (v != 0)
            player.bat.AddForce(v, 0, 0, forceMode);

        if (Input.GetKeyDown(KeyCode.R)) invert = !invert;
        if (Input.GetKeyDown(KeyCode.T)) backView = !backView;
        
    }

    private void ChangeBatControlled()
    {
        if (newBat != null && newBat != oldBat)
        {
            onChangeBatCallBack?.Invoke(oldBat, newBat);

            player.bat = newBat;
            newBat = oldBat;
            oldBat = player.bat;
            Debug.Log("Control Changed");
        }
    }
}

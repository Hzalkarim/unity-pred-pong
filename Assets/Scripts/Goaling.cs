using UnityEngine.UI;
using UnityEngine;
using System;

public class Goaling : MonoBehaviour
{
    public Server opponentTeam;
    public Text opponentScore;
    public Transform opponentStartPoint;
    public event Action<Text> OnGoalCallBack;

    private PlayDisk disk;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (disk == null) disk = collision.gameObject.GetComponentInParent<PlayDisk>();
        OnGoalCallBack?.Invoke(opponentScore);
        disk.ResetDisk(opponentTeam, opponentStartPoint);
        Debug.Log("Goal");
    }
}

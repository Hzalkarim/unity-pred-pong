using UnityEngine.UI;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public Goaling[] goals;
    private void Start()
    {
        int count = goals.Length;
        for (int i = 0; i < count; i++)
        {
            goals[i].OnGoalCallBack += IncreaseScore;
        }
    }

    public static void IncreaseScore(Text team)
    {
        if (int.TryParse(team.text, out int hhe))
        {
            team.text = (hhe + 1).ToString();
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Failed");
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using DancingLineFanmade.Level;

public class LocalPanel : MonoBehaviour
{
    public Text userName;
    public Text gemCount;
    public Text percentageShow;
    public Text scoreShow;
    public Text gradeShow;

    internal Photon.Realtime.Player player;
    internal int checkpointInt;
    internal int gemCountInt;
    internal int scoreInt;
    internal int percentageInt;
    internal int minusedScoreInt;
    internal int addScoreInt;
    internal string gradeString;

    private void Update()
    {
        player = PhotonNetwork.LocalPlayer;

        if (player.NickName != userName.text)
            userName.text = player.NickName;

        // 检查玩家颜色
        if (player.CustomProperties.ContainsKey("ColorR") && player.CustomProperties.ContainsKey("ColorG") && player.CustomProperties.ContainsKey("ColorB"))
        {
            GetComponent<Image>().color = new Color((int)player.CustomProperties["ColorR"] / 255f, (int)player.CustomProperties["ColorG"] / 255f, (int)player.CustomProperties["ColorB"] / 255f, 0.5f);
        }

        gemCountInt = int.Parse(player.CustomProperties["GemCount"].ToString());
        percentageInt = int.Parse(player.CustomProperties["Percentage"].ToString());

        gemCount.text = $"{gemCountInt} / 10";
        percentageShow.text = $"{percentageInt}%";

        minusedScoreInt = player.CustomProperties.ContainsKey("MinusedScore") ? int.Parse(player.CustomProperties["MinusedScore"].ToString()) : 0;
        addScoreInt = player.CustomProperties.ContainsKey("AddScore") ? int.Parse(player.CustomProperties["AddScore"].ToString()) : 0;

        bool isGuidenceEnabled;
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Guidence"))
            isGuidenceEnabled = (bool)PhotonNetwork.LocalPlayer.CustomProperties["Guidence"];
        else
            isGuidenceEnabled = false;

        float score = (percentageInt * 50 + gemCountInt * 100 - minusedScoreInt) * (isGuidenceEnabled ? 0.8f : 1f);
        scoreInt = (int)score;

        if (percentageInt >= 100) scoreInt += 1000;

        scoreShow.text = scoreInt.ToString();

        switch (scoreInt)
        {
            case int n when n >= 9800:
                gradeString = "EX+";
                break;
            case int n when n >= 9000:
                gradeString = "EX";
                break;
            case int n when n >= 8000:
                gradeString = "A+";
                break;
            case int n when n >= 7000:
                gradeString = "A";
                break;
            case int n when n >= 6000:
                gradeString = "B+";
                break;
            case int n when n >= 5000:
                gradeString = "B";
                break;
            case int n when n >= 4000:
                gradeString = "C";
                break;
            case int n when n >= 3000:
                gradeString = "D";
                break;
            case int n when n >= 2000:
                gradeString = "E";
                break;
            default:
                gradeString = "F";
                break;
        }

        gradeShow.text = gradeString;

        if (player.CustomProperties.ContainsKey("Grade"))
            player.CustomProperties["Grade"] = gradeString;
        else
            player.CustomProperties.Add("Grade", gradeString);

        if (player.CustomProperties.ContainsKey("Score"))
            player.CustomProperties["Score"] = scoreInt;
        else
            player.CustomProperties.Add("Score", scoreInt);
    }
}

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

    internal int checkpointInt;
    internal int gemCountInt;
    internal int scoreInt;
    internal int percentageInt;
    internal string gradeString;

    private void Update()
    {
        if (PhotonNetwork.LocalPlayer.NickName != userName.text)
            userName.text = PhotonNetwork.LocalPlayer.NickName;

        float ColorR = PhotonNetwork.LocalPlayer.CustomProperties["ColorR"].ToString() == "" ? 1 : float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["ColorR"].ToString());
        float ColorG = PhotonNetwork.LocalPlayer.CustomProperties["ColorG"].ToString() == "" ? 1 : float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["ColorG"].ToString());
        float ColorB = PhotonNetwork.LocalPlayer.CustomProperties["ColorB"].ToString() == "" ? 1 : float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["ColorB"].ToString());

        Color theColor = new Color(ColorR, ColorG, ColorB, 0.5f);

        GetComponent<Image>().color = theColor;
        gemCountInt = int.Parse(PhotonNetwork.LocalPlayer.CustomProperties["GemCount"].ToString());
        percentageInt = int.Parse(PhotonNetwork.LocalPlayer.CustomProperties["Percentage"].ToString());
        checkpointInt = int.Parse(PhotonNetwork.LocalPlayer.CustomProperties["Checkpoint"].ToString());

        gemCount.text = $"{gemCountInt} / 10";
        percentageShow.text = $"{percentageInt}%";

        scoreInt = percentageInt * 50 + gemCountInt * 100 + (checkpointInt >= 0 ? checkpointInt : 0) * 1000;

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

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Grade"))
            PhotonNetwork.LocalPlayer.CustomProperties["Grade"] = gradeString;
        else
            PhotonNetwork.LocalPlayer.CustomProperties.Add("Grade", gradeString);

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
            PhotonNetwork.LocalPlayer.CustomProperties["Score"] = scoreInt;
        else
            PhotonNetwork.LocalPlayer.CustomProperties.Add("Score", scoreInt);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using DancingLineFanmade.Level;
using Photon.Realtime;

public class PlayerDash : MonoBehaviour
{
    public Photon.Realtime.Player player;
    public Text gemCountShow;
    public Text percentageShow;
    public Text scoreShow;
    public Text gradeShow;

    internal int gemCount;
    internal int percentage;
    internal int score;
    internal string grade;

    private void Start()
    {
        // 初始化
        if (player.CustomProperties.ContainsKey("GemCount") == false)
            player.CustomProperties.Add("GemCount", 0);
        if (player.CustomProperties.ContainsKey("Percentage") == false)
            player.CustomProperties.Add("Percentage", 0);
        if (player.CustomProperties.ContainsKey("Score") == false)
            player.CustomProperties.Add("Score", 0);
        if (player.CustomProperties.ContainsKey("Grade") == false)
            player.CustomProperties.Add("Grade", "F");

    }
    private void Update()
    {
        gemCount = int.Parse(player.CustomProperties["GemCount"].ToString());
        percentage = int.Parse(player.CustomProperties["Percentage"].ToString());
        score = int.Parse(player.CustomProperties["Score"].ToString());
        grade = player.CustomProperties["Grade"].ToString();

        float ColorR = player.CustomProperties["ColorR"].ToString() == "" ? 1 : float.Parse(player.CustomProperties["ColorR"].ToString());
        float ColorG = player.CustomProperties["ColorG"].ToString() == "" ? 1 : float.Parse(player.CustomProperties["ColorG"].ToString());
        float ColorB = player.CustomProperties["ColorB"].ToString() == "" ? 1 : float.Parse(player.CustomProperties["ColorB"].ToString());

        GetComponent<Image>().color = new Color(ColorR, ColorG, ColorB, 0.5f);

        gemCountShow.text = $"{gemCount}";
        percentageShow.text = $"{percentage}%";
        scoreShow.text = $"{score}";
        gradeShow.text = grade;
    }
}

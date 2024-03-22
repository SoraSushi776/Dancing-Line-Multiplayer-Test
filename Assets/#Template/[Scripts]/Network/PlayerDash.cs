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

    internal int lastScore;


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
        if (player.CustomProperties.ContainsKey("MinusedScore") == false)
            player.CustomProperties.Add("MinusedScore", 0);

    }
    private void Update()
    {
        gemCount = int.Parse(player.CustomProperties["GemCount"].ToString());
        percentage = int.Parse(player.CustomProperties["Percentage"].ToString());
        score = int.Parse(player.CustomProperties["Score"].ToString());
        grade = player.CustomProperties["Grade"].ToString();

        // 检查玩家颜色
        if (player.CustomProperties.ContainsKey("ColorR") && player.CustomProperties.ContainsKey("ColorG") && player.CustomProperties.ContainsKey("ColorB"))
        {
            GetComponent<Image>().color = new Color((int)player.CustomProperties["ColorR"] / 255f, (int)player.CustomProperties["ColorG"] / 255f, (int)player.CustomProperties["ColorB"] / 255f, 0.5f);
        }

        gemCountShow.text = $"{gemCount} / 10";

        percentageShow.text = $"{percentage}%";

        gradeShow.text = grade;

        if (score != lastScore)
        {
            scoreShow.text = score.ToString();
            lastScore = score;
        }
    }
}

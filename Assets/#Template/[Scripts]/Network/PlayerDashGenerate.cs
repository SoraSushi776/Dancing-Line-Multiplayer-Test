using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDashGenerate : MonoBehaviour
{
    private void Start()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal)
                continue;

            GameObject dash = Resources.Load<GameObject>("Prefabs/PlayerInfo");
            GameObject dashInstance = Instantiate(dash, transform);

            dashInstance.GetComponent<PlayerDash>().player = player;
        }
    }
}

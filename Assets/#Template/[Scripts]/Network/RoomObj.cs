using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using DG.Tweening;

public class RoomObj : MonoBehaviour
{
    public Text roomInfo;

    public void SetRoomInfo(string roomName, int playerCount, int maxPlayers, GameObject roomListContent, Vector3 roomListContentPos)
    {
        // 设置房间信息
        roomInfo.text = roomName + " (" + playerCount + "/" + maxPlayers + ")";

        // 添加按钮事件
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            // 加入房间
            PhotonNetwork.JoinRoom(roomName);
            roomListContent.transform.DOLocalMoveY(roomListContentPos.y - 100, 0.2f);
            roomListContent.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.2f);
            roomListContent.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() => roomListContent.SetActive(false));
        });
    }
}

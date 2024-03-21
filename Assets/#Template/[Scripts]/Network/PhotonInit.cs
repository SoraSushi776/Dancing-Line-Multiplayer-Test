using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using ExitGames.Client.Photon;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    // 场景变量
    public GameObject loginPanel;
    public GameObject roomListPanel;
    public GameObject createRoomPanel;
    public GameObject roomPanel;
    public GameObject roomContent;
    public GameObject playerListContent;
    public GameObject roomJoinPanel;
    public GameObject forceStart;
    public InputField userNameInput;
    public InputField roomNameInput;
    public InputField roomJoinInput;
    public Toggle isPrivateToggle;
    public Text userNameShow;
    public Text roomNameShow;
    public Text statusShow;
    public Text playerCountShow;
    public Text roomListInfoText;
    public Image blackMask;

    // 脚本变量
    public StatusText statusText;
    public Sprite windowsSprite;
    public Sprite appleSprite;
    public Sprite androidSprite;
    public Sprite linuxSprite;
    internal string userName { get; private set; }
    internal Vector3 loginPanelPosition { get; private set; }
    internal Vector3 roomPanelPosition { get; private set; }
    internal Vector3 createRoomPanelPosition { get; private set; }
    internal Vector3 roomListPanelPosition { get; private set; }
    internal Vector3 roomJoinPanelPosition { get; private set; }
    internal bool buttonCanClick = true;


    void Start()
    {
        // 显示登录面板
        loginPanel.SetActive(true);
        loginPanel.GetComponent<CanvasGroup>().alpha = 1;
        loginPanel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        loginPanelPosition = loginPanel.transform.localPosition;

        roomPanel.SetActive(false);
        roomPanel.GetComponent<CanvasGroup>().alpha = 0;
        roomPanel.transform.localRotation = Quaternion.Euler(0, 0, 5);
        roomPanelPosition = roomPanel.transform.localPosition;
        roomPanel.transform.localPosition = new Vector3(roomPanelPosition.x, roomPanelPosition.y - 150, roomPanelPosition.z);

        createRoomPanel.SetActive(false);
        createRoomPanel.GetComponent<CanvasGroup>().alpha = 0;
        createRoomPanel.transform.localRotation = Quaternion.Euler(0, 0, 5);
        createRoomPanelPosition = createRoomPanel.transform.localPosition;
        createRoomPanel.transform.localPosition = new Vector3(createRoomPanelPosition.x, createRoomPanelPosition.y - 150, createRoomPanelPosition.z);

        roomListPanel.SetActive(false);
        roomListPanel.GetComponent<CanvasGroup>().alpha = 0;
        roomListPanel.transform.localRotation = Quaternion.Euler(0, 0, 5);
        roomListPanelPosition = roomListPanel.transform.localPosition;
        roomListPanel.transform.localPosition = new Vector3(roomListPanelPosition.x, roomListPanelPosition.y - 150, roomListPanelPosition.z);

        roomJoinPanel.SetActive(false);
        roomJoinPanel.GetComponent<CanvasGroup>().alpha = 0;
        roomJoinPanelPosition = roomJoinPanel.transform.localPosition;
        roomJoinPanel.transform.localPosition = new Vector3(roomJoinPanelPosition.x, roomJoinPanelPosition.y - 500, roomJoinPanelPosition.z);

        blackMask.gameObject.SetActive(false);
        blackMask.color = new Color(0, 0, 0, 0);
        userNameShow.text = "";

        // 连接状态
        statusShow.text = statusText.On_WAITING;

        Debug.Log("准备就绪");
    }

    void Update()
    {
        if (roomContent.transform.childCount == 0)
        {
            roomListInfoText.text = "没有房间哦！\n快去创建一个吧！";
        }
        else
        {
            roomListInfoText.text = "";
        }
    }

    // 登录并连接到Photon服务器
    public void Connect()
    {
        if (!buttonCanClick) return;
        buttonCanClick = false;

        // 校验用户名
        if (userNameInput.text.Length > 0)
        {
            userName = userNameInput.text;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            userName = "Player" + UnityEngine.Random.Range(0, 1000);
            PhotonNetwork.ConnectUsingSettings();
        }
        Debug.Log("用户名：" + userName);

        // 更新连接状态
        statusShow.text = statusText.On_CONNECTING_TO_MASTER;

        // 隐藏登录面板
        loginPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        loginPanel.transform.DOLocalMoveY(loginPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        loginPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => loginPanel.SetActive(false));

        buttonCanClick = true;
    }

    // 退出登录
    public void Logout()
    {
        if (!buttonCanClick) return;
        buttonCanClick = false;

        // 断开连接
        PhotonNetwork.Disconnect();

        // 清空玩家信息
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();

        // 更新连接状态
        statusShow.text = statusText.On_WAITING;

        // 隐藏房间面板
        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomListPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomListPanel.SetActive(false));

        // 显示登录面板
        loginPanel.SetActive(true);
        loginPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        loginPanel.transform.DOLocalMoveY(loginPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        loginPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);

        buttonCanClick = true;

    }

    // 显示创建房间面板
    public void ShowCreateRoomPanel()
    {
        if (!buttonCanClick) return;
        buttonCanClick = false;

        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomPanel.SetActive(false));

        createRoomPanel.SetActive(true);
        createRoomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.transform.DOLocalMoveY(createRoomPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc).onComplete = () => buttonCanClick = true;
    }

    // 创建并加入房间
    public void CreateRoom()
    {
        if (!buttonCanClick) return;
        buttonCanClick = false;

        // 隐藏创建房间面板
        createRoomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.transform.DOLocalMoveY(createRoomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => createRoomPanel.SetActive(false));

        // 校验房间名
        if (roomNameInput.text.Length > 0)
        {
            roomNameInput.text = roomNameInput.text + " (" + UnityEngine.Random.Range(0, 1000) + ")";
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = 4,
                IsVisible = isPrivateToggle.isOn,
                IsOpen = true

            };
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
        }
        else
        {
            roomNameInput.text = PhotonNetwork.LocalPlayer.NickName + " (" + UnityEngine.Random.Range(0, 1000) + ")";
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = 4,
                IsVisible = isPrivateToggle.isOn,
                IsOpen = true,
            };
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
        }

        // 更新连接状态
        statusShow.text = statusText.On_JOINING;

        buttonCanClick = true;
    }

    // 显示加入房间面板
    public void ShowJoinRoomPanel()
    {
        roomJoinPanel.SetActive(true);
        blackMask.gameObject.SetActive(true);
        blackMask.DOFade(0.5f, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.OutCirc);
        roomJoinPanel.transform.DOLocalMoveY(roomJoinPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        roomJoinPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
    }

    // 隐藏加入面板
    public void HideJoinRoomPanel()
    {
        blackMask.DOFade(0, 0.5f).SetEase(Ease.OutCirc).onComplete = () => blackMask.gameObject.SetActive(false);
        roomListPanel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutCirc);
        roomJoinPanel.transform.DOLocalMoveY(roomJoinPanelPosition.y - 500, 0.5f).SetEase(Ease.OutCirc);
        roomJoinPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomJoinPanel.SetActive(false));
    }

    // 手动输入房间名加入
    public void JoinRoom()
    {
        // 校验房间名
        if (roomJoinInput.text.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomJoinInput.text);
            HideJoinRoomPanel();

            // 隐藏房间面板
            roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
            roomListPanel.transform.DOLocalMoveY(roomListPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
            roomListPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomListPanel.SetActive(false));
        }
        else
        {
            statusShow.text = "请输入房间名";
        }

        // 更新连接状态
        statusShow.text = statusText.On_JOINING;
    }

    // 离开房间
    public void LeaveRoom()
    {
        if (!buttonCanClick) return;
        buttonCanClick = false;

        PhotonNetwork.LeaveRoom();

        // 更新连接状态
        statusShow.text = statusText.On_WAITING;

        // 清空玩家信息
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();

        // 销毁玩家列表
        foreach (Transform child in playerListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // 如果是房主
        if (PhotonNetwork.IsMasterClient)
        {
            // 如果房间还有人，重新分配房主
            if (PhotonNetwork.PlayerList.Length > 0)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
            }
            // 否则销毁
            else
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.RemovedFromList = true;
                PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
                PhotonNetwork.CurrentRoom.PlayerTtl = 0;
            }
        }

        // 隐藏房间面板
        roomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        roomPanel.transform.DOLocalMoveY(roomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        roomPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomPanel.SetActive(false));

        // 显示房间面板
        roomListPanel.SetActive(true);
        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
    }

    // 准备
    public void Ready()
    {
        // 设置准备状态
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Ready"] = !(bool)PhotonNetwork.LocalPlayer.CustomProperties["Ready"];
        }
        else
        {
            PhotonNetwork.LocalPlayer.CustomProperties.Add("Ready", true);
        }

        // 更新玩家列表
        UpdatePlayerList();

        // 让其他玩家知道
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }

    // 强制开始游戏
    public void ForceStart()
    {
        blackMask.gameObject.SetActive(true);
        blackMask.DOFade(1, 0.5f).SetEase(Ease.OutCirc);

        StartGame();
    }

    // 加入房间成功
    public override void OnJoinedRoom()
    {
        // 更新连接状态
        statusShow.text = statusText.On_JOINED;

        // 显示房间信息
        roomNameShow.text = PhotonNetwork.CurrentRoom.Name;

        // 添加玩家信息
        var thePlayerHash = PhotonNetwork.LocalPlayer.CustomProperties;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.Windows:
                thePlayerHash.Add("OS", "Windows");
                break;
            case OperatingSystemFamily.MacOSX:
                thePlayerHash.Add("OS", "Apple");
                break;
            case OperatingSystemFamily.Linux:
                thePlayerHash.Add("OS", "Linux");
                break;
            case OperatingSystemFamily.Other:
                thePlayerHash.Add("OS", "Android");
                break;
        }

        if (thePlayerHash.ContainsKey("ColorR") || thePlayerHash.ContainsKey("ColorG") || thePlayerHash.ContainsKey("ColorB"))
        {
            thePlayerHash.Remove("ColorR");
            thePlayerHash.Remove("ColorG");
            thePlayerHash.Remove("ColorB");
        }

        thePlayerHash.Add("ColorR", UnityEngine.Random.Range(0, 255));
        thePlayerHash.Add("ColorG", UnityEngine.Random.Range(0, 255));
        thePlayerHash.Add("ColorB", UnityEngine.Random.Range(0, 255));

        PhotonNetwork.LocalPlayer.SetCustomProperties(thePlayerHash);

        // 显示房间面板 
        createRoomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.transform.DOLocalMoveY(createRoomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => createRoomPanel.SetActive(false));

        roomPanel.SetActive(true);
        roomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        roomPanel.transform.DOLocalMoveY(roomPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        roomPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);

        // 判断是否是房主
        if (PhotonNetwork.IsMasterClient)
        {
            forceStart.SetActive(true);
        }
        else
        {
            forceStart.SetActive(false);
        }

        // 更新玩家列表
        Invoke("UpdatePlayerList", 0.5f);
    }

    // 有玩家加入房间
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // 更新玩家列表
        Invoke("UpdatePlayerList", 0.5f);
    }

    // 有玩家离开房间
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // 删除那个玩家的信息
        otherPlayer.CustomProperties.Clear();

        // 更新玩家列表
        Invoke("UpdatePlayerList", 0.5f);
    }

    // 连接服务器成功
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = userName;

        // 更新连接状态
        statusShow.text = statusText.On_CONNECTED_TO_MASTER;
        userNameShow.text = $"登录为：{userName}";

        // 显示房间面板
        roomListPanel.SetActive(true);
        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);

        // 加入大厅
        PhotonNetwork.JoinLobby();

        buttonCanClick = true;
    }

    // 更新房间列表
    public override void OnRoomListUpdate(System.Collections.Generic.List<RoomInfo> roomList)
    {
        // 清空房间列表
        foreach (Transform child in roomContent.transform)
        {
            Destroy(child.gameObject);
        }

        // 更新房间列表
        foreach (RoomInfo room in roomList)
        {
            if (room.PlayerCount == 0 && room.RemovedFromList) continue;
            GameObject roomObj = Instantiate(Resources.Load("Prefabs/Room") as GameObject, roomContent.transform);
            roomObj.GetComponent<RoomObj>().SetRoomInfo(room.Name, room.PlayerCount, room.MaxPlayers, roomListPanel, roomListPanelPosition);
        }
    }

    // 连接服务器失败
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 更新连接状态
        statusShow.text = statusText.On_CONNECTION_FAILED + "\n" + cause.ToString();

        // 隐藏所有面板
        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomListPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomListPanel.SetActive(false));

        createRoomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.transform.DOLocalMoveY(createRoomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        createRoomPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => createRoomPanel.SetActive(false));

        roomPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 5), 0.5f).SetEase(Ease.OutCirc);
        roomPanel.transform.DOLocalMoveY(roomPanelPosition.y - 150, 0.5f).SetEase(Ease.OutCirc);
        roomPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => roomPanel.SetActive(false));

        // 显示登录面板
        loginPanel.SetActive(true);
        loginPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        loginPanel.transform.DOLocalMoveY(loginPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        loginPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        // 更新玩家列表
        Invoke("UpdatePlayerList", 0.5f);

        // 如果全员准备，开始游戏
        bool allReady = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("Ready"))
            {
                if (!(bool)player.CustomProperties["Ready"])
                {
                    allReady = false;
                }
            }
            else
            {
                allReady = false;
            }
        }

        if (allReady && PhotonNetwork.PlayerList.Length > 1)
        {
            blackMask.gameObject.SetActive(true);
            blackMask.DOFade(1, 0.5f).SetEase(Ease.OutCirc);
            Invoke("StartGame", 1);
        }
    }

    // 开始游戏
    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("GameScene");
    }

    // 更新玩家列表
    public void UpdatePlayerList()
    {
        // 清空玩家列表
        foreach (Transform child in playerListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // 更新玩家列表
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            // 创建玩家对象
            GameObject playerObj = Instantiate(Resources.Load("Prefabs/PlayerDash") as GameObject, playerListContent.transform);
            Image BackGround = playerObj.transform.Find("Background").GetComponent<Image>();
            Image Icon = BackGround.transform.Find("Internal").transform.Find("Icon").GetComponent<Image>();
            Text UserName = BackGround.transform.Find("Internal").transform.Find("UserName").GetComponent<Text>();
            Toggle Prepared = BackGround.transform.Find("Internal").transform.Find("Prepared").GetComponent<Toggle>();

            // 设置玩家头像
            switch (player.CustomProperties["OS"])
            {
                case "Windows":
                    Icon.sprite = windowsSprite;
                    break;
                case "Apple":
                    Icon.sprite = appleSprite;
                    break;
                case "Android":
                    Icon.sprite = androidSprite;
                    break;
                case "Linux":
                    Icon.sprite = linuxSprite;
                    break;
            }

            // 设置玩家名字
            UserName.text = player.NickName;

            // 检查特殊玩家
            if (player.IsLocal)
            {
                UserName.text += " (你)";
            }
            if (player.IsMasterClient)
            {
                UserName.text += " (房主)";
            }

            // 检查玩家准备状态
            if (player.CustomProperties.ContainsKey("Ready"))
            {
                if ((bool)player.CustomProperties["Ready"])
                {
                    Prepared.gameObject.SetActive(true);
                }
                else
                {
                    Prepared.gameObject.SetActive(false);
                }
            }
            else
            {
                Prepared.gameObject.SetActive(false);
            }

            // 检查玩家颜色
            if (player.CustomProperties.ContainsKey("ColorR") && player.CustomProperties.ContainsKey("ColorG") && player.CustomProperties.ContainsKey("ColorB"))
            {
                BackGround.color = new Color((int)player.CustomProperties["ColorR"] / 255f, (int)player.CustomProperties["ColorG"] / 255f, (int)player.CustomProperties["ColorB"] / 255f, 0.5f);
            }
        }

        // 刷新LayoutGroup  
        StartCoroutine(RefreshLayoutGroup(playerListContent));

        // 更新玩家数量
        playerCountShow.text = PhotonNetwork.PlayerList.Length + " / 4";

    }

    // 加入房间失败
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // 更新连接状态
        statusShow.text = statusText.On_JOIN_FAILED + "\n" + message;

        // 显示房间面板
        roomListPanel.SetActive(true);
        roomListPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.transform.DOLocalMoveY(roomPanelPosition.y, 0.5f).SetEase(Ease.OutCirc);
        roomListPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
    }

    // 刷新LayoutGroup
    IEnumerator RefreshLayoutGroup(GameObject obj)
    {
        if (obj.GetComponent<VerticalLayoutGroup>() != null)
        {
            obj.GetComponent<VerticalLayoutGroup>().enabled = false;
            yield return new WaitForEndOfFrame();
            obj.GetComponent<VerticalLayoutGroup>().enabled = true;
        }
        else if (obj.GetComponent<HorizontalLayoutGroup>() != null)
        {
            obj.GetComponent<HorizontalLayoutGroup>().enabled = false;
            yield return new WaitForEndOfFrame();
            obj.GetComponent<HorizontalLayoutGroup>().enabled = true;
        }
        else if (obj.GetComponent<GridLayoutGroup>() != null)
        {
            obj.GetComponent<GridLayoutGroup>().enabled = false;
            yield return new WaitForEndOfFrame();
            obj.GetComponent<GridLayoutGroup>().enabled = true;
        }
    }
    [Serializable]
    public class StatusText
    {
        public string On_WAITING = "准备就绪";
        public string On_CONNECTING_TO_MASTER = "连接到服务器中";
        public string On_CONNECTED_TO_MASTER = "已连接到服务器";
        public string On_CONNECTION_FAILED = "连接已丢失";
        public string On_JOINING = "加入房间中";
        public string On_JOINED = "已加入房间";
        public string On_JOIN_FAILED = "加入房间失败";

    }
}

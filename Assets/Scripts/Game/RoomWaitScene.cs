using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using UnityEngine.SceneManagement;

public class RoomWaitScene : MonobitEngine.MonoBehaviour
{
    public GameObject m_button_;
    public GameObject m_Camvas;
    // ルーム参加数のオブジェクト
    private GameObject m_RoomMemberCountObj;

    // プレイヤー情報を表示するオブジェクト
    private GameObject[] m_PlayerInfos;

    // Start is called before the first frame update
    void Start()
    {
        if(!MonobitNetwork.inRoom)
        {
            Debug.Log("ルームに入っていない");
            return;
        }
        m_RoomMemberCountObj = Instantiate(Resources.Load("uGUI_Text")) as GameObject;
        m_RoomMemberCountObj.transform.SetParent(m_Camvas.transform);
        m_RoomMemberCountObj.transform.localPosition = new Vector3(0, 0, 0);
        UpdatePlayersCount();

        // ルームに入れるプレイヤーの最大数分だけプレイヤー情報表示を作成する
        m_PlayerInfos = new GameObject[MonobitNetwork.room.maxPlayers];
        for(int i = 0; i < MonobitNetwork.room.maxPlayers; ++i)
        {
            // PlayerInfoGUI.csが追加されたPrefabをインスタンス化
            m_PlayerInfos[i] = Instantiate(Resources.Load("PlayerInfoGUI")) as GameObject;
            m_PlayerInfos[i].transform.SetParent(m_Camvas.transform);
            m_PlayerInfos[i].transform.localPosition = new Vector3(0, -24 * i - 24, 0);
        }

        // 作成したプレイヤー情報表示に参加プレイヤーの情報を設定
        int length = MonobitNetwork.playerList.Length;
        for(int i = 0; i < length; ++i)
        {
            m_PlayerInfos[i].GetComponent<PlayerInfoGUI>().Set(MonobitNetwork.playerList[i]);
            m_PlayerInfos[i].SetActive(true);
        }
    }

    // 他プレイヤーが入室した際のコールバック
    private void OnOtherPlayerConnected(MonobitEngine.MonobitPlayer newPlayer)
    {
        int length = m_PlayerInfos.Length;
        for (int i = 0; i < length; ++i)
        {
            PlayerInfoGUI playerInfo = m_PlayerInfos[i].GetComponent<PlayerInfoGUI>();
            if(playerInfo.IsEntry)
            {
                continue;
            }
            playerInfo.Set(newPlayer);
            m_PlayerInfos[i].SetActive(true);
            break;
        }
            UpdatePlayersCount();
    }

    // 他プレイヤーが退室した際のコールバック
    private void OnOtherPlayerDisconnected(MonobitEngine.MonobitPlayer otherPlayer)
    {
        int length = m_PlayerInfos.Length;
        for (int i = 0; i < length; ++i)
        {
            PlayerInfoGUI playerInfo = m_PlayerInfos[i].GetComponent<PlayerInfoGUI>();
            if (playerInfo.ID != otherPlayer.ID)
            {
                continue;
            }
            playerInfo.Clear();
            m_PlayerInfos[i].SetActive(false);
            break;
        }
        UpdatePlayersCount();
    }

    // ルーム人数の表示を更新する
    private void UpdatePlayersCount()
    {
        m_RoomMemberCountObj.GetComponent<Text>().text = MonobitNetwork.room.name +
           "(" + MonobitNetwork.room.playerCount + "/" + MonobitNetwork.room.maxPlayers + ")";
    }

    public void OnClickLeave()
    {
        MonobitNetwork.DisconnectServer();
    }

    // サーバーから切断した時に呼ばれるコールバック
    public void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");

        OnClickLeave();

        SceneManager.LoadScene("Title");
    }
}

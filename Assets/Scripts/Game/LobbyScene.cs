using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonobitEngine;

public class LobbyScene : MonobitEngine.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!MonobitNetwork.inLobby)
        {
            Debug.Log("Not in lobby");
            return;
        }
    }

    // サーバーから切断した時に呼ばれるコールバック
    public void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");

        OnClickBack();

        SceneManager.LoadScene("Title");
    }

    public void OnClickCreateRoom()
    {
        SceneManager.LoadScene("CreateRoom");
    }

    public void OnClickJoinRoom()
    {
        SceneManager.LoadScene("JoinRoom");
    }

    // タイトル画面へ戻る
    public void OnClickBack()
    {
        MonobitNetwork.DisconnectServer();
    }
}

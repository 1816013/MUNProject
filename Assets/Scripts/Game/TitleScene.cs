using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonobitEngine;

public class TitleScene : MonobitEngine.MonoBehaviour
{
    private string userName = "";

    public void OnClickStart()
    {
        Debug.Log("ゲーム開始");
        // 自動でデフォルトのロビーへと入室する
        MonobitEngine.MonobitNetwork.autoJoinLobby = true;




        MonobitNetwork.playerName = userName;

        MonobitNetwork.ConnectServer("SinpleChat_v1.0");
        // 
    }

  

    private void OnJoinedLobby()
    {
        //SceneManager.LoadScene("Lobby");
    }

    public void OnConnectedToMonobit()
    {
        Debug.Log("接続しました");
    }

    public void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");
    }

    public void OnConnectToServerFailed(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("接続に失敗しました: " + cause.ToString());
    }

    public void OnConnectionFail(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("サーバーとの接続後に何らかの原因で切断されました: " + cause.ToString());
    }

    public void OnMonobitMaxConnectionReached()
    {
        Debug.Log("サーバーに接続しているクライアント数が上限に達しています");
    }

    public void OnClickExit()
    {
#if UNITY_EDITER
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif

    }
}

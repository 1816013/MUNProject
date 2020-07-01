using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonobitEngine;

public class TitleScene : MonobitEngine.MonoBehaviour
{
    private string userName = "";
    public InputField inputField_;
    public Text text_;

    // テキスト反映
    public void OnNameEdit(string text)
    {
        text_.text = text;
        userName = text_.text;
    }

    // スタートボタンクリック時
    public void OnClickStart()
    {
        // 名前が入力されていない時なにもしない     
        if(userName == "")
        {
            Debug.Log("名前が入力されていません");
            return;         
        }
        Debug.Log("ゲーム開始");
        // 自動でデフォルトのロビーへと入室する
        MonobitEngine.MonobitNetwork.autoJoinLobby = true;


        MonobitNetwork.playerName = userName;

        MonobitNetwork.ConnectServer("SinpleChat_v1.0");
        // 
    }

    // ロビーに入室した際のコールバック
    private void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    // MUNサーバーとの接続に成功した際に呼ばれるコールバック
    public void OnConnectedToMonobit()
    {
        Debug.Log("接続しました");
    }

    // サーバーから切断した時に呼ばれるコールバック
    public void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");
    }

    // MUNサーバーとの接続に失敗したときのコールバック
    public void OnConnectToServerFailed(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("接続に失敗しました: " + cause.ToString());
    }

    // MUNサーバーとの接続後に何らかの原因で切断されたときに呼ばれるコールバック
    public void OnConnectionFail(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("サーバーとの接続後に何らかの原因で切断されました: " + cause.ToString());
    }

    // サーバーへの接続数が上限だった際に呼ばれるコールバック
    public void OnMonobitMaxConnectionReached()
    {
        Debug.Log("サーバーに接続しているクライアント数が上限に達しています");
    }

    // 終了ボタンが押された時に呼びたい処理
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif

    }
}

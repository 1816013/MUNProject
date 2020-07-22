using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;

public class ChatGameScript : MonobitEngine.MonoBehaviour
{
    // ルーム名
    private string roomName = "";

    // チャット発言文
    private string chatWord = "";

    // チャット発言ログ
    List<string> chatLog = new List<string>();

    // RPC受信関数
    [MunRPC]
    private void RecvChat(string senderName, string senderWord)
    {
        chatLog.Add(senderName + " : " + senderWord);
        if (chatLog.Count > 10)
        {
            chatLog.RemoveAt(0);
        }
    }


    // GUI制御
    private void OnGUI()
    {
        if (!MonobitNetwork.inRoom)
        {
            return;
        }
        //ルーム内のプレイヤー一覧の表示
        GUILayout.BeginHorizontal();
        GUILayout.Label("PlayerList : ");
        foreach (MonobitPlayer player in MonobitNetwork.playerList)
        {
            GUILayout.Label(player.name + " ");
        }
        GUILayout.EndHorizontal();

        // ルームからの退室
        if (GUILayout.Button("Leave Room", GUILayout.Width(150)))
        {
            MonobitNetwork.DisconnectServer();
            chatLog.Clear();
        }

        // チャット発言文の入力
        GUILayout.BeginHorizontal();
        GUILayout.Label("Message : ");
        chatWord = GUILayout.TextField(chatWord, GUILayout.Width(400));

        GUILayout.EndHorizontal();

        // チャット発言文を送信する
        if (GUILayout.Button("Send", GUILayout.Width(100)))
        {
            monobitView.RPC("RecvChat",
                MonobitTargets.All,
                MonobitNetwork.playerName,
                chatWord);
            chatWord = "";
        }

        // チャットログを表示する
        string msg = "";
        for (int i = 0; i < 10; ++i)
        {
            msg += ((i < chatLog.Count) ? chatLog[i] : "") + "\r\n";
        }
        GUILayout.TextArea(msg);                    
    }

    private void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");
        SceneManager.LoadScene("Title");
    }
}

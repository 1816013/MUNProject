using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonobitEngine;


// ルーム作成画面
public class CreateScene : MonobitEngine.MonoBehaviour
{
    // ルーム設定情報
    static private RoomSettings m_RoomSettings;
    // ルーム名
    private string m_RoomName = string.Empty;

    public GameObject m_PasswordFieldObj;
    public GameObject m_PasswordTextObj;
    public Toggle m_toggle;
    public InputField m_roomNameInput;
    public InputField m_passwordInput;

    private void Start()
    {
        if (!MonobitNetwork.inLobby)
        {
            Debug.Log("Not in Lobby");
        }
        m_RoomSettings = new RoomSettings()
        {
            // 公開か非公開か設定できる
            isVisible = true,

            // 入室を許可するかを設定できる
            isOpen = true,

            // 作成するルームの最大人数を4人に設定する
            maxPlayers = 4,

            // パスワードとして利用するためのカスタムパラメータを作成
            roomParameters = new Hashtable() { { "password", "empty" } },

            // 上記だけだとルーム外からカスタムパラメータを扱うことができないので、
            // 扱えるように設定する
            lobbyParameters = new string[] { "password" }
        };
        m_toggle.onValueChanged.AddListener(delegate { OnCheckPrivateSetting(m_toggle.isOn); });
        m_roomNameInput.onValueChanged.AddListener(delegate { OnInputRoomName(m_roomNameInput.text); });
        m_passwordInput.onValueChanged.AddListener(delegate { OnInputPassword(m_passwordInput.text); });
    }

    // ルームが作成された際に呼ばれるコールバック
    public void OnCreatedRoom()
    {
        SceneManager.LoadScene("RoomWait");
    }

    // ルームの作成が失敗した際に呼ばれるコールバック
    public void OnCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("OnCreateRoomFailed : errorCode" + codeAndMsg[0] + ", message = " + codeAndMsg[1]);
    }

    // MUNサーバーとの接続を切った際に呼ばれるコールバック
    public void OnDisconnectedFromServer()
    {
        Debug.Log("サーバーから切断されました");
        SceneManager.LoadScene("Title");
    }

    // ルーム名入力時に呼ばれる
    private void OnInputRoomName(string roomName)
    {
        m_RoomName = roomName;
    }

    // プライベート設定変更時に呼ばれる
    private void OnCheckPrivateSetting(bool isOn)
    {
        m_PasswordFieldObj.SetActive(isOn);
        m_PasswordTextObj.SetActive(isOn);
        if (!isOn)
        {
            m_RoomSettings.roomParameters["password"] = "empty";
        }
    }

    // パスワード入力時に呼ばれる
    private void OnInputPassword(string password) 
    {
        if(!m_RoomSettings.roomParameters.ContainsKey("password"))
        {
            return;
        }
        m_RoomSettings.roomParameters["password"] = password;
    }

    // 作成ボタンが押された際に呼ばれる
    public void OnClickCreate()
    {
        // 引数にはルーム名、Start()内で設定したルーム設定、ロビーはデフォルトのロビー
        // なためnull、を入れています。
        MonobitNetwork.CreateRoom(m_RoomName, m_RoomSettings, null);
    }

    // 戻るボタンが押された際に呼ばれる
    public void OnClickBack()
    {
        SceneManager.LoadScene("Lobby");
    }
    
}

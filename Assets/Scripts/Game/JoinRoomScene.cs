using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MonobitEngine;

public class JoinRoomScene : MonobitEngine.MonoBehaviour
{
    public GameObject m_Canvas;
    public GameObject m_BackButton;
    public GameObject m_JoinButton;

    private GameObject m_InputRoomName;

    private GameObject m_InputPassword;

    private string m_RoomName = string.Empty;

    private string m_Password = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        if (!MonobitNetwork.inLobby)
        {
            Debug.Log("Not in Lobby");
        }
        m_BackButton.GetComponent<Button>().onClick.AddListener(OnClickBack);
        m_JoinButton.GetComponent<Button>().onClick.AddListener(OnClickJoin);

        m_InputRoomName = Instantiate(Resources.Load("uGUI_JoinInputField")) as GameObject;
        m_InputRoomName.transform.SetParent(m_Canvas.transform);
        m_InputRoomName.transform.localPosition = new Vector3(130, 130, 0);
        m_InputRoomName.transform.Find("Placeholder").GetComponent<Text>().text = "Input Room Name";
        m_InputRoomName.GetComponent<InputField>().onEndEdit.AddListener(OnInputRoomName);

        m_InputPassword = Instantiate(Resources.Load("uGUI_JoinInputField")) as GameObject;
        m_InputPassword.transform.SetParent(m_Canvas.transform);
        m_InputPassword.transform.localPosition = new Vector3(130, 90, 0);
        m_InputPassword.transform.Find("Placeholder").GetComponent<Text>().text = "Input Password";
        m_InputPassword.GetComponent<InputField>().onEndEdit.AddListener(OnInputPassword);
    }

    private void OnJoinedRoom()
    {
        SceneManager.LoadScene("RoomWait");
    }

    private void OnJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Join Room Failed : errorCode = " + codeAndMsg[0] + ", message = " +
            codeAndMsg[1]);
    }

    private void OnDisconnectedFromServer()
    {
        SceneManager.LoadScene("Title");
    }

    private void OnInputRoomName(string roomName)
    {
        m_RoomName = roomName;
    }

    private void OnInputPassword(string password)
    {
        m_Password = password;
    }

    private void OnClickJoin()
    {
        RoomData[] roomData = MonobitNetwork.GetRoomData();
        int length = roomData.Length;

        for(int i = 0; i < length; ++i)
        {
            if (!roomData[i].name.Equals(m_RoomName))
            {
                continue;
            }
            if(!roomData[i].customParameters["password"].Equals(m_Password))
            {
                Debug.Log("Incorrect Password");

                return;
            }
            MonobitNetwork.JoinRoom(m_RoomName);

            break;
        }
    }

    private void OnClickBack()
    {
        SceneManager.LoadScene("Lobby");
    }
}

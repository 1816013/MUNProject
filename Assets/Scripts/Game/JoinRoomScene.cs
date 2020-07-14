using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MonobitEngine;

public class JoinRoomScene : MonobitEngine.MonoBehaviour
{
    public GameObject m_BackButton;

    private string m_RoomName = string.Empty;

    private string m_Password = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        if(!MonobitNetwork.inLobby)
        {
            Debug.Log("Not in Lobby");
        }
        m_BackButton.GetComponent<Button>().onClick.AddListener(OnClickBack);
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

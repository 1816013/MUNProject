using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class ScrollViewButton : UnityEngine.MonoBehaviour
{
    private RoomData m_Data;

    public void Set(RoomData roomData)
    {
        m_Data = roomData;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        gameObject.GetComponentInChildren<Text>().text = m_Data.name + ", "
        + m_Data.playerCount + "/4";
    }

    private void OnClick()
    {
        MonobitNetwork.JoinRoom(m_Data.name);
    }
}

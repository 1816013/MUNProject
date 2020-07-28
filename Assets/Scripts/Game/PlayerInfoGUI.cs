using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class PlayerInfoGUI : MonobitEngine.MonoBehaviour
{
    public GameObject m_Canvas;
    // プレイヤー情報を表示するためのテキスト
    private Text m_PlayerInfoText;

    // プレイヤー情報
    private MonobitPlayer m_Player;

    // プレイヤーのID
    public int ID { get; private set; }

    // 表示済みかどうか
    public bool IsEntry { get; private set; }

    private void Awake()
    {
        GameObject textObj = Instantiate(Resources.Load("uGUI_Text")) as GameObject;
        textObj.transform.SetParent(this.transform);
        textObj.transform.localPosition = Vector3.zero;
        m_PlayerInfoText = textObj.GetComponent<Text>();
        m_PlayerInfoText.text = "NoEntry";
        ID = int.MaxValue;
        IsEntry = false;
    }

    public void Set(MonobitPlayer player)
    {
        m_Player = player;
        ID = m_Player.ID;
        IsEntry = true;
        InfoUpdate();
    }

    public void Clear()
    {
        m_Player = null;
        m_PlayerInfoText.text = "No Entry";
        ID = int.MaxValue;
        IsEntry = false;
    }

    public void InfoUpdate()
    {
        if(!IsEntry)
        {
            return;
        }
        string readyText = "";
        if (!m_Player.isHost && m_Player.customParameters["ready"] != null)
        {
            if ((bool)m_Player.customParameters["ready"])
            {
                readyText = "Ready";
            }
            else
            {
                readyText = "NotReady";
            }
        }
        
        string attribute = m_Player.isHost ? "Host" : "Guest";
        m_PlayerInfoText.text = m_Player.ID + ":" + m_Player.name + " " + attribute + readyText;
       
    }
}

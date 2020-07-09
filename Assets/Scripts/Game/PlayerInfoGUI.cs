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
        //textObj.transform.localPosition;
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
        string attribute = m_Player.isHost ? "Host" : "Guest";
        m_PlayerInfoText.text = m_Player.ID + ":" + m_Player.name + " " + attribute;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

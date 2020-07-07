using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class SceneTitle : MonobitEngine.MonoBehaviour
{
    // UIを表示するキャンバス    
    private GameObject m_CanvasObj;
    // タイトルテキスト    
    private GameObject m_TitleTxtObj;
    // スタートボタン    
    private GameObject m_StartBtnObj;
    // 終了ボタン   
    private GameObject m_ExitBtnOnj;

    // Start is called before the first frame update   
    void Start()
    {
        // UIを表示するためのキャンバス      
        m_CanvasObj = Instantiate(Resources.Load("Canvas")) as GameObject;
        // タイトルテキスト表示   
        m_TitleTxtObj = Instantiate(Resources.Load("Text")) as GameObject;
        m_TitleTxtObj.transform.SetParent(m_CanvasObj.transform);
        m_TitleTxtObj.transform.localPosition = new Vector3(72.0f, 128.0f, 0.0f);
        m_TitleTxtObj.GetComponent<Text>().text = "Game Title";
        // スタートボタンの作成  
        m_StartBtnObj = Instantiate(Resources.Load("Button")) as GameObject;
        m_StartBtnObj.transform.SetParent(m_CanvasObj.transform);
        m_StartBtnObj.transform.localPosition = new Vector3(0.0f, -80.0f, 0.0f);
        m_StartBtnObj.GetComponent<Button>().GetComponentInChildren<Text>().text = "Start";
        m_StartBtnObj.GetComponent<Button>().onClick.AddListener(OnClickStart);
        // 終了ボタンの作成      
        m_ExitBtnOnj = Instantiate(Resources.Load("Button")) as GameObject;
        m_ExitBtnOnj.transform.SetParent(m_CanvasObj.transform);
        m_ExitBtnOnj.transform.localPosition = new Vector3(0.0f, -180.0f, 0.0f);
        m_ExitBtnOnj.GetComponent<Button>().GetComponentInChildren<Text>().text = "Exit";
        m_ExitBtnOnj.GetComponent<Button>().onClick.AddListener(OnClickExit);
    }
    // Update is called once per frame  
    void Update()
    {

    }
    // スタートボタンが押された際に行いたい処理    
    private void OnClickStart()
    {
        // スタートボタンが押されたときにサーバーへ接続させたい     
        // 引数にはゲームのバージョンをいれてあげるといいです       
        MonobitEngine.MonobitNetwork.ConnectServer("DungeonAction_v_1_0"); 
    }
    // 終了ボタンが押された際に行いたい処理    
    private void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    // MUNサーバーとの接続に成功した際に呼ばれる接続コールバック  
    public void OnConnectedToMonobit()
    {
        Debug.Log("接続しました");
    }
    // サーバから切断したときに呼ばれる接続コールバック   
    public void OnDisconnectedFromServer()
    {
        Debug.Log("切断しました");
    }
    // MUNサーバーとの接続に失敗した際に呼ばれる接続コールバック
    public void OnConnectToServerFailed(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("接続に失敗しました:" + cause.ToString());
    }
    // MUNサーバーとの接続後に何らかの原因で切断されたときに呼ばれる接続コールバック 
    public void OnConnectionFail(MonobitEngine.DisconnectCause cause)
    {
        Debug.Log("サーバーとの接続後に何らかの原因で切断されました:" + cause.ToString());
    }
    // サーバーへの接続数が上限だった際に呼ばれる接続コールバック 
    public void OnMonobitMaxConnectionReached()
    {
        Debug.Log("サーバーに接続しているクライアント数が上限に達しています");
    } 
}

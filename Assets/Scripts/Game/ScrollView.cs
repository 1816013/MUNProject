using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class ScrollView : UnityEngine.MonoBehaviour
{
    private GameObject m_ButtonPrefab;

    public GameObject m_scrollViewObj;
    public GameObject m_UpdateButton;

    private List<GameObject> m_ButtonList;
    // Start is called before the first frame update
    void Start()
    {
        m_ButtonPrefab = Resources.Load("UI_ScrollViewButton") as GameObject;
        m_ButtonList = new List<GameObject>();
        m_UpdateButton.GetComponent<Button>().onClick.AddListener(OnClickUpDateRoomList);
        UpdateRoomData();
    }
    
    private void UpdateRoomData()
    {
        var scrollViewContent = m_scrollViewObj.transform.Find("Viewport/Content");
        int roomCount = MonobitNetwork.GetRoomData().Length;
        for(int i = 0; i < roomCount; i++)
        {
            RoomData roomData = MonobitNetwork.GetRoomData()[i];
            if (!roomData.customParameters["password"].Equals("empty"))
            {
                continue;
            }
            GameObject button = (GameObject)Instantiate(m_ButtonPrefab);
            button.transform.SetParent(scrollViewContent, false);

            button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, i * -30, 0);
            


            button.GetComponent<ScrollViewButton>().Set(MonobitNetwork.GetRoomData()[i]);

            m_ButtonList.Add(button);
        }
    }

    public void DestroyAll()
    {
        int length = m_ButtonList.Count;
        for(int i = 0; i < length; ++i)
        {
            if(m_ButtonList[i]== null)
            {
                continue;
            }
            Destroy(m_ButtonList[i]);
        }
        m_ButtonList.Clear();
        m_ButtonList.TrimExcess();
    }

    private void OnClickUpDateRoomList()
    {
        ScrollView scrollView = m_scrollViewObj.GetComponent<ScrollView>();
        scrollView.DestroyAll();
        scrollView.UpdateRoomData();
    }
}

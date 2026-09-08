using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class News_Load : MonoBehaviour
{
    [SerializeField] private Text headline;
    [SerializeField] private Image newsimg;
    [SerializeField] private Text newstext;
    [SerializeField] private Button Left_btn;
    [SerializeField] private Button Right_btn;

    [SerializeField] private Sprite[] img = new Sprite[3];
    [SerializeField] private int page = 1;
    [SerializeField] private int endpage;
    [SerializeField] private int[] index;
    [SerializeField] private int event_num;

    // Start is called before the first frame update
    void Start()
    {
        index = new int[GameManager.Instance.Page];
        endpage = GameManager.Instance.Page;
        if(GameManager.Instance.Page == 0)
        {
            GameManager.Instance.Warning = true;
            GameManager.Instance.Min -= 20;
            GameManager.Instance.AntiMin -= 20;
            GameManager.Instance.People -= 20;
            enabled = false;
            SceneManager.LoadScene("MainStory_Event");
            return;
        }
            
        int cnt = 0;
        for(int i = 0; i < GameManager.EVENT; i++)
        {
            if (GameManager.Instance.Selected[i])
            {
                index[cnt] = GameManager.Instance.randArray[i];
                cnt++;
            }
        }

        img = Resources.LoadAll<Sprite>("News_img");
        if (GameManager.Instance.People < 40)
            newsimg.sprite = img[0];
        else if (GameManager.Instance.People < 70)
            newsimg.sprite = img[1];
        else
            newsimg.sprite = img[2];

        RefreshPage();
    }

    // Refresh only when the visible page changes.
    private void RefreshPage()
    {
        if (index == null || index.Length == 0)
            return;

        event_num = index[page - 1];
        if (GameManager.Instance.event_checkArray[event_num] == 1)
        {
            headline.text = TP5_Events.Min_Opinion.Min_OpinionList[event_num].headline;
            newstext.text = TP5_Events.Min_Opinion.Min_OpinionList[event_num].newstext;
        } 
        else if (GameManager.Instance.event_checkArray[event_num] == 2)
        {
            headline.text = TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[event_num].headline;
            newstext.text = TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[event_num].newstext;
        }
            
    }

    public void Left_Click()
    {
        if (page <= 1)
            return;

        page--;
        RefreshPage();
    }

    public void Right_Click()
    {
        if (page >= endpage)
            return;

        page++;
        RefreshPage();
    }
}

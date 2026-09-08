using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opinion_Load : MonoBehaviour
{
    [SerializeField] private Image EventImage;
    [SerializeField] private Text EventText;
    [SerializeField] private Text Min_text;
    [SerializeField] private Text AntiMin_text;

    [SerializeField] private GameObject[] Min_Power = new GameObject[10];
    [SerializeField] private GameObject[] Min_Money = new GameObject[10];
    [SerializeField] private GameObject[] AntiMin_Power = new GameObject[10];
    [SerializeField] private GameObject[] AntiMin_Money = new GameObject[10];

    public Sprite[] ImageArray = new Sprite[GameManager.TIME * 3];

    private int EventNum;

    // Start is called before the first frame update
    void Start()
    {
        ImageArray = Resources.LoadAll<Sprite>("Content");
        EventNum = GameManager.Instance.playing_event;
        Debug.Log("해결중인 사건번호 " + EventNum);
    }

    private void Update()
    {
        // Render after all Start methods; this snapshot lasts until the next scene load.
        enabled = false;
        RefreshView();
    }

    private void RefreshView()
    {
        EventImage.sprite = ImageArray[EventNum];
        EventText.text = TP5_Events.Data.DataList[EventNum].text;
        Min_text.text = TP5_Events.Min_Opinion.Min_OpinionList[EventNum].text;
        AntiMin_text.text = TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[EventNum].text;

        for (int i = 0; i < 10; i++)
        {
            if ((TP5_Events.Min_Opinion.Min_OpinionList[EventNum].power * -1) >= i + 1)
                Min_Power[i].SetActive(true);
            else
                Min_Power[i].SetActive(false);

            if ((TP5_Events.Min_Opinion.Min_OpinionList[EventNum].money * -1) >= i + 1)
                Min_Money[i].SetActive(true);
            else
                Min_Money[i].SetActive(false);

            if ((TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[EventNum].power * -1) >= i + 1)
                AntiMin_Power[i].SetActive(true);
            else
                AntiMin_Power[i].SetActive(false);

            if ((TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[EventNum].money * -1) >= i + 1)
                AntiMin_Money[i].SetActive(true);
            else
                AntiMin_Money[i].SetActive(false);
        }
    }
}

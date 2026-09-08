using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStory_Load : MonoBehaviour
{
    [SerializeField] private Image EventImage;
    [SerializeField] private Text EventText;
    [SerializeField] private Text Min_text;
    [SerializeField] private Text AntiMin_text;

    [SerializeField] private Sprite[] ImageArray = new Sprite[9];
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = GameManager.Instance.Year - 1;
        ImageArray = Resources.LoadAll<Sprite>("Main");
    }

    // Update is called once per frame
    void Update()
    {
        // Render after all Start methods; this snapshot lasts until the next scene load.
        enabled = false;
        RefreshView();
    }

    private void RefreshView()
    {
        EventImage.sprite = ImageArray[index];
        EventText.text = TP5_Events.MainStory.MainStoryList[index].text;
        Min_text.text = TP5_Events.MainStory.MainStoryList[index].min_text;
        AntiMin_text.text = TP5_Events.MainStory.MainStoryList[index].antimin_text;
    }
}
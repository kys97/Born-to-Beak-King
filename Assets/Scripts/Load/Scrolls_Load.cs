using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolls_Load: MonoBehaviour
{
    [SerializeField] private Image[] ButtonImage = new Image[GameManager.EVENT];
    [SerializeField] private Button[] Scroll = new Button[GameManager.EVENT];

    [SerializeField] private GameObject[] Power = new GameObject[10];
    [SerializeField] private GameObject[] Money = new GameObject[10];

    private Sprite[] ImageArray = new Sprite[GameManager.TIME * 3]; 

    // Start is called before the first frame update
    void Start()
    {
        ImageArray = Resources.LoadAll<Sprite>("Content");
    }

    private void Update()
    {
        // Render after all Start methods; this snapshot lasts until the next scene load.
        enabled = false;
        RefreshView();
    }

    private void RefreshView()
    {
        var confirmedImage = Resources.Load<Sprite>("eventlist_confirmed");
        for (int i = 0; i < GameManager.EVENT; i++)
        {
            if (GameManager.Instance.Selected[i] == true)
            {
                Scroll[i].interactable = false;
                Scroll[i].GetComponent<Image>().sprite = confirmedImage;
                ButtonImage[i].enabled = false;
            }
            else
                ButtonImage[i].sprite = ImageArray[GameManager.Instance.randArray[i]];
        }

        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Instance.Power >= i + 1)
                Power[i].SetActive(true);
            else
                Power[i].SetActive(false);

            if (GameManager.Instance.Money >= i + 1)
                Money[i].SetActive(true);
            else
                Money[i].SetActive(false);
        }
    }
}

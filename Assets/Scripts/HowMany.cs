using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HowMany : MonoBehaviour
{
    private Text Number;
    private int Count=0;

    private void Start()
    {
        Number = GameObject.Find("ScrollNum_txt").GetComponent<Text>();

        for (int n = 0; n < GameManager.Instance.Selected.Length; n++)
        {
            if (GameManager.Instance.Selected[n] == false)
            {
                Count++;
            }
        }

        Number.text = Count.ToString();

    }

}

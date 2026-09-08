using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject[] Ending_Images = new GameObject[9];

    public void EndingOn(){
        Panel.SetActive(true);

        for(int i=0; i < 9; i++)
        {
            Debug.Log(i);
            if (GameManager.Instance.Ending[i])
                Ending_Images[i].SetActive(true);
        }
    }

    public void EndingOff()
    {
        Panel.SetActive(false);
    }
}

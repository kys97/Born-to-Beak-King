using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainResult_Load : MonoBehaviour
{
    [SerializeField] private Text result;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.MainSelect == 1)
            result.text = TP5_Events.MainStory.MainStoryList[GameManager.Instance.Year - 1].min_result;
        else if(GameManager.Instance.MainSelect == 2)
            result.text = TP5_Events.MainStory.MainStoryList[GameManager.Instance.Year - 1].antimin_result;
    }

}

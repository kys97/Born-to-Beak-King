using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Image : MonoBehaviour
{
    [SerializeField] private GameObject[] Power = new GameObject[10];
    [SerializeField] private GameObject[] Money = new GameObject[10];
    [SerializeField] private GameObject Message;

    private int displayedPower = int.MinValue;
    private int displayedMoney = int.MinValue;

    private void Update()
    {
        var game = GameManager.Instance;
        if (displayedPower != game.Power)
        {
            RefreshIcons(Power, game.Power);
            displayedPower = game.Power;
        }
        if (displayedMoney != game.Money)
        {
            RefreshIcons(Money, game.Money);
            displayedMoney = game.Money;
        }

        if (game.Warning)
        {
            Message.SetActive(true);
            game.Warning = false;
        }
    }

    private static void RefreshIcons(GameObject[] icons, int value)
    {
        for (int i = 0; i < icons.Length; i++)
            icons[i].SetActive(value > i);
    }
}
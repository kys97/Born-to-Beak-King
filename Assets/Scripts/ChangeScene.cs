using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    
    public void End_Game()
    {
        GameManager.Instance.Reset();
        SceneManager.LoadScene("Main");
    }

    public void To_Room()
    {
        SceneManager.LoadScene("Room");
    }

    public void To_Scrolls()
    {
        SceneManager.LoadScene("Scrolls");
    }

    public void To_Opinion()
    {
        GameManager.Instance.playing_index = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        GameManager.Instance.playing_event = GameManager.Instance.randArray[GameManager.Instance.playing_index];
        SceneManager.LoadScene("Opinion");
    }

    public void To_News()
    {
        if (GameManager.Instance.Status())
            SceneManager.LoadScene("News");
        else
            SceneManager.LoadScene("Result");
    }

    public void To_MainStory()
    {
        SceneManager.LoadScene("MainStory_Event");
    }

    public void To_Result()
    {
        int select = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        GameManager.Instance.MainSelect = select;
        bool ending = false;

        switch (GameManager.Instance.Year)
        {
            case 3: if (select == 1) ending = true;
                break;
            case 6: 
            case 9: if (select == 2) ending = true;
                break;
            default:break;
        }
        if (ending)
        {
            GameManager.Instance.MainStoryEnding = true;
            SceneManager.LoadScene("Result");
        }
        else if(GameManager.Instance.Year == 9 && select == 1)
        {
            SceneManager.LoadScene("Result");
        }
        else
            SceneManager.LoadScene("MainStory_Result");
    }

    public void Next_Year()
    {
        GameManager.Instance.SetNewYear();

        SceneManager.LoadScene("Room");
    }

    public void Min_Opinion()
    {
        var opinion = TP5_Events.Min_Opinion.Min_OpinionList[GameManager.Instance.playing_event];
        ApplyOpinion(1, opinion.money, opinion.power);
    }

    public void AntiMin_Opinion()
    {
        var opinion = TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[GameManager.Instance.playing_event];
        ApplyOpinion(2, opinion.money, opinion.power);
    }

    private void ApplyOpinion(int choice, int money, int power)
    {
        if (!Check_Message(money, power))
            return;

        var game = GameManager.Instance;
        int index = game.playing_index;
        game.playing_event = game.randArray[index];
        game.Selected[index] = true;
        game.Page++;
        game.event_checkArray[game.playing_event] = choice;
        game.Money += money;
        game.Power += power;
        To_Scrolls();
    }
    public bool Check_Message(int m, int p)
    {
        bool result = false;

        if (GameManager.Instance.Money + m < 0 && GameManager.Instance.Power + p < 0)
            Message_On("돈도 부족하고\n정치를 진행할 기력도 부족합니다...");
        else if (GameManager.Instance.Money + m < 0)
            Message_On("돈이 없습니다...");
        else if (GameManager.Instance.Power + p < 0)
            Message_On("정치를 진행할 기력이 없습니다... ");
        else
            result = true;

        return result;
    }

    public void Message_On(string m)
    {
        transform.Find("Text").gameObject.GetComponent<Text>().text = m;
        gameObject.SetActive(true);
    }

    public void Message_Off()
    {
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

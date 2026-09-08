using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //게임 정보
    public const int TIME = 15;//분기당 총 사건 수
    public const int EVENT = 4;//한 회차 사건 수

    //플레이 정보
    public int Year = 0;//집권년도
    public int Timeline = 0;//0초기 1중기 2말기
    public int Page = 0;//해결한 사건 갯수
    public bool[] Selected = new bool[EVENT];//상소문 완료여부
    public int playing_index = -1;
    public int playing_event = -1;//해결중인 사건 번호 default : -1
    public int[] event_checkArray;//사건 해결 여부 배열 1-민초, 2-반민초, 0-해결안함
    public int[] randArray = new int[EVENT];//한 회차 당 랜덤 사건 번호
    public bool Nextturn = false;//회차 종료 여부
    public int MainSelect = 0;//메인스토리 선택지 1-민 2-반민

    //엔딩
    public bool MainStoryEnding = false;
    public bool[] Ending = new bool[9];

    //메세지창
    public bool Warning = false;//상소문 해결 하나도 안하고 회차 종료했는지

    //플레이어 스테이터스
    public int Money = 10;
    public int Power = 10;
    public int Min = 50;
    public int AntiMin = 50;
    public int People = 50;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("싱글톤 안됌");
            }

            return _instance;
        }
    }

    private void Start()
    {
        UGS.UnityGoogleSheet.Load<TP5_Events.Data>();
        UGS.UnityGoogleSheet.Load<TP5_Events.Min_Opinion>();
        UGS.UnityGoogleSheet.Load<TP5_Events.AntiMin_Opinion>();
        UGS.UnityGoogleSheet.Load<TP5_Events.MainStory>();

        event_checkArray = new int[TIME * 3];
        for (int i = 0; i < TIME * 3; i++)
            event_checkArray[i] = 0;

        SetNewYear();
    }

    public void SetNewYear()
    {
        Money = 10;
        Power = 10;
        Timeline = Year / 3;
        Year += 1;
        Page = 0;
        playing_index = -1;
        playing_event = -1;
        for (int i=0;i < EVENT; i++)
            Selected[i] = false;

        for (int i = 0; i < EVENT; i++)
        {
            int rand_num = UnityEngine.Random.Range(0, TIME);//0~14
            randArray[i] = rand_num + (Timeline * TIME);
            if (event_checkArray[randArray[i]] == 0)
            {
                for (int j = 0; j < i; j++)
                    if (randArray[j] == randArray[i])
                        i--;
            }
            else i--;
        }
        Nextturn = false;

        Debug.Log("랜덤 상소문 생성 " + randArray[0] + " " + randArray[1] + " " + randArray[2] + " " + randArray[3]);
    }

    public bool Status()
    {
        Debug.Log("statue");
        for(int i = 0; i < EVENT; i++)
        {
            Debug.Log("For");
            int num = randArray[i];
            if (Selected[i])
            {
                Debug.Log("if문 들어옴 " + i);
                switch (event_checkArray[num])
                {
                    case 1:
                        Debug.Log("민초파 계산중");
                        People += TP5_Events.Min_Opinion.Min_OpinionList[num].people;
                        Min += TP5_Events.Min_Opinion.Min_OpinionList[num].min;
                        AntiMin += TP5_Events.Min_Opinion.Min_OpinionList[num].anti_min;
                        break;
                    case 2:
                        Debug.Log("반초파 계산중");
                        People += TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[num].people;
                        Min += TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[num].min;
                        AntiMin += TP5_Events.AntiMin_Opinion.AntiMin_OpinionList[num].anti_min;
                        break;
                    default: break;
                }
            }
        }

        if (Min <= 0 || AntiMin <= 0 || People <= 0)
            return false;
        else
            return true;
    }

    public void Reset()
    {
        Min = 50;
        AntiMin = 50;
        People = 50;
        Year = 0;
        Money = 10;
        Power = 10;
        Page = 0;
        Timeline = 0;//0초기 1중기 2말기
        MainSelect = 0;//메인스토리 선택지 1-민 2-반민
        MainStoryEnding = false;
        Warning = false;//상소문 해결 하나도 안하고 회차 종료했는지
        for (int i = 0; i < EVENT; i++)
            Selected[i] = false;
        for (int i = 0; i < TIME * 3; i++)
            event_checkArray[i] = 0;
        SetNewYear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ShowResult : MonoBehaviour
{
    public Text ResultText;
    public Image ResultImage;

    public Sprite [] EndingImage = new Sprite [9];

    public Button Next;
    public Button EndGame;

    private int [] Status = new int [3];
    private string [] Name = new string [3] { "반민초파", "민초파", "백새" };

    //엔딩 기준 계산
    private int Max;
    private int Min;
    private int Sub;
    private int Sum;
    private double Stdev;
    private float Average;
    private float Average2;

    private int[] Status2 = new int[3];

    //텍스트 이용 변수
    private string NamePart = "" ;

    void Start()
    {
        Status[0] = GameManager.Instance.AntiMin;//반민초파 지지도
        Status[1] = GameManager.Instance.Min;//민초파 지지도
        Status[2] = GameManager.Instance.People;//백새 지지도

        if (GameManager.Instance.MainStoryEnding)
        {
            switch (GameManager.Instance.Year)
            {
                case 3:
                    ResultImage.sprite = EndingImage[0];
                    GameManager.Instance.Ending[0] = true;
                    ResultText.text = "호로새는 마치 호로새끼처럼 그에게 매우 친절했던 옥새를 처단하고 스스로 왕위에 올랐다. "; 
                    break;
                case 6:
                    ResultImage.sprite = EndingImage[1];
                    GameManager.Instance.Ending[1] = true;
                    ResultText.text = "뱀나라의 특산품을 옥새가 의심하자, 뱀나라는 선전포고를 걸어왔고, 옥새는 전쟁 중 전사했다."; 
                    break;
                case 9:
                        ResultImage.sprite = EndingImage[2];
                    GameManager.Instance.Ending[2] = true;
                    ResultText.text = "쫑간나새는 화려한 언변으로 옥새를 구술렸고, 풀려난 쫑간나새는 반역을 일으켜 옥새를 죽이고 스스로 왕위에 올랐다.";
                    break;
                default: break;
            }
        }else if (Status[0] <= 0 || Status[1] <= 0 || Status[2] <= 0)
        {
            EndGame.gameObject.SetActive(true);
            Next.gameObject.SetActive(false);

            if (Status[2] <= 0)
            {
                ResultImage.sprite = EndingImage[5];
                GameManager.Instance.Ending[5] = true;
                NamePart = "백새";
            }
            else if(Status[0] <= 0)
            {
                ResultImage.sprite = EndingImage[3];
                GameManager.Instance.Ending[3] = true;
                NamePart = "반민초파";
            }
            else
            {
                ResultImage.sprite = EndingImage[4];
                GameManager.Instance.Ending[4] = true;
                NamePart = "민초파";
            }

            ResultText.text = "옥새력 00년\n" + NamePart + "의 반란이 발생해 옥새는 폐위되었다\n 옥새가 나라를 위한다는 명목으로 " +
                NamePart + "을 등한시 한 것이 원인이었다";

        }
        else
        {
            EndGame.gameObject.SetActive(false);
            Next.gameObject.SetActive(true);

            ResultImage.sprite = EndingImage[6];
            GameManager.Instance.Ending[6] = true;

            ResultText.text = "옥새력 9년 \n 옥새는 반역을 일으키려는 호로새를 처단했다.\n"+
                "옥새는 뛰어난 판단력으로 외교 분쟁에서도 신새계를 지켰으며 수많은 독살의 위협에서도 살아남았다. \n" +
                "다사다난한 재위 기간이었지만 그는 천수를 누리며 장수했다.";
        }

    }

    public void NextEnding()
    {
        Max = Status.Max();
        Min = Status.Min();
        Sub = Max - Min;
        
        EndGame.gameObject.SetActive(true);
        Next.gameObject.SetActive(false);

        if (Sub > 20)
        {//평범한 왕 엔딩

            ResultImage.sprite = EndingImage[7];
            GameManager.Instance.Ending[7] = true;

            if (21 <= Sub && Sub <= 40)
            {
                ResultText.text = "옥새의 국정 운영은 때때로 위태로웠다. 때로는 나라가 혼란에 빠지기도, 내분되기도 했다." +
                    "그럴 때마다 옥새는 균형을 찾고자 피땀흘리며 노력했다.\n 옥새력 00년 00일, 그는 더 이상 고생하지 않아도 되는 곳으로 떠났다.";
            }
            else if (41 <= Sub && Sub <= 60)
            {
                ResultText.text = "옥새의 국정 운영은 대부분 위태로웠다. 안정된 시기보다는 혼란에 빠진 시기가 많았다." +
                    "옥새도 처음에는 균형을 찾고자 했으나, 날이 갈수록 그는 지쳐갔다. 옥새가 죽은 후에 나라가 혼란스러워질 것은 자명한 사실이다.\n" +
                    "옥새력 00년 00일, 그는 후대에 큰 짐을 넘기고 세상을 떠났다.";
            }
            else if (61 <= Sub && Sub <= 80)
            {
                ResultText.text = "옥새의 국정 운영은 혼란 그 자체였다. 내분이 멈췄던 적은 한시라도 없으며, 궁궐에서는 항상 고성이 오갔다." +
                    "옥새도 균형을 잡기를 포기했었다. 옥새는 나라를 조용히 파멸로 이끌고 있었다.\n 옥새력 00년 00일, 난세의 암군은 세상을 떠났다.";
            }
            else
            {
                ResultText.text = "옥새 치하의 신새계는 망하지 않는 것이 이상할 정도였다. 나라는 이미 파별 직전으로 혼란에 빠졌다." +
                    "옥새가 죽은 후에 나라는 내전에 빠질 것이 자명했다.\n 옥새력 00년 00일, 신새계의 마지막 왕은 세상을 떠났다.";
            }

        }
        else
        {//공평한 왕 엔딩

            ResultImage.sprite = EndingImage[8];
            GameManager.Instance.Ending[8] = true;

            Status2[0] = Status[0] * Status[0];
            Status2[1] = Status[1] * Status[1];
            Status2[2] = Status[2] * Status[2];

            Average = (float)Status.Average();
            Average2 = (float)Status2.Average();

            Stdev = Mathf.Sqrt(Average2 - Average*Average);

            Sum = Status.Sum();

            //1
            if (Stdev < 3)
            {
                ResultText.text = "옥새는 큰 잡음 없이 모든 세력의 말에 귀를 기울이고, 미천한 신분이라도 그들을 존중하며 나라를 훌륭히 이끌어 왔다.";
            }
            else if (3 <= Stdev && Stdev < 7)
            {
                ResultText.text = "약간의 삐걱거림은 있었으나, 공평하게 귀를 기울이려는 그의 노력 덕분에 새들의 나라는 안정적으로 운영되었다.";
            }
            else
            {
                ResultText.text = "옥새는 공평하고자 노력했지만, 항상 어딘가 위태로워 보였다." +
                    " 아슬아슬했지만, 그의 즉위 기간 동안에는 국가가 한시적으로 균형을 되찾았었다는 사실은 부정할 수 없었다.";
            }

            //2 지지도 총합 +3 지지도 총합, 특징적 세력
            if (Sum > 240)
            {   for (int n = 0; n <= 2; n++)
                {
                    if (Status[n] == Max)
                    {
                        NamePart += Name[n] ;
                    }
                }

                ResultText.text += "국가를 훌륭히 이끌어 온 성군의 죽음에, 온 나라는 슬픔에 잠겼다."+
                    "특히"+ NamePart +"는, 그들을 아껴주던 옥새의 죽음에 더욱 비통해 했다.\n";
            }
            else if (80 <= Sum && Sum <= 240)
            {
                for (int n = 0; n <= 2; n++)
                {
                    if (Status[n] == Max)
                    {
                        NamePart += Name[n];
                    }
                }

                ResultText.text += "몇몇 비판이 따르긴 했지만, 국가를 안정적으로 이끌어 온 옥새의 죽음에 나라는 잠시 활기를 잃었다."+
                    NamePart + "는 더 이상 그들을 존중해주던 옥새의 죽음에 허탈해 했다.\n";
            }
            else
            {
                for (int n = 0; n <= 2; n++)
                {
                    if (Status[n] == Min)
                    {
                        NamePart += Name[n];
                    }
                }

                ResultText.text += "옥새가 죽었지만, 그의 죽음을 슬퍼하는 이를 찾아볼 수 없었다."+
                    "심지어"+ NamePart + "는 옥새가 잘 죽었다며 무덤에 침을 뱉기도 했다.\n";
            }
           
            //4 공통
            ResultText.text += "옥새력 00년 00일, 그는 기나긴 여정 끝에 이곳에 잠들었다.";

        }

    }
   
}

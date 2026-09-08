using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject OkSae;
    public GameObject MinHead;
    public GameObject AntiMinHead;
    public GameObject[] Min_bird = new GameObject[3];
    public GameObject[] AntiMin_bird = new GameObject[3];
    public Sprite[] OkSae_sprites = new Sprite[3];
    public Sprite[] MinHead_sprites = new Sprite[3];
    public Sprite[] AntiMinHead_sprites = new Sprite[3];
    public Sprite[] Min_sprites = new Sprite[3];
    public Sprite[] AntiMin_sprites = new Sprite[3];

    private int oksae_num, min_num, antimin_num;

    // Start is called before the first frame update
    void Start()
    {
        min_num = Check_State(GameManager.Instance.Min);
        antimin_num = Check_State(GameManager.Instance.AntiMin);
        oksae_num = (GameManager.Instance.Year - 1) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        OkSae.GetComponent<SpriteRenderer>().sprite = OkSae_sprites[oksae_num];
        MinHead.GetComponent<SpriteRenderer>().sprite = MinHead_sprites[min_num];
        AntiMinHead.GetComponent<SpriteRenderer>().sprite = AntiMinHead_sprites[antimin_num];
        for (int i = 0; i < 3; i++)
            Min_bird[i].GetComponent<SpriteRenderer>().sprite = Min_sprites[min_num];
        for (int i = 0; i < 3; i++)
            AntiMin_bird[i].GetComponent<SpriteRenderer>().sprite = AntiMin_sprites[antimin_num];
    }

    private int Check_State(int num)
    {
        int r;
        switch (num / 10)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                r = 0; break;
            case 4:
            case 5:
            case 6:
                r = 1; break;
            default: r = 2; break;
        }

        return r;
    }
}

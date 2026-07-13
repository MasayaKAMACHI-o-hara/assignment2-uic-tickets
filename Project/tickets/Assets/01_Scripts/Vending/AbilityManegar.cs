//アビリティマネージャーだよ
using System.Collections;
using UnityEngine;

public class AbilityManeger : MonoBehaviour
{
    public bool[] AbilityId;
    public bool[] AbilityTimer;
    public int[] AbilityTime;

    public HideIfFar[] TrashBox;//ゴミ箱の位置特定
    public KanMove Kan;//缶関連のスクリプト
    public CapsuleCollider KanAtari;//缶の当たり判定
    public SyakaSyaka syaka;//シャカゲージのスクリプト

    public void GetAbility(int id)
    {
        if (!AbilityId[id])
        {
            AbilityId[id] = true;
            AbilityTime[id] = 60;

            if (id == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    TrashBox[i].ExtendVisibleDistance(9999f, 60f);
                }
            }
            else if (id == 1)
            {
                Debug.Log("人の蹴る力が増えるよーん。");
                NPCManager.Instance.humanCoffee = true;
            }
            else if (id == 2)
            {
                Debug.Log("車にあたってもやられないよーん。");
                Kan.carInvincible = true;
            }
            else if (id == 3)
            {
                Debug.Log("コインの判定を広げるよーん。");
                KanAtari.radius = 20;
            }
            else if (id == 4)
            {
                Debug.Log("コインの取得量増やすよーん。");
                Kan.CoinUp = true;
            }
            else if (id == 5)
            {
                Debug.Log("スペースで降下できるよーん。");
                syaka.IsActiveSpace = true;
            }



        }
        else
        {
            AbilityTime[id] = 60;
        }

    }

    public IEnumerator Time(int id)
    {
        AbilityTimer[id] = true;

        yield return new WaitForSeconds(1);

        AbilityTime[id]--;

        if (AbilityTime[id] == 0)
        {
            if (id == 1)
            {
                Debug.Log("人の蹴る力がもどるよーん。");
                NPCManager.Instance.humanCoffee = false;
            }
            else if (id == 2)
            {
                Debug.Log("車にあたったらやられるよーん。");
                Kan.carInvincible = false;
            }
            else if (id == 3)
            {
                Debug.Log("コインの判定もどるよーん。");
                KanAtari.radius = 0.77f;
            }
            else if (id == 4)
            {
                Debug.Log("コインの取得量もどるよーん。");
                Kan.CoinUp = false;
            }
            else if (id == 5)
            {
                Debug.Log("スペースで降下できないよーん。");
                syaka.IsActiveSpace = false;

            }

            AbilityId[id] = false;
        }

        AbilityTimer[id] = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 6; i++)
        {

            if (AbilityTime[i] > 0 && !AbilityTimer[i])
            {
                StartCoroutine(Time(i));
            }
        }
    }
}
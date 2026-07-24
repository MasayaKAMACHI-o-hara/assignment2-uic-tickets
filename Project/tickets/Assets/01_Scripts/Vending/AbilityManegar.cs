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

            //ゴミ箱位置矢印
            if (id == 0)
            {
                //for (int i = 0; i < 15; i++) TrashBox[i].ExtendVisibleDistance(9999f, 60f);
                TrashBoxSearch.ActivePosiGingerAle(true);
            }
            //人の蹴る力
            else if (id == 1) NPCManager.Instance.humanCoffee = true;
            //車無敵
            else if (id == 2)
            {
                Kan.carInvincible = true;
                Kan.CarBarrierObject.SetActive(true);
            }
            //コイン取得範囲
            else if (id == 3)
            {
                KanAtari.radius = 20;
                Kan.CoinHaniObject.SetActive(true);
                Kan.CoinhaniOn = true;
            }
            //コイン取得倍率
            else if (id == 4) Kan.CoinUp = true;
            //SPACEキー落下
            else if (id == 5) syaka.IsActiveSpace = true;
            



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
      

        if (AbilityTime[id] <= 0)
        {
           Debug.Log("yobidasaretayo" + id);
            //ゴミ箱位置矢印
            if (id == 0)
            {
                TrashBoxSearch.ActivePosiGingerAle(false);
            }
            //人の蹴る力
            else if (id == 1) NPCManager.Instance.humanCoffee = false;
            //車無敵
            else if (id == 2)
            {
                Kan.carInvincible = false;
                Kan.CarBarrierObject.SetActive(false);
            }
            //コイン取得範囲
            else if (id == 3)
            {
                KanAtari.radius = 0.77f;
                Kan.CoinHaniObject.SetActive(false);
                Kan.CoinhaniOn = false;
            }
            //コイン取得倍率
            else if (id == 4)
            {
                Kan.CoinUp = false;
            }
            //SPACEキー落下
            else if (id == 5) syaka.IsActiveSpace = false;

            AbilityId[id] = false;
        }

        AbilityTimer[id] = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {

            if (AbilityTime[i] > 0 && !AbilityTimer[i])
            {
                StartCoroutine(Time(i));
            }
        }
    }
}
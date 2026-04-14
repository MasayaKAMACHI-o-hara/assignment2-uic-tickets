using UnityEngine;
using System.Collections;

public class Coinscript : MonoBehaviour
{
    [Header("取得時のパーティクル")]
    public GameObject getEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // KanというTagのオブジェクトが当たると消える
        if (other.CompareTag("Kan"))
        {
            // スコアを増やすプログラム
            Debug.Log("スコアを増やす");

            // ここから下 演出

            // パーティクルの発生
            if (getEffectPrefab != null)
            {
                GameObject effect = Instantiate(getEffectPrefab, transform.position, Quaternion.identity);
            }

            // アニメーション
            StartCoroutine(GetAnime());
        }
    }

    IEnumerator GetAnime()
    {
        // 獲得時にコインが跳ねて消える演出のアニメーション

        float time = 0f;

        while (time < 0.5f)
        {
            time += Time.deltaTime;

            float t = time / 0.5f;

            float height = Mathf.Sin(t * Mathf.PI) * 1.5f;
            transform.position = transform.position + Vector3.up * height;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, t);

            yield return null;
        }

        Destroy(gameObject);
    }
}
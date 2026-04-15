using UnityEngine;
using System.Collections;

public class Coinscript : MonoBehaviour
{
    [Header("�擾���̃p�[�e�B�N��")]
    public GameObject getEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Kan�Ƃ���Tag�̃I�u�W�F�N�g��������Ə�����
        if (other.CompareTag("Kan"))
        {
            // �X�R�A�𑝂₷�v���O����
            Debug.Log("�X�R�A�𑝂₷");

            // �������牺 ���o

            // �p�[�e�B�N���̔���
            if (getEffectPrefab != null)
            {
                GameObject effect = Instantiate(getEffectPrefab, transform.position, Quaternion.identity);
            }

            // �A�j���[�V����
            StartCoroutine(GetAnime());
        }
    }

    IEnumerator GetAnime()
    {
        // �l�����ɃR�C�������˂ď����鉉�o�̃A�j���[�V����

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
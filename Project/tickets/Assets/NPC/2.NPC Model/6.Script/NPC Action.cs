using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    [Header("NPC�̃I�u�W�F�N�g")]
    [SerializeField] private Transform Transform;
    [SerializeField] private Animator animator;
    [Header("�ړ��o�R�n�_�̃I�u�W�F�N�g")]
    [SerializeField] private Transform[] wayTransform;
    [SerializeField] private Collider[] wayCollider;
    [Header("��")]
    [SerializeField] private Rigidbody KanRigidbody;
    [SerializeField] private AudioSource KanAudioSource;
    [SerializeField] private AudioClip KanAudioClip;
    [Header("�X�s�[�J�[")]
    [SerializeField] private AudioSource audioSource;
    //�A�j���[�V������bool�֐�
    bool idle = true;    //��~
    bool walk = false;   //���
    bool kickNow = false;//�R��
    [Header("�R��")]
    [SerializeField] float kickPower = 30f;
    [SerializeField] public bool HumanCoffee = false;
    [Header("�|�[�Y")]
    [SerializeField] private PauseSistem pauseSistem;
    //��~���ԃJ�E���^�[
    float idleTime = 0;

    //�ړ����x
    float spped = 1.5f;

    //�o�R�n�_�̃i���o�[
    int waypoint = 0;
    //�o�R�n�_�̗v�f��
    int pointMax;

    //�����ݒ�
    void Start()
    {
        //�A�j���[�V�����̏�����Ԑݒ�
        animator.SetBool("idle", true);
        animator.SetBool("walk", false);

        //�o�R�n�_�̗v�f����擾
        pointMax = wayTransform.Length;
    }

    //���C���֐�
    void Update()
    {
        //�R���Ă��Ȃ����
        if (!kickNow)
        {
            // idle��
            if (idle)
            {
                //��~���ԃJ�E���g
                idleTime += Time.deltaTime;

                //��~���Ԃ��I�������
                if (idleTime >= 4)
                {
                    //��ԁE�A�j���[�V�����؂�ւ�
                    idle = false;
                    animator.SetBool("idle", false);
                    walk = true;
                    animator.SetBool("walk", true);

                    //��~���ԃ��Z�b�g
                    idleTime = 0;

                    //�Ō�̌o�R�n�_�i���o�[�Ȃ�
                    if (waypoint + 1 == pointMax)
                        waypoint = 0;//�����ʒu�̌o�R�n�_�i���o�[��w��
                                     //�Ō�ȊO�Ȃ�
                    else
                        waypoint++;//���̌o�R�n�_�i���o�[��w��

                    //�w�肳�ꂽ�o�R�n�_�̕��������
                    Transform.LookAt(wayTransform[waypoint]);

                    //�����Đ�
                    audioSource.Play();

                }
            }
            // walk��
            if (walk)
            {
                //�o�R�n�_�����̃x�N�g����v�Z
                Vector3 direction = (wayTransform[waypoint].position - Transform.position).normalized;

                //�o�R�n�_�Ɍ������ĕ��
                transform.position += direction * spped * Time.deltaTime;

                //�|�[�Y���Ȃ�
                if (pauseSistem.IsActivePause)
                {
                    //�����ꎞ��~
                    audioSource.Pause();
                }
                else//�|�[�Y������Ȃ����
                {
                    //�����ꎞ��~���
                    audioSource.UnPause();
                }
            }
        }
    }

    //�o�R�n�_�������֐�
    private void OnTriggerEnter(Collider other)
    {
        //�o�R�n�_�ɐG����
        if (other == wayCollider[waypoint])
        {
            //��ԁE�A�j���[�V�����؂�ւ�
            idle = true;
            animator.SetBool("idle", true);
            walk = false;
            animator.SetBool("walk", false);
            //������~
            audioSource.Stop();
        }
    }

    //�ʔ������֐�
    public void OnKanEnter()
    {
        //������~
        audioSource.Stop();

        // �L�b�N�A�j���[�V������Đ�
        animator.SetTrigger("kick");

        //�R���Ă���
        kickNow = true;
    }

    //�A�j���[�V�����C�x���g�E�R��֐�
    public void kick()
    {
        if (HumanCoffee == false)
        {
            if (KanRigidbody != null)
            {
                // 1. ��΂����������iNPC�̐��ʕ����j
                Vector3 kickDirection = Transform.forward;

                // 2. ������ɕ�������͂������Ɓu�R�������v���o�܂��i���D�݂Łj
                kickDirection += Vector3.up * 1.5f;

                // 3. �͂������i���x����Z�b�g���Ă��������ƈ��肵�܂��j
                KanRigidbody.linearVelocity = Vector3.zero; // �O�̓�������Z�b�g(Unity2023�ȍ~��linearVelocity)
                KanRigidbody.AddForce(kickDirection.normalized * kickPower, ForceMode.Impulse);
                KanAudioSource.PlayOneShot(KanAudioClip);
                Debug.Log("�ʂ�R��΂��܂����I");
            }
        }
    }
    //�A�j���[�V�����C�x���g�E�L�b�N�A�j���[�V�����I�����֐�
    public void KickEnd()
    {
        //�����Đ�
        audioSource.Play();

        //�R���Ă��Ȃ�
        kickNow = false;
    }
}


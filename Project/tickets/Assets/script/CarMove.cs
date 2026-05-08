using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CarMove : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f; // 前進する力

    public GameObject Kan;
    public AudioSource CarSE;
    public float maxDistance = 10f; // 音が消える距離
    public float minDistance = 2f;  // 音が最大になる距離
    public AudioClip Clash;
    public GameObject[] wheel;
    public GameObject nextpoint;

    public MeshRenderer CarColor;

    public Material[] ColorID;

    [SerializeField] private PauseSistem pauseSistem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var colorNomber = Random.Range(0, 5);
        CarColor.material = ColorID[colorNomber];
    }

    public void TurnMove(GameObject Pointname,GameObject Next)
    {
        if(nextpoint == Pointname)
        {
            transform.LookAt(Next.transform);
            nextpoint = Next;

        }

       
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Kan.transform.position);

        if (distance < maxDistance)
        {
            // 距離に応じて音量を 0.0 ～ 1.0 の間で調整
            // (1.0 - 割合) で、近いほど音を大きくする
            float volume = 1f - ((distance - minDistance) / (maxDistance - minDistance));
            CarSE.volume = Mathf.Clamp01(volume);

            if (!CarSE.isPlaying) CarSE.Play();

            //�|�[�Y���Ȃ�
            if (pauseSistem.IsActivePause)
            {
                //�����ꎞ��~
                CarSE.Pause();
            }
            else//�|�[�Y������Ȃ����
            {
                //�����ꎞ��~���
                CarSE.UnPause();
            }
        }
        else
        {
            // 離れすぎたら音を止めるか、ボリュームを0にする
            CarSE.volume = 0;
            if (CarSE.isPlaying) CarSE.Stop();
        }
    }


    void FixedUpdate()
    {
       
                transform.LookAt(nextpoint.transform);
                //rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
                transform.Translate(Vector3.forward * speed * Time.deltaTime); 
            

            for (int i = 0; i < 4; i++)
            {
                wheel[i] .transform.Rotate( 360 * Time.deltaTime,0,0);
            }

        
    }
}

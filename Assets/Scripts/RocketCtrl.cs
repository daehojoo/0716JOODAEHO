using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using static UnityEngine.ParticleSystem;

public class RocketCtrl : MonoBehaviour
{
    public GameObject Star_Effects;
    public AudioClip clip;
    public AudioSource source;
    private Transform tr;
    float h=0f,v=0f;
    public string enemyTag = "ENEMIES";
    private float halfHeight, halfWidth;
    public GameObject coinbullet;
    [SerializeField] private PadCtrl pad;
    public Transform firePos;
    void Start()
    {
        source = GetComponent<AudioSource>();
        tr = GetComponent<Transform>();
        halfHeight = Screen.height * 0.5f;
        halfWidth = Screen.width * 0.5f;
        pad = GameObject.Find("imageJoyStickPad").GetComponent<PadCtrl>();
    }
    public void OnStickPos(Vector3 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            JoyStickCtrl();
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            JoyStickCtrl();
        }
        CameraOutLimit();
        QuitApp();
    }
    private void JoyStickCtrl()
    {
        if (GetComponent<Rigidbody2D>())
        {
            Vector2 speed = GetComponent<Rigidbody2D>().velocity;
            speed.x = 4 * h;
            speed.y = 4 * v;
            GetComponent<Rigidbody2D>().velocity = speed;

        }
    }
    private void CameraOutLimit()
    {
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -8.0f, 8.0f), Mathf.Clamp(tr.position.y, -2.5f, 4.0f), tr.position.z);
    }
    void QuitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(collision.gameObject);
            GameObject eff = Instantiate(Star_Effects, new Vector3(tr.position.x, tr.position.y, -3f), Quaternion.identity);
            source.PlayOneShot(clip, 1.0f);
            Destroy(eff, 0.5f);

            GameManager.Instance.TurnOn();
        }
    }
    public void Fire()
    {
        Instantiate(coinbullet, firePos.position, Quaternion.identity);
       
    }
}

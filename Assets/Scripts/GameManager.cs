using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//1.프리팹 2.생성시 시간간격 3. 생성시 크기 랜덤 y축좌표 상하위치도 랜덤
public class GameManager : MonoBehaviour
{
    public GameObject Asteroid;
    public GameObject[] gameObjects;
    private int pivot = 0;

    public static GameManager Instance;
    [Header("스폰관련변수")]
    public GameObject prefab;
    private float timePrev;
    public float yMaxValue = 3.5f;
    public float yMinValue = -1.5f;

    [Header("CameraShake 관련변수")]
    public Vector3 PosCamera; //원래 기존의 카메라 위치를 받는 변수
    public float HitBeginTime;//로켓이 운석과 충돌할때의 시간을 저장 하는 변수
    private bool isHit = false; //로켓이 운석이랑 충돌했는지 아닌지 여부를 판단하는 Bool변수

    public bool isGameOver = false;


    void Start()
    {
        timePrev = Time.time;
        Instance = this;
        gameObjects = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            GameObject gameObject = Instantiate(Asteroid);
            gameObjects[i] = gameObject;
            gameObject.SetActive(false);
        }
        StartCoroutine("EnableCube");
    }
    IEnumerator EnableCube()
    {
        yield return new WaitForSeconds(2f);
        gameObjects[pivot++].SetActive(true);
        if (pivot == 10) pivot = 0;
        StartCoroutine("EnableCube");
    }


    void Update()
    {
       
        if (isHit)
        {
            float x = Random.Range(-0.02f, 0.02f);
            float y = Random.Range(-0.02f, 0.02f);
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - HitBeginTime > 0.3f)
            {
                isHit = false;
                Camera.main.transform.position = PosCamera;

                //HitBeginTime = Time.time ;
            }
        }
    }
    public void TurnOn()
    {
        isHit = true;
        PosCamera = Camera.main.transform.position; //카메라가 흔들리기전 카메라위치값을 저장한다.
        HitBeginTime = Time.time;


    }
    //void SpawnAsteroid()
    //{
    //    float RandomYpos = Random.Range(yMinValue, yMaxValue);
    //    float _scale = Random.Range(1, 3);
    //    prefab.transform.localScale = Vector3.one * _scale;
    //    Instantiate(prefab, new Vector3(12, RandomYpos, prefab.transform.position.z), Quaternion.identity);

    //}
}

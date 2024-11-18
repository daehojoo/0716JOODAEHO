using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//1.������ 2.������ �ð����� 3. ������ ũ�� ���� y����ǥ ������ġ�� ����
public class GameManager : MonoBehaviour
{
    public GameObject Asteroid;
    public GameObject[] gameObjects;
    private int pivot = 0;

    public static GameManager Instance;
    [Header("�������ú���")]
    public GameObject prefab;
    private float timePrev;
    public float yMaxValue = 3.5f;
    public float yMinValue = -1.5f;

    [Header("CameraShake ���ú���")]
    public Vector3 PosCamera; //���� ������ ī�޶� ��ġ�� �޴� ����
    public float HitBeginTime;//������ ��� �浹�Ҷ��� �ð��� ���� �ϴ� ����
    private bool isHit = false; //������ ��̶� �浹�ߴ��� �ƴ��� ���θ� �Ǵ��ϴ� Bool����

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
        PosCamera = Camera.main.transform.position; //ī�޶� ��鸮���� ī�޶���ġ���� �����Ѵ�.
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

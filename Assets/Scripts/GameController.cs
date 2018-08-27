using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject floorSample;

    CameraController cam;

    TowerFloor topFloor;
    Transform topTr;
    float maxSize;

    [SerializeField] int towerHeight;
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] GameObject EndGameUI;
    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
    }

    void Start()
    {
        //reset game
        towerHeight = 1;

        //delete tower
        TowerFloor[] t = GameObject.FindObjectsOfType<TowerFloor>();
        foreach (TowerFloor tt in t)
            Destroy(tt.gameObject);

        //recreate first floor
        GameObject f = Instantiate(floorSample);
        topFloor = f.GetComponent<TowerFloor>();
        f.transform.position = Vector3.zero;
        topTr = f.transform;
        maxSize = 1f;

        scoreCounter.text = "Height: 1";
    }

    public void Reset()
    {
        Start();
        cam.Reset();
    }

    void Update()
    {
        if (topTr != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject f = Instantiate(floorSample);
                TowerFloor t = f.GetComponent<TowerFloor>();
                t.prevFloor = topFloor;
                topFloor = t;
                topTr = f.transform;
                topTr.position = new Vector3(0, towerHeight * 0.5f, 0);
                towerHeight++;
                topTr.localScale = new Vector3(0f, 0.25f, 0f);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                topTr.localScale += new Vector3(1, 0, 1) * Time.deltaTime;
                if (topTr.localScale.x > maxSize * 1.1f)
                {
                    GameOver();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (topTr.localScale.x > maxSize)
                {
                    GameOver();
                }
                else if (topTr.localScale.x > maxSize - 0.05f) // perfect
                {
                    topTr.localScale = new Vector3(maxSize, 0.25f, maxSize);
                    maxSize += 0.21f;
                    topFloor.StartChain();
                    scoreCounter.text = "Height: " + towerHeight;
                }
                else
                {
                    maxSize = topTr.localScale.x;
                    scoreCounter.text = "Height: " + towerHeight;
                }
                cam.UpdateHeight(1 + towerHeight * 0.5f);
            }
        }
    }

    void GameOver()
    {
        topTr = null;
        topFloor.Mark();
        cam.LookDown(towerHeight);

        EndGameUI.SetActive(true);
    }
}
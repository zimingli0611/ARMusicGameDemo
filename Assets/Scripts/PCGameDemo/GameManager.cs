using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform appearPos;
    public GameObject musicItemPrefab;
    public Text scoreText;

    private float timeCount = 0;
    private float[] appearTimeCount = { 2.0f, 3.2f, 3.7f, 4.4f, 5.0f};
    private int nextAppearIndex = 0;
    private GameObject tempObject;

    private Transform firstChildObj;
    private float tempCal = 0;

    private float maximumInterval = 0.5f;
    public static float hitHeight = -3.0f;
    public static float score = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (nextAppearIndex < appearTimeCount.Length && timeCount > appearTimeCount[nextAppearIndex]) 
        {
            tempObject = Instantiate(musicItemPrefab, appearPos.position, appearPos.rotation);
            tempObject.transform.parent = this.transform;
            nextAppearIndex++;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckHit();
        }

        scoreText.text = score.ToString("f0");
    }

    void CheckHit()
    {
        firstChildObj = this.transform.GetChild(0);
        tempCal = Mathf.Abs(firstChildObj.position.y - hitHeight);
        if (tempCal <= maximumInterval)
        {
            AddScore((maximumInterval - tempCal) / maximumInterval);
            Destroy(firstChildObj.gameObject);
        }
    }

    void AddScore(float rate)
    { 
        score += rate * 100;
    }
}

using Rokid.UXR.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARGameManager : MonoBehaviour
{
    // Click Control Part
    private int preClickCount;
    private int curClickCount;

    // Begin Part
    private float beginTime = 2.0f;
    private float beginTimeCount = 0;
    private int beginClick = 2;
    private int beginClickCount = 0;
    private bool ifBegin = false;
    public GameObject notBeginBall;

    // Setting Part
    private Vector3 clickPos;

    // Game Control Part
    public Transform appearPos;
    public GameObject musicItemPrefab;
    public Text scoreText;
    public GameObject scoreTextObj;

    private float timeCount = 0;
    private float[] appearTimeCount = { 0.0f, 1.2f, 2.7f, 3.4f, 5.0f };
    private int nextAppearIndex = 0;
    private GameObject tempObject;

    private Transform firstChildObj;
    private float tempCal = 0;

    private float maximumInterval = 0.1f;
    public static float hitHeight;
    public static float dropHeight = 0.4f;
    public static float dropSpeed = 0.05f;
    public static float score = 0.0f;

    // Restart Control
    public float restartDelay = 5f;
    private bool ifGameBegin = false;
    private bool restart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ifBegin)
            notBeginBall.SetActive(false);
        else
        {
            notBeginBall.transform.position = GetSkeletonPose(SkeletonIndexFlag.INDEX_FINGER_TIP, HandType.RightHand).position;
        }

        // Click Control
        preClickCount = curClickCount;
        curClickCount = ARFingerClick.clickCount;

        // If Begin Control
        if (!ifBegin)
        {
            if (beginTimeCount == 0)
            {
                if (preClickCount != curClickCount)
                {
                    beginTimeCount += Time.deltaTime;
                    beginClickCount++;
                }
            }
            else 
            {
                beginTimeCount += Time.deltaTime;
                if (preClickCount != curClickCount)
                    beginClickCount++;
                if (beginTimeCount > beginTime)
                { 
                    beginTimeCount = 0;
                    beginClickCount = 0;
                }
            }
            if (beginClickCount >= beginClick)
            {
                ifBegin = true;
                clickPos = ARFingerClick.clickPos;
                hitHeight = clickPos.y;
                appearPos.position = clickPos + new Vector3(0.0f, dropHeight, 0.0f);
                scoreTextObj.transform.position = clickPos + new Vector3(-0.03f, 0.03f, 0.0f);
            }
        }

        // Game Play
        if (ifBegin)
        {
            timeCount += Time.deltaTime;
            if (nextAppearIndex < appearTimeCount.Length && timeCount > appearTimeCount[nextAppearIndex])
            {
                tempObject = Instantiate(musicItemPrefab, appearPos.position, appearPos.rotation);
                tempObject.transform.parent = this.transform;
                nextAppearIndex++;
            }

            if (preClickCount != curClickCount)
            {
                CheckHit();
            }

            scoreText.text = score.ToString("f0");

            // Restart Control
            if (!ifGameBegin && this.transform.childCount > 0)
                ifGameBegin = true;
            if (nextAppearIndex >= appearTimeCount.Length &&
                ifGameBegin && this.transform.childCount == 0 && !restart)
            {
                restart = true;
                StartCoroutine(ReloadSceneAfterDelay());
            }
        }
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
    private Pose GetSkeletonPose(SkeletonIndexFlag index, HandType hand)
    {
        return GesEventInput.Instance.GetSkeletonPose(index, hand);
    }

    IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        score = 0;
        SceneManager.LoadScene(0);
    }
}

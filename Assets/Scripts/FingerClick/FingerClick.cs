using Rokid.UXR.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerClick : MonoBehaviour
{
    public Text thumb, index, clickCountText;
    public Text debugUp, debugDown;

    public int clickCount = 0;
    private float thumbY, indexY;
    private bool upState, downState;
    private bool preUpState, preDownState, timeBeginCount;
    private float triggerTime = 0.3f, timeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        clickCountText.text = "0";
        upState = false;
        downState = false;

        timeBeginCount = false;
        preUpState = false;
        preDownState = false;
    }

    // Update is called once per frame
    void Update()
    {
        thumbY = GetSkeletonPose(SkeletonIndexFlag.THUMB_TIP, HandType.RightHand).position.y;
        indexY = GetSkeletonPose(SkeletonIndexFlag.INDEX_FINGER_TIP, HandType.RightHand).position.y;

        thumb.text = thumbY.ToString();
        index.text = indexY.ToString();

        preUpState = upState;
        preDownState = downState;
        if (Mathf.Abs(thumbY - indexY) <= 0.03f) downState = true;
        else downState = false;
        if (Mathf.Abs(thumbY - indexY) >= 0.035f) upState = true;
        else upState = false;

        debugUp.text = upState.ToString();
        debugDown.text = downState.ToString();   

        if (preUpState && !upState)
        {
            timeCount = 0;
            timeBeginCount = true;
        }

        if (timeBeginCount)
        {
            timeCount += Time.deltaTime;
            if (timeCount > triggerTime) 
            {
                timeBeginCount = false;
                timeCount = 0;
            }
            if (!preDownState && downState)
            {
                IsClick();
                timeBeginCount = false;
                timeCount = 0;
            }
        }

        clickCountText.text = clickCount.ToString();
    }

    void IsClick()
    { 
        clickCount++;
    }

    private Pose GetSkeletonPose(SkeletonIndexFlag index, HandType hand)
    {
        return GesEventInput.Instance.GetSkeletonPose(index, hand);
    }
}

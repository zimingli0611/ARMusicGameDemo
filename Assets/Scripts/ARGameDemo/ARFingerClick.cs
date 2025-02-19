using Rokid.UXR.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ARFingerClick : MonoBehaviour
{
    public static int clickCount = 0;
    public static Vector3 clickPos;
    private float thumbY, indexY;
    private bool upState, downState;
    private bool preUpState, preDownState, timeBeginCount;
    private float triggerTime = 0.3f, timeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
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

        preUpState = upState;
        preDownState = downState;
        if (Mathf.Abs(thumbY - indexY) <= 0.03f) downState = true;
        else downState = false;
        if (Mathf.Abs(thumbY - indexY) >= 0.035f) upState = true;
        else upState = false;

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
                RecordClickPoint(GetSkeletonPose(SkeletonIndexFlag.INDEX_FINGER_TIP, HandType.RightHand).position);
                timeBeginCount = false;
                timeCount = 0;
            }
        }
    }

    void IsClick()
    {
        clickCount++;
    }

    void RecordClickPoint(Vector3 newClickPos)
    {
        if (clickCount == 1)
            clickPos = newClickPos;
        else
            clickPos = (clickPos * (clickCount - 1) + newClickPos) / clickCount;
    }

    private Pose GetSkeletonPose(SkeletonIndexFlag index, HandType hand)
    {
        return GesEventInput.Instance.GetSkeletonPose(index, hand);
    }
}

using UnityEngine;
using UnityEngine.UI;
using Rokid.UXR.Interaction;
using System;
using System.Runtime.CompilerServices;

public class FingerTrack : MonoBehaviour
{
    public GameObject obj1, obj2, obj3, obj4, obj5;

    void Update()
    {
        obj1.transform.position = GetSkeletonPose(SkeletonIndexFlag.THUMB_TIP, HandType.RightHand).position;
        obj2.transform.position = GetSkeletonPose(SkeletonIndexFlag.INDEX_FINGER_TIP, HandType.RightHand).position;
        obj3.transform.position = GetSkeletonPose(SkeletonIndexFlag.MIDDLE_FINGER_TIP, HandType.RightHand).position;
        obj4.transform.position = GetSkeletonPose(SkeletonIndexFlag.RING_FINGER_TIP, HandType.RightHand).position;
        obj5.transform.position = GetSkeletonPose(SkeletonIndexFlag.PINKY_TIP, HandType.RightHand).position;
    }

    private Pose GetSkeletonPose(SkeletonIndexFlag index, HandType hand)
    {
        return GesEventInput.Instance.GetSkeletonPose(index, hand);
    }
}

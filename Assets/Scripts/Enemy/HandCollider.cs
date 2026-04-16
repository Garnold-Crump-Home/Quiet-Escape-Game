using UnityEngine;

public class HandAvoidanceTrigger : MonoBehaviour
{
    public SmartAvoidance ai;
    public LayerMask obstacleMask;

    public enum HandType { Left, Right, Front }
    public HandType handType;

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & obstacleMask) != 0)
        {
            if (handType == HandType.Left) ai.leftHandHit = true;
            if (handType == HandType.Right) ai.rightHandHit = true;
            if (handType == HandType.Front) ai.frontHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & obstacleMask) != 0)
        {
            if (handType == HandType.Left) ai.leftHandHit = false;
            if (handType == HandType.Right) ai.rightHandHit = false;
            if (handType == HandType.Front) ai.frontHit = false;
        }
    }
}
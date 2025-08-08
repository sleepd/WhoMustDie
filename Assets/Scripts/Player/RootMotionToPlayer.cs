using UnityEngine;

public class RootMotionToPlayer : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void OnAnimatorMove()
    {
        Vector3 delta = playerController.animator.deltaPosition;
        playerController.UpdateRootMotionDelta(delta);
    }
}

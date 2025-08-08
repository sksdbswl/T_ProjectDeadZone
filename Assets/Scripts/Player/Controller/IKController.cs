using UnityEngine;

public class IKController : MonoBehaviour
{
    private Animator animator;

    private Transform rightHandIKTarget;
    private Transform leftHandIKTarget;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIKTargets(Transform rightHandTarget, Transform leftHandTarget)
    {
        rightHandIKTarget = rightHandTarget;
        leftHandIKTarget = leftHandTarget;
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (rightHandIKTarget)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);
        }

        if (leftHandIKTarget)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
        }
    }
}
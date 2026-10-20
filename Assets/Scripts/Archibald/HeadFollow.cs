using UnityEngine;

public class HeadLook : MonoBehaviour
{
    [Header("Tracking Targets")]
    [SerializeField] private Transform headBone;
    [SerializeField] private Transform headAimTarget;

    [Header("Movement Settings")]
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private Vector3 boneRotationOffset;

    void LateUpdate()
    {
        if (headBone == null || headAimTarget == null) return;

        Vector3 targetDirection = headAimTarget.position - headBone.position;

        if (targetDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            targetRotation *= Quaternion.Euler(boneRotationOffset);

            headBone.rotation = Quaternion.Slerp(headBone.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }
}
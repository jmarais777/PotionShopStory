using UnityEngine;
public class PickUpObject : MonoBehaviour
{
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void heldLayer()
    {
        int heldLayerIndex = LayerMask.NameToLayer("HeldObject");

        if (heldLayerIndex != -1)
        {
            gameObject.layer = heldLayerIndex;
        }
    }

    public void defaultLayer()
    {
        gameObject.layer = 0;
    }

    public void PickUp(Transform holdPoint)
    {
//        heldLayer();

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;

//        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

//        rb.isKinematic = true;
        
    }

    public void Drop()
    {
//        defaultLayer();

        rb.useGravity = true;
        transform.SetParent(null);

//        rb.isKinematic = false;       
    }
    public void MoveToHoldPoint(Vector3 targetPosition)
    {
        rb.MovePosition(targetPosition);
    }

    public void Throw(Vector3 impulse)
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(impulse, ForceMode.Impulse);
    }
}
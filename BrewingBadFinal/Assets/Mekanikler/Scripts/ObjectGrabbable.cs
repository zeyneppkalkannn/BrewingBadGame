using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    public string itemName; // Nesnenin ad�

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Tag'e g�re itemName'i ayarla
        itemName = gameObject.tag;
    }

    public void PickUp(Transform parent)
    {
        rb.isKinematic = true;
        transform.SetParent(parent);
    }

    public void Drop()
    {
        rb.isKinematic = false;
        transform.SetParent(null);
    }
}

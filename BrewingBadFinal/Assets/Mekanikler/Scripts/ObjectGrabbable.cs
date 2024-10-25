using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    public string itemName; // Nesnenin adý

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Tag'e göre itemName'i ayarla
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

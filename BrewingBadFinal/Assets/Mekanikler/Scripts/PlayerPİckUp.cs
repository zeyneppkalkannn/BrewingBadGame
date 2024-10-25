using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform playerCameraTransform;
    public float pickUpDistance = 2f;
    public LayerMask pickUpLayerMask;
    private Transform pickedUpObject = null;

    void Update()
    {
        // 'E' tuşuna basıldığında nesne almayı kontrol et
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E tuşuna basıldı");
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                Debug.Log("Raycast başarılı, nesne tespit edildi: " + raycastHit.transform.name);
                if (raycastHit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
                {
                    Debug.Log("ObjectGrabbable bileşeni bulundu");
                    pickedUpObject = raycastHit.transform;
                    objectGrabbable.PickUp(playerCameraTransform);
                }
                else
                {
                    Debug.Log("ObjectGrabbable bileşeni bulunamadı");
                }
            }
            else
            {
                Debug.Log("Raycast başarısız");
            }
        }

        // 'Q' tuşuna basıldığında nesneyi bırakmayı kontrol et
        if (Input.GetKeyDown(KeyCode.Q) && pickedUpObject != null)
        {
            Debug.Log("Q tuşuna basıldı");
            if (pickedUpObject.TryGetComponent(out ObjectGrabbable objectGrabbable))
            {
                Debug.Log("Nesne bırakılıyor: " + pickedUpObject.name);
                objectGrabbable.Drop();
                pickedUpObject = null;
            }
        }
    }
}

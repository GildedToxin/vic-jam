using UnityEngine;

public class InventoryItemRotation : MonoBehaviour
{
    [SerializeField] GameObject itemToRotate;
    private bool isDragging = false;
    void Update()
    {
        if (Input.GetMouseButton(0))
            isDragging = true;
        else
            isDragging = false;

        if (isDragging)
            DragRotate();
    }

    private void DragRotate()
    {
        float rotationSpeed = 1000f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        itemToRotate.transform.Rotate(Vector3.up, -mouseX, Space.World);
        itemToRotate.transform.Rotate(Vector3.right, mouseY, Space.World);
    }
}

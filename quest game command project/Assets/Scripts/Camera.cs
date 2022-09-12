using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 300f; // Чувствительность мыши
    public float minUpDownLookAngle = -75f;
    public float maxUpDownLookAngle = 75f;
    float xRotation = 0f;

    private InteractableObject _prevInteractable;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        // Повороты по оси Y (вверх-вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minUpDownLookAngle, maxUpDownLookAngle);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Повороты по оси X (вправо-влево)
        playerBody.Rotate(Vector3.up * mouseX);


        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            var interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null )
            {
                if (interactable != _prevInteractable)
                {
                    print("outlined");
                    interactable.SwitchOutline();
                    _prevInteractable = interactable;
                }
                
                if (interactable.type == InteractableObject.TypeInteract.Door )
                {
                    var door = hit.collider.GetComponent<Door>();
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (interactable.hasInteract && door._opened) {interactable.hasInteract = false; }
                        else if (!interactable.hasInteract && !door._opened) {interactable.hasInteract = true; }
                    }
                }
            }
            else if (_prevInteractable != null)
            {
                print("disoutlined");
                _prevInteractable.SwitchOutline();
                _prevInteractable = null;
            }

            
        }
    }
}

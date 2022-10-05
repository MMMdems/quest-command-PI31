using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal otherPortal;
    
    private Camera _portalCamera;
    private Camera _playerCamera;
    
    void Start()
    {
        _portalCamera = GetComponentInChildren<Camera>();
        _playerCamera = Camera.main;
        
        if (_portalCamera.targetTexture != null)
        {
            _portalCamera.targetTexture.Release();
        }
        _portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        otherPortal.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = _portalCamera.targetTexture;
    }
    
    void Update()
    {
        PortalRecursion();
    }
    
    private void PortalRecursion()
    {
        // Position
        Vector3 viewerPos = otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(_playerCamera.transform.position);
        viewerPos = new Vector3(-viewerPos.x, viewerPos.y, -viewerPos.z);
        _portalCamera.transform.localPosition = viewerPos;

        // Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0,180,0));
        _portalCamera.transform.rotation = difference * _playerCamera.transform.rotation;

        // Clipping
        _portalCamera.nearClipPlane = viewerPos.magnitude;
    }

    private void Teleport(Transform obj)
    {
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        obj.position = otherPortal.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // Rotation
        Quaternion difference = otherPortal.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        obj.rotation = difference * obj.rotation;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
            print(zPos);
            if (zPos < 0) Teleport(other.transform);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }

}

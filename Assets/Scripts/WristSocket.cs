using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : MonoBehaviour
{
    [SerializeField] private float offsetY;
    private XRGrabInteractable socketedInteractable;

    /// <summary>
    /// Attach the XRGrabInteratable in the center (+offset) of the socket. Also Disable Rigidbody and collider
    /// </summary>
    /// <param name="_objectToAttach">The XRGrabInteractable to attach</param>
    private void Attach(XRGrabInteractable _objectToAttach)
    {
        Debug.Log($"Attach {_objectToAttach}");
        socketedInteractable = _objectToAttach;
        socketedInteractable.attachTransform = transform;
        socketedInteractable.transform.position = transform.position + offsetY * transform.up;
        socketedInteractable.GetComponent<Rigidbody>().useGravity = false;
        socketedInteractable.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Release()
    {
        Debug.Log($"Release {socketedInteractable}");
        socketedInteractable.attachTransform = null;
        socketedInteractable.GetComponent<Rigidbody>().useGravity = true;
        socketedInteractable.GetComponent<Rigidbody>().isKinematic = false;
        socketedInteractable = null;
    }

    private void OnTriggerEnter(Collider _other)
    {
        socketedInteractable = _other.GetComponentInParent<XRGrabInteractable>();
        if (socketedInteractable != null)
            Attach(socketedInteractable);
    }


    protected void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponentInParent<XRGrabInteractable>() == socketedInteractable)
        {
            Release();
        }
    }
}

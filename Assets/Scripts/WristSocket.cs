using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : MonoBehaviour
{
    [SerializeField] private float offsetY;
    private XRGrabInteractable socketedInteractable;

    /// <summary>
    /// Attach the XRGrabInteratable in the center (+offset) of the socket. Also makes the Rigidbody kinematic
    /// </summary>
    /// <param name="_objectToAttach">The XRGrabInteractable to attach</param>
    private void Attach(XRGrabInteractable _objectToAttach)
    {
        Debug.Log($"Attach {_objectToAttach}");
	    socketedInteractable = _objectToAttach;
		
	    // force drop the holdedObject
		//socketedInteractable.onSelectExited?.Invoke(socketedInteractable.selectingInteractor);

		socketedInteractable.attachTransform = transform;
		socketedInteractable.transform.position = transform.position + offsetY * transform.up;
		socketedInteractable.transform.parent = transform;
		var rb = socketedInteractable.GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
		socketedInteractable.onSelectEntered.AddListener(_ => Release());
	}

    private void Release() 
    {
        Debug.Log($"Release {socketedInteractable}");
        socketedInteractable.transform.parent = null;
		socketedInteractable.onSelectEntered.RemoveAllListeners();
		socketedInteractable.attachTransform = null;
        socketedInteractable.GetComponent<Rigidbody>().useGravity = true;
        socketedInteractable.GetComponent<Rigidbody>().isKinematic = false;
        socketedInteractable = null;
    }

	private void OnTriggerEnter(Collider _other)
    {
	    var interactable = _other.GetComponentInParent<XRGrabInteractable>();
        if (interactable != null &&
			interactable.selectingInteractor != null && // if is grabbed 
            interactable.attachTransform == null)		// if not already attached
            Attach(interactable);
    }

}

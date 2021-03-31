using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : MonoBehaviour
{
    [SerializeField] private float offsetY;
    public Inventory parentInventory;
    private XRGrabInteractable socketedInteractable;
    private Vector3 baseScale;

	/// <summary>
	/// Return true if the socket contain an object, false otherwise
	/// </summary>
    public bool ContainObject => socketedInteractable != null;

	/// <summary>
	/// Attach the XRGrabInteractable in the center (+offset) of the socket. Also makes the Rigidbody kinematic
	/// </summary>
	/// <param name="_objectToAttach">The XRGrabInteractable to attach</param>
	private void Attach(XRGrabInteractable _objectToAttach)
    {
        Debug.Log($"Attach {_objectToAttach}");
	    socketedInteractable = _objectToAttach;
	    baseScale = socketedInteractable.transform.localScale;
	    
	    // since XRGrabInteractable modifies the rigidbodies after grab event we must modify the rb after it
		socketedInteractable.selectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Attach");
			var rb = socketedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.isKinematic = true;
			socketedInteractable.transform.parent = transform;
			socketedInteractable.selectExited.RemoveAllListeners();
		});

		// force drop the holdedObject and don't allow regrab it with the same input
		socketedInteractable.selectingInteractor.allowSelect = false;
		StartCoroutine(AllowSelect(socketedInteractable.selectingInteractor));
		
		socketedInteractable.retainTransformParent = false;
		socketedInteractable.transform.position = transform.position + offsetY * transform.up;
		socketedInteractable.selectEntered.AddListener(_ => Release());

		parentInventory.AddObject(socketedInteractable.gameObject);
	}

    private IEnumerator AllowSelect(XRBaseInteractor interactor)
    {
		yield return new WaitForEndOfFrame();
		interactor.allowSelect = true;
    }

	private void Release() 
    {
        Debug.Log($"Release {socketedInteractable}");
		socketedInteractable.selectEntered.RemoveAllListeners();
		socketedInteractable.transform.localScale = baseScale;
		var detachedInteractable = socketedInteractable; // used for lambda capture
		// same as above
		socketedInteractable.selectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Release");
			var rb = detachedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = true;
			rb.isKinematic = false;
			detachedInteractable.transform.parent = null;
			detachedInteractable.selectExited.RemoveAllListeners();
		});
		parentInventory.RemoveObject(socketedInteractable.gameObject);
	    socketedInteractable = null;
	}

	private void OnTriggerEnter(Collider _other)
	{
		if (ContainObject)
			return;

	    var interactable = _other.GetComponentInParent<XRGrabInteractable>();
	    if (interactable != null &&
			parentInventory.CanAddObject &&				// not in animation
	        interactable.selectingInteractor != null && // if is grabbed 
	        interactable.transform.parent == null)		// if not already attached
	    {
            Attach(interactable);
	    }
    }

}

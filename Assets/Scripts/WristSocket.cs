using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : MonoBehaviour
{
    [SerializeField] private float offsetY;
    public Inventory parentInventory;
    private XRGrabInteractable socketedInteractable;
    private Vector3 baseScale;

    public bool ContainObject => socketedInteractable != null;

	/// <summary>
	/// Attach the XRGrabInteratable in the center (+offset) of the socket. Also makes the Rigidbody kinematic
	/// </summary>
	/// <param name="_objectToAttach">The XRGrabInteractable to attach</param>
	private void Attach(XRGrabInteractable _objectToAttach)
    {
        Debug.Log($"Attach {_objectToAttach}");
	    socketedInteractable = _objectToAttach;
	    baseScale = socketedInteractable.transform.localScale;
	    
	    // since XRGrabInteractable modifies the rigidbodies after grab event we must modify the rb after it
		socketedInteractable.onSelectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Attach");
			var rb = socketedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.isKinematic = true;
			socketedInteractable.transform.parent = transform;
			socketedInteractable.onSelectExited.RemoveAllListeners();
		});

		// force drop the holdedObject and don't allow regrab it with the same input
		socketedInteractable.selectingInteractor.allowSelect = false;
		StartCoroutine(AllowSelect(socketedInteractable.selectingInteractor));
		
		socketedInteractable.retainTransformParent = false;
		socketedInteractable.transform.position = transform.position + offsetY * transform.up;
		socketedInteractable.onSelectEntered.AddListener(_ => Release());

		parentInventory.AddObject(socketedInteractable.gameObject);
	}

    IEnumerator AllowSelect(XRBaseInteractor interactor)
    {
		yield return new WaitForEndOfFrame();
		interactor.allowSelect = true;
    }

	private void Release() 
    {
        Debug.Log($"Release {socketedInteractable}");
		socketedInteractable.onSelectEntered.RemoveAllListeners();
		socketedInteractable.transform.localScale = baseScale;
		var detachedInteractable = socketedInteractable; // used for lambda capture
		// same as above
		socketedInteractable.onSelectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Release");
			var rb = detachedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = true;
			rb.isKinematic = false;
			detachedInteractable.transform.parent = null;
			detachedInteractable.onSelectExited.RemoveAllListeners();
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
	        interactable.selectingInteractor != null && // if is grabbed 
	        interactable.transform.parent == null)		// if not already attached
	    {
            Attach(interactable);
	    }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : XRSocketInteractor
{

    // Is the socket active, and object is being held by different interactor
    public override bool isSelectActive => base.isSelectActive;

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        // Move while ignoring physics, and no smoothing
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    protected override void OnHoverEntering(XRBaseInteractable interactable)
    {

    }
    protected override void OnHoverExiting(XRBaseInteractable interactable)
    {

    }

    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {

    }

    protected override void OnSelectExiting(XRBaseInteractable interactable)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class TeleportationHandler : MonoBehaviour
{

    public GameObject baseControllerGameObject;
    public GameObject teleportationGameObject;

    public InputActionReference teleportationActivationReference;

    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;

    // Start is called before the first frame update
    private void Start()
    {
        teleportationActivationReference.action.performed += teleportModeActivate;
        teleportationActivationReference.action.canceled += teleportModeCancel;
    }

    private void teleportModeActivate(InputAction.CallbackContext obj)
    {
        onTeleportActivate.Invoke();
    }

    private void teleportModeCancel(InputAction.CallbackContext obj)
    {
        Invoke("DeactivateTeleport", .1f);
    }

    void DeactivateTeleport()
    {
        onTeleportCancel.Invoke();
    }
}

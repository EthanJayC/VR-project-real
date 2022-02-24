using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] InputActionReference controllerActionGrip;
    [SerializeField] InputActionReference controllerActionTrigger;

    private Animator handAnimator;

    private void Awake()
    {
        controllerActionGrip.action.performed += gripPress;
        controllerActionTrigger.action.performed += triggerPress;

        handAnimator = GetComponent<Animator>();
    }

    private void triggerPress(InputAction.CallbackContext context)
    {
        handAnimator.SetFloat("Trigger", context.ReadValue<float>());
    }

    private void gripPress(InputAction.CallbackContext context)
    {
        handAnimator.SetFloat("Grip", context.ReadValue<float>());
    }
}

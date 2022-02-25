using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleUIScript : MonoBehaviour
{

    public InputActionReference toggleReference;

    private void Awake()
    {
        toggleReference.action.started += toggleUI;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= toggleUI;
    }

    private void toggleUI(InputAction.CallbackContext obj)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }
}

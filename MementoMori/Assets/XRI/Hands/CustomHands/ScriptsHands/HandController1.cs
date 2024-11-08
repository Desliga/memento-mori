using UnityEngine;
using UnityEngine.InputSystem;

public class HandController1 : MonoBehaviour
{
    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;

    private Animator handAnimator;

    private const string trigger = "Trigger";
    private const string grip = "Grip";

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gripInputAction.action.performed += gridPressed;
        triggerInputAction.action.performed += triggerPressed;
    }

    private void OnDisable()
    {
        gripInputAction.action.performed -= gridPressed;
        triggerInputAction.action.performed -= triggerPressed;
    }

    private void triggerPressed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(trigger, obj.ReadValue<float>());
    }
    private void gridPressed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(grip, obj.ReadValue<float>());
    }

}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject ShopUI_Display;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CinemachineInputProvider cinemachineInput;

    private void Start()
    {
        // Ensure that the reference to the PlayerStateMachine is assigned
        if (playerStateMachine == null)
        {
            Debug.LogError("PlayerStateMachine reference is not set in ShopInteractable.");
        }
    }
    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void Interact(Transform interactorTransform)
    {
        ShopUI_Display.SetActive(true);

        playerStateMachine.enabled = false;
        cinemachineInput.enabled = false;
    }

    public void ExitShop()
    {
        playerStateMachine.enabled = true;
        cinemachineInput.enabled = true;

        ShopUI_Display.SetActive(false);
    }

}

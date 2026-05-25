using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject signSprite;
    private bool canPress;
    public Transform playTrans;
    private PlayerInput playerInput;
    private IInteractable targetItem;
    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        anim = signSprite.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerInput.actions["GamePlay/Confirm"].performed += OnConfirmPerformed;
        playerInput.actions["GamePlay/Confirm"].canceled += OnConfirmCanceled;
    }
    private void OnConfirmPerformed(InputAction.CallbackContext context)
    {
        if (canPress) {
            targetItem.TriggerAction();
        }
    }
    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
    }

    

    private void OnDisable()
    {
        playerInput.actions["GamePlay/Confirm"].performed -= OnConfirmPerformed;
        playerInput.actions["GamePlay/Confirm"].canceled -= OnConfirmCanceled;
    }

    private void Update()
    {
        signSprite.SetActive(canPress);
        signSprite.transform.localScale = playTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
        else canPress = false;
    }

    

}

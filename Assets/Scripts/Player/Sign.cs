using UnityEngine;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject signSprite;
    public bool canPress;
    public Transform playTrans;
    private void Awake()
    {
        //anim = GetComponentInChildren<Animator>();
        //anim = signSprite.GetComponent<Animator>();
    }

    private void Update()
    {
        signSprite.SetActive(canPress);
        signSprite.transform.localScale = playTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Interactable"))
            canPress = true; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
            canPress = false;
    }

}

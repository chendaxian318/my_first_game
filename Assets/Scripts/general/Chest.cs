using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    public void TriggerAction()
    {
        if (!isDone) OpenChest();
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone? openSprite : closeSprite;
    }
    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        GetComponent<AudioDefination>()?.PlayAudioClip();
        this.gameObject.tag = "Untagged";
    }

}

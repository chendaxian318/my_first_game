using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("ÊÂ¼þ¼àÌý")]
    public CharacterEventSO healthEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        
    }

    

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }
    private void OnHealthEvent(Character character)
    {
        var persentage = character.healthCurrent / character.healthAll;
        playerStatBar.OnHealthChange(persentage);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
            healthDelayImage.fillAmount -= Time.deltaTime;

    }

    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount  = percentage; 
    }
}

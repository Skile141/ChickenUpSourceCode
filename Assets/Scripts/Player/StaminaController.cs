using UnityEngine;
using UnityEngine.UI;

public class StaminaController: MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillImage;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fillImage.color = gradient.Evaluate(1f);
    }
    public void SetStamina(float health)
    {
        slider.value = health;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}

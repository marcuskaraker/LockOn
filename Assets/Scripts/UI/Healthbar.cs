using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthbarImage;
    public Image backgroundImage;

    public void SetHealthPercentage(float value)
    {
        healthbarImage.fillAmount = value;
    }
}

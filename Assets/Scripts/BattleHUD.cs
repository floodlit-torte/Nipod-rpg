using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Image hpSlider;
    [SerializeField] private Image spSlider;
    
    public void SetHUD(Health unit)
    {
        hpSlider.fillAmount = unit.CurrentHP;
        spSlider.fillAmount = unit.CurrentSP;
    }

    public void setHP(float hp)
    {
        hpSlider.fillAmount = hp;
    } 
    
    public void setSP(int sp)
    {
        spSlider.fillAmount = sp;
    }
}

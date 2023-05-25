using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
    }
    
    [SerializeField] private int maxHealthPoints;
    public int MaxHealthPoints
    {
        get { return maxHealthPoints; }
    }
    
    [SerializeField] private int maxSoulPoints;
    public int MaxSoulPoints
    {
        get { return maxSoulPoints; }
    }

    private int _currentHP;
    public int CurrentHP
    {
        get { return _currentHP; }
    }
    
    private int _currentSP;
    public int CurrentSP
    {
        get { return _currentSP; }
    }
    

    private BattleHUD _battleHUD;

    private void Awake()
    {
        _currentHP = maxHealthPoints;
        _currentSP = maxSoulPoints;
        _battleHUD = GetComponent<BattleHUD>();
    }

    public bool TakeDamage(int damage)
    {
        _currentHP -= Mathf.Abs(damage);
        //_battleHUD.setHP((float)_currentHP / (float)maxHealthPoints);

        //return _currentHP <= 0 ? true : false;
        if (_currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int hp)
    {
        if (_currentSP >= hp)
        {
            _currentHP += hp;
            _currentSP -= hp;
        }
        else return;
    }
}

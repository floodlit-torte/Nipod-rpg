using System;
using UnityEngine;
using UnityEngine.Serialization;

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

    public void TakeDamage(int damage)
    {
        _currentHP -= Mathf.Abs(damage);
        _battleHUD.setHP((float)_currentHP / (float)maxHealthPoints);

        if (_currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

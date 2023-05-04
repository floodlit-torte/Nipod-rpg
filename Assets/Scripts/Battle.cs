using System;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private Battle _battleManager;
    private Health _enemy;
    private Vector3 _enemyPos = new Vector3(7f, 0f, -1f);
    private Quaternion _enemyRot = new Quaternion(0f, -70f, 0f, 0f);
    
    private void Awake()
    {
        if (_battleManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _battleManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Health SpawnEnemy()
    {
        return Instantiate(_enemy, _enemyPos, _enemyRot);
    }
    
    public void SetEnemy(Health enemy)
    {
        this._enemy = enemy;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float meleeAtkRange = 100f;
    
    private PlayerBindings _playerBindings;
    private Battle _battleManager;
    private Animator _animator;

    private void Awake()
    {
        _playerBindings = new PlayerBindings();
        _battleManager = FindObjectOfType<Battle>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerBindings.Enable();
    }

    private void OnDisable()
    {
        _playerBindings.Disable();
    }

    private void Update()
    {
        MeleeAttack();
    }

    private void MeleeAttack()
    {
        if (_playerBindings.PlayerControlls.MeleeAttack.triggered)
        {
            _animator.SetTrigger("isAttacking");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, meleeAtkRange))
            {
                Debug.Log(hit.transform.name);
                Health enemy = hit.transform.GetComponent<Health>();  
                if (enemy != null)
                {
                    Debug.Log(enemy.name);
                    _battleManager.SetEnemy(enemy);
                    Invoke(nameof(LoadBattleScene), 1f);
                }
            }
        }
    }

    private void LoadBattleScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

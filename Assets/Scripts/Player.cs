using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float meleeAtkRange = 100f;
    
    private PlayerBindings _playerBindings;
    private Animator _animator;

    private Vector3 _lastPos;

    private void Awake()
    {
        _playerBindings = new PlayerBindings();
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
                if (hit.transform.CompareTag("enemy"))
                {
                    _lastPos = transform.position;
                    Debug.Log("battle beginning");
                    Invoke(nameof(LoadBattleScene), 1f);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    private void LoadBattleScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerBindings _playerBindings;
    private Animator _animator;

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
        
    }

    private void MeleeAttack()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerBindings.PlayerControlls.MeleeAttack.triggered && other.CompareTag("enemy"))
        {
            MeleeAttack();
        }
    }
}

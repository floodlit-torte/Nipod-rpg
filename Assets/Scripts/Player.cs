using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerBindings _playerBindings;

    private void Awake()
    {
        _playerBindings = new PlayerBindings();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (_playerBindings.PlayerControlls.MeleeAttack.triggered && other.CompareTag("enemy"))
        {
            MeleeAttack();
        }
    }
}

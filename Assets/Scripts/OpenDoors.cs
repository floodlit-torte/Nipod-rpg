using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Serialization;

public class OpenDoors : MonoBehaviour
{
    [Tooltip("Radius in which player can open the door.")]
    [SerializeField] private float availableRadius = 6f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private InputAction openDoors;
    [SerializeField] private Vector3 openedRightDoor;
    [SerializeField] private Vector3 openedLeftDoor;

    private bool _isInRadius;
    
    private void OnEnable()
    {
        openDoors.Enable();
    }

    private void OnDisable()
    {
        openDoors.Disable();
    }

    private void Update()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (CheckOutAvaibility() && openDoors.ReadValue<float>() > 0.1f)
        {
            leftDoor.DORotate(openedLeftDoor, 1f);
            rightDoor.DORotate(openedRightDoor, 1f);
        }
    }
    
    private bool CheckOutAvaibility()
    {
        Vector3 target = player.position; 
        float distnace = Vector3.Distance(transform.position, target);
        
        if (distnace < availableRadius) 
            _isInRadius = true;
        
        else _isInRadius = false;

        return _isInRadius;
    }
    
#if UNITY_EDITOR    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, availableRadius);
    }
#endif 
}

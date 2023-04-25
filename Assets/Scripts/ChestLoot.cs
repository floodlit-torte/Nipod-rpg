using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestLoot : MonoBehaviour
{
    
    [Tooltip("Radius in which player can open the chest.")]
    [SerializeField] private float availableRadius = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform topMesh;
    [SerializeField] private InputAction openChest;
    
    private bool _isInRadius;
    private Vector3 _openedChest = new Vector3(-155f, 0f, 0f);

    private void OnEnable()
    {
        openChest.Enable();
    }

    private void OnDisable()
    {
        openChest.Disable();
    }

    private void Update()
    {
        if (CheckOutAvaibility() && openChest.ReadValue<float>() > 0.1f)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (!CheckOutAvaibility())
            return;
        topMesh.DORotate(_openedChest, 0.5f);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, availableRadius);
    }
#endif    
}

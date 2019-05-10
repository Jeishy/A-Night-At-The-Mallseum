using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] [Tooltip("The movement speed increase of the enemy")] private float _moveSpeedIncrease;
    [SerializeField] [Tooltip("The initial movement speed of the enemy")] private int _initMoveSpeed;
    [SerializeField] private MeshRenderer _meshRenderer;


    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _initMoveSpeed;
    }

    private void FixedUpdate()
    {
        Debug.Log(IsPlayerInRange());
        if (IsPlayerInRange())
        {
            // Play appear particle effect
            // Re-enable mesh renderer   
            _meshRenderer.enabled = true;
        }
        else
        {
            // Play disappear particle effect
            // Disable mesh renderer
            _meshRenderer.enabled = false;
        }

        _agent.SetDestination(_playerTrans.position);
    }

    // Function that checks if the player is within range and in line of sight
    private bool IsPlayerInRange()
    {
        NavMeshHit hit;
        Debug.DrawRay(transform.position, Vector3.Normalize(_playerTrans.position - transform.position) * 100, Color.green);
        // Do raycast to player
        if (!_agent.Raycast(_playerTrans.position, out hit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopMovement()
    {
        // Stop the enemy's movement
        _agent.isStopped = true;
    }

    public void ResumeMovement()
    {
        // Resume the enemy's movement
        _agent.isStopped = false;
    }

    public void SetNewMoveSpeed()
    {
        _agent.speed += _moveSpeedIncrease;
    }
}

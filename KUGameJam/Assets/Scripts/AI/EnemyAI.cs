using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    Patrol, Pursue, Alerted
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] [Tooltip("The movement speed increase of the enemy")] private float _moveSpeedIncrease;
    [SerializeField] [Tooltip("The initial movement speed of the enemy")] private int _initMoveSpeed;
    [SerializeField] [Tooltip("The max time that the enemy will follow the player after player leaves line of sight")] private float _maxFollowTime;
    [SerializeField] private float _maxPursueDistance;
    [SerializeField] [Tooltip("List of all waypoint's transforms for the enemy")] private List<Transform> _waypoints = new List<Transform>();

    private NavMeshAgent _agent;
    public EnemyStates _currentState;
    private EnemyStates _lastState;
    private float _followTime;
    private bool _isNewStateSet;
    private Flashlight _flashlight;
    private bool _isMovementStopped;
    private int destPoint = 0;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _initMoveSpeed;
        _currentState = EnemyStates.Patrol;
        _flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        _isMovementStopped = false;

        GoToNextPoint();
    }

    private void FixedUpdate()
    {
        if (IsPlayerInLineOfSight())
        {
            // Play appear particle effect
            // Set enemy to pursuit state
            SetNewState(EnemyStates.Pursue);
        }
        else if (!IsPlayerInLineOfSight() && _lastState == EnemyStates.Pursue)
        {
            // Set the enemy to the follow state
            SetNewState(EnemyStates.Alerted);
        }

        if (_isMovementStopped && IsPlayerInLineOfSight() && !_flashlight.IsLightOn)
        {
            _isMovementStopped = false;
            SetNewState(EnemyStates.Pursue);
            ResumeMovement();
        }

        switch (_currentState)
        {
            case EnemyStates.Patrol:
                Patrol();
                break;
            case EnemyStates.Pursue:
                Pursue();
                break;
            case EnemyStates.Alerted:
                StartCoroutine(Alerted());
                break;
            default:
                Debug.LogError("Enemy state is null.");
                break;
        }
    }

    // Function that checks if the player is within range and in line of sight
    private bool IsPlayerInLineOfSight()
    {
        NavMeshHit hit;
        // Do raycast to player
        if (!_agent.Raycast(_playerTrans.position, out hit) && GetDistanceToPlayer() < _maxPursueDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Patrol()
    {
        if (_lastState == EnemyStates.Alerted)
        {
            _agent.ResetPath();
            _lastState = _currentState;
        }

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    // Function for pursuing the player once in line of sight
    private void Pursue()
    {
        // Follow player
        _agent.SetDestination(_playerTrans.position);
    }

    // Function for following the player, after player has left line of sight
    private IEnumerator Alerted()
    {
        Vector3 lastKnownPlayerPosition = _playerTrans.position;
        float maxTime = Time.time + _maxFollowTime;
        _followTime = Time.time;
        while (_followTime < maxTime)
        {
            _followTime += Time.deltaTime;
            _agent.SetDestination(lastKnownPlayerPosition);
            yield return new WaitForEndOfFrame();
            Debug.Log("Alerted..");
        }
        // Go back to patrolling after being alerted 
        SetNewState(EnemyStates.Patrol);
    }

    private float GetDistanceToPlayer()
    {
        float distance;
        distance = Mathf.Abs(Vector3.Distance(transform.position, _playerTrans.position));
        return distance;
    }

    private void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (_waypoints.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        _agent.destination = _waypoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % _waypoints.Count;
    }

    public void SetNewState(EnemyStates newState)
    {
        _lastState = _currentState;
        _currentState = newState;
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

    private void OnTriggerStay(Collider col)
    {
        // If enemy enters light cone, stop the enemy
        if (col.CompareTag("LightCone"))
        {
            // Stop enemy's movement
            StopMovement();
            _isMovementStopped = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // If enemy leaves light cone, resume enemy movement
        if (col.CompareTag("LightCone"))
        {
            // Stop enemy's movement
            ResumeMovement();
        }
    }
}

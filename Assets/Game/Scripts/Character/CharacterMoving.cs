using UnityEngine.AI;
using UnityEngine;
using System;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Character _character;

    private Vector3 _nextPos;
    private RaycastHit _hit;

    public static Action OnMove;
    public static Action OnContinueMove;
    public static Action OnStopMoving;
    public static Action<Vector3> OnSetNewPosition;
    public static Action<RaycastHit> OnSetHit;

    private void OnEnable()
    {
        OnMove += Move;
        OnContinueMove += ContinueMove;
        OnStopMoving += StopMoving;
        OnSetNewPosition += SetNewPosition;
        OnSetHit += SetRaycastHit;
    }

    private void OnDisable()
    {
        OnMove -= Move;
        OnContinueMove -= ContinueMove;
        OnStopMoving -= StopMoving;
        OnSetNewPosition -= SetNewPosition;
        OnSetHit -= SetRaycastHit;
    }

    private void SetNewPosition(Vector3 position)
    {
        _nextPos = position;
    }

    private void SetRaycastHit(RaycastHit hit)
    {
        _hit = hit;
    }

    
    private void Move()
    {


        animator.SetBool("Move", true);

        _navMeshAgent.SetDestination(_nextPos);
       
    }

    private void FixedUpdate()
    {
        //   Debug.Log("Has path: " +_navMeshAgent.hasPath + "\nPath pending: " + _navMeshAgent.pathPending + "\nPath end position: " + _navMeshAgent.pathEndPosition);
        Debug.Log(_navMeshAgent.angularSpeed);
        if (!_navMeshAgent.hasPath && !_navMeshAgent.pathPending) animator.SetBool("Move", false) ;
    }

    private void ContinueMove()
    {
        animator.SetBool("Move", true);

        _navMeshAgent.isStopped = false;

        _navMeshAgent.SetDestination(_nextPos);
    }
    private void StopMoving()
    {
        animator.SetBool("Move", false);
        _navMeshAgent.isStopped = true;
    }
    
}

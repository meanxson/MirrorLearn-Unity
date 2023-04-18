using System;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Camera _camera;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    [ClientCallback]
    private void Update()
    {
        if (!isOwned)
            return;

        if (!Input.GetMouseButtonDown(1))
            return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity))
            return;
        
        CmdMove(hit.point);
    }

    [Command]
    private void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out var hit, 1f, NavMesh.AllAreas))
            return;

        _navMeshAgent.SetDestination(hit.position);
    }

    public override void OnStartAuthority() => _camera = Camera.main;
    
}

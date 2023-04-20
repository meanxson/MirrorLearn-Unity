using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent _onSelect;
    [SerializeField] private UnityEvent _onDeselect;

    public UnitMovement UnitMovement { get; private set; }

    private void Awake() => UnitMovement = GetComponent<UnitMovement>();

    #region Client

    [Client]
    public void Select()
    {
        if (!isOwned)
            return;

        _onSelect?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!isOwned)
            return;

        _onDeselect?.Invoke();
    }

    #endregion
}
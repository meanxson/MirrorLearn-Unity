using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    private Camera _camera;

    public List<Unit> selectedUnits { get; } = new List<Unit>();

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            foreach (var selectedUnit in selectedUnits) selectedUnit.Deselect();
            selectedUnits.Clear();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
    }

    private void ClearSelectionArea()
    {
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity,_layerMask))
            return;

        if (!hit.collider.TryGetComponent(out Unit unit))
            return;
        
        if (!unit.isOwned)
            return;
        
        selectedUnits.Add(unit);

        foreach (var selectedUnit in selectedUnits) selectedUnit.Select();
    }
}
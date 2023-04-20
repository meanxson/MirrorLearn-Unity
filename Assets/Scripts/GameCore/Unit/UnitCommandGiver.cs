using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnitSelectionHandler _selectionHandler;

    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            return;

        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask))
            return;
        
        TryMove(hit.point);
    }

    private void TryMove(Vector3 position)
    {
        foreach (var unit in _selectionHandler.selectedUnits) unit.UnitMovement.CmdMove(position);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class UnitMoverBase : MonoBehaviour
{
    [SerializeField] protected MapController _mapController;

    [SerializeField] protected DeathDetector _deathDetector;

    [SerializeField] protected UnitControllerBase _unitController;

    [SerializeField] protected float _speed;

    protected List<List<GameField>> Map;

    protected GameField NextField;

    protected Vector2 NextFieldPosition;

    protected bool IsUnitMoving;

    public virtual void Start()
    {
        Map = _mapController.GetMap();
    }

    protected virtual void MoveUnit(DirectionVectorType direction)
    {
        IsUnitMoving = true;

        NextField = _mapController.GetNextField(direction, _unitController.CurrentRowIndex,
            _unitController.CurrentColumnIndex);

        NextFieldPosition = new Vector2(NextField.PositionX, NextField.PositionY);
    }

    protected void GoToField(Vector2 fieldPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, fieldPosition, Time.deltaTime * _speed);
    }

    protected bool IsTheLastFieldOfTheMap(DirectionVectorType direction)
    {
        if (_unitController.CurrentColumnIndex == 16 && direction == DirectionVectorType.Right ||
            _unitController.CurrentColumnIndex == 0 && direction == DirectionVectorType.Left ||
            _unitController.CurrentRowIndex == 0 && direction == DirectionVectorType.Up ||
            _unitController.CurrentRowIndex == 8 && direction == DirectionVectorType.Down)
        {
            return true;
        }

        return false;
    }

    protected bool IsTheReverseDirection(DirectionVectorType direction, DirectionVectorType lastDirection)
    {
        if (direction == DirectionVectorType.Right && lastDirection == DirectionVectorType.Left ||
            direction == DirectionVectorType.Left && lastDirection == DirectionVectorType.Right ||
            direction == DirectionVectorType.Up && lastDirection == DirectionVectorType.Down ||
            direction == DirectionVectorType.Down && lastDirection == DirectionVectorType.Up)
        {
            return true;
        }

        return false;
    }
}
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogMover : UnitMoverBase
{
    [SerializeField] private PlayerController _playerController;

    private DirectionVectorType _lastDirection;

    private void Update()
    {
        if (!IsUnitMoving && _unitController.IsUnitAlive() && _playerController.IsUnitAlive())
        {
            MoveUnit(DetermineDirection());
        }

        _deathDetector.СheckIfPigHasBeenEaten(_unitController.CurrentRowIndex, _unitController.CurrentColumnIndex,
            FieldValueType.Pig);
    }

    private DirectionVectorType DetermineDirection()
    {
        int randomNumber = Random.Range(0, 4);

        return (DirectionVectorType) randomNumber;
    }

    protected override void MoveUnit(DirectionVectorType direction)
    {
        if (IsTheLastFieldOfTheMap(direction) ||
            _mapController.IsFieldStone(_mapController.GetNextField(direction, _unitController.CurrentRowIndex,
                _unitController.CurrentColumnIndex)) || IsTheReverseDirection(direction, _lastDirection))
        {
            return;
        }

        _lastDirection = direction;

        base.MoveUnit(direction);

        _unitController.ChangeSprite(direction);

        StartCoroutine(GoDog(direction));
    }

    private IEnumerator GoDog(DirectionVectorType direction)
    {
        while (transform.position.x < NextFieldPosition.x && direction == DirectionVectorType.Right ||
               transform.position.x > NextFieldPosition.x && direction == DirectionVectorType.Left ||
               transform.position.y > NextFieldPosition.y && direction == DirectionVectorType.Down ||
               transform.position.y < NextFieldPosition.y && direction == DirectionVectorType.Up)
        {
            if (!_unitController.IsUnitAlive() || !_playerController.IsUnitAlive())
            {
                IsUnitMoving = false;
                yield break;
            }

            GoToField(NextFieldPosition);
            yield return null;
        }

        Map[_unitController.CurrentRowIndex][_unitController.CurrentColumnIndex].FieldValue = FieldValueType.Empty;
        _unitController.CurrentRowIndex = _mapController.GetIndex(NextField, IndexType.RowIndex);
        _unitController.CurrentColumnIndex = _mapController.GetIndex(NextField, IndexType.ColumnIndex);
        Map[_unitController.CurrentRowIndex][_unitController.CurrentColumnIndex].FieldValue = FieldValueType.Dog;
        IsUnitMoving = false;
    }
}
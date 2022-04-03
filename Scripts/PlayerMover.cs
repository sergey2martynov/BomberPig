using System.Collections;
using UnityEngine;

public class PlayerMover : UnitMoverBase
{
    [SerializeField] private UserInputManager _userInputManager;

    [SerializeField] private PlayerController _player;

    public override void Start()
    {
        base.Start();

        _userInputManager.DirectionPressed += MoveUnit;
    }

    private void OnDestroy()
    {
        _userInputManager.DirectionPressed -= MoveUnit;
    }

    protected override void MoveUnit(DirectionVectorType direction)
    {
        if (IsTheLastFieldOfTheMap(direction) ||
            _mapController.IsFieldStone(_mapController.GetNextField(direction, _player.CurrentRowIndex,
                _player.CurrentColumnIndex)) || IsUnitMoving || !_player.IsUnitAlive())
        {
            return;
        }

        base.MoveUnit(direction);

        StartCoroutine(GoPig(direction));
    }

    private IEnumerator GoPig(DirectionVectorType direction)
    {
        while (transform.position.x < NextFieldPosition.x && direction == DirectionVectorType.Right ||
               transform.position.x > NextFieldPosition.x && direction == DirectionVectorType.Left ||
               transform.position.y > NextFieldPosition.y && direction == DirectionVectorType.Down ||
               transform.position.y < NextFieldPosition.y && direction == DirectionVectorType.Up)
        {
            GoToField(NextFieldPosition);
            yield return null;
        }

        Map[_player.CurrentRowIndex][_player.CurrentColumnIndex].FieldValue = FieldValueType.Empty;
        _player.CurrentRowIndex = _mapController.GetIndex(NextField, IndexType.RowIndex);
        _player.CurrentColumnIndex = _mapController.GetIndex(NextField, IndexType.ColumnIndex);
        IsUnitMoving = false;
        Map[_player.CurrentRowIndex][_player.CurrentColumnIndex].FieldValue = FieldValueType.Pig;
    }

    public bool GetIsPigMoving()
    {
        return IsUnitMoving;
    }

    public GameField GetNextField()
    {
        return NextField;
    }
}
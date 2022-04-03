using System;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    [SerializeField] private MapController _mapController;

    [SerializeField] private UnitControllerBase _dogController;

    public event Action PigExploded;

    public event Action DogExploded;
    
    public event Action PigEated;
    
    public void CheckFieldsAround(GameField gameField)
    {
        for (int numberOfDirection = 0; numberOfDirection < 4; numberOfDirection++)
        {
            GameField checkedField = _mapController.GetNextField((DirectionVectorType) numberOfDirection,
                _mapController.GetIndex(gameField, IndexType.RowIndex),
                _mapController.GetIndex(gameField, IndexType.ColumnIndex));
            
            if (checkedField != null && _mapController.IsDogInTheField(checkedField))
            {
                DogExploded?.Invoke();
            }
            else if (checkedField != null && _mapController.IsPigInTheField(checkedField))
            {
                PigExploded?.Invoke();
            }

        }
        
        if (_mapController.IsDogInTheField(gameField))
        {
            DogExploded?.Invoke();
        }
        else if (_mapController.IsPigInTheField(gameField))
        {
            PigExploded?.Invoke();
        }
    }

    public void СheckIfPigHasBeenEaten(int rowIndex, int columnIndex, FieldValueType fieldValue)
    {
        for (int i = 0; i < 4; i++)
        {
            GameField fieldChecked = _mapController.GetNextField((DirectionVectorType) i, rowIndex,
                columnIndex);
            
            if (fieldChecked != null && fieldChecked.FieldValue == fieldValue)
            {
                PigEated?.Invoke();

                _dogController.ChangeSprite((DirectionVectorType)i);
            }
        }
    }
}

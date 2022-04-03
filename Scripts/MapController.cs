using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private List<List<GameField>> _rows;

    private List<GameField> _columns;

    private float _fieldPitch = 1.1f;

    private int _numberOfRows = 9;

    private int _numberOfColumns = 17;

    private float _xPositionDifference = 0.125f;

    private float _yPositionDifference = 0.975f;

    public void CreateMap()
    {
        _rows = new List<List<GameField>>();

        float startingPositionXField = -8.3f;

        float startingPositionYField = 3.85f;

        for (int rowIndex = 0; rowIndex < _numberOfRows; rowIndex++)
        {
            _columns = new List<GameField>();

            for (int columnIndex = 0; columnIndex < _numberOfColumns; columnIndex++)
            {
                GameField gameField = new GameField();

                if (columnIndex % 2 == 1 && rowIndex % 2 == 1)
                {
                    gameField.FieldValue = FieldValueType.Stone;
                }
                else
                {
                    gameField.FieldValue = FieldValueType.Empty;
                }

                gameField.PositionX = startingPositionXField + _fieldPitch * columnIndex;

                gameField.PositionY = startingPositionYField;

                _columns.Add(gameField);
            }

            startingPositionXField = startingPositionXField - _xPositionDifference;

            startingPositionYField = startingPositionYField - _yPositionDifference;

            _rows.Add(_columns);
        }
    }

    public bool IsFieldStone(GameField field)
    {
        if (field.FieldValue == FieldValueType.Stone)
        {
            return true;
        }

        return false;
    }

    public List<List<GameField>> GetMap()
    {
        return _rows;
    }

    public GameField GetNextField(DirectionVectorType direction, int currentRowIndex, int currentColumnIndex)
    {
        if (direction == DirectionVectorType.Right && currentColumnIndex != 16)
        {
            return _rows[currentRowIndex][currentColumnIndex + 1];
        }

        if (direction == DirectionVectorType.Left && currentColumnIndex != 0)
        {
            return _rows[currentRowIndex][currentColumnIndex - 1];
        }

        if (direction == DirectionVectorType.Up && currentRowIndex != 0)
        {
            return _rows[currentRowIndex - 1][currentColumnIndex];
        }

        if (direction == DirectionVectorType.Down && currentRowIndex != 8)
        {
            return _rows[currentRowIndex + 1][currentColumnIndex];
        }

        return null;
    }

    public int GetIndex(GameField gameField, IndexType indexType)
    {
        for (int rowIndex = 0; rowIndex < 9; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < 17; columnIndex++)
            {
                if (_rows[rowIndex][columnIndex] == gameField)
                {
                    if (indexType == IndexType.ColumnIndex)
                    {
                        return columnIndex;
                    }

                    return rowIndex;
                }
            }
        }

        return default;
    }

    public bool IsDogInTheField(GameField gameField)
    {
        if (gameField.FieldValue == FieldValueType.Dog)
        {
            return true;
        }

        return false;
    }

    public bool IsPigInTheField(GameField gameField)
    {
        if (gameField.FieldValue == FieldValueType.Pig)
        {
            return true;
        }

        return false;
    }
}
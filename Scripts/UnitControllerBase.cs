using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitControllerBase : MonoBehaviour
{
    [SerializeField] protected GameObject _animalRightPrefab;

    [SerializeField] protected GameObject _animalLeftPrefab;

    [SerializeField] private GameObject _animalUpPrefab;

    [SerializeField] private GameObject _animalDownPrefab;

    [SerializeField] protected DeathDetector _deathDetector;

    [SerializeField] private MapController _mapController;
    
    [SerializeField] private int _startColumnIndex;

    [SerializeField] private int _startRowIndex;

    protected GameObject CurrentSprite;

    public int CurrentColumnIndex { get; set; }

    public int CurrentRowIndex { get; set; }

    private List<List<GameField>> _map;

   private bool _isUnitAlive = true;
    
    private float _rotation = 0;
    
    
    public virtual void Start()
    {
        _map = _mapController.GetMap();

        CurrentColumnIndex = _startColumnIndex;

        CurrentRowIndex = _startRowIndex;
    }
    
    public void ChangeSprite(DirectionVectorType direction)
    {
        if (direction == DirectionVectorType.Down)
        {
            TurnOffSprite(_animalDownPrefab);
        }
        else if (direction == DirectionVectorType.Up)
        {
            TurnOffSprite(_animalUpPrefab);
        }
        else if (direction == DirectionVectorType.Left)
        {
            TurnOffSprite(_animalLeftPrefab);
        }
        else if (direction == DirectionVectorType.Right)
        {
            TurnOffSprite(_animalRightPrefab);
        }
    }
    
    protected void TurnOffSprite(GameObject newSprite)
    {
        CurrentSprite.SetActive(false);
        CurrentSprite = newSprite;
        CurrentSprite.SetActive(true);
    }
    
    public void Update()
    {
        CurrentSprite.transform.position = transform.position;
    }
    
    private IEnumerator TurnTheAnimal()
    {

        _isUnitAlive = false;
        
        while (_rotation < 6)
        {
            transform.Rotate(0, 0, _rotation);

            _rotation += 0.1f;
            
            yield return null;
        }
    }

    protected void WrapperTurnTheAnimal()
    {
        StartCoroutine(TurnTheAnimal());
    }

    public bool IsUnitAlive()
    {
        return _isUnitAlive;
    }
    
    public virtual void ReturnStartPosition()
    {
        MakeFieldEmpty();
        
        CurrentColumnIndex = _startColumnIndex;

        CurrentRowIndex = _startRowIndex;

        var unitTransform = transform;
        
        unitTransform.rotation = Quaternion.identity;
        
        _rotation = 0;

        _isUnitAlive = true;

        unitTransform.position = new Vector2(_map[CurrentRowIndex][CurrentColumnIndex].PositionX, _map[CurrentRowIndex][CurrentColumnIndex].PositionY);
    }

    private void MakeFieldEmpty()
    {
        _map[CurrentRowIndex][CurrentColumnIndex].FieldValue = FieldValueType.Empty;
    }
}

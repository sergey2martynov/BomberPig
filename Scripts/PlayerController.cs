using UnityEngine;

public class PlayerController : UnitControllerBase
{
    [SerializeField] private UserInputManager _userInputManager;

    public override void Start()
    {
        base.Start();

        CurrentSprite = _animalRightPrefab;

        _userInputManager.DirectionPressed += ChangeSprite;

        _deathDetector.PigExploded += WrapperTurnTheAnimal;

        _deathDetector.PigEated += WrapperTurnTheAnimal;
    }

    private void OnDestroy()
    {
        _userInputManager.DirectionPressed -= ChangeSprite;

        _deathDetector.PigExploded -= WrapperTurnTheAnimal;

        _deathDetector.PigEated -= WrapperTurnTheAnimal;
    }

    public override void ReturnStartPosition()
    {
        base.ReturnStartPosition();

        TurnOffSprite(_animalRightPrefab);
    }
}
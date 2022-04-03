public class DogController : UnitControllerBase
{
    public override void Start()
    {
        base.Start();

        CurrentSprite = _animalLeftPrefab;

        _deathDetector.DogExploded += WrapperTurnTheAnimal;
    }

    public override void ReturnStartPosition()
    {
        base.ReturnStartPosition();

        TurnOffSprite(_animalLeftPrefab);
    }
}
using System.Collections;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    [SerializeField] private DeathDetector _deathDetector;

    [SerializeField] private PlayerController _player;

    [SerializeField] private DogController _dogController;

    private void Start()
    {
        _deathDetector.DogExploded+= WrapperReloadLevel;
        _deathDetector.PigExploded += WrapperReloadLevel;
        _deathDetector.PigEated += WrapperReloadLevel;
    }

    private void OnDestroy()
    {
        _deathDetector.DogExploded -= WrapperReloadLevel;
        _deathDetector.PigExploded -= WrapperReloadLevel;
        _deathDetector.PigEated -= WrapperReloadLevel;
    }

    private void WrapperReloadLevel()
    {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(3);
        
        _player.ReturnStartPosition();
        _dogController.ReturnStartPosition();
    }
}

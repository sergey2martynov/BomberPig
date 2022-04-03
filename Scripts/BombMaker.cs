using System.Collections.Generic;
using UnityEngine;

public class BombMaker : MonoBehaviour
{
    [SerializeField] private ButtonController _buttonController;

    [SerializeField] private PlayerController _player;

    [SerializeField] private MapController _mapController;

    [SerializeField] private PlayerMover _playerMover;

    [SerializeField] private GameObject _bombPrefab;

    [SerializeField] private GameObject _bombs;

    [SerializeField] private DetonationOfBomb _detonationOfBomb;

    private List<List<GameField>> _map;

    private void Start()
    {
        _map = _mapController.GetMap();

        _buttonController.ButtonClicked += CreateBomb;
    }

    private void OnDestroy()
    {
        _buttonController.ButtonClicked -= CreateBomb;
    }

    private void CreateBomb()
    {
        GameObject bomb;

        GameField spawnField;

        if (_playerMover.GetIsPigMoving())
        {
            spawnField = _playerMover.GetNextField();

            Vector2 spawnBombPosition = new Vector2(spawnField.PositionX, spawnField.PositionY);

            bomb = Instantiate(_bombPrefab, spawnBombPosition, Quaternion.identity, _bombs.transform);
        }
        else
        {
            spawnField = _map[_player.CurrentRowIndex][_player.CurrentColumnIndex];

            Vector2 spawnBombPosition = new Vector2(spawnField.PositionX, spawnField.PositionY);

            bomb = Instantiate(_bombPrefab, spawnBombPosition, Quaternion.identity, _bombs.transform);
        }

        StartCoroutine(_detonationOfBomb.ExplodeBomb(bomb, spawnField));
    }
}
using System;
using UnityEngine;

public class DishSpawnerCounter : BaseCounter
{
    [SerializeField] private int _maxPlates = 4;
    [SerializeField] private KitchenWieldable[] _currentPlates;
    private float _SpawnTimer = 0;
    [SerializeField] private float _spawnDelay = 2;
    [SerializeField] private GameObject _plate;

    private void Start()
    {
        _SpawnTimer = 0;
    }
    private void Update()
    {
        if (_currentPlates.Length < _maxPlates)
        {
            _SpawnTimer += Time.deltaTime;
            if (_SpawnTimer >= _spawnDelay)
            {
                _SpawnTimer = 0;
                AddToArr(SpawnKitchenWieldable());
            }
        }
        if (_currentPlates.Length > 0) KitchenWieldableHeld = _currentPlates[_currentPlates.Length - 1];
    }
    public override void Interacted(IKitchenWieldableParent player)
    {
        if (!player.KitchenWieldableHeld)
        {
            SwapWieldablesWith(player);
            if (_currentPlates.Length >= 2) KitchenWieldableHeld = _currentPlates[_currentPlates.Length - 2];
            if (_currentPlates.Length > 0) RemoveLastFromArr();
        };
    }

    private void RemoveLastFromArr()
    {
        Array.Resize(ref _currentPlates, _currentPlates.Length - 1);
    }

    private void AddToArr(KitchenWieldable kitchenWieldable)
    {
        Array.Resize(ref _currentPlates, _currentPlates.Length + 1);
        _currentPlates[^1] = kitchenWieldable;
    }

    private KitchenWieldable SpawnKitchenWieldable()
    {
        float _spawnOffset = _currentPlates.Length * 0.1f;
        GameObject newPlate = Instantiate(_plate, SpawnPoint.transform.position + new Vector3(0, _spawnOffset, 0), SpawnPoint.rotation);
        return newPlate.GetComponent<KitchenWieldable>();
    }
}

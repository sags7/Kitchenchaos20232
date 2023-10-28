using System.Collections.Generic;
using UnityEngine;

public class DishSpawnerCounter : BaseCounter
{
    [SerializeField] private int _maxPlates = 4;
    [SerializeField] private GameObject _plate;
    [SerializeField] private float _spawnDelay = 3f;
    private List<KitchenWieldable> _currentPlates;
    private float _SpawnTimer = 0;

    private void Start()
    {
        _SpawnTimer = 0;
        _currentPlates = new List<KitchenWieldable>();
    }
    private void Update()
    {
        SpawnerTimer();
    }

    private void SpawnerTimer()
    {
        if (_currentPlates.Count < _maxPlates)
        {
            _SpawnTimer += Time.deltaTime;
            if (_SpawnTimer >= _spawnDelay)
            {
                _SpawnTimer = 0;
                AddToList(SpawnKitchenWieldable());
            }
        }

    }
    public override void Interacted(IKitchenWieldableParent player)
    {
        if (!player.KitchenWieldableHeld && _currentPlates.Count > 0)
        {
            PopulatePlateOrSwap(player);
            RemoveLastFromList();
            if (_currentPlates.Count != 0) KitchenWieldableHeld = _currentPlates[^1];
            else KitchenWieldableHeld = null;
        }
        else if (Plate.CountItems(player.KitchenWieldableHeld._kitchenWieldableSO, Plate.AcceptableItems) > 0 && player.KitchenWieldableHeld is KitchenWieldable && !(player.KitchenWieldableHeld is Plate) && _currentPlates.Count > 0)
        {
            PopulatePlateOrSwap(player);
            SwapWieldablesWith(player);
            RemoveLastFromList();
            if (_currentPlates.Count != 0) KitchenWieldableHeld = _currentPlates[^1];
            else KitchenWieldableHeld = null;
        }
    }

    private void RemoveLastFromList()
    {
        _currentPlates.RemoveAt(_currentPlates.Count - 1);
    }

    private void AddToList(KitchenWieldable kitchenWieldable)
    {
        _currentPlates.Add(kitchenWieldable);
        KitchenWieldableHeld = kitchenWieldable;
    }

    private KitchenWieldable SpawnKitchenWieldable()
    {
        float _spawnOffset = _currentPlates.Count * 0.1f;
        GameObject newPlate = Instantiate(_plate, SpawnPoint.transform.position + new Vector3(0, _spawnOffset, 0), SpawnPoint.rotation);
        return newPlate.GetComponent<KitchenWieldable>();
    }
}

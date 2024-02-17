using System.Collections.Generic;
using UnityEngine;

public class SliceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Transform[] _positions;

    [SerializeField] private float _timeToSpawn = 2.5f;
    private float _currentTime;

    private List<GameObject> _slices = new List<GameObject>();

    private void OnEnable()
    {
        GameState.Instance.GameStarted += Build;
    }
    private void OnDisable()
    {
        GameState.Instance.GameStarted -= Build;
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState == GameState.State.InGame)
        {
            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Build();
            }
        }
    }
    public void Build()
    {
        if(_timeToSpawn >= 1.5f)
            _timeToSpawn -= 0.05f;
        _currentTime = _timeToSpawn;
        var slice = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);
        slice.transform.position = _positions[Random.Range(0, _positions.Length)].position;
        _slices.Add(slice);
    }
    public void Clear()
    {
        foreach (var slice in _slices)
        {
            Destroy(slice);
        }
    }
}
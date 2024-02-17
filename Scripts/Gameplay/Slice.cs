using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public Type CurrentType;
    public float Force = 300f;

    [SerializeField] private GameObject _ghostPrefab;

    private Transform _target;
    private Rigidbody2D _rigidBody;

    private bool _isBackward;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Circle").transform;
    }
    private void SpawnGhost()
    { 
        var ghost = Instantiate(_ghostPrefab);
        ghost.transform.position = transform.position;
        ghost.GetComponent<SpriteRenderer>().DOFade(0, 0.4f).SetLink(ghost);
        ghost.transform.DOScale(0, 0.4f).SetLink(ghost);
        Destroy(ghost, 0.4f);
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        Vector2 direction;
        if(!_isBackward)
            direction = (_target.position - transform.position).normalized;
        else
            direction = (_target.position - transform.position).normalized * -1;

        _rigidBody.velocity = direction * Force * Time.fixedDeltaTime;
    }
    public void ChangeDirection()
    {
        InvokeRepeating("SpawnGhost", 0f, 0.03f);
        Force *= 5;
        _isBackward = true;
        Destroy(gameObject, 2f);
    }
}

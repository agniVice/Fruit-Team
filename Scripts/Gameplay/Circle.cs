using DG.Tweening;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private int _rotationAngle;
    private Transform _firstFruit;
    private Transform _secondFruit;

    private void Start()
    {
        _firstFruit = transform.GetChild(0);
        _secondFruit = transform.GetChild(1);
    }

    private void OnEnable()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
    }
    private void OnPlayerMouseDown()
    {
        Rotate();
    }
    private void Rotate()
    {
        _rotationAngle -= 180;
        transform.DOLocalRotate(new Vector3(0, 0, _rotationAngle), 0.3f).SetLink(gameObject).SetEase(Ease.Linear);
        _firstFruit.DOLocalRotate(new Vector3(0, 0, -_rotationAngle), 0.3f).SetLink(gameObject).SetEase(Ease.Linear);
        _secondFruit.DOLocalRotate(new Vector3(0, 0, -_rotationAngle), 0.3f).SetLink(gameObject).SetEase(Ease.Linear);
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.PopUp, 1, 0.8f);
    }
}

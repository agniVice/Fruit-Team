using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Type CurrentType;
    [SerializeField] private GameObject _particlePrefab;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Slice"))
        {
            if (collision.gameObject.GetComponent<Slice>().CurrentType == CurrentType)
            {
                PlayerScore.Instance.AddScore();
                AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ScoreAdd, Random.Range(1, 1.1f));
                collision.gameObject.GetComponent<Slice>().ChangeDirection();

                _camera.DOShakePosition(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
                _camera.DOShakeRotation(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
            }
            else
            {
                _camera.DOShakePosition(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
                _camera.DOShakeRotation(0.4f, 0.2f, fadeOut: true).SetUpdate(true);

                SpawnParticle();

                transform.DOScale(0, 0.3f).SetLink(gameObject).SetUpdate(true);

                Destroy(collision.gameObject);

                GameState.Instance.FinishGame();
            }
        }
    }
}

using UnityEngine;

public class DieMobs : MonoBehaviour
{

    private float _playerFeetOffset;
    private Rigidbody2D _playerRb;
    private float _mobOffset;
    private Collider2D _col;
    private GameObject[] _enemies;
    [SerializeField] private float _mobOffsetCoef = 0.5f;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private float _forceValue = 15f;
    [SerializeField] public bool _inRangeForInstantKill;
    void Awake()
    {
        GameObject _player = GameObject.FindWithTag(_playerTag);
        _col = GetComponent<Collider2D>();
        _mobOffset = _col.bounds.extents.y * _mobOffsetCoef;
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _playerFeetOffset = _player.GetComponent<Collider2D>().bounds.extents.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.y - _playerFeetOffset > _col.bounds.center.y + _mobOffset && _playerRb.linearVelocityY < 0 && other.CompareTag(_playerTag))
        {
            Destroy(gameObject);
            _playerRb.AddForceY(_forceValue, ForceMode2D.Impulse);
        }

        if(other.CompareTag("RangeForInstantKill") && _inRangeForInstantKill == true)
        {
            for (int i = 0; i < _enemies.Length; i++)
                Destroy(_enemies[i]);

            _inRangeForInstantKill = false;
        }

    }



}

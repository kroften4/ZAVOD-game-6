using UnityEditor.Rendering;
using UnityEditor.UIElements;
using UnityEngine;

public class KillMobs : MonoBehaviour
{

    private float _playerFeetOffset;
    private Rigidbody2D _playerRb;
    private float _mobOffset;
    private Collider2D _col;
    [SerializeField] private float _mobOffsetCoef = 0.5f;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private float _forceValue = 15f;
    void Awake()
    {
        GameObject _player = GameObject.FindWithTag(_playerTag);
        _col = GetComponent<Collider2D>();
        _mobOffset = _col.bounds.extents.y * _mobOffsetCoef; 
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _playerFeetOffset = _player.GetComponent<Collider2D>().bounds.extents.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"_mobOffset {_mobOffset}, transform.TransformPoint(_col.bounds.center).y {transform.TransformPoint(_col.bounds.center).y}, other.transform.position.y - _playerFeetOffset {other.transform.position.y - _playerFeetOffset}, _col.bounds.center {_col.bounds.center}");

        if (other.transform.position.y - _playerFeetOffset > _col.bounds.center.y + _mobOffset && _playerRb.linearVelocityY < 0 && other.CompareTag(_playerTag))
        {
            Destroy(gameObject);
            _playerRb.AddForceY(_forceValue, ForceMode2D.Impulse);
        }
    }



}

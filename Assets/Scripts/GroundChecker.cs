using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundRaycastLength = 0.02f;
    [SerializeField] private float _wallRaycastLength = 0.02f;
    [SerializeField] private LayerMask _groundLayerMask;
    private float _borderOffsetX;
    private float _borderOffsetY;

    private void Awake()
    {
        _borderOffsetX = GetComponent<Collider2D>().bounds.extents.x;
        _borderOffsetY = GetComponent<Collider2D>().bounds.extents.y;
    }

    public int GetWallDirection()
    {
        bool leftWall = Physics2D.Raycast(
            new Vector2(transform.position.x - _borderOffsetX, transform.position.y),
            Vector2.left,
            _wallRaycastLength,
            _groundLayerMask
        );
        bool rightWall = Physics2D.Raycast(
          new Vector2(transform.position.x + _borderOffsetX, transform.position.y),
          Vector2.right,
          _wallRaycastLength,
          _groundLayerMask
        );


        if (leftWall)
            return -1;
        if (rightWall)
            return 1;
        return 0;
    }

    public bool IsGrounded()
    {

        float _raycastOffsetY = _borderOffsetY;
        bool hitLeft = Physics2D.Raycast(
            new Vector2(transform.position.x - _borderOffsetX, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        bool hitCenter = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        bool hitRight = Physics2D.Raycast(
            new Vector2(transform.position.x + _borderOffsetX, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        return hitLeft || hitCenter || hitRight;
    }
}

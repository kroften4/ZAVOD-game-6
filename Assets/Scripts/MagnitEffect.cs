using System.Collections;
using UnityEngine;

public class MagnitEffect : MonoBehaviour
{
    public bool magnit = false;
    public float increaseFactor = 30f;
    public float duration = 10f;

    private CircleCollider2D _circleCollider;

    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (magnit)
        {
            StartCoroutine(IncreaseCollider());
            magnit = false;
        }
    }

    private IEnumerator IncreaseCollider()
    {
        _circleCollider.radius *= increaseFactor;

        yield return new WaitForSeconds(duration);

        _circleCollider.radius /= increaseFactor;
    }
}

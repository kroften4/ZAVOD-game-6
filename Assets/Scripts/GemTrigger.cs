using UnityEngine;

public class Gem_Trigger : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreScript.scoreValue += 1;
            Destroy(gameObject);
        }
    }
}

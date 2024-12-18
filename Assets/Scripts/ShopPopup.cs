using UnityEngine;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;
    private ShopCardGenerator _shopCardGenerator;
    [SerializeField] private float _interval = 30;
    private float _currTime = 0;

    private void Start()
    {
        _shopCardGenerator = _shopCanvas.GetComponent<ShopCardGenerator>();
    }

    private void Update()
    {
        _currTime += Time.deltaTime;
        if ( _currTime > _interval )
        {
            _currTime = 0;
            _shopCardGenerator.GenerateCards();
            _shopCanvas.SetActive(true);
        }
    }
}

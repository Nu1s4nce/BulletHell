using TMPro;
using UnityEngine;
using Zenject;

public class MainShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _powerUpPanel;
    [SerializeField] private GameObject _shopEnterTextContainer;

    private bool _isShopOpened;
    private bool _isInShopZone;

    private IInputService _inputService;


    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Awake()
    {
        _isShopOpened = false;
        _isInShopZone = false;
    }

    private void Update()
    {
        if (_isInShopZone && _inputService.E_Clicked())
        {
            _isShopOpened = !_isShopOpened;
            _powerUpPanel.SetActive(_isShopOpened);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _shopEnterTextContainer.SetActive(true);
        _isInShopZone = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _shopEnterTextContainer.SetActive(false);
        _isInShopZone = false;
    }
}
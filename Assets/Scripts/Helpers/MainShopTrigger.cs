using UnityEngine;

public class MainShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _powerUpPanel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        _powerUpPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _powerUpPanel.SetActive(false);
    }
}

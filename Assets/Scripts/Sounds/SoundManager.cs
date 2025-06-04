using UnityEngine;

public class SoundManager : MonoBehaviour, ISoundManager
{
    public AudioSource soundSource;
    
    [SerializeField] private AudioClip playerDamageReceive;
    [SerializeField] private AudioClip refreshShopSound;
    [SerializeField] private AudioClip currencyPickupSound;
    [SerializeField] private AudioClip foodPickupSound;

    public void PlayPlayerDamageReceive()
    {
        soundSource.PlayOneShot(playerDamageReceive, 0.5f);
    }
    public void PlayRefreshShop()
    {
        soundSource.PlayOneShot(refreshShopSound);
    }
    public void PlayCurrencyPickUp()
    {
        soundSource.PlayOneShot(currencyPickupSound);
    }
    public void PlayFoodPickUp()
    {
        soundSource.PlayOneShot(foodPickupSound, 0.5f);
    }
}

public interface ISoundManager
{
    void PlayPlayerDamageReceive();
    void PlayRefreshShop();
    void PlayCurrencyPickUp();
    void PlayFoodPickUp();
}

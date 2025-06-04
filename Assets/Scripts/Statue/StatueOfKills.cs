using TMPro;
using UnityEngine;
using Zenject;

public class StatueOfKills : MonoBehaviour
{
    private IHeroProvider _heroProvider;
    private IProgressService _progressService;

    [SerializeField] private float distanceToPlayer;
    [SerializeField] private TMP_Text text;
    private bool _isPlayerInRange;
    

    [Inject]
    public void Construct(IHeroProvider heroProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _heroProvider = heroProvider;
    }
    private void Start()
    {
        text.gameObject.SetActive(true);
        _progressService.EnemyKilled += UpdateText;
    }

    private void Update()
    {
        _isPlayerInRange = Vector3.Distance(_heroProvider.GetHeroPosition(), transform.position) < distanceToPlayer;

        //text.gameObject.SetActive(_isPlayerInRange);
    }

    private void UpdateText()
    {
        text.text = $"Kills: {_progressService.GetNumberOfKills()} / 30";
    }
}

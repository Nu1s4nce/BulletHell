using TMPro;
using UnityEngine;
using Zenject;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text statsTextObject;
    [SerializeField] private TMP_Text weaponStatsTextObject;
    
    private IConfigProvider _configProvider;
    private IProgressService _progressService;

    [Inject]
    private void Construct(IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _configProvider = configProvider;
    }
    private void Update()
    {
        statsTextObject.text = SetupHeroStatsText();
        weaponStatsTextObject.text = SetupWeaponStatsText();
    }

    private string SetupHeroStatsText()
    {
        string tempStr = "";
        tempStr += $"Move speed : {_configProvider.GetHeroConfig().MoveSpeed + _progressService.ProgressData.HeroData.HeroStatsData[StatId.MoveSpeed]}\n";
        tempStr += $"Collectables pick range : {_configProvider.GetHeroConfig().CollectablesPickRange + _progressService.ProgressData.HeroData.HeroStatsData[StatId.CollectablesPickRange]}\n";
        tempStr += $"Collectables value : {_configProvider.GetHeroConfig().CollectablesValue + _progressService.ProgressData.HeroData.HeroStatsData[StatId.CollectablesValue]}\n";
        tempStr += $"Food heal value  : {_configProvider.GetHeroConfig().FoodHealValue + _progressService.ProgressData.HeroData.HeroStatsData[StatId.FoodHealValue]}\n";
        
        return tempStr;
    }

    private string SetupWeaponStatsText()
    {
        string tempStr = "";
        
        tempStr += "\n AXE \n";
        
        tempStr += $"Damage : {GetAxeConfigData().Damage + _progressService.ProgressData.HeroData.HeroStatsData[StatId.Damage]}\n";
        tempStr += $"Attack range : {GetAxeConfigData().AttackRange + _progressService.ProgressData.HeroData.HeroStatsData[StatId.AttackRange]}\n";
        tempStr += $"Attack rate : {GetAxeConfigData().AttackRate + _progressService.ProgressData.HeroData.HeroStatsData[StatId.AttackRate]}\n";
        tempStr += $"Projectile speed : {GetAxeConfigData().ProjectileSpeed + _progressService.ProgressData.HeroData.HeroStatsData[StatId.ProjectileSpeed]}\n";
        tempStr += $"Multi shot targets : {GetAxeConfigData().MultishotTargets + _progressService.ProgressData.HeroData.HeroStatsData[StatId.MultiShotTargets]}\n";
        
        tempStr += "\n Fire ring \n";
        
        tempStr += $"Damage : {GetRingConfigData().Damage + _progressService.ProgressData.HeroData.HeroStatsData[StatId.Damage]}\n";
        tempStr += $"Attack range : {GetRingConfigData().AttackRange + _progressService.ProgressData.HeroData.HeroStatsData[StatId.AttackRange]}\n";
        tempStr += $"Attack rate : {GetRingConfigData().AttackRate + _progressService.ProgressData.HeroData.HeroStatsData[StatId.AttackRate]}\n";
        
        return tempStr;
    }

    private WeaponsConfigData GetAxeConfigData()
    {
        return _configProvider.GetWeaponsConfig(0);
    } 
    private WeaponsConfigData GetRingConfigData()
    {
        return _configProvider.GetWeaponsConfig(1);
    } 
}

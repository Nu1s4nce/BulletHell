using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDescriptions", menuName = "EnemyDescriptions")]
public class EnemyDescriptions : ScriptableObject
{
    [SerializeField] private List<EnemyDescription> _listMelee;
    [SerializeField] private List<EnemyDescription> _listRange;

    public List<EnemyDescription> ListMelee => _listMelee;
    public List<EnemyDescription> ListRange => _listRange;
}
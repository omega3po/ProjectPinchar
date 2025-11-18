using Entities.Base;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameUI : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Character targetEnemy;
    void Start()
    {
        text.text = targetEnemy.CharacterName;
    }

}

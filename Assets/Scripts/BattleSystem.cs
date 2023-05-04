using System.Collections;
using UnityEngine;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleState state;
    [SerializeField] private Health player;
    [SerializeField] private Health enemy;

    private Battle _battleManager;
    private BattleHUD _playerHUD;
    private BattleHUD _enemyHUD;
    private void Awake()
    {
        _battleManager = FindObjectOfType<Battle>();
    }

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        enemy = _battleManager.SpawnEnemy().GetComponent<Health>();
        
        _playerHUD.SetHUD(player);

        yield return new WaitForSeconds(2f);
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack()
    {
        //enemy.TakeDamage();
        yield return new WaitForSecondsRealtime(2f);
    }
}

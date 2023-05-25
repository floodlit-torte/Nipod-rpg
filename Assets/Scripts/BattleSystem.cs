using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleState state;
    [SerializeField] private Health player;
    [SerializeField] private GameObject[] enemyPrefabs; //array of possible enemies
    [SerializeField] private TMP_Text dialogueText;
    
    [SerializeField] private BattleHUD _playerHUD;
    [SerializeField] private BattleHUD _enemyHUD;

    private Health enemy; //enemy in current battle


    private Animator _playerAnimator;
    private Animator _enemyAnimator;
    
    //Position and rotation to spawn enemy
    private Vector3 _enemyPos = new Vector3(7f, 0f, -1f);

    private WaitForSecondsRealtime _waitTime; //waiting time between actions
    private float coolDown = 2f;

    private Vector3 _pstarterPos;
    private Quaternion _pstarterRot;

    private int _victoryScene = 3;


    private void Start()
    {
        state = BattleState.START;
        _waitTime = new WaitForSecondsRealtime(coolDown);

        _playerAnimator = player.GetComponent<Animator>();

        _pstarterPos = player.transform.position;
        _pstarterRot = player.transform.rotation;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        GameObject enemyGO = SpawnEnemy();
        enemyGO.transform.LookAt(player.transform);
        enemy = enemyGO.GetComponent<Health>();
        _enemyAnimator = enemyGO.GetComponent<Animator>();

        _playerHUD.SetHUD(player);

        yield return _waitTime;
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private GameObject SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        return Instantiate(enemyPrefabs[index], _enemyPos, Quaternion.identity);
    }

    private void PlayerTurn()
    {
        dialogueText.enabled = true;
        dialogueText.text = "Choose an action.";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerSkill());
    }

    private IEnumerator PlayerSkill()
    {
        player.Heal(50);
        dialogueText.text = "50HP restored";
        yield return _waitTime;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator PlayerAttack()
    {
        bool isDead = enemy.TakeDamage(player.Damage);

        _playerAnimator.SetTrigger("MeleeAttack");
        dialogueText.text = $"You made {player.Damage} damage";
        yield return _waitTime;
        _enemyAnimator.SetTrigger("Hit");

        player.transform.position = _pstarterPos;
        player.transform.rotation = _pstarterRot;

        if (isDead)
        {
            _enemyAnimator.SetTrigger("Die");
            yield return _waitTime;
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return _waitTime;
        bool isDead = player.TakeDamage(enemy.Damage);
        _enemyAnimator.SetTrigger("Attack");
        dialogueText.text = $"{enemy.Damage} damege taken.";
        _playerAnimator.SetTrigger("Hit");
        _playerHUD.setHP(player.CurrentHP);

        yield return _waitTime;

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if(state == BattleState.WON)
            SceneManager.LoadScene(_victoryScene);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}

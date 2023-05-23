using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;
using UnityEngine.UI;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleState state;
    [SerializeField] private Health player;
    [SerializeField] private GameObject[] enemyPrefabs; //array of possible enemies
    [SerializeField] private Health enemy; //enemy in current battle
    [SerializeField] private Text dialogueText;

    private BattleHUD _playerHUD;
    private BattleHUD _enemyHUD;

    private Random _random = new Random();
    
    //Position and rotation to spawn enemy
    private Vector3 _enemyPos = new Vector3(7f, 0f, -1f);
    private Quaternion _enemyRot = new Quaternion(0f, -70f, 0f, 0f);

    private WaitForSecondsRealtime _waitTime; //waiting time between actions
    private float coolDown = 2f;

    private void Start()
    {
        state = BattleState.START;
        _waitTime = new WaitForSecondsRealtime(coolDown);
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        GameObject enemyGO = SpawnEnemy();
        enemy = enemyGO.GetComponent<Health>();

        _playerHUD.SetHUD(player);

        yield return _waitTime;
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private GameObject SpawnEnemy()
    {
        return Instantiate(enemyPrefabs[_random.NextInt(0, enemyPrefabs.Length)], _enemyPos, _enemyRot);
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack()
    {
        bool isDead = enemy.TakeDamage(player.Damage);
        
        //update enemyHUD
        
        yield return _waitTime;

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        //play animation
        //enemy.TakeDamage()
    }

    private IEnumerator EnemyTurn()
    {
        bool isDead = player.TakeDamage(enemy.Damage);

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Victory Scene
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); // Defeated Scene
        }
    }
}

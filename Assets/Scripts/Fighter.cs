using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO monster respawn into a random monster
// TODO save player progress somehow
public class Fighter : MonoBehaviour
{
  public FighterScriptableObject fighterScriptableObject;
  public Fighter otherFighter;
  public Text nameText;
  public Text attackText;
  public Text attacksPerSecondText;
  public Text currentHpText;
  public Text defenseText;
  public Text levelText;
  public Animator animator;
  public int currentHp;
  public int maxHp;
  public int attack;
  public float attacksPerSecond;
  public int defense;
  public int level;
  private float nextTimeToAttack = 0f;

  // Use this for initialization
  protected void Start()
  {
    level = 1;
    currentHp = fighterScriptableObject.maxHp;
    maxHp = fighterScriptableObject.maxHp;
    attack = fighterScriptableObject.attack;
    attacksPerSecond = fighterScriptableObject.attacksPerSecond;
    defense = fighterScriptableObject.defense;
    nameText.text = fighterScriptableObject.name;
    animator.runtimeAnimatorController = fighterScriptableObject.animatorController;
    DisplayUpdatedStats();
    PostStart();
  }

  protected virtual void PostStart()
  {
    // Override this
  }

  protected virtual void DisplayUpdatedStats()
  {
    attackText.text = "Attack: " + attack.ToString();
    attacksPerSecondText.text = "Attacks/sec: " + attacksPerSecond.ToString("0.00");
    currentHpText.text = "HP: " + currentHp.ToString();
    defenseText.text = "Def: " + defense.ToString();
    levelText.text = "Lvl: " + level.ToString();
  }

  // Update is called once per frame
  void Update()
  {
    if (!animator.GetBool("dead") && !otherFighter.animator.GetBool("dead") && Time.time >= nextTimeToAttack)
    {
      animator.SetBool("attacking", true);
      bool isKillingBlow = otherFighter.TakeDamage(this);
      if (isKillingBlow)
      {
        OnKill(otherFighter.fighterScriptableObject.experienceGiven);
      }
      nextTimeToAttack = Time.time + 1 / attacksPerSecond;
      StartCoroutine(WaitForAttack());
    }
  }

  IEnumerator WaitForAttack()
  {
    yield return new WaitForSeconds(1 / (attacksPerSecond * 4));
    animator.SetBool("attacking", false);
  }

  /**
	 * @returns whether the attack is a killing blow
	 */
  public bool TakeDamage(Fighter attacker)
  {
    int expectedDamage = attacker.attack - defense;
    if (expectedDamage > 0)
    {
      // TODO display damage number
      currentHp -= expectedDamage;
      if (currentHp <= 0)
      {
        OnDeath();
        return true;
      }
      else
      {
        currentHpText.text = "HP: " + currentHp.ToString();
        return false;
      }
    }
    return false;
  }

  protected virtual void OnDeath()
  {
    currentHpText.text = "Dead";
    animator.SetBool("dead", true);
  }

  protected virtual void OnKill(int experienceGained)
  {
    // Override this
  }
}

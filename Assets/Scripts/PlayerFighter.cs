using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFighter : Fighter
{
  public Text availableStatPointsText;
  public Text experienceText;

  private int availableStatPoints = 5;
  private int experience = 0;
  private int experienceNeededForLevelUp = 1;

  protected override void OnKill(int experienceGained)
  {
    experience += experienceGained;
    while (experience >= experienceNeededForLevelUp)
    {
      experienceNeededForLevelUp = experienceNeededForLevelUp * 2 + 1;
      level++;
      availableStatPoints++;
      DisplayUpdatedStats();
    }
  }

  protected override void DisplayUpdatedStats()
  {
    base.DisplayUpdatedStats();
    availableStatPointsText.text = "Stat Points: " + availableStatPoints.ToString();
    experienceText.text = "Exp: " + experience.ToString();
  }

  public void IncreaseMaxHP()
  {
    if (availableStatPoints <= 0)
    {
      return;
    }
    availableStatPoints--;
    maxHp++;
    currentHp = maxHp;
    DisplayUpdatedStats();
  }

  public void IncreaseMaxAttack()
  {
    if (availableStatPoints <= 0)
    {
      return;
    }
    availableStatPoints--;
    attack++;
    DisplayUpdatedStats();
  }

  public void IncreaseMaxAttackSpeed()
  {
    if (availableStatPoints <= 0)
    {
      return;
    }
    availableStatPoints--;
    attacksPerSecond += 0.1f;
    DisplayUpdatedStats();
  }

  public void IncreaseMaxDefense()
  {
    if (availableStatPoints <= 0)
    {
      return;
    }
    availableStatPoints--;
    defense++;
    DisplayUpdatedStats();
  }

  protected override void OnDeath()
  {
    base.OnDeath();
    StartCoroutine(WaitThenRespawn());
  }

  private void Respawn()
  {
    animator.SetBool("dead", false);
    currentHp = maxHp;
    DisplayUpdatedStats();
  }

  // TODO respawn time must be faster than opponent's attack speed
  IEnumerator WaitThenRespawn()
  {
    yield return new WaitForSeconds(5.0f);
    Respawn();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "FighterScriptableObject")]
public class FighterScriptableObject : ScriptableObject
{
  public new string name;
  public int maxHp;
  public int attack;
  public float attacksPerSecond;
  public int defense;
  public int experienceGiven;
  public RuntimeAnimatorController animatorController;
}

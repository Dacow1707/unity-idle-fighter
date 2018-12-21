using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFighter : Fighter {

	private FighterScriptableObject[] bosses;
  private FighterScriptableObject[] monsters;

	protected override void PostStart() {
		monsters = Resources.LoadAll<FighterScriptableObject>("FighterScriptableObjects/Monsters");
    bosses = Resources.LoadAll<FighterScriptableObject>("FighterScriptableObjects/Bosses");
	}

	protected override void OnKill(int experienceGained) {
		level++;
		DisplayUpdatedStats();
	}

	protected override void OnDeath() {
		base.OnDeath();
		StartCoroutine(WaitThenRespawn());
	}

	private void Respawn() {
		animator.SetBool("dead", false);
    currentHp = fighterScriptableObject.maxHp;
		maxHp = fighterScriptableObject.maxHp;
		attack = fighterScriptableObject.attack;
		attacksPerSecond = fighterScriptableObject.attacksPerSecond;
		defense = fighterScriptableObject.defense;
		nameText.text = fighterScriptableObject.name;
		animator.runtimeAnimatorController = fighterScriptableObject.animatorController;
		DisplayUpdatedStats();
	}

	// TODO respawn time must be faster than opponent's attack speed
	IEnumerator WaitThenRespawn() 
	{
		yield return new WaitForSeconds(0.5f);

    if(Random.value > 0.9) { //%10 percent chance
      fighterScriptableObject = bosses[Random.Range (0, bosses.Length)];
    } else {
      fighterScriptableObject = monsters[Random.Range (0, monsters.Length)];
    }
		Respawn();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
	None,
	Melee,
	Ranged,
}

[CreateAssetMenu(fileName = "New Unit", menuName = "Cards/Unit")]
public class Unit : SpringsCard
{
	public string attack;
	public string health;
	public AttackType attackType;
}

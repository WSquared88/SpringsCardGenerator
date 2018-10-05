using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
	Invalid = -1,
	Unit,
	Worker,
	Spell,
}

public abstract class SpringsCard : ScriptableObject
{
	public new string name;
	public CardType type;
	public string goldCost;
	public string manaCost;
	public string effect;
	public bool hasSiege;
}

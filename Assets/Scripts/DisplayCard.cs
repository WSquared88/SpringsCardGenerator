using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour
{
	public Image[] cardFrames;
	public Text nameText;
	public Text goldText;
	public Text manaText;
	public Text attackText;
	public Text healthText;
	public Text effectText;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ActivateCardImage(SpringsCard card)
	{
		DeactivateOldCard();
		nameText.text = card.name;
		goldText.text = card.goldCost;
		manaText.text = card.manaCost;
		effectText.text = card.effect;

		if(card is Unit)
		{
			cardFrames[0].enabled = true;
			Unit unit = card as Unit;
			attackText.text = unit.attack;
			healthText.text = unit.health;
		}
		else if(card is Spell)
		{
			cardFrames[1].enabled = true;
		}
		else if(card is Worker)
		{
			cardFrames[2].enabled = true;
		}
	}

	void DeactivateOldCard()
	{
		for(int i = 0;i<cardFrames.Length;i++)
		{
			cardFrames[i].enabled = false;
		}
	}
}

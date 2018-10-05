using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardBackgrounds
{
	Unit = 0,
	Spell,
	Worker,

	CardBackgroundsCount,
}

public class DisplayCard : MonoBehaviour
{
	public GameObject[] cardFrames;
	public Text nameText;
	public Text goldText;
	public Text manaText;
	public Text attackText;
	public Text healthText;
	public Text effectText;
	public Image SiegeIcon;

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
			cardFrames[(int)CardBackgrounds.Unit].SetActive(true);
			Unit unit = card as Unit;
			attackText.text = unit.attack;
			healthText.text = unit.health;
			attackText.enabled = true;
			healthText.enabled = true;
			SiegeIcon.enabled = true;
		}
		else if(card is Spell)
		{
			cardFrames[(int)CardBackgrounds.Spell].SetActive(true);
		}
		else if(card is Worker)
		{
			cardFrames[(int)CardBackgrounds.Worker].SetActive(true);
		}
	}

	void DeactivateOldCard()
	{
		for(int i = 0;i<cardFrames.Length;i++)
		{
			cardFrames[i].SetActive(false);
		}

		attackText.enabled = false;
		healthText.enabled = false;
		SiegeIcon.enabled = false;
	}
}

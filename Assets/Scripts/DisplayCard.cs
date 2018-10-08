using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardBackgrounds
{
	Unit = 0,
	Spell,
	Worker,
    Token,

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
	public GameObject[] attackTypeImages;
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

		switch (card.type)
		{
			case CardType.Unit:
			{
				cardFrames[(int)CardBackgrounds.Unit].SetActive(true);
				Unit unit = card as Unit;
				attackText.text = unit.attack;
				healthText.text = unit.health;

				if(unit.attackType > AttackType.Invalid)
				{
					attackTypeImages[(int)unit.attackType].SetActive(true);
				}

				attackText.enabled = true;
				healthText.enabled = true;
				SiegeIcon.enabled = unit.hasSiege;
				break;
			}
			case CardType.Spell:
			{
				cardFrames[(int)CardBackgrounds.Spell].SetActive(true);
				break;
			}
			case CardType.Worker:
			{
				cardFrames[(int)CardBackgrounds.Worker].SetActive(true);
				break;
			}
            case CardType.Token:
            {
                cardFrames[(int)CardBackgrounds.Token].SetActive(true);
                Unit unit = card as Unit;
                attackText.text = unit.attack;
                healthText.text = unit.health;

                if (unit.attackType > AttackType.Invalid)
                {
                    attackTypeImages[(int)unit.attackType].SetActive(true);
                }

                attackText.enabled = true;
                healthText.enabled = true;
                SiegeIcon.enabled = unit.hasSiege;
                break;
            }
		}
	}

	void DeactivateOldCard()
	{
		for(int i = 0;i<cardFrames.Length;i++)
		{
			cardFrames[i].SetActive(false);
		}

		for(int i = 0;i<attackTypeImages.Length;i++)
		{
			attackTypeImages[i].SetActive(false);
		}

		attackText.enabled = false;
		healthText.enabled = false;
		SiegeIcon.enabled = false;
	}
}

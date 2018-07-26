using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CardTextParser : MonoBehaviour
{
	public string path = "Assets/cardFile.csv";
	public DisplayCard cardUI;
	public Dropdown cardDropdown;
	List<SpringsCard> cards = new List<SpringsCard>();

	// Use this for initialization
	void Start ()
	{
		StreamReader reader = new StreamReader(path);
		string line;
		List<string> cardNames = new List<string>();

		while((line = reader.ReadLine()) != null)
		{
			cardNames.Add(GenerateCard(line));
		}

		cardDropdown.AddOptions(cardNames);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public string GenerateCard(string cardText)
	{
		string[] tokenizedText = cardText.Split(',');
		SpringsCard card;

		if (tokenizedText[1] == CardType.Unit.ToString())
		{
			Unit unit = new Unit();
			unit.attack = tokenizedText[4];
			unit.health = tokenizedText[5];
			//unit.attackType = (AttackType)Enum.Parse(typeof(AttackType), tokenizedText[6]);
			card = unit;
			//card.type = CardType.Unit;
		}
		else if(tokenizedText[1] == CardType.Spell.ToString())
		{
			card = new Spell();
		}
		else if(tokenizedText[1] == CardType.Worker.ToString())
		{
			card = new Worker();
		}
		else
		{
			card = new Unit();
		}

		card.name = tokenizedText[0];
		card.goldCost = tokenizedText[2];
		card.manaCost = tokenizedText[3];
		card.effect = tokenizedText[7];
		cards.Add(card);

		return card.name;
	}

	public void OnCardSelected(int selectionIndex)
	{
		cardUI.ActivateCardImage(cards[selectionIndex]);
	}
}

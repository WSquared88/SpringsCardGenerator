using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

enum CardParserItems
{
	CardName = 0,
	CardType,
	GoldCost,
	ManaCost,
	Attack,
	Health,
	AttackType,
	HasSiege,
	CardEffect,

	CardParserItemsCount,
}

public class CardTextParser : MonoBehaviour
{
    public string pathWithoutExtension = "Assets/cardFile";
	string pathExtension = ".tsv";
    public DisplayCard cardUI;
    public Dropdown cardDropdown;
    List<SpringsCard> cards = new List<SpringsCard>();

    // Use this for initialization
    void Start()
    {
        StreamReader reader = new StreamReader(pathWithoutExtension + pathExtension);
        string line;
        List<string> cardNames = new List<string>();

        while ((line = reader.ReadLine()) != null)
        {
            string cardName = GenerateCard(line);

            if (cardName != null)
            {
                cardNames.Add(cardName);
            }
        }

        cardDropdown.AddOptions(cardNames);
        cardDropdown.RefreshShownValue();
        OnCardSelected(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GenerateCard(string cardText)
    {
        string[] tokenizedText = cardText.Split('\t');
        SpringsCard card;
        bool addCard = true;

        if (tokenizedText[(int)CardParserItems.CardType] == CardType.Unit.ToString())
        {
            Unit unit = new Unit();
            unit.attack = tokenizedText[(int)CardParserItems.Attack];
            unit.health = tokenizedText[(int)CardParserItems.Health];
            //unit.attackType = (AttackType)Enum.Parse(typeof(AttackType), tokenizedText[(int)CardParserItems.AttackType]);

			if(tokenizedText[(int)CardParserItems.HasSiege] == "TRUE")
			{
				unit.hasSiege = true;
			}
			else
			{
				unit.hasSiege = false;
			}

			unit.type = CardType.Unit;
            card = unit;
        }
        else if (tokenizedText[(int)CardParserItems.CardType] == CardType.Spell.ToString())
        {
            card = new Spell();
			card.type = CardType.Spell;
		}
        else if (tokenizedText[(int)CardParserItems.CardType] == CardType.Worker.ToString())
        {
            card = new Worker();
			card.type = CardType.Worker;
		}
        else
        {
            //If tokenizedText[1] doesn't equal one of the other ifs then the line is probably one of the headers
            card = new Unit();
            addCard = false;
        }

		int goldCost;
		int manaCost; 
		bool goldConvertedSuccessfully = int.TryParse(tokenizedText[(int)CardParserItems.GoldCost], out goldCost);
		bool manaConvertedSuccessfully = int.TryParse(tokenizedText[(int)CardParserItems.ManaCost], out manaCost);

		if (goldConvertedSuccessfully && goldCost > 0)
		{
			tokenizedText[(int)CardParserItems.GoldCost] += "G";
		}

		if (manaConvertedSuccessfully && manaCost > 0)
		{
			tokenizedText[(int)CardParserItems.ManaCost] += "M";
		}

		card.name = tokenizedText[(int)CardParserItems.CardName];
        card.goldCost = tokenizedText[(int)CardParserItems.GoldCost];
        card.manaCost = tokenizedText[(int)CardParserItems.ManaCost];
        card.effect = tokenizedText[(int)CardParserItems.CardEffect];

        if (addCard)
        {
            cards.Add(card);
            return card.name;
        }

        return null;
    }

    public void OnCardSelected(int selectionIndex)
    {
        cardUI.ActivateCardImage(cards[selectionIndex]);
    }

    public int GetNumOfCards()
    {
        return cards.Count;
    }

    public SpringsCard GetCard(int cardIndex)
    {
        return cards[cardIndex];
    }
}

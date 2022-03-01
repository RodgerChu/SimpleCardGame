using System;
using System.Linq;
using LanguageExt;

namespace SimpleCardGame
{
	class Program
	{
		static void Main(string[] args)
		{
			var random = new Some<Random>(new Random(DateTime.Now.Second));
			var cardValues = new Some<CardValue[]>(Enum.GetValues<CardValue>());
			var cardSuits = new Some<CardSuit[]>(Enum.GetValues<CardSuit>());
			var trumpSuit = GetRandomCardSuit(random, cardSuits);
			var deck = CreateCardDeck(cardValues, cardSuits);
			deck.ShuffleDeck(random).SplitDeckInHalves().ProceedGame((cardOne, cardTwo) =>
			{
				//if both cards have trump suit or neither then compare only values
				if (cardOne.suit == trumpSuit && cardTwo.suit == trumpSuit ||
				    cardOne.suit != trumpSuit && cardTwo.suit != trumpSuit)
				{
					if (cardOne.value == cardTwo.value)
						return CardCompareResult.Tie;
					if (cardOne.value > cardTwo.value)
						return CardCompareResult.CardOne;
					return CardCompareResult.CardTwo;
				}
				
				return cardOne.suit == trumpSuit 
					? CardCompareResult.CardOne 
					: CardCompareResult.CardTwo;
			});
		}

		private static CardDeck CreateCardDeck(CardValue[] cardValues, CardSuit[] cardSuits)
		{
			return new CardDeck(cardValues.Collect(value =>
			{
				return cardSuits.Map(suit => new CardInfo(suit, value));
			}).ToArray());
		}

		private static CardSuit GetRandomCardSuit(Random random, CardSuit[] suits)
		{
			return new Some<CardSuit>(suits[random.Next(suits.Length)]);
		}
	}
}
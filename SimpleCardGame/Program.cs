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
			Logger.Log(deck);
			var shuffledDeck = ShuffleCards(random, deck);
			Logger.Log(shuffledDeck);
			var splittedDeck = SplitDeckInHalves(shuffledDeck);
			Logger.Log(splittedDeck);
			var gameResult = ProceedGame(splittedDeck, trumpSuit);
			Logger.Log(gameResult);
			Console.ReadKey();
		}

		private static Some<CardDeck> CreateCardDeck(Some<CardValue[]> cardValues, Some<CardSuit[]> cardSuits)
		{
			return new Some<CardDeck>(new CardDeck(cardValues.Value.Collect(value =>
			{
				return cardSuits.Value.Map(suit => new CardInfo(suit, value));
			}).ToArray()));
		}

		private static Some<CardSuit> GetRandomCardSuit(Some<Random> random, Some<CardSuit[]> suits)
		{
			return new Some<CardSuit>(suits.Value[random.Value.Next(suits.Value.Length)]);
		}

		private static Some<CardDeck> ShuffleCards(Some<Random> random, Some<CardDeck> cards)
		{
			CardInfo[] shuffledCards = new CardInfo[cards.Value.cards.Length];
			cards.Value.cards.CopyTo(shuffledCards, 0);
			for (int i = 0; i < shuffledCards.Length - 1; ++i) 
			{
				var r = random.Value.Next(i, cards.Value.cards.Length);
				(shuffledCards[r], shuffledCards[i]) = (shuffledCards[i], shuffledCards[r]);
			}

			return new Some<CardDeck>(new CardDeck(shuffledCards));
		}

		private static Some<SplittedInTwoCardDeck> SplitDeckInHalves(Some<CardDeck> cards)
		{
			var halfDeckLength = cards.Value.cards.Length / 2;
			var pOneCards = new CardInfo[halfDeckLength];
			var pTwoCards = new CardInfo[halfDeckLength];

			var pOneCardIndex = 0;
			var pTwoCardIndex = 0;
			cards.Value.ForEach((index, card) =>
			{
				if ((index + 1) % 2 == 0)
				{
					pOneCards[pOneCardIndex] = card;
					pOneCardIndex++;
				}
				else
				{
					pTwoCards[pTwoCardIndex] = card;
					pTwoCardIndex++;
				}
			});
			
			return new Some<SplittedInTwoCardDeck>(new SplittedInTwoCardDeck(pOneCards, pTwoCards));
		}

		private static Some<CardGameResult> ProceedGame(Some<SplittedInTwoCardDeck> playersDecks, Some<CardSuit> trumpSuit)
		{
			var playerOnePoints = 0;
			var playerTwoPoints = 0;
			playersDecks.Value.ForEach((index, pOneCard, pTwoCard) =>
			{
				var receivedPoints = GetPointsDeltaFromCards(pOneCard, pTwoCard, trumpSuit);
				playerOnePoints += receivedPoints.Value.playerOnePointsDelta.Value;
				playerTwoPoints += receivedPoints.Value.playerTwoPointsDelta.Value;
			});

			return new Some<CardGameResult>(new CardGameResult(new Points(playerOnePoints), new Points(playerTwoPoints)));
		}

		private static Some<CardGamePointsDelta> GetPointsDeltaFromCards(
			Some<CardInfo> pOneCard, 
			Some<CardInfo> pTwoCard,
			Some<CardSuit> trumpSuit)
		{
			var playerOnePoints = 0;
			var playerTwoPoints = 0;
			if (pOneCard.Value.suit == trumpSuit.Value)
			{
				playerOnePoints += 2;
			}
			else if (pOneCard.Value.suit == trumpSuit.Value)
			{
				playerTwoPoints += 2;
			}
			else if (pOneCard.Value.value > pTwoCard.Value.value)
			{
				playerOnePoints += 2;
			}
			else if (pTwoCard.Value.value > pOneCard.Value.value)
			{
				playerTwoPoints += 2;
			}
			else
			{
				playerOnePoints++;
				playerTwoPoints++;
			}

			return new Some<CardGamePointsDelta>(new CardGamePointsDelta(new Points(playerOnePoints), new Points(playerTwoPoints)));
		}
	}
}
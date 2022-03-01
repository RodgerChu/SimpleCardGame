using System;

namespace SimpleCardGame
{
	public readonly struct CardDeck
	{
		public readonly CardInfo[] cards;

		public CardDeck(CardInfo[] cards) => this.cards = cards;

		public void ForEach(Action<int, CardInfo> action)
		{
			for (int i = 0; i < cards.Length; i++)
			{
				action.Invoke(i, cards[i]);
			}
		}

		public CardDeck ShuffleDeck(Random random)
		{
			CardInfo[] shuffledCards = new CardInfo[cards.Length];
			cards.CopyTo(shuffledCards, 0);
			for (int i = 0; i < shuffledCards.Length - 1; ++i) 
			{
				var r = random.Next(i, cards.Length);
				(shuffledCards[r], shuffledCards[i]) = (shuffledCards[i], shuffledCards[r]);
			}
			
			return new CardDeck(shuffledCards);
		}
		
		public SplittedInTwoCardDeck SplitDeckInHalves()
		{
			var halfDeckLength = cards.Length / 2;
			var pOneCards = new CardInfo[halfDeckLength];
			var pTwoCards = new CardInfo[halfDeckLength];

			var pOneCardIndex = 0;
			var pTwoCardIndex = 0;
			ForEach((index, card) =>
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
			
			return new SplittedInTwoCardDeck(pOneCards, pTwoCards);
		}
	}
}
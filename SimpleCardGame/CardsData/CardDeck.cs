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
	}
}
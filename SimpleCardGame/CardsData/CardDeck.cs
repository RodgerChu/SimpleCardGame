namespace SimpleCardGame
{
	public readonly struct CardDeck
	{
		public readonly CardInfo[] cards;

		public CardDeck(CardInfo[] cards) => this.cards = cards;
	}
}
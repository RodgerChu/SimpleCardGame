namespace SimpleCardGame
{
	public enum CardSuit
	{
		Spades, Hearts, Cross, Diamonds
	}

	public enum CardValue
	{
		Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
		Jack, Queen, King, Ace
	}
	
	public struct CardInfo
	{
		public readonly CardSuit suit;
		public readonly CardValue value;

		public CardInfo(CardSuit suit, CardValue value)
		{
			this.suit = suit;
			this.value = value;
		}
	}
}
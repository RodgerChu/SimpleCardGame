namespace SimpleCardGame
{
	public readonly struct Points
	{
		public readonly int Value;

		public Points(int value)
		{
			Value = value;
		}
	}

	public readonly struct CardGameResult
	{
		public readonly Points playerOnePoints;
		public readonly Points playerTwoPoints;

		public CardGameResult(Points playerOnePoints, Points playerTwoPoints)
		{
			this.playerOnePoints = playerOnePoints;
			this.playerTwoPoints = playerTwoPoints;
		}
	}

	public readonly struct CardGamePointsDelta
	{
		public readonly Points playerOnePointsDelta;
		public readonly Points playerTwoPointsDelta;

		public CardGamePointsDelta(Points pOneDelta, Points pTwoDelta)
		{
			playerOnePointsDelta = pOneDelta;
			playerTwoPointsDelta = pTwoDelta;
		}
	}
}
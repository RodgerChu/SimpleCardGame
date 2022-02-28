using System;

namespace SimpleCardGame
{
	public readonly struct SplittedInTwoCardDeck
	{
		public readonly CardInfo[] firstHalf;
		public readonly CardInfo[] secondHalf;

		public SplittedInTwoCardDeck(CardInfo[] firstHalf, CardInfo[] secondHalf)
		{
			this.firstHalf = firstHalf;
			this.secondHalf = secondHalf;
		}

		public void ForEach(Action<int, CardInfo, CardInfo> action)
		{
			for (int i = 0; i < firstHalf.Length && i < secondHalf.Length; i++)
			{
				action.Invoke(i, firstHalf[i], secondHalf[i]);
			}
		}
	}
}
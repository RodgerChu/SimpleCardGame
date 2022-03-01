using System;

namespace SimpleCardGame
{
	public enum CardCompareResult
	{
		CardOne, CardTwo, Tie
	}
	
	public readonly struct SplittedInTwoCardDeck
	{
		public delegate CardCompareResult CardCompareDelegate(CardInfo cardOne, CardInfo cardTwo);
		
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
		
		public CardGameResult ProceedGame(CardCompareDelegate compareDelegate)
		{
			var playerOnePoints = 0;
			var playerTwoPoints = 0;
			ForEach((index, pOneCard, pTwoCard) =>
			{
				var compareResult = compareDelegate(pOneCard, pTwoCard);
				switch (compareResult)
				{
					case CardCompareResult.Tie:
						playerOnePoints += 1;
						playerTwoPoints += 1;
						break;
					case CardCompareResult.CardOne:
						playerOnePoints += 2;
						break;
					case CardCompareResult.CardTwo:
						playerTwoPoints += 2;
						break;
				}
			});

			return new CardGameResult(new Points(playerOnePoints), new Points(playerTwoPoints));
		}
	}
}
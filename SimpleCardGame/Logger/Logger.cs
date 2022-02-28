using System;
using LanguageExt;
using SimpleCardGame;

public static class Logger
{
	public static void Log(Some<CardDeck> cards)
	{
		Console.WriteLine("Cards: ");
		foreach (var cardInfo in cards.Value.cards)
		{
			Console.WriteLine($"value: {cardInfo.value.ToString()} | suit: {cardInfo.suit.ToString()}");
		}
	}

	public static void Log(Some<SplittedInTwoCardDeck> splittedDecks)
	{
		Console.WriteLine("Player one cards | Player two cards");
		var pOneCards = splittedDecks.Value.firstHalf;
		var pTwoCards = splittedDecks.Value.secondHalf;
		for (int i = 0; i < pOneCards.Length; i++)
		{
			Console.WriteLine($"{pOneCards[i].value.ToString()} {pOneCards[i].suit.ToString()} | {pTwoCards[i].value.ToString()} {pTwoCards[i].suit.ToString()}");
		}
	}

	public static void Log(Some<CardGameResult> gamePoints)
	{
		var pointsValue = gamePoints.Value;
		Console.WriteLine("Game ended with players score: ");
		Console.WriteLine($"player one – {pointsValue.playerOnePoints.Value.ToString()}");
		Console.WriteLine($"player two – {pointsValue.playerTwoPoints.Value.ToString()}");

		var winnerStr = pointsValue.playerOnePoints.Value > pointsValue.playerTwoPoints.Value
			? "winner – player one"
			: pointsValue.playerOnePoints.Value < pointsValue.playerTwoPoints.Value
				? "winner – player two"
				: "Game resulted in TIE";
		
		Console.WriteLine(winnerStr);
	}
}
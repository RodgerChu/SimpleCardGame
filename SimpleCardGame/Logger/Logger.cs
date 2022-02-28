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
		splittedDecks.Value.ForEach((index, pOneCard, pTwoCard) =>
		{
			Console.WriteLine($"{pOneCard.value.ToString()} {pOneCard.suit.ToString()} | {pTwoCard.value.ToString()} {pTwoCard.suit.ToString()}");
		});
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
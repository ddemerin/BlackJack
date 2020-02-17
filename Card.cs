namespace BlackJack
{
public class Card
 {
    public string Rank { get; set; }
    public string Suit { get; set; }
    public string Color { get; set; }

    public string DisplayCard()
    {
        return $"{Rank} of {Suit}";
    }

    public int GetCardValue()
    {
        if (Rank == "Ace")
        {
            return 11;
        }
        else if (Rank == "King" || Rank == "Queen" || Rank == "Jack")
        {
            return 10;
        }
        else
        {
            return int.Parse(Rank);
        }
    }
 }

}
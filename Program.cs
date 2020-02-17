using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
  class Program
  {
  
  // method for getting the total value in hand
  public static int Total(List<Card> hand)
  {
    var total = 0;
    for (int i = 0; i < hand.Count; i++)
    {
      total += hand[i].GetCardValue();
    }
    return total;
  } 
  
  // method for displaying what cards are in hand
  public static string CardList(List<Card> cards)
  {
    var list = "";
    for (int i = 0; i < cards.Count; i++)
    {
      list += cards[i].DisplayCard() + ", ";
    }
    return list;
  }

  // deck shuffler 
    static void Main(string[] args)
    {   
      // creating the while loop to play the game
      bool playerTurn = true;
      bool dealerTurn = true;
      bool playing = true;

      // start game
      while (playing == true)
      {
      // list for card suits
      var suit = new List<string>() {"Clubs", "Diamonds", "Hearts","Spades"};
      // list for card ranks
      var rank = new List<string>() {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
      // empty list to put cards after suits and ranks are combined
      var deck = new List<Card>() {};

      // method for combining ranks and suits
      for (var i = 0; i < suit.Count; i++)
      {
        for (var n = 0; n < rank.Count; n++)
          {
            var card = new Card();
            card.Rank = rank[n];
            card.Suit = suit[i];
            if (card.Suit == "Diamonds" || card.Suit == "Hearts")
            {
              card.Color = "Red";
            }
            else
            {
              card.Color = "Black";
            }
          deck.Add(card);
          }
        }

      // method to shuffle the deck
      for (int i = deck.Count - 1; i >= 0; i--)
      {
        var n = new Random().Next(i);
      //  temporary deck made to shift the random card selected before putting it back into the list order
        var temp = deck[n];
        deck[n] = deck[i];
        deck[i] = temp;

      }
      
      // hands are dealt to dealer and player

      // house deals to the dealer, keeps cards in hand hidden from dealer
      var dealerHand = new List<Card>() {};
      // deals first card to dealer
      dealerHand.Add(deck[0]);
      // variable to show the dealer's first card
      var firstCard = CardList(dealerHand);
      // deals second card to dealer
      dealerHand.Add(deck[1]);
      // removes cards from deck
      deck.RemoveAt(0);
      deck.RemoveAt(0);
      // calls the CardList method to display cards in dealer's hand
      var dealerList = CardList(dealerHand);
      // calls Total method to add up the dealer's total
      var dealerTotal = Total(dealerHand);

      // house deals to player and displays what's in players hand and total value 
      var playerHand = new List<Card>() {}; 
      // deals cards to player
      playerHand.Add(deck[0]);
      playerHand.Add(deck[1]);
      // removes cards from dexck
      deck.RemoveAt(0);
      deck.RemoveAt(0);
      // calls the CardList method to display cards in player's hand
      var playerList = CardList(playerHand);
      // calls Total method to add up the player's total
      var playerTotal = Total(playerHand);

        // if player hits 21 on the draw, program skips playerTurn  
        if (playerTotal == 21 )
        {
        Console.WriteLine($"\n\nPlayer received {playerList}for a total of {playerTotal}.");
        Console.WriteLine("\n\nPlayer wins!");
        playerTurn = false;
        dealerTurn = false;
        }

        // player turn
        while (playerTurn == true)
        {
        //  clears console to remove clutter/noise when playing game
          Console.Clear();
          Console.WriteLine($"\n\nPlayer received {playerList}for a total of {playerTotal}.");
          Console.WriteLine($"\n\nDealer is holding {firstCard}.");
          Console.WriteLine("\n\nWould you like to (HIT) or (STAY)");
          var playerInput = Console.ReadLine().ToLower();
          if (playerInput == "stay")
          {
            playerTurn = false;
          }
          else if (playerInput == "hit" && playerTotal <= 20)
          {
            Console.Clear();
            playerHand.Add(deck[0]);
            deck.RemoveAt(0);
            playerList = CardList(playerHand);
            playerTotal = Total(playerHand);
            if (playerTotal > 21)
            {
            Console.WriteLine($"\n\nPlayer received {playerList}for a total of {playerTotal}.");
            Console.WriteLine("\nPlayer busts! Player loses!");
            playerTurn = false;
            dealerTurn = false;
            }
            if (playerTotal == 21 || playerInput == "stay")
            {
            playerTurn = false;
            }
          }
          else if (playerTotal > 21)
          {
            Console.WriteLine($"\n\nPlayer received {playerList}for a total of {playerTotal}.");
            Console.WriteLine("\nPlayer busts! Player loses!");
            playerTurn = false;
            dealerTurn = false;
          }
        }

        // dealer turn
        while (dealerTurn == true)
        {
            Console.Clear();
            Console.WriteLine($"\n\nPlayer received {playerList}for a total of {playerTotal}.");
            Console.WriteLine($"\n\nDealer received {dealerList}for a total of {dealerTotal}.");
            if (dealerTotal < 17 || dealerTotal < playerTotal) 
            {
            dealerHand.Add(deck[0]);
            deck.RemoveAt(0);
            dealerList = CardList(dealerHand);
            dealerTotal = Total(dealerHand);
              if ( dealerTotal < 17 && dealerTotal == 21)
              {
              dealerList = CardList(dealerHand);
              dealerTotal = Total(dealerHand);
              Console.WriteLine("\n\nDealer wins!");
              dealerTurn = false;
              }
            }
            else if (dealerTotal > 21)
            {
            dealerList = CardList(dealerHand);
            dealerTotal = Total(dealerHand);
            Console.WriteLine("\n\nDealer busts! Player wins!");
            dealerTurn = false;
            }
            else if (dealerTotal > playerTotal || dealerTotal == 21)
            {
            dealerList = CardList(dealerHand);
            dealerTotal = Total(dealerHand);
            Console.WriteLine("\n\nDealer wins!");
            dealerTurn = false;
            }
            else if (playerTotal > dealerTotal || playerTotal == 21)
            {
            dealerList = CardList(dealerHand);
            dealerTotal = Total(dealerHand);
            Console.WriteLine("\n\nPlayer wins!");
            dealerTurn = false;
            }
            // Condition for a "push"
            else if (playerTotal == dealerTotal)
            {
            dealerList = CardList(dealerHand);
            dealerTotal = Total(dealerHand);
            Console.WriteLine("\n\nPlayer and Dealer push! Game is a draw!");
            dealerTurn = false;
            }
        }
      Console.WriteLine("\n\nWould you like to play again? Yes or No?");
      var repeat = Console.ReadLine().ToLower();
      if (repeat != "yes" && repeat != "no")
      {
          Console.WriteLine("\n\nThat's not an answer! Would you like to play again? Yes or No?");
          repeat = Console.ReadLine().ToLower();
      }
      if (repeat == "yes")
      {
        Console.Clear();
        playerHand.Clear();
        dealerHand.Clear();
        playerTurn = true;
        dealerTurn = true;
      } 
      else if (repeat == "no")
      {
        Console.WriteLine("Thanks for playing!");
        playing = false;
      }
      }
    }
  }
}
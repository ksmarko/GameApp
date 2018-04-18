using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class AliasBehaviour : IPlayBehaviour
    {
        public delegate void GameStateHandler(string message);
        public static event GameStateHandler AliasEvent;
        private int _playersCount;

        public AliasBehaviour(int playersCount)
        {
            this._playersCount = playersCount;
        }

        public void Play()
        {
            AliasEvent.Invoke("We are playing alias...");

            List<string> cardsSet = new List<string>(100);
            int teamsCount = _playersCount % 2 == 0 ? (_playersCount / 2) : (_playersCount / 2 - 1);
            int[] players = new int[teamsCount];
            Random rand = new Random(DateTime.Now.Millisecond);

            foreach (string el in (File.ReadAllText("words.txt").Split(' ')))
                cardsSet.Add(el);

            AliasEvent.Invoke($"Teams count - {teamsCount}");

            while (players.Max() != 50)
                for (int i = 0; i < teamsCount; i++)
                {
                    AliasEvent.Invoke($"Team {i} is moving. The word is {cardsSet.ElementAt(rand.Next(0, cardsSet.Count - 1))}");
                    if (rand.Next(-5, 10) < 0)
                    {
                        AliasEvent.Invoke($"Player didn't guess the word");
                        players[i]--;
                    }
                    else
                    {
                        AliasEvent.Invoke($"Player guess the word");
                        players[i]++;
                    }
                }

            AliasEvent.Invoke($"Game ends. Team {players.ToList().IndexOf(players.Max())} is winner!");
        }

        public void AddObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        public void NotifyObservers(string data)
        {
            throw new NotImplementedException();
        }
    }
}

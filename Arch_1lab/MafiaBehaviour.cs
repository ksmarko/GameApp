using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class MafiaBehaviour : IPlayBehaviour, IObservable
    {
        //public delegate void GameStateHandler(string message);
        //public static event GameStateHandler MafiaEvent;
        private int _playersCount;
        private List<IObserver> observers;

        public MafiaBehaviour(int playersCount)
        {
            this._playersCount = playersCount;
            observers = new List<IObserver>();
        }

        public void Play()
        {
            //MafiaEvent.Invoke("We are playing mafia...");
            NotifyObservers("We are playing mafia...");

            Random rand = new Random(DateTime.Now.Millisecond);
            //MafiaEvent.Invoke($"Count of players - {_playersCount}\n...\nSome static script to simulate game\n...\n");
            NotifyObservers($"Count of players - {_playersCount}\n...\nSome static script to simulate game\n...\n");

            if (rand.Next(-5, 10) > 0)
                //MafiaEvent.Invoke("The mafia is dead");
                NotifyObservers("The mafia is dead");
            else
                //MafiaEvent.Invoke("The mafia is winner");
                NotifyObservers("The mafia is winner");
        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers(string data)
        {
            foreach (var el in observers)
                el.Update(data);
        }
    }
}

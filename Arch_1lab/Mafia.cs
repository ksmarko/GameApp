using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    class Mafia : Game, IObservable, IObserver
    {
        //public override event GameStateHandler GameEvent;
        private bool _isVerified = false;
        private IObservable subject;

        public override event GameStateHandler GameEvent;

        public Mafia(int playersCount)
        {
            Settings = new Settings(7, false, false, true, false, false);
            Name = "Mafia";
            playBehaviour = new MafiaBehaviour(_playersCount);
            subject = playBehaviour;
            _playersCount = playersCount;

            subject.AddObserver(this);

            //MafiaBehaviour.MafiaEvent += new MafiaBehaviour.GameStateHandler((string message) => GameEvent.Invoke(message));
        }

        public override void Play()
        {
            if (!_isVerified)
                throw new InvalidOperationException("Game is not verified");

            playBehaviour.Play();
        }
       
        public override bool Verify(Settings other)
        {
            _isVerified = (base.Verify(other) && other.Cards) ? true : false;
            _playersCount = other.PlayersCount;

            return _isVerified;
        }

        public override void NotifyObservers(string data)
        {
            base.NotifyObservers(data);
        }

        public void Update(string data)
        {
            NotifyObservers(data);
        }
    }
}

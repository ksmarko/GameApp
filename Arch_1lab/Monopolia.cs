using System;
using System.Linq;

namespace Arch_1lab
{
    class Monopolia : Game
    {
        public override event GameStateHandler GameEvent;
        private bool _isVerified = false;

        public Monopolia(int playersCount)
        {
            Settings = new Settings(2, true, true, false, true, true);
            Name = "Monopolia";
            _playersCount = playersCount;

            playBehaviour = new MonopoliaBehaviour(_playersCount);
            MonopoliaBehaviour.MonopoliaEvent += new MonopoliaBehaviour.GameStateHandler((string message) => GameEvent.Invoke(message));
        }
        
        public override void Play()
        {
            if (!_isVerified)
                throw new InvalidOperationException("Game is not verified");

            playBehaviour.Play();
        }

        public override bool Verify(Settings other)
        {
            _isVerified = (base.Verify(other) && other.GameField && other.Chips && other.Cube && other.Money) ? true : false;
            _playersCount = other.PlayersCount;

            return _isVerified;
        }
    }
}

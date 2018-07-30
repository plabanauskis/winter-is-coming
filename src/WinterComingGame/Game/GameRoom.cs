using System;
using System.Collections.Generic;
using System.Linq;

namespace WinterComingGame.Game
{
    class GameRoom : IGameRoom
    {
        private IDictionary<string, Game> _games = new Dictionary<string, Game>();
        private object _lock = new object();

        public void StartGame(string name, IPlayer player)
        {
            lock (_lock)
            {
                var alreadyPlaying = FindGame(player) != null;

                if (alreadyPlaying)
                {
                    throw new InvalidOperationException("Player is already in the game.");
                }

                var newGame = new Game(name);

                if (_games.TryGetValue(name, out var game))
                {
                    throw new InvalidOperationException($"The game '{name}' already exists.");
                }

                _games.Add(name, newGame);

                newGame.Join(player);
            }
        }

        public Game FindGame(string name)
        {
            if (!_games.TryGetValue(name, out var game))
            {
                return null;
            }

            return game;
        }

        public Game FindGame(IPlayer player) =>
            _games.Values.Where(g => g.Players.Contains(player)).SingleOrDefault();
    }
}

using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using MovieStreaming.Message;

namespace MovieStreaming.Actor
{
    class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;


        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            this.Receive<IncrementPlayCountMessage>(message => HandleIncrementPlayCountMesage(message));

        }

        private void HandleIncrementPlayCountMesage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;

                Console.WriteLine($"MoviePlayCounterActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]}");
            }

            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }
        }
    }
}

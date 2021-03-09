using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using MovieStreaming.Message;
using MovieStreaming.Exceptions;

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

            }

            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }


            // simulated bugs
            // if we get a title played more than 3 times we throw this simulatedCorruptStateException error
            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (message.MovieTitle == "Partial Recoil")
            {
                throw new TerribleMovieException();
            }

            Console.WriteLine($"MoviePlayCounterActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]}");

        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            base.AroundPreRestart(cause, message);

            Console.WriteLine("MoviePlayCounterActor has restarted");

        }


    }
}

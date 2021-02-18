using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using MovieStreaming.Message;
namespace MovieStreaming.Actor
{
    class UserActor : ReceiveActor
    {
        // We can add some state to our userActor

        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("User Actor actor created");

            
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage());

        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                Console.WriteLine("Error: Cannot start playing another movie before stopping existing one");
            }

            else
            {
                StartPlayingMovie(message.MovieTitle);
            }

        }

        private void StartPlayingMovie(string movieTitle)
        {

            _currentlyWatching = movieTitle;

            Console.WriteLine($"User is watching {_currentlyWatching}");
        }

        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                Console.WriteLine("Cannot stop playing if nothing is playing");
            }

            else
            {
                StopPlayingCurrentMovie();
            }
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"User has stopped watching {_currentlyWatching}");
            _currentlyWatching = null;
        }

        public override void AroundPreStart()
        {
            Console.WriteLine("User Actor PreStart");
        }

        public override void AroundPostStop()
        {
            Console.WriteLine("User Actor PostStop");
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            Console.WriteLine("User Actor PreRestart");
            base.AroundPreRestart(cause, message);
        }

        public override void AroundPostRestart(Exception cause, object message)
        {

            Console.WriteLine("User Actor PostRestart");
            
            base.AroundPostRestart(cause, message);
        }

    }
}

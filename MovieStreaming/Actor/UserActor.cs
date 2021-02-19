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

            Console.WriteLine("Setting initial behaviour to stopped");

            // To set the initial behaviour to stopped we call it in the constructor. (Check if this is always how its done)

            Stopped();


            //Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            //Receive<StopMovieMessage>(message => HandleStopMovieMessage());

        }

        // We need to define below what happens when we recieve a message for the playing behaviour.
        private void Playing()
        {
            Receive<PlayMovieMessage>(message => Console.WriteLine("Error: cannot start playing another movie before stopping existing one"));
            
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => Console.WriteLine("Error: Cannot stop a movie if no movies are playing"));

            Console.WriteLine("User Actor has now Stopped");

        }


        private void StartPlayingMovie(string movieTitle)
        {

            _currentlyWatching = movieTitle;

            Console.WriteLine($"User is watching {_currentlyWatching}");


            // We are going to switch our behaviour from stopped to playing behaviour.
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"User has stopped watching {_currentlyWatching}");
            _currentlyWatching = null;


            Become(Stopped);
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

using System;
using Akka.Actor;
using MovieStreaming.Actor;
using MovieStreaming.Message;

namespace MovieStreaming
{
    class Program
    {

        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            // we don't want to use the new keyword, we want to use factories to create things for us.
            // MovieStreaming = new ActorSystemImpl();
            // when we use these factories we ensure that everything is set up correctly.
            
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");
            // The name of the actor system is important because it actually forms part of the address. If we are 
            // referencing remote actors.


            //We need to defin some props to create an instance of our playback actor.
            // Also we don't want to create our props using the new keyword instead we want to use a 
            // one of the Akkka.Net factory methods.
            Props playbackActorProps = Props.Create<PlaybackActor>();


            // Here we create our PlaybackActor. inside the MovieStreamingActory system.
            // We also got the reference to the actor in our playbackActorRef.
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            // We have already have the reference to our actor in this playbackActorRef variable. So we can use this
            // actor reference to tell the actor do something. 


            playbackActorRef.Tell(new PlayMovieMessage(1, "A"));
            playbackActorRef.Tell(new PlayMovieMessage(2, "B"));
            playbackActorRef.Tell(new PlayMovieMessage(3, "C"));
            playbackActorRef.Tell(new PlayMovieMessage(4, "D"));


            Console.ReadLine();
            
            // Tell actor system (and all child actors) to shutdown
            MovieStreamingActorSystem.Terminate();

            // Wait for the actor system to finish shutting down.
            MovieStreamingActorSystem.Terminate().Wait();

            Console.WriteLine("Actor system shutdown");
            Console.ReadLine();
            // shutdown method was deprecated and then removed, and has been replaced with terminate.

        }
    }
}

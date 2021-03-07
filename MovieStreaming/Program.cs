using System;
using Akka.Actor;
using MovieStreaming.Actor;
using MovieStreaming.Message;
using System.Threading;

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
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "playbackActor");



            // We have already have the reference to our actor in this playbackActorRef variable. So we can use this
            // actor reference to tell the actor do something. 


            do
            {

                Thread.Sleep(2000);
                Console.WriteLine("Enter a command: ");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    var userId = int.Parse(command.Split(",")[1]);
                    var movieTitle = command.Split(",")[2];

                    var message = new PlayMovieMessage(userId, movieTitle);
                    playbackActorRef.Tell(message);

                }

                if (command.StartsWith("stop"))
                {
                    var userId = int.Parse(command.Split(",")[1]);
                    var message = new StopMovieMessage(userId);
                    playbackActorRef.Tell(message);

                }


                if (command == "exit")
                {
                    Console.ReadLine();
                    Environment.Exit(1);
                }


            } while (true);



            Console.ReadLine();
               
            //// tell actor system (and all child actors) to shutdown
            MovieStreamingActorSystem.Terminate();

            //// wait for the actor system to finish shutting down.
            MovieStreamingActorSystem.Terminate().Wait();

            Console.WriteLine("actor system shutdown");
            Console.ReadLine();
            //// shutdown method was deprecated and then removed, and has been replaced with terminate.

        }
    }
}

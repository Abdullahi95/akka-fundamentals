using System;
using Akka.Actor;
using MovieStreaming.Message;

namespace MovieStreaming.Actor
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Console.WriteLine("Creating PlaybackStatisticsActor");
            IActorRef childMoviePlayCounterActor = Context.ActorOf(Props.Create(() => new MoviePlayCounterActor()), "MoviePlayCounterActor");

            this.Receive<IncrementPlayCountMessage>(message => childMoviePlayCounterActor.Tell(message));

        }


       

    }
}
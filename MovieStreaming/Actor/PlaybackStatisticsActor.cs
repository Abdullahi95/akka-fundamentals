using System;
using Akka.Actor;
using MovieStreaming.Exceptions;
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

        // Inside this method we are going to create a supervisor strategy and then return it as the retun value from this method.

        // this custom stragety will be used whenever a child actor fails.
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if (exception is SimulatedCorruptStateException)
                {
                    return Directive.Restart;
                }

                if (exception is TerribleMovieException)
                {
                    return Directive.Resume;
                }

                return Directive.Restart;
            });
        }

    }
}
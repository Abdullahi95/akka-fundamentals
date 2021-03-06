using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using MovieStreaming.Message;

namespace MovieStreaming.Actor
{
    public class PlaybackActor : ReceiveActor
    {
        // Less code than the untyped actor.

        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            // Remember we use Context.ActorOf to create child actors.
            // Here we are creating an actor -> this actors is a child of PlaybackActor.
            IActorRef userCoordinatorActorRef = Context.ActorOf(Props.Create<UserCoordinatorActor>(), "userCoordinatorActor");
            IActorRef playbackStatisticsActorRef = Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "playbackStatisticActor");

            this.Receive<PlayMovieMessage>(message => userCoordinatorActorRef.Tell(message));
            this.Receive<StopMovieMessage>(message => userCoordinatorActorRef.Tell(message));



            // 1.  We are going to specify the message types our playback actor is going to handle. This time
            // we are going to do it with the receive actor api.

            // 2. We configure the receive actor to handle a certain type of message with the recieve method.

            // 3. There are a number of different overloads for the receive method, one of them allows us to specify a 
            // a predicate to further refine whether these play movie messages will be handled or not.

            // 4. So below we run the HandlePlayMovieMessage, when our actor receives a message of type PlayMovieMessage.

             // 5. Now we can further refine whether a message will be handled or not. We can do this with using one of the overloads of the receive methods which allows us to specify a predicate to determine whether we should handle the message or not.
            
            // 6. So below we are only handling for the messages with the userid are greating than 0.

            // Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId > 0);
        }

        // This method gets executed when a playmoviemessage is received.
        //private void HandlePlayMovieMessage(PlayMovieMessage playMovieMessage)
        //{
        //    Console.WriteLine($"Movie Title : {playMovieMessage.MovieTitle},  UserId: {playMovieMessage.UserId}");
          
        //}

        // the base classes implemenetation of PreStart() is empty so there is no need for base.AroundPreStart();
       //  this method is called before the actor starts processing the message.
        public override void AroundPreStart()
        {
            //  this is called before the actor starts processing any messages.
            Console.WriteLine("PlaybackActor PreStart");
        }

        public override void AroundPostStop()
        {
            // this is called after the stopping phase 
            Console.WriteLine("PlaybackActor PostStop");

        }

        public override void AroundPostRestart(Exception cause, object message)
        {

            Console.WriteLine("PlaybackActor PostRestart because: " + cause);
            // there is a reason why we use the base classes AroundPostRestart
            base.AroundPostRestart(cause, message);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PlaybackActor PreStart because: " + reason);
            // there is a reason why we use the base classes AroundPreRestart
            base.PreRestart(reason, message);
        }







    }
}

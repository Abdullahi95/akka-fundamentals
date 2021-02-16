using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using MovieStreaming.Message;

namespace MovieStreaming.Actor
{
    public class PlaybackActor : ReceiveActor
    {

        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            // 1.  We are going to specify the message types our playback actor is going to handle. This time
            // we are going to do it with the receive actor api.

            // 2. We configure the receive actor to handle a certain type of message with the recieve method.

            // 3. There are a number of different overloads for the receive method, one of them allows us to specify a 
            // a predicate to further refine whether these play movie messages will be handled or not.

            // 4. So below we run the HandlePlayMovieMessage, when our actor receives a message of type PlayMovieMessage.

            // 5. Now we can further refine whether a message will be handled or not. We can do this with using one of the overloads of the receive methods which allows us to specify a predicate to determine whether we should handle the message or not.
            
            // 6. So below we are only handling for the messages with the userid is equal to 42.

           Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId == 42);
        }

        // This method gets executed when a playmoviemessage is received.
        private void HandlePlayMovieMessage(PlayMovieMessage playMovieMessage)
        {
            Console.WriteLine(playMovieMessage.MovieTitle);
            Console.WriteLine(playMovieMessage.UserId);
        }

        // Less code than the untyped actor.
    }
}

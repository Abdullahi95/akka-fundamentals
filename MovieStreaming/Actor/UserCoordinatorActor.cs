using Akka.Actor;
using System;
using System.Collections.Generic;
using MovieStreaming.Message;


namespace MovieStreaming.Actor
{
    public class UserCoordinatorActor : ReceiveActor
    {
        // a dictionary of key of int, and the value of type IActorRef.
        //Essentially this is a dictionary of all our child actor references.

        private readonly Dictionary<int, IActorRef> _users; // you can set a readonly field within a constructor

        public UserCoordinatorActor()
        {
            Console.WriteLine("Creating UserCoordinatorActor");

            _users = new Dictionary<int, IActorRef>();

            // Remember you define the message this actor is going to handle in the constructor.

            this.Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childUserActorRef = _users[message.UserId];

                childUserActorRef.Tell(message);
            });

            this.Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childUserActorRef = _users[message.UserId];

                childUserActorRef.Tell(message);

            });

        }

        // We want to create an actor for the given userId if it doesnt exist.
        private void CreateChildUserIfNotExists(int userId)
        {
            if (_users.ContainsKey(userId))
            {
                Console.WriteLine($"The User Actor with Id : {userId} already exists");
            }

            else
            {
                Props userActorProps = Props.Create<UserActor>(userId);

                IActorRef childUserActor = Context.ActorOf(userActorProps, $"UserActor_{userId}");

                _users.Add(userId, childUserActor);

                Console.WriteLine($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {_users.Count})");

            }
        }
    }
}
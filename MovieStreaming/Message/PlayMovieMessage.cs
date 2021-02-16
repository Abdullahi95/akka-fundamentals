using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreaming.Message
{
    public class PlayMovieMessage
    {
        // We want our properties to be immutable, we don't want to change any of the fields after creating the object.
        // The only time we set it the fields are in the constructor. 
        public int UserId { get; private set; }
        public string MovieTitle { get; private set; }


        public PlayMovieMessage(int id, string title)
        {
            this.UserId = id;
            this.MovieTitle = title;
        }


    }
}

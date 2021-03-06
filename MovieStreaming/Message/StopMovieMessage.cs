using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreaming.Message
{
    class StopMovieMessage
    {

        public int UserId { get; private set; }


        public StopMovieMessage(int userId)
        {
            this.UserId = userId;
        }


    }
}

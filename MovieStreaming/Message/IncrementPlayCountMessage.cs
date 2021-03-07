using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreaming.Message
{
    class IncrementPlayCountMessage
    {
        public string MovieTitle { get; private set; }

        public IncrementPlayCountMessage(string movieTitle)
        {
            this.MovieTitle = movieTitle;
        }


    }
}

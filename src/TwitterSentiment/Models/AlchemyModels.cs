using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterSentiment.Models
{
    public class DocSentiment
    {
        public string type { get; set; }
        public string score { get; set; }
        public string mixed { get; set; }
    }

    public class Sentiment
    {
        public string status { get; set; }
        public string usage { get; set; }
        public string url { get; set; }
        public string language { get; set; }
        public DocSentiment docSentiment { get; set; }
    }
}
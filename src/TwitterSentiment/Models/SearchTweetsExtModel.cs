using System.Collections.Generic;
using TwitterOAuth.RestAPI.Models.Twitter;
using TwitterSentiment.Controllers;

namespace TwitterSentiment.Models
{
    public class SearchTweetsExtModel
    {
        public SearchMetadata search_metadata { get; set; }
        public List<StatusExt> statuses { get; set; }
    }
}
﻿using TwitterOAuth.RestAPI.Models.Twitter;

namespace TwitterSentiment.Models
{
    public class StatusExt
    {
        public object contributors { get; set; }
        public object coordinates { get; set; }
        public string created_at { get; set; }
        public Entities entities { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public object geo { get; set; }
        public object id { get; set; }
        public string id_str { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public object in_reply_to_user_id { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public string lang { get; set; }
        public Metadata metadata { get; set; }
        public object place { get; set; }
        public int retweet_count { get; set; }
        public bool retweeted { get; set; }
        public string source { get; set; }
        public string text { get; set; }
        public bool truncated { get; set; }
        public User user { get; set; }
        public bool positive { get; set; }
        public double sentiment_score { get; set; }
    }
}
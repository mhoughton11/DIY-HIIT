﻿using System;

namespace DIYHIIT.Library.Settings
{
    public class Settings
    {
        public enum HostOptions
        {
            Production = 1,
            LocalHost
        };

        public enum FeedItemType
        {
            Workout = 1,
            Post = 2,
            Exercise = 3
        };
    }
}

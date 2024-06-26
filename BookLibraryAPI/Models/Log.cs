﻿using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? MessageTemplate { get; set; }
        [MaxLength(128)]
        public string? Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
    }
}

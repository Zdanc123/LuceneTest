using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneTest.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
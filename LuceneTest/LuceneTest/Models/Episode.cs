using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuceneTest.Models
{
    public class Episode
    {

       public int Id { get; set; }
        [Required(ErrorMessage = "Empty Name Field")]
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
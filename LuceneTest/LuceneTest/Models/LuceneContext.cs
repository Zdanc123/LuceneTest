using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace LuceneTest.Models
{
    public class LuceneContext : DbContext
    {
        public LuceneContext() : base("LuceneContext") { }
        public DbSet<Character>Characters { get; set; }

        public System.Data.Entity.DbSet<LuceneTest.Models.Episode> Episodes { get; set; }

        public System.Data.Entity.DbSet<LuceneTest.Models.CharacterEpisode> CharacterEpisodes { get; set; }
        
    }
}
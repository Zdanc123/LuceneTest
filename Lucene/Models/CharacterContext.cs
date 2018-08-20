using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CharacterContext : DbContext
    {
        public CharacterContext() : base("CharacterContext") { }
        public DbSet<Character> Characters { get; set; }
        
    }
}
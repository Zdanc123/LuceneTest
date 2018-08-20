using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2
{
    public class LuceneService
    {
        private string indexPath = @"C:\Users\zdanc\Desktop\Index.txt";
        private Analyzer analyzer { get; set; }
        public IndexWriter writer { get; set; }
        private Directory luceneIndexDirectory { get; set; }
        public LuceneService()
        {
            InitialiseLucene();
        }


        public void InitialiseLucene()
        {
            //if (System.IO.Directory.Exists(indexPath))
            //{
            //    System.IO.Directory.Delete(indexPath, true);
            //}
           this.luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(indexPath));
            this.analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            this.writer  = new IndexWriter(luceneIndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED); //Create an Index writer object.;

        }


        

        public void BuildIndex(List<Character>characters)
        {
            foreach (var character in characters)
            {
                Document doc = new Document();
                if (character.Name != null)
                {
                    doc.Add(new Field("Name", character.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                }
              
                if (character.CharacterID != null)
                {
                    doc.Add(new Field("PersonID", character.CharacterID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                }

                
                writer.AddDocument(doc);
            }
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
            
         
           // Response.Write("Indexing Done");
        }



        public List<Character> Search(string searchTerm)
        {
           
            //IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Name", analyzer);
            Query query = parser.Parse(searchTerm);


            
            Searcher searcher = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(luceneIndexDirectory, true));
            TopScoreDocCollector collector = TopScoreDocCollector.Create(100, true);
            searcher.Search(query, collector);

            var matches = collector.TopDocs().ScoreDocs;
            List<Character> results = new List<Character>();
            Character sampleCharacter = null;
           
            foreach (var item in matches)
            {
                var id = item.Doc;
                var doc = searcher.Doc(id);
                sampleCharacter.Name = doc.GetField("Name").StringValue;
                sampleCharacter.CharacterID = int.Parse(doc.GetField("CharacterID").StringValue);
                 results.Add(sampleCharacter);
            }

            luceneIndexDirectory.Dispose();
            return results;
          
        }

    }
}
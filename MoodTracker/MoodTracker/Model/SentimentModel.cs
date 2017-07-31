using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodTracker.Model
{
    public class SentimentModel
    {
        public List<Document> Results { get; set; }
    }

    public class Document
    {
        public string id
        {
            get;
            set;
        }

        public string score
        {
            get;
            set;
        }
    }
}

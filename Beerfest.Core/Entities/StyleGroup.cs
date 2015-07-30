using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beerfest.Core.Infrastructure.Mongo;
using MongoDB.Bson;

namespace Beerfest.Core.Entities {
    public class StyleGroup : MongoEntity {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
        public IList<string> Styles { get; set; } 
    }

}
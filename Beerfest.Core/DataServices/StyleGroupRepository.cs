using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Beerfest.Core.Entities;
using Beerfest.Core.Infrastructure;
using Beerfest.Core.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Beerfest.Core.DataServices {

    public interface IStyleGroupRepository : IMongoRepository<StyleGroup> {
        IList<StyleGroup> GetByType(string type);
    }

    public class StyleGroupRepository : MongoRepository<StyleGroup>, IStyleGroupRepository
    {
        public StyleGroupRepository(MongoDatabase database) : base(database, "styles") {}

        public IList<StyleGroup> GetByType(string type) {
            IList<StyleGroup> groups;

            if (type.IsNullOrWhiteSpace()) {
                groups = GetAll().OrderBy(g => g.Order).ToList();
            } else {
                groups = base.Get(s => s.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return groups;
        }

        public Dictionary<string, StyleGroup> GetDictionary(string type) {
            var groups = GetByType(type);
            var dict = new Dictionary<string, StyleGroup>(); 

            foreach (var group in groups) {
                foreach (var style in group.Styles) {
                    dict.Add(style,group);
                }
            }
            return dict;
        }

    }
}
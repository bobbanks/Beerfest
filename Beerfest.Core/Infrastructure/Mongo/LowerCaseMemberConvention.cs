using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Beerfest.Core.Infrastructure.Mongo {

    public class LowerCaseMemberConvention : IMemberMapConvention {

        public string Name { get { return "LowerCaseMemberConvention"; } }

        public void Apply(BsonMemberMap memberMap) {
            memberMap.SetElementName(memberMap.MemberName.ToLower());
        }
    }

}
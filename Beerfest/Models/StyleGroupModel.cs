using Beerfest.Core.Entities;

namespace Beerfest.Models {

    public class StyleGroupModel {

        public StyleGroupModel(StyleGroup entity) {
            Name = entity.Name;
            Color = entity.Color;
        }

        public string Name { get; set; }
        public string Color { get; set; }
    }

}
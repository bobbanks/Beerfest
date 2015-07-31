using System.Collections;
using System.Web.Management;
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

    public class BeerModel {
        public string Brewery { get; set; }
        public string Name { get; set; }
        public double? Abv { get; set; }
        public int? Ibu { get; set; }
        public string Style { get; set; }
        public string Type { get; set; }
        public int? BaScore { get; set; }
        public int? BrosScore { get; set; }
        public double? UntappdScore { get; set; }
        public string ImageUrl { get; set; }
        public string UntappdBeerId { get; set; }

        public string Color {
            get {
                switch (Type) {
                    case "ale":
                        return "ffb627";
                    case "lager":
                        return "33a1fd";
                    case "hybrid":
                        //return "d3a4e5";
                        return "99AC92";
                    default:
                        return "dddddd";
                }
            }
        }

        public string ScoreToDisplay {
            get {
                if (BrosScore != null) {
                    return BrosScore.ToString();
                } else 
                if (BaScore != null) {
                    return BaScore.ToString();
                } else {
                    return "N/A";
                }
            }
        }

        public BeerModel(Beer entity, string type) {
            Brewery = entity.Brewery;
            Name = entity.Name;
            Abv = entity.Abv;
            Ibu = entity.Ibu;
            Style = entity.Style;
            Type = type;
            BaScore = entity.BaScore;
            BrosScore = entity.BrosScore;
            UntappdScore = entity.UntappdScore;
            ImageUrl = entity.ImageUrl;
            UntappdBeerId = entity.UntappdBeerId;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerfest.Core.DataServices;
using Beerfest.Core.Entities;
using Beerfest.Models;

namespace Beerfest.Controllers
{
    public class HomeController : Controller {

        private readonly IStyleGroupRepository _styleGroupRepository;
        private readonly IBeerRepository _beerRepository;


        public HomeController(IStyleGroupRepository styleGroupRepository, IBeerRepository beerRepository) {
            _styleGroupRepository = styleGroupRepository;
            _beerRepository = beerRepository;
        }

        public ActionResult Index() {
//            var styleGroups = _styleGroupRepository.GetByType("");
//
//            var model = new Dictionary<string, List<StyleGroupModel>> {
//                {"all", new List<StyleGroupModel>()},
//                {"ale", new List<StyleGroupModel>()},
//                {"lager", new List<StyleGroupModel>()},
//                {"hybrid", new List<StyleGroupModel>()},
//            };
//
//            foreach (var styleGroup in styleGroups) {
//                model["all"].Add(new StyleGroupModel(styleGroup));
//                model[styleGroup.Type].Add(new StyleGroupModel(styleGroup));
//            }

            var beers = GetBeerDictionary();

            return Json(beers, JsonRequestBehavior.AllowGet);
        }


        private Dictionary<string, List<Beer>> GetBeerDictionary() {

            var beers = _beerRepository.GetAll().OrderBy(b => b.Name);
            var styleGroups = _styleGroupRepository.GetAll().ToList();

            var dict = new Dictionary<string, List<Beer>>() {
                {"all", new List<Beer>()},
                {"ale", new List<Beer>()},
                {"lager", new List<Beer>()},
                {"hybrid", new List<Beer>()}
            };

            foreach (var beer in beers) {
                dict["all"].Add(beer);
                var styleType = styleGroups.FirstOrDefault(sg => sg.Styles.Contains(beer.Style));
                if (styleType != null) {
                    dict[styleType.Type].Add(beer);
                }

            }

            return dict;
        }
    }
}
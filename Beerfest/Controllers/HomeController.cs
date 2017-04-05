using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerfest.Core.DataServices;
using Beerfest.Core.Entities;
using Beerfest.Core.Infrastructure;
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
            
            var sortCookie = Request.Cookies["sortField"];

            var sortField = "name";
            if (sortCookie != null) {
                sortField = sortCookie.Value;
            }

            var beers = GetBeerDictionary(sortField);

            return View(beers);
        }


        private Dictionary<string, List<BeerModel>> GetBeerDictionary(string sortField) {

            var beers = _beerRepository.GetAll();

            switch (sortField) {
                case "name":
                    beers = beers.OrderBy(b => b.Name);
                    break;
                case "abv":
                    beers = beers.OrderByDescending(b => b.Abv);
                    break;
                case "ibu":
                    beers = beers.OrderByDescending(b => b.Ibu);
                    break;
            }

            var styleGroups = _styleGroupRepository.GetAll().ToList();

            var dict = new Dictionary<string, List<BeerModel>>() {
                {"all", new List<BeerModel>()},
                {"ale", new List<BeerModel>()},
                {"lager", new List<BeerModel>()},
                {"hybrid", new List<BeerModel>()}
            };

            foreach (var beer in beers) {
                if (!beer.Name.IsNullOrWhiteSpace()) {
                    var styleType = styleGroups.FirstOrDefault(sg => sg.Styles.Contains(beer.Style));
                    if (styleType != null) {
                        var model = new BeerModel(beer, styleType.Type);
                        dict["all"].Add(model);
                        dict[styleType.Type].Add(model);
                    } else {
                        //throw new ApplicationException(string.Format("Could not find style '{0}' for '{1}'", beer.Style, beer.Name));
                    }
                }
            }

            return dict;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beerfest.Core.DataServices;
using Beerfest.Models;

namespace Beerfest.Controllers
{
    public class HomeController : Controller {

        private readonly IStyleGroupRepository _styleGroupRepository;

        public HomeController(IStyleGroupRepository styleGroupRepository) {
            _styleGroupRepository = styleGroupRepository;
        }

        public ActionResult Index() {
            var styleGroups = _styleGroupRepository.GetByType("");

            var model = new Dictionary<string, List<StyleGroupModel>> {
                {"all", new List<StyleGroupModel>()},
                {"ale", new List<StyleGroupModel>()},
                {"lager", new List<StyleGroupModel>()},
                {"hybrid", new List<StyleGroupModel>()},
            };

            foreach (var styleGroup in styleGroups) {
                model["all"].Add(new StyleGroupModel(styleGroup));
                model[styleGroup.Type].Add(new StyleGroupModel(styleGroup));
            }
            return View(model);
        }
    }
}
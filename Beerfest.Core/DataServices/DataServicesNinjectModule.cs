using Beerfest.Core.Entities;
using Ninject.Modules;

namespace Beerfest.Core.DataServices {

    public class DataServicesNinjectModule : NinjectModule {

        public override void Load() {
            Bind<IStyleGroupRepository>().To<StyleGroupRepository>().InSingletonScope();
            Bind<IBeerRepository>().To<BeerRepository>().InSingletonScope();
        }

    }

}
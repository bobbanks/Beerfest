using Ninject.Modules;

namespace Beerfest.Core.DataServices {

    public class DataServicesNinjectModule : NinjectModule {

        public override void Load() {
            Bind<IStyleGroupRepository>().To<StyleGroupRepository>().InSingletonScope();
        }

    }

}
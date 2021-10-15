using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;


#if EA_FACADE
using EAAPI = MDD4All.EAFacade.DataModels.Contracts;
#else
using EAAPI = EA;
#endif

#if EA_FACADE
namespace MDD4All.EAFacade.ModelTree.ViewModels
#else
namespace MDD4All.EnterpriseArchitect.ModelTree.ViewModels
#endif
{
    public class PackageNodeViewModel : ModelNodeViewModel
    {
        private EAAPI.Package _package;

        public PackageNodeViewModel(EAAPI.Package package)
        {
            _package = package;
        }

        public string Title
        {
            get 
            {
                string result = "📁 ";

                if(_package.Element != null)
                {
                    if(!string.IsNullOrEmpty(_package.Element.Stereotype))
                    {
                        result += "«" + _package.Element.Stereotype + "» ";
                    }
                }

                result += _package.Name;

                return result; 
            }

            set 
            { 
            }
        }

        public List<ModelNodeViewModel> Children
        {
            get
            {
                List<ModelNodeViewModel> result = new List<ModelNodeViewModel>();

                for(short counter= 0; counter < _package.Packages.Count; counter++)
                {
                    EAAPI.Package childPackage = _package.Packages.GetAt(counter) as EAAPI.Package;

                    PackageNodeViewModel childPackageViewModel = new PackageNodeViewModel(childPackage);

                    result.Add(childPackageViewModel);
                }

                return result;
            }
        }

    }
}

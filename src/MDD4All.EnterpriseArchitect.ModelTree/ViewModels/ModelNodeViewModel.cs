using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
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
    public abstract class ModelNodeViewModel : ViewModelBase
    {
    }
}

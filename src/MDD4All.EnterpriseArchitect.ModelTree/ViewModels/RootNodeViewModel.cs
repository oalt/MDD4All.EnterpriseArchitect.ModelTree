using MDD4All.UI.DataModels.Tree;
using System.Collections.ObjectModel;
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
    internal class RootNodeViewModel : ModelNodeViewModel
    {
        public RootNodeViewModel(string connectionString,
                                 ITree tree, ModelNodeViewModel parentNode,
                                 EAAPI.Repository repository) : base(tree, parentNode, repository)  
        {
            IsExpanded = true;
            _title = connectionString;
        }

        public override ObservableCollection<ITreeNode> Children { get; set; } = new ObservableCollection<ITreeNode>();

        private string _title = string.Empty;

        public override string Title
        {
            get { return _title; }
        }

        public override string Icon { set; get; } = "EALOGO";
    }
}
using MDD4All.UI.DataModels.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
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
    public class RepositoryTreeViewModel : ITree
    {
        private EAAPI.Repository _repository;

        public RepositoryTreeViewModel(EAAPI.Repository repository) 
        {
            _repository = repository;

            RootNodeViewModel rootNodeViewModel = new RootNodeViewModel(repository.ConnectionString, this, null, _repository);

            rootNodeViewModel.Tree = this;

            foreach (EAAPI.Package model in _repository.Models)
            {
                rootNodeViewModel.Children.Add(new PackageNodeViewModel(model, this, rootNodeViewModel, _repository)
                {
                    Icon = "images/EA/Model.png",
                    Tree = this
                });
            }

            _treeRootNodes.Add(rootNodeViewModel);
        }

        private ObservableCollection<ITreeNode> _treeRootNodes = new ObservableCollection<ITreeNode>();

        public ObservableCollection<ITreeNode> TreeRootNodes
        {
            get { return _treeRootNodes; }
        }

        public ITreeNode SelectedNode { get; set; }

        public bool IncludeDiagrams { get; set; } = true;

        public bool IncludeElements { get; set; } = true;

        public bool IncludeEmbeddedElements { get; set; } = true;

    }
}

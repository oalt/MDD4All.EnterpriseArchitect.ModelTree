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

        public RepositoryTreeViewModel(EAAPI.Repository repository,
                                       bool includeElements = true,
                                       bool includeEmbeddedElements = true,
                                       bool includeDiagrams = true) 
        {
            _repository = repository;

            IncludeElements = includeElements;
            IncludeEmbeddedElements = includeEmbeddedElements;
            IncludeDiagrams = includeDiagrams;

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

        public RepositoryObjectViewModel SelectedRepositoryObject
        {
            get
            {
                RepositoryObjectViewModel result = null;

                if (SelectedNode != null)
                {
                    ModelNodeViewModel modelNodeViewModel = SelectedNode as ModelNodeViewModel;

                    if (modelNodeViewModel != null)
                    {
                        result = modelNodeViewModel.Descriptor;
                    }
                }

                return result;
            }
        }

        public bool IncludeDiagrams { get; set; } = true;

        public bool IncludeElements { get; set; } = true;

        public bool IncludeEmbeddedElements { get; set; } = true;

    }
}

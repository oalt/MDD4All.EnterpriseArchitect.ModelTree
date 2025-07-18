using System.Collections.ObjectModel;
using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.UI.DataModels.Tree;
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
    internal class PackageNodeViewModel : ModelNodeViewModel
    {
        private EAAPI.Package _package;

        public PackageNodeViewModel(EAAPI.Package package, ITree tree,
                                    ModelNodeViewModel parentNode,
                                    EAAPI.Repository repository) : base(tree, parentNode, repository)
        {
            _package = package;
            IsExpanded = false;
            InitializeCildren();
        }

        public override string Title
        {
            get 
            {
                string result = "";

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
        }

        private void InitializeCildren()
        {
            if (((RepositoryTreeViewModel)Tree).IncludeDiagrams)
            {
                for (short counter = 0; counter < _package.Diagrams.Count; counter++)
                {
                    EAAPI.Diagram diagram = _package.Diagrams.GetAt(counter) as EAAPI.Diagram;

                    DiagramNodeViewModel diagramNodeViewModel = new DiagramNodeViewModel(diagram, Tree, this, Repository);

                    _children.Add(diagramNodeViewModel);
                }
            }

            for (short counter = 0; counter < _package.Packages.Count; counter++)
            {
                EAAPI.Package childPackage = _package.Packages.GetAt(counter) as EAAPI.Package;

                PackageNodeViewModel childPackageViewModel = new PackageNodeViewModel(childPackage, Tree, this, Repository);
                

                _children.Add(childPackageViewModel);
            }

            if (((RepositoryTreeViewModel)Tree).IncludeElements)
            {
                for (short counter = 0; counter < _package.Elements.Count; counter++)
                {
                    EAAPI.Element element = _package.Elements.GetAt(counter) as EAAPI.Element;

                    if (element.Type != "Package")
                    {
                        ElementNodeViewModel elementNodeViewModel = new ElementNodeViewModel(element, Tree, this, Repository);

                        _children.Add(elementNodeViewModel);
                    }
                }
            }
        }

        private ObservableCollection<ITreeNode> _children = new ObservableCollection<ITreeNode>();

        public override ObservableCollection<ITreeNode> Children
        {
            get
            {
                return _children;
            }

            set
            {
            }
        }

        public override string Icon { set; get; } = "📁";
    }
}

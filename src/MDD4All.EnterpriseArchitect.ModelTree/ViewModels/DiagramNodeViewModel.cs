using MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.UI.DataModels.Tree;
using System;
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
    internal class DiagramNodeViewModel : ModelNodeViewModel
    {

        private EAAPI.Diagram _diagram;

        public DiagramNodeViewModel(EAAPI.Diagram diagram, 
                                    ITree tree, 
                                    ModelNodeViewModel parentNode,
                                    EAAPI.Repository repository) : base(tree, parentNode, repository)
        {
            _diagram = diagram;
        }

        public override ObservableCollection<ITreeNode> Children { get; set; } = new ObservableCollection<ITreeNode>();

        public override string Title
        {
            get
            {
                return _diagram.Name;
            }
        }

        public override string Icon
        {
            get
            {
                string result = "";

                if(_diagram.Type == "Use Case")
                {
                    result = "images/EA/UseCaseDiagram.png";
                }
                else if (_diagram.Type == "Logical")
                {
                    result = "images/EA/ClassDiagram.png";
                }
                else if (_diagram.Type == "Activity")
                {
                    result = "images/EA/ActivityDiagram.png";
                }
                else if (_diagram.Type == "Object")
                {
                    result = "images/EA/ObjectDiagram.png";
                }
                else
                {
                    result = "▣";
                }

                return result;
            }

            set
            { }
        }
    }
}

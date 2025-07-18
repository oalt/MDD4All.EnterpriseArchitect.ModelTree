using MDD4All.UI.DataModels.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
#if EA_FACADE
using EAAPI = MDD4All.EAFacade.DataModels.Contracts;
using MDD4All.EAFacade.Manipulations;
#else
using EAAPI = EA;
#endif

#if EA_FACADE
namespace MDD4All.EAFacade.ModelTree.ViewModels
#else
namespace MDD4All.EnterpriseArchitect.ModelTree.ViewModels
#endif
{
    internal class ElementNodeViewModel : ModelNodeViewModel
    {
        private EAAPI.Element _element;


        public ElementNodeViewModel(EAAPI.Element element, ITree tree,
                                    ModelNodeViewModel parentNode,
                                    EAAPI.Repository repository) : base(tree, parentNode, repository)
        {
            _element = element;
            InitializeCildren();
        }

        private void InitializeCildren()
        {
            if (((RepositoryTreeViewModel)Tree).IncludeDiagrams)
            {
                for (short counter = 0; counter < _element.Diagrams.Count; counter++)
                {
                    EAAPI.Diagram diagram = _element.Diagrams.GetAt(counter) as EAAPI.Diagram;

                    DiagramNodeViewModel diagramNodeViewModel = new DiagramNodeViewModel(diagram, Tree, this, Repository);
                    diagramNodeViewModel.Tree = Tree;

                    _children.Add(diagramNodeViewModel);
                }
            }

            if (((RepositoryTreeViewModel)Tree).IncludeEmbeddedElements)
            {
                for (short counter = 0; counter < _element.EmbeddedElements.Count; counter++)
                {
                    EAAPI.Element element = _element.EmbeddedElements.GetAt(counter) as EAAPI.Element;

                    if (element.Type != "Package")
                    {
                        ElementNodeViewModel elementNodeViewModel = new ElementNodeViewModel(element, Tree, this, Repository);

                        _children.Add(elementNodeViewModel);
                    }
                }
            }

            for (short counter = 0; counter < _element.Elements.Count; counter++)
            {
                EAAPI.Element element = _element.Elements.GetAt(counter) as EAAPI.Element;

                if (element.Type != "Package")
                {
                    ElementNodeViewModel elementNodeViewModel = new ElementNodeViewModel(element, Tree, this, Repository);

                    _children.Add(elementNodeViewModel);
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

        public override string Title
        {
            get
            {
                string result = "";

                if (!string.IsNullOrEmpty(_element.Stereotype))
                {
                    result = "«" + _element.Stereotype + "» ";
                }

                if (!string.IsNullOrEmpty(_element.Name))
                {
                    result += _element.Name;
                }

                string classifierName = _element.GetClassifierName(Repository);

                if(!string.IsNullOrEmpty(classifierName))
                {
                    if (_element.Name == string.Empty)
                    {
                        result += " :" + classifierName;
                    }
                    else
                    {
                        result += ": " + classifierName;
                    }
                }

                return result;
            }
        }

        public override string Icon
        {
            get
            {
                string result = "";

                if (_element.Type == "UseCase")
                {
                    result = "images/EA/UseCase.png";
                }
                else if (_element.Type == "Class")
                {
                    result = "images/EA/Class.png";
                }
                else if (_element.Type == "Interface")
                {
                    result = "images/EA/Interface.png";
                }
                else if( _element.Type == "Actor")
                {
                    result = "images/EA/Actor.png";
                }
                else if(_element.Type == "Object")
                {
                    result = "images/EA/Object.png";
                }
                else if (_element.Type == "Port")
                {
                    result = "images/EA/Port.png";
                }
                else if(_element.Type == "Activity")
                {
                    result = "images/EA/Activity.png";
                }
                else if (_element.Type == "Action")
                {
                    result = "images/EA/Action.png";
                }
                else if (_element.Type == "Component")
                {
                    result = "images/EA/Component.png";
                }
                else
                {
                    result = "⍰";
                }

                return result;
            }

            set
            { }
        }
    }
}

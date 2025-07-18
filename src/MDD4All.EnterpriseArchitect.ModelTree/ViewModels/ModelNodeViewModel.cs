using GalaSoft.MvvmLight;
using MDD4All.UI.DataModels.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public abstract class ModelNodeViewModel : ViewModelBase, ITreeNode
    {

        public ModelNodeViewModel(ITree tree, ModelNodeViewModel parentNode, EAAPI.Repository repository)
        {
            Tree = tree;
            Parent = parentNode;
            Repository = repository;
        }

        public ITree Tree { get; set; }

        public abstract ObservableCollection<ITreeNode> Children { get; set; }

        public ITreeNode Parent { get; set; }

        public int Index { get; set; }

        public bool HasChildNodes
        {
            get
            {
                return Children.Count > 0;
            }
        }

        public bool IsExpanded { get; set; }

        public bool IsSelected
        {
            get
            {
                bool result = false;
                if(Tree != null)
                {
                    if(Tree.SelectedNode == this)
                    {
                        result = true;
                    }
                }
                return result;
            }
            set { }
        }

        public bool IsLoading
        {
            get { return false; }
        }

        public bool IsDisabled { get; set; }

        public string DragDropOperationInformation { get; set; } = string.Empty;

        public event EventHandler TreeStateChanged;

        public abstract string Title { get; }

        public abstract string Icon { get; set; }

        public EAAPI.Repository Repository { get; private set; }
    }
}

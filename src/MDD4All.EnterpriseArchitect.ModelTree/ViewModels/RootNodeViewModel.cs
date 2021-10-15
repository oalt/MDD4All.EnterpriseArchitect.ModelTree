using System.Collections.ObjectModel;

namespace MDD4All.EAFacade.ModelTree.ViewModels
{
    public class RootNodeViewModel : ModelNodeViewModel
    {
        public ObservableCollection<ModelNodeViewModel> Children { get; set; } = new ObservableCollection<ModelNodeViewModel>();
    }
}
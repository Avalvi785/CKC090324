
using System;
using UnityEngine.UI.Extensions;
namespace CKC
{
    public class Context : FancyScrollRectContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
}

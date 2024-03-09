
using System.Linq;
using UnityEngine;

namespace CKC
{
    class DataGenerator : MonoBehaviour
    {
        public CircularScrollView scrollView = default;
        public Transform ContentPanel;
        public float Spacing;
        private void Start()
        {
            Initialization();
        }

        private void OnEnable()
        {
        DraggedIconHolder.isIconDropped+= Initialization;
        }  
        private void OnDisable()
        {
        DraggedIconHolder.isIconDropped-= Initialization;
        }
        
        private void Initialization()
        {
            scrollView.JumpTo(1);
            scrollView.Spacing = Spacing;
            scrollView.cellCount = ContentPanel.childCount;
            GenerateCells(scrollView.cellCount);
        }
        void GenerateCells(int dataCount)
        {
            var items = Enumerable.Range(0, dataCount)
                .Select(i => new ItemData($"Cell {i}"))
                .ToArray();

            scrollView.UpdateData(items);
        }
    }

   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
namespace CKC
{
    class CircularScrollCell : FancyScrollRectCell<ItemData, Context>
    {

        public override void Initialize()
        {

        }

        public override void UpdateContent(ItemData itemData)
        {

        }

        protected override void UpdatePosition(float normalizedPosition, float localPosition)
        {
            base.UpdatePosition(normalizedPosition, localPosition);
            var wave = Mathf.Sin(normalizedPosition * Mathf.PI) * -100;
            transform.localPosition += Vector3.right * wave;

        }
    }
}

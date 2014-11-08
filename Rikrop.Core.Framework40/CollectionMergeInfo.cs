using System.Collections.Generic;

namespace Rikrop.Core.Framework
{
    public class CollectionMergeInfo<TSourceElement, TTargetElement>
    {
        public readonly List<TTargetElement> ToAdd = new List<TTargetElement>();
        public readonly List<TSourceElement> ToDelete = new List<TSourceElement>();
        public readonly Dictionary<TSourceElement, TTargetElement> Equal = new Dictionary<TSourceElement, TTargetElement>();
    }
}

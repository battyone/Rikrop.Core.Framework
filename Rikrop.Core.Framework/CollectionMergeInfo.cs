using System.Collections.Generic;

namespace Rikrop.Core.Framework
{
    /// <summary>
    /// »нформаци€ о сли€нии коллекций.
    /// </summary>
    /// <typeparam name="TSourceElement">“ип элементов исходной коллекции.</typeparam>
    /// <typeparam name="TTargetElement">“ип элементов целевой коллекции.</typeparam>
    public class CollectionMergeInfo<TSourceElement, TTargetElement>
    {
        /// <summary>
        /// Ёлементы, которые содержатс€ только в целевой коллекции, но отсутствуют в исходной.
        /// </summary>
        public readonly List<TTargetElement> ToAdd = new List<TTargetElement>();

        /// <summary>
        /// Ёлементы, которые содержатс€ только в исходной коллекции, но отсутствуют в целевой.
        /// </summary>
        public readonly List<TSourceElement> ToDelete = new List<TSourceElement>();

        /// <summary>
        /// —опоставление элементов исходной и целевой коллекций, которые содержатс€ в обеих коллекци€х.
        /// </summary>
        public readonly Dictionary<TSourceElement, TTargetElement> Equal = new Dictionary<TSourceElement, TTargetElement>();
    }
}

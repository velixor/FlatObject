using System.Collections.Generic;

namespace FlatObject
{
    /// <summary>
    ///     Плоское представление объекта
    /// </summary>
    public interface IFlatObject
    {
        /// <summary>
        ///     Потомки объекта
        /// </summary>
        /// <remarks>
        ///     Под <i>потомками</i> подразумеваются все свойства объекта, а так же свойства этих свойств <b>рекурсивно</b>
        ///     Доступны только потомки ссылочного типа
        /// </remarks>
        IEnumerable<object> Descendants { get; }

        /// <summary>
        ///     Список имён всех предков объекта, от корневого до родителя
        /// </summary>
        /// <param name="includeRoot">Требуется ли включить в путь корневого предка</param>
        /// <param name="includeLeaf">Требуется ли включить в путь сам объект</param>
        /// <returns></returns>
        string[] GetDescendantPath(object obj, bool includeRoot = false, bool includeLeaf = false);

        /// <remarks>
        ///     Для объектов из коллекции и корневого объекта возвращается имя типа, для остальных имя свойства
        /// </remarks>
        string GetDescendantName(object obj);

        bool IsRootObject(object obj);
    }
}
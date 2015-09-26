using System;

namespace Portfolio.Dal.Utils
{
    /// <summary>
    /// Генератор уникальных частей Guid'а
    /// </summary>
    public static class UniqueGuidPartGenerator
    {
        /// <summary>
        /// Генерирует уникальную часть Guid'а
        /// </summary>
        /// <param name="initialSymbolsCount">Начальное количество символов Guid'а</param>
        /// <param name="isExist">Функция, которая проверяет существование части Guid'а</param>
        public static string Generate(short initialSymbolsCount, Func<string, bool> isExist)
         {
             var guid = Guid.NewGuid().ToString("N");
             var index = initialSymbolsCount;
             string guidPart;
             while (true)
             {
                 guidPart = guid.Substring(0, index);
                 if (!isExist(guidPart))
                 {
                     break;
                 }

                 if (++index <= guid.Length)
                 {
                     continue;
                 }

                 guid = Guid.NewGuid().ToString("N");
                 index = initialSymbolsCount;
             }

             return guidPart;
         }
    }
}
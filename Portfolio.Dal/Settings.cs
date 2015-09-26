using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using TsSoft.Commons.Collections;

namespace Portfolio.Dal
{
    /// <summary>
    /// Настройки
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Путь к файловому хранилищу на диске
        /// </summary>
        public static string StorageDirectory { get { return ConfigurationManager.AppSettings["Storage.Directory"]; } }
    }
}
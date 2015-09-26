namespace Portfolio.Dal.FileStorage
{
    /// <summary>
    /// Представляет файл в хранилище
    /// </summary>
    public struct FileInStorage
    {
        public string ContainerId { get; set; }

        public string FileName { get; set; }
    }
}
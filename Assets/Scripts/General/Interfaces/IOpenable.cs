namespace General.Interfaces
{
    public interface IOpenable
    {
        public bool IsOpen { get; set; }
        void Open();
        void Close();
    }
}

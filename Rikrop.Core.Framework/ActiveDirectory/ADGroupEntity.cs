namespace Rikrop.Core.Framework.ActiveDirectory
{
    public class ADGroupEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
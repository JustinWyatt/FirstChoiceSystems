using System;

namespace FirstChoiceSystems.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }

    public class Entity : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
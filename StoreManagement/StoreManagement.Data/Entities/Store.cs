using System;
using GenericRepository;

namespace StoreManagement.Data.Entities
{
    public class Store : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Layout { get; set; }
         
    }

}
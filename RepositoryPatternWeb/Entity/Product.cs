using Entity.Commons;

namespace Entity
{
    public class Product: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image {  get; set; }
        public Category Category { get; set; }
        public int CaterogyId { get; set; }

        

    }
}

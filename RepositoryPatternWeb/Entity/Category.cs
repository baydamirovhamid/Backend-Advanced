using Entity.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Category: BaseEntity<int>
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public override int Id { get;  set; }
        public string Name { get; set; }

    }
}

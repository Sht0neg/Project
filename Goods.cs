using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project
{
    public class Goods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Name { get; set;}

        public string Intenational { get; set;}

        public DateTime? DataBegin { get; set; }

        public DateTime? DataEnd { get; set; }

        public bool? Availability { get; set; }

        public string RF { get; set; }

        public string ProducerName { get; set; }

        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public string Batch { get; set; }

        public double Price { get; set; }

        public int Total { get; set; }
    }
}

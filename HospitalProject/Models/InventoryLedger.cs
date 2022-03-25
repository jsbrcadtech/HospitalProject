using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace HospitalProject.Models
{
    public class InventoryLedger
    {

        [Key]
        public int Id { get; set; }
        public int Delta { get; set; }
        public DateTime CreationDate { get; set; }

        // An inventory ledger is attached to one inventory
        // An inventory can be attached to many inventory ledgers  
        [ForeignKey("Inventories")]
        public int InventoryId { get; set; }
        public virtual Inventory Inventories { get; set; }
    }
}
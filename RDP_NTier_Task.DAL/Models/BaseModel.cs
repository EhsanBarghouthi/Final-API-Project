using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Models
{
    public enum Status
    {
        Active = 1 , InActive = 2,
    }
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime createdAT { get; set; } = DateTime.Now;
        public Status status { get; set; } = Status.Active;
    }
}

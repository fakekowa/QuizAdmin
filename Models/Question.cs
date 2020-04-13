using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAdmin.Models
{
    public class Question
    {
        // Primary Key
        public int Id { get; set; }
        public string Statement { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

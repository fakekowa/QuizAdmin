using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAdmin.Models.DTO
{
    public class QuestionsDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "statement")]
        public string Statement { get; set; }
        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Url
{
    public class GameQueryParams
    {
        public virtual int likesGreaterThan { get; set; }
        public virtual List<String> platform { get; set; }
        public virtual String publisher { get; set; }
        public virtual String publishedAfter { get; set; }
        public virtual int minAgeRating { get; set; }
        public virtual int? maxAgeRating { get; set; }
        public virtual int minCommentsAmount { get; set; }
    }
}

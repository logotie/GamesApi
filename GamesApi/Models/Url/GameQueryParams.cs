using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Url
{
    public class GameQueryParams
    {
        public virtual int likesGreater { get; set; }
        public virtual List<String> platform { get; set; }
        public virtual String publisher { get; set; }
        public virtual String publishedAfter { get; set; }
        public virtual int minAge { get; set; }
        public virtual int? maxAge { get; set; }
        public virtual int minComments { get; set; }
    }
}

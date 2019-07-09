using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Game
{
    public class Comment
    {
        public virtual string user { get; set; }
        public virtual string message { get; set; }
        public virtual string dateCreated { get; set; }
        public virtual int like { get; set; }
    }
}

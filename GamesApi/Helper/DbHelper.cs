using GamesApi.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Helper
{
    public class DbHelper
    {
        /// <summary>
        /// The dates are stored in the db as a long from epoch
        /// We convert the long to 'yyyy-mm-dd'
        /// </summary>
        /// <param name="games">Game objects from the db, sourced from a JSON</param>
        //TODO Add unit test for this method
        public static void FormatCommentDatesForUi(List<Game> games)
        {
            games.
                ForEach(game => game.gameDetails.comments.ToList().
                ForEach(comment => comment.dateCreated = 
                convertLongToDateTimeString(Convert.ToDouble(comment.dateCreated))));
        }

        /// <summary>
        /// Converts long to 'yyyy-mm-dd'
        /// </summary>
        /// <param name="timeAfterEpoch"></param>
        /// <returns></returns>
        private static string convertLongToDateTimeString(double timeAfterEpoch)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timeAfterEpoch));

            string a = dateTimeOffset.DateTime.ToString("yyyy-MM-dd");

            return a;
        }
    }
}

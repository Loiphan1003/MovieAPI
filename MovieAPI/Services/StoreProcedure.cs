using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class StoreProcedure
    {
        public StoreProcedure() {
            
        }

        public FormattableString FindGenreByMovieId(Guid movieId)
        {
            FormattableString formattableString = $"EXECUTE FindGenreByMovieId @MovieId = {movieId}";
            return formattableString;
        }

        public FormattableString FindCastByMovieId(Guid movieId)
        {
            FormattableString formattableString = $"EXECUTE FindCastByMovieId @MovieId = {movieId}";
            return formattableString;
        }
    }
}

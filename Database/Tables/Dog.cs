namespace csharp_dao.Database.Tables
{
    /// <summary>
    /// Représente un objet de notre table dogs
    /// </summary>
    public class Dog
    {
        // <summary>
        /// Champ id sur notre table dogs
        /// </summary>
        public int Id { get; set; }

        // <summary>
        /// Champ name sur notre table dogs
        /// </summary>
        public string Name { get; set; }

        // <summary>
        /// Champ breed sur notre table dogs
        /// </summary>
        public string Breed { get; set; }

        // <summary>
        /// Champ birth_date sur notre table dogs
        /// </summary>
        public DateTime Birthdate { get; set; }
    }
}
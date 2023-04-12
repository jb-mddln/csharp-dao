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
        public string Name { get; set; } = "";

        // <summary>
        /// Champ breed sur notre table dogs
        /// </summary>
        public string Breed { get; set; } = "";

        // <summary>
        /// Champ birth_date sur notre table dogs
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Retourne les informations du chien sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du chien</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Breed: {this.Breed}, Birthdate: {(this.Birthdate.HasValue ? this.Birthdate.Value.ToString("dd/MM/yyyy") : "null")}";
        }
    }
}
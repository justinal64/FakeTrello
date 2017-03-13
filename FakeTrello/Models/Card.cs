using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeTrello.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // Auxiliary: given a card instance,
        // return the list it belongs to.
        public List BelongsTo { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<Collaborator> Collaborators { get; set; }
    }
}
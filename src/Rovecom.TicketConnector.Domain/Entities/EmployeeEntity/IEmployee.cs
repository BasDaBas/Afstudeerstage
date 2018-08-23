namespace Rovecom.TicketConnector.Domain.Entities
{
    /// <summary>
    /// An entity that represents an employee
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        string LastName { get; set; }
    }
}
using Dapper;
using Rovecom.TicketConnector.Domain.MSP.MspTechnicianEntity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Repositories
{
    public class MspTechnicianRepository : IMspTechnicianRepository
    {
        /// <summary>
        /// The connection to the database
        /// </summary>
        private IDbConnection Connection => Transaction.Connection;

        /// <summary>
        /// The transaction with the database
        /// </summary>
        private IDbTransaction Transaction { get; }

        /// <inheritdoc />
        public MspTechnicianRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public MspTechnician GetByEmailAddress(string emailAddress)
        {
            var result = Connection.Query("SELECT sdu.userid, sdu.firstname, sdu.lastname, aci.emailid " +
                                          "FROM sduser as sdu " +
                                          "LEFT JOIN aaausercontactinfo as auci ON sdu.userid = auci.user_id " +
                                          "LEFT JOIN aaacontactinfo as aci ON auci.contactinfo_id = aci.contactinfo_id " +
                                          "WHERE sdu.status = 'ACTIVE' " +
                                          "AND aci.emailid = '@EmailAddress'", new { Email = emailAddress }, Transaction);

            return MapTechnician(result);
        }

        public MspTechnician GetById(long id)
        {
            var result = Connection.Query("SELECT sdu.userid, sdu.firstname, sdu.lastname, aci.emailid " +
                                          "FROM sduser as sdu " +
                                          "LEFT JOIN aaausercontactinfo as auci ON sdu.userid = auci.user_id " +
                                          "LEFT JOIN aaacontactinfo as aci ON auci.contactinfo_id = aci.contactinfo_id " +
                                          "WHERE sdu.status = 'ACTIVE' " +
                                          "AND sdu.userid = '@Id'", new { Id = id }, Transaction);

            return MapTechnician(result);
        }

        public IEnumerable<MspTechnician> GetAll()
        {
            var result = Connection.Query("SELECT sdu.firstname, sdu.lastname, emailid " +
                                          "FROM sduser as sdu " +
                                          "LEFT JOIN aaausercontactinfo as auci ON sdu.userid = auci.user_id " +
                                          "LEFT JOIN aaacontactinfo as aci ON auci.contactinfo_id = aci.contactinfo_id " +
                                          "WHERE sdu.status = \'ACTIVE\'", transaction: Transaction);

            return result.Select(res => MapTechnician(res)).Select(x => (MspTechnician)x);
        }

        /// <summary>
        /// Maps the account.
        /// </summary>
        /// <param name="result">The dynamic result</param>
        /// <returns>An MSP technician entity from the dynamic result.</returns>
        private static MspTechnician MapTechnician(dynamic result)
        {
            return new MspTechnician
            {
                Id = result.userid,
                EmailAddress = result.emailid,
                FirstName = result.firstname,
                LastName = result.lastname
            };
        }
    }
}
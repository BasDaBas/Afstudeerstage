using Dapper;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Repositories
{
    public class MspWorklogTypeRepository : IMspWorklogTypeRepository
    {
        /// <summary>
        /// The connection to the database
        /// </summary>
        private IDbConnection Connection => Transaction.Connection;

        /// <summary>
        /// The transaction with the database
        /// </summary>
        private IDbTransaction Transaction { get; }

        /// <summary>
        /// Default constructor for <see cref="IMspWorklogTariffTypeRepository"/>
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/></param>
        public MspWorklogTypeRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            CreateOrUpdateSequences();
        }

        private void CreateOrUpdateSequences()
        {
            // Create sequence for org_id column in sdorganization table
            const string picklistIdSeqQuery = "DO $$ BEGIN CREATE SEQUENCE picklist_id_seq OWNED BY udf_picklistvalues.picklistid; " +
                                              "EXCEPTION WHEN duplicate_table THEN END $$ LANGUAGE plpgsql;";
            Connection.Execute(picklistIdSeqQuery);
        }

        /// <inheritdoc />
        public IEnumerable<MspWorklogType> GetAllActive()
        {
            var result = Connection.Query("SELECT picklistid, value " +
                                          "FROM udf_picklistvalues " +
                                          "WHERE tablename = 'WorkLog_Fields' " +
                                          "AND columname = 'UDF_CHAR1' " +
                                          "AND NOT value LIKE 'Removed%'", transaction: Transaction);

            return result.Select(res => MapWorklogType(res)).Select(x => (MspWorklogType)x);
        }

        /// <inheritdoc />
        public IEnumerable<MspWorklogType> GetAll()
        {
            var result = Connection.Query("SELECT picklistid, value " +
                                          "FROM udf_picklistvalues " +
                                          "WHERE tablename = 'WorkLog_Fields' " +
                                          "AND columname = 'UDF_CHAR1'", transaction: Transaction);

            return result.Select(res => MapWorklogType(res)).Select(x => (MspWorklogType)x);
        }

        /// <inheritdoc />
        public void Add(MspWorklogType worklogType)
        {
            const string query = "INSERT INTO udf_picklistvalues(picklistid, tablename, columname, value) " +
                                 "VALUES (nextval('picklist_id_seq'), 'WorkLog_Fields', 'UDF_CHAR1', @Desc) RETURNING picklistid";

            var param = new
            {
                Desc = worklogType.Description
            };

            worklogType.Id = Connection.ExecuteScalar<long>(query, param, Transaction);
        }

        /// <inheritdoc />
        public void Remove(MspWorklogType worklogType)
        {
            const string query = "UPDATE udf_picklistvalues" +
                                 "SET value = @Desc " +
                                 "WHERE picklistid = @Id";

            var param = new
            {
                Desc = $"Removed - {worklogType.Description}",
                Id = worklogType.Id,
            };

            worklogType.Id = Connection.ExecuteScalar<long>(query, param, Transaction);
        }

        // Maps the dynamic result
        private static MspWorklogType MapWorklogType(dynamic res)
        {
            var type = new MspWorklogType { Id = res.picklistid, Description = res.value, IsRemoved = false };
            if (type.Description.Contains("Removed"))
                type.IsRemoved = true;
            return type;
        }
    }
}
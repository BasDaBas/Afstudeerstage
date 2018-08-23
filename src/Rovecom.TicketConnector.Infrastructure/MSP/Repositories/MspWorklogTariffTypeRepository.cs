using Dapper;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Repositories
{
    /// <inheritdoc />
    public class MspWorklogTariffTypeRepository : IMspWorklogTariffTypeRepository
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
        public MspWorklogTariffTypeRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            CreateOrUpdateSequences();
        }

        /// <inheritdoc />
        public IEnumerable<MspWorklogTariffType> GetAllActive()
        {
            var result = Connection.Query("SELECT picklistid, value " +
                                          "FROM udf_picklistvalues " +
                                          "WHERE tablename = 'WorkLog_Fields' " +
                                          "AND columname = 'UDF_CHAR2' " +
                                          "AND NOT value LIKE 'Removed%'", transaction: Transaction);

            return result.Select(res => MapWorklogTariffType(res)).Select(x => (MspWorklogTariffType)x);
        }

        /// <inheritdoc />
        public IEnumerable<MspWorklogTariffType> GetAll()
        {
            var result = Connection.Query("SELECT picklistid, value " +
                                          "FROM udf_picklistvalues " +
                                          "WHERE tablename = 'WorkLog_Fields' " +
                                          "AND columname = 'UDF_CHAR2'", transaction: Transaction);

            return result.Select(res => MapWorklogTariffType(res)).Select(x => (MspWorklogTariffType)x);
        }

        /// <inheritdoc />
        public void Add(MspWorklogTariffType worklogTariffType)
        {
            const string query = "INSERT INTO udf_picklistvalues(picklistid, tablename, columname, value) " +
                                 "VALUES (nextval('picklist_id_seq'), 'WorkLog_Fields', 'UDF_CHAR2', @Desc) RETURNING picklistid";

            var param = new
            {
                Desc = worklogTariffType.Description
            };

            worklogTariffType.Id = Connection.ExecuteScalar<long>(query, param, Transaction);
        }

        /// <inheritdoc />
        public void Remove(MspWorklogTariffType worklogTariffType)
        {
            const string query = "UPDATE udf_picklistvalues" +
                                 "SET value = @Desc " +
                                 "WHERE picklistid = @Id";

            var param = new
            {
                Desc = $"Removed - {worklogTariffType.Description}",
                Id = worklogTariffType.Id,
            };

            worklogTariffType.Id = Connection.ExecuteScalar<long>(query, param, Transaction);
        }

        // Maps the dynamic result
        private static MspWorklogTariffType MapWorklogTariffType(dynamic res)
        {
            var type = new MspWorklogTariffType(res.picklistid, res.value);
            if (type.Description.Contains("Removed"))
                type.IsRemoved = true;
            return type;
        }

        // Creates or updates sequences for tables related to MSP account entity
        private void CreateOrUpdateSequences()
        {
            // Create sequence for org_id column in sdorganization table
            const string picklistIdSeqQuery = "DO $$ BEGIN CREATE SEQUENCE picklist_id_seq OWNED BY udf_picklistvalues.picklistid; " +
                                              "EXCEPTION WHEN duplicate_table THEN END $$ LANGUAGE plpgsql;";
            Connection.Execute(picklistIdSeqQuery);
        }
    }
}
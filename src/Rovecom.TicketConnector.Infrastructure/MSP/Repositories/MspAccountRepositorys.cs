using Dapper;
using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Repositories
{
    /// <inheritdoc cref="IMspAccountRepository" />
    public class MspAccountRepository : IMspAccountRepository
    {/// <inheritdoc />
        public MspAccountRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            CreateOrUpdateSequences();
        }

        private IDbConnection Connection => Transaction.Connection;

        private IDbTransaction Transaction { get; }

        /// <inheritdoc />
        public void Create(MspAccount account)
        {
            //Create account
            CreateAccount(ref account);

            //Create row with extra information regarding site
            CreateSite(ref account);
            CreateSiteDefinition(account);

            //Create rows with extra information regarding account
            var ciId = CreateCiReference(account);
            CreateAccountInformation(account);
            CreateAccountDefinition(account, ciId);
            CreateAccountCode(account, ciId);
        }

        /// <inheritdoc />
        public MspAccount Get(long id)
        {
            MspAccount account = null;

            var result = Connection.Query("SELECT ad.org_id, ad.org_name, ad.defaultsiteid, ci.description, aci.emailid," +
                                  " aci.landline, aci.fax, aci.web_url, acci.attribute_303 FROM accountdefinition as ad" +
                                  " JOIN ci ON ad.ciid = ci.ciid" +
                                  " JOIN sdorgcontactinfo as sdoci ON ad.org_id = sdoci.org_id" +
                                  " LEFT JOIN accountci as acci ON ad.ciid = acci.ciid" +
                                  " JOIN aaacontactinfo as aci ON sdoci.contactinfo_id = aci.contactinfo_id" +
                                  " WHERE ad.org_id=@ID", new { Id = id }, Transaction).SingleOrDefault();
            if (result != null)
                account = MapAccount(result);

            return account;
        }

        public MspAccount GetByDefaultSiteId(long id)
        {
            MspAccount account = null;

            var result = Connection.Query("SELECT ad.org_id, ad.org_name, ad.defaultsiteid, ci.description, aci.emailid," +
                                          " aci.landline, aci.fax, aci.web_url, acci.attribute_303 FROM accountdefinition as ad" +
                                          " JOIN ci ON ad.ciid = ci.ciid" +
                                          " JOIN sdorgcontactinfo as sdoci ON ad.org_id = sdoci.org_id" +
                                          " LEFT JOIN accountci as acci ON ad.ciid = acci.ciid" +
                                          " JOIN aaacontactinfo as aci ON sdoci.contactinfo_id = aci.contactinfo_id" +
                                          " WHERE ad.defaultsiteid=@ID", new { Id = id }, Transaction).SingleOrDefault();
            if (result != null)
                account = MapAccount(result);

            return account;
        }

        /// <inheritdoc />
        public IEnumerable<MspAccount> GetAll()
        {
            var result = Connection.Query("SELECT ad.org_id, ad.org_name, ad.defaultsiteid, ci.description, aci.emailid," +
                                  " aci.landline, aci.fax, aci.web_url, acci.attribute_303 FROM accountdefinition as ad" +
                                  " JOIN ci ON ad.ciid = ci.ciid" +
                                  " JOIN sdorgcontactinfo as sdoci ON ad.org_id = sdoci.org_id" +
                                  " LEFT JOIN accountci as acci ON ad.ciid = acci.ciid" +
                                  " JOIN aaacontactinfo as aci ON sdoci.contactinfo_id = aci.contactinfo_id", transaction: Transaction);

            return result.Select(res => MapAccount(res)).Select(x => (MspAccount)x).ToList();
        }

        /// <inheritdoc />
        public MspAccount GetByCode(string code)
        {
            MspAccount account = null;

            var result = Connection.Query("SELECT ad.org_id, ad.org_name, ad.defaultsiteid, ci.description, aci.emailid," +
                                  " aci.landline, aci.fax, aci.web_url, acci.attribute_303 FROM accountdefinition as ad" +
                                  " JOIN ci ON ad.ciid = ci.ciid" +
                                  " LEFT JOIN sdorgcontactinfo as sdoci ON ad.org_id = sdoci.org_id" +
                                  " LEFT JOIN accountci as acci ON ad.ciid = acci.ciid" +
                                  " LEFT JOIN aaacontactinfo as aci ON sdoci.contactinfo_id = aci.contactinfo_id" +
                                  " WHERE acci.attribute_303=@Code", new { Code = code }, Transaction).SingleOrDefault();
            if (result != null)
                account = MapAccount(result);

            return account;
        }

        /// <inheritdoc />
        public void Update(MspAccount account)
        {
            //Update account
            UpdateAccount(account);
            UpdateAccountInformation(account);
            var ciId = UpdateAccountDefinition(account);
            UpdateAccountCode(account, ciId);
            UpdateCiReference(account, ciId);
        }

        private void UpdateAccountCode(MspAccount account, long ciId)
        {
            const string query = "UPDATE accountci SET attribute_303 = @Code, WHERE ciid = @CiId";

            var param = new
            {
                CiId = ciId,
                Code = account.Code
            };

            Connection.Execute(query, param, Transaction);
        }

        private void UpdateAccountInformation(MspAccount account)
        {
            const string query = "UPDATE aaacontactinfo " +
                                 "SET emailid = @EmailAddress, landline = @Tel, fax= @Fax, web_url = @Website " +
                                 "FROM sdorgcontactinfo " +
                                 "WHERE sdorgcontactinfo.contactinfo_id = aaacontactinfo.contactinfo_id " +
                                 "AND org_id = @MspId)";

            var param = new
            {
                Email = account.EmailAddress,
                Tel = account.TelephoneNumber,
                Fax = account.FaxNumber,
                Id = account.Id
            };

            Connection.Execute(query, param, Transaction);
        }

        private long UpdateAccountDefinition(MspAccount account)
        {
            var query = "UPDATE accountdefinition " +
                        "SET org_name = @Name, lastupdatedtime = @UnixTimeStamp " +
                        "WHERE org_id = @MspId RETURNING ciid";
            var param = new
            {
                Name = account.Name,
                UnixTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            return Connection.ExecuteScalar<long>(query, param, Transaction);
        }

        private void UpdateCiReference(MspAccount account, long ciid)
        {
            const string query = "UPDATE ci SET ciname = @Name, description = @Desc WHERE ciid = @CiId";

            var param = new
            {
                CiId = ciid,
                Name = account.Name,
            };

            Connection.Execute(query, param, Transaction);
        }

        private void UpdateAccount(MspAccount account)
        {
            const string query = "UPDATE sdorganization SET name = @Name, description = @Desc WHERE org_id = @MspId";

            var param = new
            {
                Id = account.Id,
                Name = account.Name,
            };

            Connection.Execute(query, param, Transaction);
        }

        /// <summary>
        /// Maps the account.
        /// </summary>
        /// <param name="result">The dynamic result</param>
        /// <returns>An account entity from the dynamic result.</returns>
        private static MspAccount MapAccount(dynamic result)
        {
            return new MspAccount
            {
                Id = result.org_id,
                Name = result.org_name,
                Code = result.attribute_303,
                DefaultSiteId = result.defaultsiteid,
                TelephoneNumber = result.landline,
                FaxNumber = result.fax,
                WebsiteUrl = result.web_url,
                EmailAddress = result.emailid,
            };
        }

        /// <summary>
        /// Creates a sdorganisation row from MSP account entity and sets the account MspId
        /// </summary>
        /// <param name="account"><see cref="MspAccount"/></param>
        private void CreateAccount(ref MspAccount account)
        {
            const string accountQuery = "INSERT INTO sdorganization(org_id, name, createdtime, description) " +
                                        "VALUES (nextval('sdo_id_ticket_seq'), @Name, @UnixTimeStamp, 'Account created from SIS Synchronisation') RETURNING org_id";

            var accountParam = new
            {
                Name = account.Name,
                UnixTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            account.Id = Connection.ExecuteScalar<long>(accountQuery, accountParam, Transaction);
        }

        /// <summary>
        /// Creates a custom value row that represents the account code
        /// </summary>
        /// <param name="account"></param>
        /// <param name="ciId"></param>
        private void CreateAccountCode(MspAccount account, long ciId)
        {
            const string query = "INSERT INTO accountci(ciid, attribute_303) " +
                                                   "VALUES (@CiId, @Code)";

            var param = new
            {
                CiId = ciId,
                Code = account.Code,
            };

            Connection.Execute(query, param, Transaction);
        }

        /// <summary>
        /// Creates an accountdefinition table row that holds account information
        /// </summary>
        /// <param name="account"><see cref="MspAccount"/></param>
        /// <param name="ciId">MspId of ci row</param>
        private void CreateAccountDefinition(MspAccount account, long ciId)
        {
            const string accDefQuery = "INSERT INTO accountdefinition(org_id, log_logo, head_logo, org_name, hasattachment, login_uri, defaultsiteid, ciid, lastupdatedtime) " +
                                       "VALUES(@AccountId, 'Default', 'Default', @Name, false, @LoginUri, @SiteId, @CiId, @UnixTimeStamp) ";

            var accDefParam = new
            {
                AccountId = account.Id,
                Name = account.Name,
                LoginUri = account.Name.Replace(" ", string.Empty).ToLower(),
                UnixTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                SiteId = account.DefaultSiteId,
                CiId = ciId,
            };
            Connection.Execute(accDefQuery, accDefParam, Transaction);
        }

        /// <summary>
        /// Creates an aaacontactinfo table row and an sdorgcontactinfo row
        /// </summary>
        /// <param name="account"><see cref="MspAccount"/></param>
        /// <returns>MspId of aaacontactinfo row</returns>
        private void CreateAccountInformation(MspAccount account)
        {
            // Creates an aaacontactinfo table row that holds
            const string accContactInfoQuery = "INSERT INTO aaacontactinfo(contactinfo_id, emailid, landline, fax, web_url) " +
                                               "VALUES(nextval('contactinfo_id_ticket_seq'), @EmailAddress, @Tel, @Fax, @Website) RETURNING contactinfo_id";

            var accContactInfoParam = new
            {
                Email = account.EmailAddress,
                Tel = account.TelephoneNumber,
                Fax = account.FaxNumber,
                Website = account.WebsiteUrl
            };

            //Creates a sdorgcontactinfo table row that links the account and the account information
            var accInfoId = Connection.ExecuteScalar<long>(accContactInfoQuery, accContactInfoParam, Transaction);

            const string accContactInfoLinkQuery = "INSERT INTO sdorgcontactinfo(org_id, contactinfo_id) " +
                                                   "VALUES (@AccountId, @InfoId)";

            var accContactInfoLinkParam = new
            {
                AccountId = account.Id,
                InfoId = accInfoId
            };

            Connection.Execute(accContactInfoLinkQuery, accContactInfoLinkParam, Transaction);
        }

        /// <summary>
        /// Creates a ci table row from a MSP account entity
        /// </summary>
        /// <param name="account"><see cref="MspAccount"/></param>
        /// <returns>MspId of ci row</returns>
        private long CreateCiReference(MspAccount account)
        {
            const string ciQuery = "INSERT INTO ci(ciid, citypeid, ciname, description, siteid) " +
                                   "VALUES(nextval('ci_id_ticket_seq'), 38, @Name, @Desc, @SiteId) RETURNING ciid";

            var ciParam = new
            {
                Name = account.Name,
                SiteId = account.DefaultSiteId
            };

            return Connection.ExecuteScalar<long>(ciQuery, ciParam, Transaction);
        }

        /// <summary>
        /// Creates or updates sequences for tables related to MSP account entity
        /// </summary>
        private void CreateOrUpdateSequences()
        {
            // Create sequence for org_id column in sdorganization table
            const string sdoSeqQuery = "DO $$ BEGIN CREATE SEQUENCE sdo_id_ticket_seq OWNED BY sdorganization.org_id; " +
                                       "EXCEPTION WHEN duplicate_table THEN END $$ LANGUAGE plpgsql;";
            Connection.Execute(sdoSeqQuery);

            // Set the highest id as starting value of the sequence
            const string sdoSeqStartValQuery = "SELECT setval('sdo_id_ticket_seq', max(org_id)) FROM sdorganization";
            Connection.Execute(sdoSeqStartValQuery);

            // Create sequence for org_id column in sdorganization table
            const string ciSeqQuery = "DO $$ BEGIN CREATE SEQUENCE ci_id_ticket_seq OWNED BY ci.ciid; " +
                                      "EXCEPTION WHEN duplicate_table THEN END $$ LANGUAGE plpgsql;";
            Connection.Execute(ciSeqQuery);

            // Set the highest id as starting value of the sequence
            const string ciSeqStartValQuery = "SELECT setval('ci_id_ticket_seq', max(ciid)) FROM ci";
            Connection.Execute(ciSeqStartValQuery);

            // Create sequence for org_id column in sdorganization table
            const string contactInfoSeqQuery = "DO $$ BEGIN CREATE SEQUENCE contactinfo_id_ticket_seq OWNED BY aaacontactinfo.contactinfo_id; " +
                                               "EXCEPTION WHEN duplicate_table THEN END $$ LANGUAGE plpgsql;";
            Connection.Execute(contactInfoSeqQuery);

            // Set the highest id as starting value of the sequence
            const string contactInfoSeqStartValQuery = "SELECT setval('contactinfo_id_ticket_seq', max(contactinfo_id)) FROM aaacontactinfo";
            Connection.Execute(contactInfoSeqStartValQuery);
        }

        /// <summary>
        /// Create sdorganisation table row from MSP account entity and sets the defaultSite MspId
        /// </summary>
        /// <param name="account">The MSP account</param>
        private void CreateSite(ref MspAccount account)
        {
            const string siteQuery = "INSERT INTO sdorganization(org_id, name, createdtime, description) " +
                                     "VALUES (nextval('sdo_id_ticket_seq'), @Name, @UnixTimeStamp, 'Site created from SIS Synchronisation') RETURNING org_id";

            var siteParam = new
            {
                Name = account.Name,
                UnixTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            account.DefaultSiteId = Connection.ExecuteScalar<long>(siteQuery, siteParam, Transaction);
        }

        /// <summary>
        /// Creates sitedefinition table row that holds site information
        /// </summary>
        /// <param name="account"></param>
        private void CreateSiteDefinition(MspAccount account)
        {
            const string siteDefQuery = "INSERT INTO sitedefinition(siteid) " +
                                        "VALUES (@SiteId)";
            Connection.Execute(siteDefQuery, new { SiteId = account.DefaultSiteId }, Transaction);
        }
    }
}
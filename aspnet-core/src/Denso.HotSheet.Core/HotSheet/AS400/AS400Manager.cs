using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Denso.HotSheet.AS400.Dto;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.HotSheet.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Denso.HotSheet.AS400.Connection;
using Denso.HotSheet.HotSheet.Enums;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Denso.HotSheet.HotSheet.AS400
{
    public class AS400Manager : IAS400Manager, ITransientDependency
    {
        private readonly IRepository<CigmaExport, long> _cigmaExportRepository;
        private readonly IRepository<HotSheetsShip, long> _HotSheetRepository;
        private readonly IDapperRepository<HotSheetsShip, long> _HotSheetDapperRepository;
        private readonly IHotSheetHistoryManager _shippingHistoryManager;
        private readonly IAS400Connection _as400Connection;

        public AS400Manager(
            IRepository<CigmaExport, long> cigmaExportRepository,
            IRepository<HotSheetsShip, long> HotSheetRepository,
            IDapperRepository<HotSheetsShip, long> HotSheetDapperRepository,
            IHotSheetHistoryManager shippingHistoryManager,
            IAS400Connection as400Connection
        )
        {
            _cigmaExportRepository = cigmaExportRepository;
            _HotSheetRepository = HotSheetRepository;
            _HotSheetDapperRepository = HotSheetDapperRepository;
            _shippingHistoryManager = shippingHistoryManager;
            _as400Connection = as400Connection;
        }

        public async Task TestConnection()
        {
            // CheckSQLVersion();
            await ExportHotSheetToAS400(120034);
        }

        public async Task<bool> ExportHotSheetToAS400(long HotSheetShiptId)
        {
            bool manifestSaved = false;
            List<string> errorMessages = new List<string>();

            string sqlQueryManifest = "EXEC GetHotSheetToExport @HotSheetShiptId";
            var sqlParamsManifest = new
            {
                HotSheetShiptId = HotSheetShiptId,
            };

            IEnumerable<AS400ManifestDto> itemsManifest = await _HotSheetDapperRepository.QueryAsync<AS400ManifestDto>(sqlQueryManifest, sqlParamsManifest);
            AS400ManifestDto manifest = itemsManifest.FirstOrDefault();

            if (manifest != null)
            {
                string queryCheckExists = $"SELECT SHFOL FROM MXMODREL.ED680PR WHERE SHFOL = {HotSheetShiptId}";
                DataSet dsCheckExistsResult = _as400Connection.ExecuteWithReturnDataSet(queryCheckExists);
                if(dsCheckExistsResult.Tables.Count > 0 && dsCheckExistsResult.Tables[0].Rows.Count == 0)
                {
                    string queryInsertManifest = @"INSERT INTO MXMODREL.ED680PR (SHID, SHFOL, SHINV, SHSUNO, SHSHP, SHCAR1, SHTES1, SHTEP1, SHORIG, SHATTO, SHDMX1, SHDMX2, SHDMX3, SHDMX4, SHDMX5,
                        SHDMXR, SHSOT1, SHSOT2, SHSOT3, SHSOT4, SHSOT5, SHSHT1, SHSHT2, SHSHT3, SHSHT4, SHSHT5, SHTOPA, SHNEWT, SHGRWT, SHINF11, SHINF12, SHINF13, SHAUX0, SHAUX1, SHAUX2,
                        SHAUX3, SHAUX4, SHAUX5, SHAUX6, SHAUX7, SHAUX8, SHAUX9, SHSTAT, SHTIME, SHDATE)
                    VALUES ('SH', ?, 0, ?, ?, ?, ?, ?, '', ?, ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '', ?,
                        ?, '', 0, 0, 0, 0, 0, '', ?, ?)";

                    // ===> Following lines for tests to get full insert query <=====
                    /*
                    queryInsertManifest = @"INSERT INTO MXMODREL.ED680PR (SHID, SHFOL, SHINV, SHSUNO, SHSHP, SHCAR1, SHTES1, SHTEP1, SHORIG, SHATTO, 
                        SHDMX1, SHDMX2, SHDMX3, SHDMX4, SHDMX5, SHDMXR, SHSOT1, SHSOT2, SHSOT3, SHSOT4, 
                        SHSOT5, SHSHT1, SHSHT2, SHSHT3, SHSHT4, SHSHT5, SHTOPA, SHNEWT, SHGRWT, SHINF11, 
                        SHINF12, SHINF13, SHAUX0, SHAUX1, SHAUX2, SHAUX3, SHAUX4, SHAUX5, SHAUX6, SHAUX7,
                        SHAUX8, SHAUX9, SHSTAT, SHTIME, SHDATE)
                    VALUES ('SH', @SHFOL, 0, @SHSUNO, @SHSHP, @SHCAR1, @SHTES1, @SHTEP1, '', @SHATTO, 
                        @SHDMX1, @SHDMX2, @SHDMX3, @SHDMX4, @SHDMX5, @SHDMXR, @SHSOT1, @SHSOT2, @SHSOT3, @SHSOT4,
                        @SHSOT5, @SHSHT1, @SHSHT2, @SHSHT3, @SHSHT4, @SHSHT5, @SHTOPA, @SHNEWT, @SHGRWT, @SHINF11,
                        @SHINF12, @SHINF13, @SHAUX0, '', @SHAUX2, @SHAUX3, '', 0, 0, 0,
                        0, 0, '', @SHTIME, @SHDATE)";
                    */

                    List<AS400ParameterDto> queryParamsManifest = new()
                    {
                        new("@SHFOL", manifest.SHFOL),
                        new("@SHSUNO", manifest.SHSUNO),
                        new("@SHSHP", manifest.SHSHP),
                        new("@SHCAR1", manifest.SHCAR1),
                        new("@SHTES1", manifest.SHTES1),
                        new("@SHTEP1", manifest.SHTEP1),
                        new("@SHATTO", manifest.SHATTO),
                        new("@SHDMX1", manifest.SHDMX1),
                        new("@SHDMX2", manifest.SHDMX2),
                        new("@SHDMX3", manifest.SHDMX3),
                        new("@SHDMX4", manifest.SHDMX4),
                        new("@SHDMX5", manifest.SHDMX5),
                        new("@SHDMXR", manifest.SHDMXR),
                        new("@SHSOT1", manifest.SHSOT1),
                        new("@SHSOT2", manifest.SHSOT2),
                        new("@SHSOT3", manifest.SHSOT3),
                        new("@SHSOT4", manifest.SHSOT4),
                        new("@SHSOT5", manifest.SHSOT5),
                        new("@SHSHT1", manifest.SHSHT1),
                        new("@SHSHT2", manifest.SHSHT2),
                        new("@SHSHT3", manifest.SHSHT3),
                        new("@SHSHT4", manifest.SHSHT4),
                        new("@SHSHT5", manifest.SHSHT5),
                        new("@SHTOPA", manifest.SHTOPA),
                        new("@SHNEWT", manifest.SHNEWT),
                        new("@SHGRWT", manifest.SHGRWT),
                        new("@SHINF11", manifest.SHINF11),
                        new("@SHINF12", manifest.SHINF12),
                        new("@SHINF13", manifest.SHINF13),
                        new("@SHAUX0", manifest.SHAUX0),
                        new("@SHAUX2", manifest.SHAUX2),
                        new("@SHAUX3", manifest.SHAUX3),
                        new("@SHTIME", manifest.SHTIME),
                        new("@SHDATE", manifest.SHDATE)
                    };
                    
                    try
                    {
                        _as400Connection.ExecuteWithNoReturn(queryInsertManifest, queryParamsManifest);

                        manifestSaved = true;
                    }
                    catch(Exception exc)
                    {
                        //OleDbException oleDbException = exc.InnerException as OleDbException;
                        //SqlException sqlException = exc.InnerException as SqlException;

                        errorMessages.Add(exc.Message);
                    }
                }

                if (manifestSaved)
                {
                    string sqlQueryManifestContainers = "EXEC GetHotSheetShipProductsToExport @HotSheetShiptId";
                    var sqlParamsManifestContainers = new
                    {
                        HotSheetShiptId = HotSheetShiptId,
                    };
                    
                    IEnumerable<AS400ManifestContainerDto> itemsContainers = await _HotSheetDapperRepository.QueryAsync<AS400ManifestContainerDto>(sqlQueryManifestContainers, sqlParamsManifestContainers);

                    string queryInsertContainer = @"INSERT INTO MXMODREL.ED681PR (SDID, SDFOL, SDCUIN, SDIDE1, SDIDE2, SDIDS1, SDIDS2, SDPRTN, SDSHQY, SDPRIC,
                                SDUNM, SDCUPO, SDSATC, SDTIME, SDDATE, SDORIG)
                            VALUES ('SH', ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                ?, ?, ?, ?, ?, ?)";
                    
                    foreach (AS400ManifestContainerDto itemContainer in itemsContainers)
                    {
                        //string queryCheckContainerExists = @$"SELECT SDFOL FROM MXMODREL.ED681PR
                        //        WHERE SDFOL = {itemContainer.SDFOL} AND SDCUIN = '{itemContainer.SDCUIN}'";

                        //DataSet dsCheckContainerExistsResult = _as400Connection.ExecuteWithReturnDataSet(queryCheckContainerExists);
                        //if (dsCheckContainerExistsResult.Tables.Count > 0 && dsCheckContainerExistsResult.Tables[0].Rows.Count == 0)
                        //{
                            List<AS400ParameterDto> queryParamsContainer = new()
                                {
                                    new("@SDFOL", itemContainer.SDFOL),
                                    new("@SDCUIN", itemContainer.SDCUIN),
                                    new("@SDIDE1", itemContainer.SDIDE1),
                                    new("@SDIDE2", itemContainer.SDIDE2),
                                    new("@SDIDS1", itemContainer.SDIDS1),
                                    new("@SDIDS2", itemContainer.SDIDS2),
                                    new("@SDPRTN", itemContainer.SDPRTN),
                                    new("@SDSHQY", itemContainer.SDSHQY),
                                    new("@SDPRIC", itemContainer.SDPRIC),
                                    new("@SDUNM", itemContainer.SDUNM),
                                    new("@SDCUPO", itemContainer.SDCUPO),
                                    new("@SDSATC", itemContainer.SDSATC),
                                    new("@SDTIME", itemContainer.SDTIME),
                                    new("@SDDATE", itemContainer.SDDATE),
                                    new("@SDORIG", itemContainer.SDORIG),
                                };

                            try
                            {
                                _as400Connection.ExecuteWithNoReturn(queryInsertContainer, queryParamsContainer);
                            }
                            catch (Exception exc)
                            {
                                errorMessages.Add(exc.Message);
                                manifestSaved = false;
                            }
                        //}
                    }
                }
               
                await _cigmaExportRepository.InsertAsync(new CigmaExport
                {
                    HotSheetShiptId = HotSheetShiptId,
                    TenantId = 1,
                    ExportedToCigma = manifestSaved,
                    ExportedDate = DateTime.Now,
                    DetailExported = manifestSaved,
                    ErrorOnExport = errorMessages.Count > 0,
                    ErrorMessage = string.Join(", ", errorMessages)
                });

                await _shippingHistoryManager.AddAsync(HotSheetShiptId, HotSheetHistoryType.ExportedToAS400, string.Join(", ", errorMessages));
            }

            return manifestSaved;
        }

        private void CheckSQLVersion()
        {
            string query = "SELECT @@VERSION";

            DataSet dataSet = new DataSet();

            //OleDbCommand cmd = new OleDbCommand(query);
            //using (OleDbConnection conn = new OleDbConnection(_connectionString))
            //{
            //    conn.Open();
            //    cmd.Connection = conn;
            //    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
            //    {
            //        adapter.Fill(dataSet);
            //    }
            //}
        }

        //string queryInsertManifest = @"INSERT INTO MXMODREL.ED680PR (SHID, SHFOL, SHINV, SHSUNO, SHSHP, SHCAR1, SHTES1, SHTEP1, SHORIG, SHATTO, SHDMX1, SHDMX2, SHDMX3, SHDMX4, SHDMX5,
        //    SHDMXR, SHSOT1, SHSOT2, SHSOT3, SHSOT4, SHSOT5, SHSHT1, SHSHT2, SHSHT3, SHSHT4, SHSHT5, SHTOPA, SHNEWT, SHGRWT, SHINF11, SHINF12, SHINF13, SHAUX0, SHAUX1, SHAUX2,
        //    SHAUX3, SHAUX4, SHAUX5, SHAUX6, SHAUX7, SHAUX8, SHAUX9, SHSTAT, SHTIME, SHDATE)
        //VALUES ('SH', @SHFOL, 0, @SHSUNO, @SHSHP, @SHCAR1, @SHTES1, @SHTEP1, '', @SHATTO, @SHDMX1, @SHDMX2, @SHDMX3, @SHDMX4, '',
        //    '', @SHSOT1, @SHSOT2, @SHSOT3, @SHSOT4, @SHSOT5, @SHSHT1, @SHSHT2, @SHSHT3, @SHSHT4, @SHSHT5, @SHTOPA, @SHNEWT, @SHGRWT, @SHINF11, @SHINF12, @SHINF13, @SHAUX0, '', '1',
        //    @SHAUX3, '', 0, 0, 0, 0, 0, '', @SHTIME, @SHDATE)";

        //string queryInsertContainer = @"INSERT INTO MXMODREL.ED681PR (SDID, SDFOL, SDCUIN, SDIDE1, SDIDE2, SDIDS1, SDIDS2, SDPRTN, SDSHQY, SDPRIC,
        //        SDUNM, SDCUPO, SDSATC, SDTIME, SDDATE, SDORIG)
        //    VALUES ('SH', @SDFOL, @SDCUIN, @SDIDE1, @SDIDE2, @SDIDS1, @SDIDS2, @SDPRTN, @SDSHQY, @SDPRIC,
        //        @SDUNM, @SDCUPO, @SDSATC, @SDTIME, @SDDATE, @SDORIG)";
    }
}

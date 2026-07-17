using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.UI;
using Dapper;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.BackgroundJobs;
using Denso.HotSheet.BackgroundJobs.Args;
using Denso.HotSheet.BackgroundJobs.Enums;
using Denso.HotSheet.Catalogs;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Files;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.HotSheet.AS400;
using Denso.HotSheet.HotSheet.DBServices;
using Denso.HotSheet.HotSheet.DBServices.Dto;
using Denso.HotSheet.HotSheet.Enums;
using Denso.HotSheet.HotSheet.Logger;
using Denso.HotSheet.HotSheet.Sheets.Dto;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Surveys;
using Denso.HotSheet.Surveys.Dto;
using Denso.HotSheet.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Abp.Dependency;
using Abp.Configuration;
using Denso.HotSheet.Configuration;
using Microsoft.Extensions.Configuration;

namespace Denso.HotSheet.Sheets
{
    [AbpAuthorize]
    public class HotSheetAppService : HotSheetAppServiceBase, IHotSheetAppService
    {  
       
        //private readonly IDapperRepository<HotSheetsShip, long> _HotSheetShipDapperRepository;
        //private readonly IRepository<HotSheetsShip, long> _HotSheetShipRepository;
        //private readonly IRepository<HotSheetShipProduct, long> _HotSheetShipProductRepository;
        //private readonly IRepository<PartNumber, long> _partNumberRepository;
        //private readonly IRepository<HotSheetShipPackaging, long> _HotSheetShipPackagingRepository;
        //private readonly IRepository<Staff, long> _staffRepository;
        //private readonly IRepository<HotSheetShipApproval, long> _HotSheetApprovalRepository;
        private readonly IRepository<Catalogs.File, long> _fileRepository;
        //private readonly IRepository<PartNumberInternal, long> _partNumberInternalRepository;        
        //private readonly IRepository<CarrierNonWorkingDay, long> _carrierNonWorkingDayRepository;
        //private readonly IUserAppService _userAppService;
        //private readonly IBackgroundJobManager _backgroundJobManager;
        //private readonly IHotSheetHistoryManager _hotSheetHistoryManager;
        //private readonly IAS400Manager _as400Manager;
        //private readonly IDBServicesManager _dBServicesManager;
        //private readonly IRepository<CustomerPlantContact, long> _customerPlantContactRepository;
        //private readonly IRepository<HotSheetShipManifest, long> _HotSheetShipManifestRepository;        

        //Nuevo LHH
        private readonly IRepository<HotSheets, long> _hotSheetsRepository;
        private readonly IDapperRepository<HotSheets, long> _hotSheetsDapperRepository;

        private readonly IRepository<PurchaseOrders, long> _purchaseOrdersRepository;
        private readonly IDapperRepository<PurchaseOrders, long> _purchaseOrdersDapperRepository;

        //private readonly IRepository<HotSheetsComments, long> _hotSheetCommentsRepository;
        //private readonly IDapperRepository<HotSheetsComments, long> _hotSheetCommentsDapperRepository;

        private readonly IConfigurationRoot _appConfiguration;

        private readonly IEmailSender _emailSender;

        //public HotSheetAppService()
        //{         
        //}

        public HotSheetAppService(
            //IRepository<HotSheetsShip, long> HotSheetShipRepository,
            //IRepository<HotSheetShipProduct, long> HotSheetShipProductRepository,
            //IRepository<PartNumber, long> partNumberRepository,
            //IRepository<HotSheetShipPackaging, long> HotSheetShipPackagingRepository,
            //IRepository<Staff, long> staffRepository,
            //IRepository<HotSheetShipApproval, long> HotSheetApprovalRepository,
            IRepository<Catalogs.File, long> fileRepository,
            //IRepository<PartNumberInternal, long> partNumberInternalRepository,
            //IDapperRepository<HotSheetsShip, long> HotSheetShipDapperRepository,
            //IRepository<CarrierNonWorkingDay, long> carrierNonWorkingDayRepository,
            //IUserAppService userAppService,
            //IBackgroundJobManager backgroundJobManager,
            //IHotSheetHistoryManager shippingHistoryManager,
            //IAS400Manager as400Manager,
            //IDBServicesManager dbServicesManager,
            //IRepository<CustomerPlantContact, long> customerPlantContactRepository,
            //IRepository<HotSheetShipManifest, long> HotSheetShipManifestRepository,          
            //IDapperRepository<HotSheetsShip, long> HotSheetsShipDapperRepository,

            //Nuevo LHH
            IRepository<HotSheets, long> hotSheetsRepository,
            IDapperRepository<HotSheets, long> hotSheetsDapperRepository,

            IRepository<PurchaseOrders, long> purchaseOrdersRepository,
            IDapperRepository<PurchaseOrders, long> purchaseOrdersDapperRepository,

            IAppConfigurationAccessor appConfigurationAccessor,

            IEmailSender emailSender //AQUI

            //IRepository<HotSheetsComments, long> hotSheetCommentsRepository
            //IDapperRepository<HotSheetsComments, long> hotSheetCommentsDapperRepository
        )
        {

            //_HotSheetShipDapperRepository = HotSheetShipDapperRepository;
            //_HotSheetShipRepository = HotSheetShipRepository;
            //_HotSheetShipProductRepository = HotSheetShipProductRepository;
            //_partNumberRepository = partNumberRepository;
            //_HotSheetShipPackagingRepository = HotSheetShipPackagingRepository;
            //_HotSheetShipDapperRepository = HotSheetShipDapperRepository;
            //_staffRepository = staffRepository;
            //_HotSheetApprovalRepository = HotSheetApprovalRepository;
            _fileRepository = fileRepository;
            //_partNumberInternalRepository = partNumberInternalRepository;
            //_carrierNonWorkingDayRepository = carrierNonWorkingDayRepository;
            //_userAppService = userAppService;
            //_backgroundJobManager = backgroundJobManager;
            //_hotSheetHistoryManager = shippingHistoryManager;
            //_as400Manager = as400Manager;
            //_dBServicesManager = dbServicesManager;
            //_customerPlantContactRepository = customerPlantContactRepository;
            //_HotSheetShipManifestRepository = HotSheetShipManifestRepository;

            //Nuevo LHH
            _hotSheetsDapperRepository = hotSheetsDapperRepository;
            _hotSheetsRepository = hotSheetsRepository;

            _purchaseOrdersDapperRepository = purchaseOrdersDapperRepository;
            _purchaseOrdersRepository = purchaseOrdersRepository;

            _appConfiguration = appConfigurationAccessor.Configuration;

            _emailSender = emailSender;
            //_hotSheetCommentsRepository = hotSheetCommentsRepository;
            //_hotSheetCommentsDapperRepository = hotSheetCommentsDapperRepository;
        }


        [HttpPost]
        public async Task<DashboardKpiDto> GetDashboard(GetDashboardInput input)
        {
            //Convertir string → DateTime
            DateTime? startDate = string.IsNullOrEmpty(input.StartDate)
                ? null
                : DateTime.Parse(input.StartDate);

            DateTime? endDate = string.IsNullOrEmpty(input.EndDate)
                ? null
                : DateTime.Parse(input.EndDate);


            var sqlParams = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            try
            {
                var wh = await _hotSheetsDapperRepository
                    .QueryAsync<SupplierKpiDto>("EXEC GetDashboard_WH @StartDate, @EndDate", sqlParams);

                var ie = await _hotSheetsDapperRepository
                    .QueryAsync<SupplierKpiDto>("EXEC GetDashboard_IE @StartDate, @EndDate", sqlParams);

                var status = await _hotSheetsDapperRepository
                    .QueryAsync<StatusKpiDto>("EXEC GetDashboard_Status @StartDate, @EndDate", sqlParams);

                var result = new DashboardKpiDto
                {
                    WH = wh.ToList(),
                    IE = ie.ToList(),
                    Status = status.ToList()
                };

                foreach (var item in result.WH)
                {
                    item.SupplierName = RemoveDiacritics(item.SupplierName);
                }

                foreach (var item in result.IE)
                {
                    item.SupplierName = RemoveDiacritics(item.SupplierName);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var chars = normalized.Where(c =>
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
                System.Globalization.UnicodeCategory.NonSpacingMark);

            return new string(chars.ToArray())
                .Normalize(System.Text.NormalizationForm.FormC);
        }


        //Este envia lista de Hotsheets sin completar para su revision a los planners.
        public async Task EnviarNotificacion_Antes(NotificacionDto input)
        {
            try
            {
                if (input == null)
                    throw new UserFriendlyException("Datos inválidos");

                if (input.Correos == null || !input.Correos.Any())
                    throw new UserFriendlyException("Sin correos");

                if (input.Registros == null || !input.Registros.Any())
                    throw new UserFriendlyException("Sin registros");

                var urlHotSheet = _appConfiguration["App:ClientRootAddress"] + "app/hotSheets";

                // ===============================
                // OBTENER IDS
                // ===============================
                var ids = input.Registros
                    .Select(x => x.Id)
                    .ToList();

                var idsString = string.Join(",", ids);

                // ===============================
                // CONSULTAR DATOS COMPLETOS
                // ===============================
                string sql = $@"
                                SELECT
                                    hs.Id,
                                    hs.PlannerName,
                                    hs.SupplierCode,
                                    hs.SupplierName,
                                    hs.PartNumber,
                                    hs.PartDescription,
                                    hs.InTransitQty,
                                    ISNULL(tm.Description, '') AS TransportModeName,
                                    ISNULL(hs.TrafficContainerFX, '') AS TrafficContainerFX,
                                    ISNULL(hs.UnitNumber, '') AS UnitNumber,
                                    hs.EtaDNMX,
                                    hs.RealShortageDate,
                                    ISNULL(ss.Description, '') AS ShortageShiftName
                                FROM DensoHotSheets hs
                                LEFT JOIN DensoTransportMode tm
                                    ON tm.Id = hs.TransportModeId
                                LEFT JOIN DensoShortageShift ss
                                    ON ss.Id = hs.ShortageShiftId
                                WHERE hs.Id IN ({idsString})
                            ";

                var registros = (await _hotSheetsDapperRepository
                    .QueryAsync<dynamic>(sql))
                    .ToList();

                if (!registros.Any())
                    throw new UserFriendlyException("No se encontraron registros");

                // ===============================
                // ARMAR FILAS
                // ===============================
                string rows = "";

                foreach (var item in registros)
                {
                    rows += $@"
                    <tr>
                        <td>{item.SupplierCode}</td>
                        <td>{item.SupplierName}</td>
                        <td>{item.PartNumber}</td>
                        <td>{item.PartDescription}</td>
                        <td style='text-align:right'>{item.InTransitQty}</td>
                        <td>{item.TransportModeName}</td>
                        <td>{item.TrafficContainerFX}</td>
                        <td>{item.UnitNumber}</td>
                        <td>{item.EtaDNMX}</td>
                        <td>{item.RealShortageDate}</td>
                        <td>{item.ShortageShiftName}</td>
                    </tr>";
                }

                // ===============================
                // ARMAR BODY
                // ===============================
                string body = $@"
                    <html>
                    <body style='font-family:Arial'>

                        <p>Estimado Planeador,</p>

                        <p>
                            Por medio del presente se informa que los siguientes materiales se encuentran actualmente registrados en la Hot Sheet y requieren seguimiento para su atención y cierre.
                        </p>

                        <p>
                            Favor de revisar el estatus de los registros mostrados a continuación y realizar las acciones correspondientes para su actualización y completado.
                        </p>

                        <p>
                            Puede consultar el detalle completo y dar seguimiento a los registros mediante la siguiente liga:
                        </p>

                        <p>
                            <a href='{urlHotSheet}'
                               style='font-size:14px;
                                      font-weight:bold;
                                      color:#0d6efd;
                                      text-decoration:none;'>
                                Consultar Hot Sheet
                            </a>
                        </p>

                        <div style='overflow-x:auto;'>

                            <table border='1'
                                   cellspacing='0'
                                   cellpadding='5'
                                   style='border-collapse:collapse;
                                          width:100%;
                                          table-layout:auto;
                                          font-size:12px;'>

                                <tr style='background:#2f75b5;color:white;'>

                                    <th style='white-space:nowrap;'>Supplier Code</th>
                                    <th style='white-space:nowrap;'>Supplier</th>
                                    <th style='white-space:nowrap;'>Part Number</th>
                                    <th style='white-space:nowrap;'>Description</th>
                                    <th style='white-space:nowrap;'>In Transit Qty</th>
                                    <th style='white-space:nowrap;'>Transport Mode</th>
                                    <th style='white-space:nowrap;'>Container FX</th>
                                    <th style='white-space:nowrap;'>Unit Number</th>
                                    <th style='white-space:nowrap;'>ETA DNMX</th>
                                    <th style='white-space:nowrap;'>Shortage Date</th>
                                    <th style='white-space:nowrap;'>Shift</th>

                                </tr>

                                {rows}

                            </table>

                        </div>

                        <br/>

                        <p>
                            Agradecemos su apoyo para dar seguimiento oportuno a estos materiales.
                        </p>

                        <p>
                            Saludos cordiales.
                        </p>

                    </body>
                    </html>";

                // ===============================
                // ENVIAR CORREOS
                // ===============================
                foreach (var correo in input.Correos.Distinct())
                {
                    await _emailSender.SendAsync(
                        correo,
                        $"Hot Sheet - Materiales pendientes de atención ({input.Planner})",
                        body,
                        true
                    );
                }

                // ===============================
                // MARCAR COMO ENVIADO
                // ===============================
                var userId = AbpSession.UserId;
                var user = await UserManager.GetUserByIdAsync(userId.Value);
                var userName = $"{user?.Name} {user?.Surname}";

                await _hotSheetsDapperRepository.ExecuteAsync(
                    $@"
                        UPDATE DensoHotSheets
                        SET EmailSent = 1,
                            EmailSentDate = GETDATE(),
                            EmailSentBy = @UserId,
                            EmailSentByName = @UserName
                        WHERE Id IN ({idsString})
                        ",
                    new
                    {
                        UserId = userId,
                        UserName = userName
                    }
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error al enviar correos: " + ex.Message);
            }
        }

        public async Task EnviarNotificacion(NotificacionDto input)
        {
            try
            {
                if (input == null)
                    throw new UserFriendlyException("Datos inválidos");

                if (input.Registros == null || !input.Registros.Any())
                    throw new UserFriendlyException("Sin registros");

                var urlHotSheet = _appConfiguration["App:ClientRootAddress"] + "app/hotSheets";

                // ===============================
                // OBTENER IDS
                // ===============================
                var ids = input.Registros
                    .Select(x => x.Id)
                    .ToList();

                var idsString = string.Join(",", ids);

                // ===============================
                // CONSULTAR DATOS COMPLETOS
                // ===============================
                string sql = $@"
            SELECT
                hs.Id,
                hs.PlannerName,
                hs.SupplierCode,
                hs.SupplierName,
                hs.PartNumber,
                hs.PartDescription,
                hs.InTransitQty,
                ISNULL(tm.Description, '') AS TransportModeName,
                ISNULL(hs.TrafficContainerFX, '') AS TrafficContainerFX,
                ISNULL(hs.UnitNumber, '') AS UnitNumber,
                hs.EtaDNMX,
                hs.RealShortageDate,
                ISNULL(ss.Description, '') AS ShortageShiftName
            FROM DensoHotSheets hs
            LEFT JOIN DensoTransportMode tm
                ON tm.Id = hs.TransportModeId
            LEFT JOIN DensoShortageShift ss
                ON ss.Id = hs.ShortageShiftId
            WHERE hs.Id IN ({idsString})
        ";

                var registros = (await _hotSheetsDapperRepository
                    .QueryAsync<dynamic>(sql))
                    .ToList();

                if (!registros.Any())
                    throw new UserFriendlyException("No se encontraron registros");

                // ===============================
                // OBTENER CORREOS POR ROL
                // ===============================
                var usuariosAuthors = await UserManager.GetUsersInRoleAsync("Authors");
                //var usuariosAuthors = await UserManager.GetUsersInRoleAsync("IE");
                
                var usuariosPC = await UserManager.GetUsersInRoleAsync("PC");
                var usuariosPQ = await UserManager.GetUsersInRoleAsync("PQ");

                var correosDestino = usuariosAuthors
                    .Where(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
                    .Select(x => x.EmailAddress.Trim())
                    .Concat(
                        usuariosPC
                            .Where(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
                            .Select(x => x.EmailAddress.Trim())
                    )
                    .Concat(
                        usuariosPQ
                            .Where(x => !string.IsNullOrWhiteSpace(x.EmailAddress))
                            .Select(x => x.EmailAddress.Trim())
                    )
                    .Distinct()
                    .ToList();

                if (!correosDestino.Any())
                {
                    throw new UserFriendlyException(
                        "No se encontraron correos configurados para los roles Authors, PC o PQ.");
                }

                // ===============================
                // ARMAR FILAS
                // ===============================
                string rows = "";

                foreach (var item in registros)
                {
                    rows += $@"
                <tr>
                    <td>{item.SupplierCode}</td>
                    <td>{item.SupplierName}</td>
                    <td>{item.PartNumber}</td>
                    <td>{item.PartDescription}</td>
                    <td style='text-align:right'>{item.InTransitQty}</td>
                    <td>{item.TransportModeName}</td>
                    <td>{item.TrafficContainerFX}</td>
                    <td>{item.UnitNumber}</td>
                    <td>{item.EtaDNMX:yyyy-MM-dd}</td>
                    <td>{item.RealShortageDate:yyyy-MM-dd}</td>
                    <td>{item.ShortageShiftName}</td>
                </tr>";
                }

                // ===============================
                // ARMAR BODY
                // ===============================
                string body = $@"
        <html>
        <body style='font-family:Arial'>

            <p>Estimado equipo,</p>

            <p>
                Por medio del presente se informa que los siguientes materiales se encuentran actualmente registrados en la Hot Sheet y requieren seguimiento para su atención y cierre.
            </p>

            <p>
                Favor de revisar el estatus de los registros mostrados a continuación y realizar las acciones correspondientes para su actualización y completado.
            </p>

            <p>
                Puede consultar el detalle completo y dar seguimiento a los registros mediante la siguiente liga:
            </p>

            <p>
                <a href='{urlHotSheet}'
                    style='font-size:14px;
                           font-weight:bold;
                           color:#0d6efd;
                           text-decoration:none;'>
                    Consultar Hot Sheet
                </a>
            </p>

            <div style='overflow-x:auto;'>

                <table border='1'
                       cellspacing='0'
                       cellpadding='5'
                       style='border-collapse:collapse;
                              width:100%;
                              table-layout:auto;
                              font-size:12px;'>

                    <tr style='background:#2f75b5;color:white;'>

                        <th>Supplier Code</th>
                        <th>Supplier</th>
                        <th>Part Number</th>
                        <th>Description</th>
                        <th>In Transit Qty</th>
                        <th>Transport Mode</th>
                        <th>Container FX</th>
                        <th>Unit Number</th>
                        <th>ETA DNMX</th>
                        <th>Shortage Date</th>
                        <th>Shift</th>

                    </tr>

                    {rows}

                </table>

            </div>

            <br/>

            <p>
                Agradecemos su apoyo para dar seguimiento oportuno a estos materiales.
            </p>

            <p>
                Saludos cordiales.
            </p>

        </body>
        </html>";

                // ===============================
                // ENVIAR CORREO
                // ===============================
                var asunto = "Hot Sheet Critical Components Daily Follow Up";

                foreach (var correo in correosDestino)
                {
                    await _emailSender.SendAsync(
                        correo,
                        asunto,
                        body,
                        true
                    );
                }

                // ===============================
                // MARCAR COMO ENVIADO
                // ===============================
                var userId = AbpSession.UserId;
                var user = await UserManager.GetUserByIdAsync(userId.Value);
                var userName = $"{user?.Name} {user?.Surname}";

                await _hotSheetsDapperRepository.ExecuteAsync(
                    $@"
            UPDATE DensoHotSheets
            SET EmailSent = 1,
                EmailSentDate = GETDATE(),
                EmailSentBy = @UserId,
                EmailSentByName = @UserName
            WHERE Id IN ({idsString})
            ",
                    new
                    {
                        UserId = userId,
                        UserName = userName
                    });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error al enviar correos: " + ex.Message);
            }
        }

        // public async Task EnviarNotificacion(NotificacionDto input)
        //{
        //    try
        //    {
        //        if (input == null)
        //            throw new UserFriendlyException("Datos inválidos");

        //        if (input.Correos == null || !input.Correos.Any())
        //            throw new UserFriendlyException("Sin correos");

        //        if (input.Registros == null || !input.Registros.Any())
        //            throw new UserFriendlyException("Sin registros");



        //        // 1. ARMAR HTML
        //        var body = $@"
        //    <html>
        //    <head>
        //        <style>
        //            body {{ font-family: Arial; }}
        //            table {{ border-collapse: collapse; width: 100%; }}
        //            th {{ background:#2f75b5; color:white; padding:8px; }}
        //            td {{ border:1px solid #ddd; padding:8px; }}
        //            tr:nth-child(even) {{ background:#f2f2f2; }}
        //        </style>
        //    </head>
        //    <body>

        //    <h3>Lista de HotSheets  - {input.Planner}</h3>

        //    <table>
        //        <tr>
        //            <th>Folio</th>
        //            <th>Parte</th>
        //            <th>Proveedor</th>
        //        </tr>
        //    ";

        //        foreach (var r in input.Registros)
        //        {
        //            body += $@"
        //        <tr>
        //            <td>{r.Folio}</td>
        //            <td>{r.Parte}</td>
        //            <td>{r.Proveedor}</td>
        //        </tr>";
        //        }

        //        body += "</table></body></html>";

        //        if (input.Correos == null || !input.Correos.Any())
        //        {
        //            throw new UserFriendlyException("No hay correos para enviar");
        //        }

        //        // 2. ENVIAR CORREOS
        //        foreach (var correo in input.Correos)
        //        {
        //            await _emailSender.SendAsync(
        //                correo,
        //                $"HotSheets - {input.Planner}",
        //                body,
        //                true
        //            );
        //        }

        //        // 3. OBTENER IDS
        //        var ids = input.Registros.Select(x => x.Id).ToList();

        //        if (ids.Any())
        //        {
        //            var idsString = string.Join(",", ids);

        //            var userId = AbpSession.UserId;
        //            var user = await UserManager.GetUserByIdAsync(userId.Value);
        //            var userName = user?.Name + " " + user?.Surname;

        //            await _hotSheetsDapperRepository.ExecuteAsync(
        //                $@"UPDATE DensoHotSheets 
        //                    SET EmailSent = 1,
        //                        EmailSentDate = GETDATE(),
        //                        EmailSentBy = @UserId,
        //                        EmailSentByName = @UserName
        //                    WHERE Id IN ({idsString})
        //                    ",
        //                new
        //                {
        //                    UserId = userId,
        //                    UserName = userName
        //                }
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UserFriendlyException("Error al enviar correos: " + ex.Message);
        //    }
        //}


        [HttpPost]
        public async Task<List<HotSheetsItemDto>> GetHotSheets(GetHotSheetInput input)
        {
            string sqlQuery = "EXEC GetHotSheets @UserId, @StatusHS, @StartDate, @EndDate, @TypeRecord";
            var sqlParams = new
            {
                UserId = AbpSession.UserId,
                StatusHS = input.StatusHS,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TypeRecord = input.TypeRecord
            };

            try
            {                
                var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<HotSheetsItemDto>(sqlQuery, sqlParams);

                return itemsDapper.ToList();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<List<HotSheetsItemDto>> GetHotSheetsReports(GetHotSheetInput input)
        {
            string sqlQuery = "EXEC GetHotSheetsReports @UserId, @StatusHS, @StartDate, @EndDate, @TypeRecord";
            var sqlParams = new
            {
                UserId = AbpSession.UserId,
                StatusHS = input.StatusHS,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TypeRecord = input.TypeRecord
            };

            try
            {
                var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<HotSheetsItemDto>(sqlQuery, sqlParams);

                return itemsDapper.ToList();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        //[HttpPost]
        //public async Task<List<ImportExportItemDto>> GetImportExports(GetImportExportInput input)
        //{
        //    string sqlQuery = "EXEC GetImportExports @UserId, @StatusHS, @StartDate, @EndDate";
        //    var sqlParams = new
        //    {
        //        UserId = AbpSession.UserId,
        //        StatusHS = input.StatusHS,
        //        StartDate = input.StartDate,
        //        EndDate = input.EndDate,
        //    };

        //    try
        //    {
        //        var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<ImportExportItemDto>(sqlQuery, sqlParams);

        //        return itemsDapper.ToList();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        [HttpPost]
        public async Task<List<PurchaseOrdersItemDto>> GetPurchaseOrders(GetPurchaseOrdersInput input)
        {
            string sqlQuery = "EXEC GetPurchaseOrders @UserId, @StatusHS, @StartDate, @EndDate";
            var sqlParams = new
            {
                UserId = AbpSession.UserId,
                StatusHS = input.StatusHS,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            try
            {
                var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<PurchaseOrdersItemDto>(sqlQuery, sqlParams);

                return itemsDapper.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<List<StarSheetsItemDto>> GetStarSheets(GetStarSheetInput input)
        {
            string sqlQuery = "EXEC GetStarSheets @UserId, @StartDate, @EndDate";
            var sqlParams = new
            {
                UserId = AbpSession.UserId,                
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            try
            {
                var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<StarSheetsItemDto>(sqlQuery, sqlParams);

                return itemsDapper.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

      

        public async Task<HotSheetsItemDetailDto> GetHotSheetById(long HotSheetId)
        {
            string sqlQuery = "EXEC GetHotSheetById @HotSheetId, @UserId";
            var sqlParams = new
            {
                HotSheetId = HotSheetId,
                UserId = AbpSession.UserId,
            };
            var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<HotSheetsItemDetailDto>(sqlQuery, sqlParams);

            //Nuevo para relacionar archivos.
            var hotSheetFound = itemsDapper.FirstOrDefault();
            if (hotSheetFound != null)
            {
                string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
                var sqlParamsFiles = new
                {
                    EntityType = "HotSheets",
                    EntityIds = HotSheetId.ToString(),
                };

                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);
                hotSheetFound.Files = itemsFilesDapper.ToList();

            }

            return hotSheetFound;
        }

        //public async Task<ImportExportItemDetailDto> GetImportExportById(long ImportExportId)
        //{
        //    string sqlQuery = "EXEC GetImportExportById @ImportExportId, @UserId";
        //    var sqlParams = new
        //    {
        //        ImportExportId = ImportExportId,
        //        UserId = AbpSession.UserId,
        //    };
        //    var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<ImportExportItemDetailDto>(sqlQuery, sqlParams);

        //    //Nuevo para relacionar archivos.
        //    var importExportFound = itemsDapper.FirstOrDefault();
        //    if (importExportFound != null)
        //    {
        //        string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
        //        var sqlParamsFiles = new
        //        {
        //            EntityType = "ImporExport",
        //            EntityIds = ImportExportId.ToString(),
        //        };

        //        var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);
        //        importExportFound.Files = itemsFilesDapper.ToList();

        //    }

        //    return importExportFound;
        //}

        public async Task<PurchaseOrdersItemDto> GetPurchaseOrderById(long PurchaseOrderId)
        {
            string sqlQuery = "EXEC GetPurchaseOrderById @PurchaseOrderId, @UserId";
            var sqlParams = new
            {
                PurchaseOrderId = PurchaseOrderId,
                UserId = AbpSession.UserId,
            };
            var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<PurchaseOrdersItemDto>(sqlQuery, sqlParams);

            //Nuevo para relacionar archivos.
            var hotSheetFound = itemsDapper.FirstOrDefault();            

            return hotSheetFound;
        }

        public async Task<StarSheetsItemDetailDto> GetStarSheetById(long StarSheetId)
        {
            string sqlQuery = "EXEC GetStarSheetById @StarSheetId, @UserId";
            var sqlParams = new
            {
                StarSheetId = StarSheetId,
                UserId = AbpSession.UserId,
            };
            var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<StarSheetsItemDetailDto>(sqlQuery, sqlParams);

            //Nuevo para relacionar archivos.
            var hotSheetFound = itemsDapper.FirstOrDefault();
            if (hotSheetFound != null)
            {
                string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
                var sqlParamsFiles = new
                {
                    EntityType = "StarSheets",
                    EntityIds = StarSheetId.ToString(),
                };

                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);
                hotSheetFound.Files = itemsFilesDapper.ToList();

            }

            return hotSheetFound;
        }

        public async Task<List<FileDto>> GetHotSheetFiles(long HotSheetId)
        {                        
            string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
            var sqlParamsFiles = new
            {
                EntityType = "HotSheet",
                EntityIds = HotSheetId.ToString(),
            };

            try
            {
                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);

                return itemsFilesDapper.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<List<FileDto>> GetImportExportFiles(long ImportExportId)
        //{
        //    string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
        //    var sqlParamsFiles = new
        //    {
        //        EntityType = "ImportExport",
        //        EntityIds = ImportExportId.ToString(),
        //    };

        //    try
        //    {
        //        var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);

        //        return itemsFilesDapper.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task<List<FileDto>> GetStarSheetFiles(long StarSheetId)
        {
            string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
            var sqlParamsFiles = new
            {
                EntityType = "StarSheet",
                EntityIds = StarSheetId.ToString(),
            };

            try
            {
                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);

                return itemsFilesDapper.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<long> UpdateStartSheetToHotSheet(List<StarSheetsItemDto> input)
        {
            long StarSheetId = 0;
            try
            {
                var IdStarsSheetList = string.Join(",", input.Select(i => i.StarSheetId));

                DynamicParameters spParams = new DynamicParameters();
                spParams.Add("@UserId", AbpSession.UserId);
                spParams.Add("@IdStarsSheetList", IdStarsSheetList);

                spParams.Add("@StarSheetIdUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

                long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync("UpdateStartSheetToHotSheet",
                    spParams, commandType: CommandType.StoredProcedure);

                StarSheetId = spParams.Get<long>("@StarSheetIdUpdated");
            }
            catch (Exception ex)
            {

                throw ex;
            }            
            
            return StarSheetId;
        }

        public async Task<long> UpdateStartSheetToIE(List<StarSheetsItemDto> input)
        {
            long StarSheetId = 0;
            try
            {
                var IdStarsSheetList = string.Join(",", input.Select(i => i.StarSheetId));

                DynamicParameters spParams = new DynamicParameters();
                spParams.Add("@UserId", AbpSession.UserId);
                spParams.Add("@IdStarsSheetList", IdStarsSheetList);

                spParams.Add("@StarSheetIdUpdated", 
                    dbType: DbType.Int64, 
                    direction: ParameterDirection.Output);

                long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync(
                    "UpdateStartSheetToIE",
                    spParams, 
                    commandType: CommandType.StoredProcedure);

                StarSheetId = spParams.Get<long>("@StarSheetIdUpdated");

                // AQUÍ VA LA NOTIFICACIÓN
                await NotificarUsuariosIE(input.Count, IdStarsSheetList);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return StarSheetId;
        }

        public async Task NotificarUsuariosIE(int totalRegistros, string ids)
        {
            var urlIE = _appConfiguration["App:ClientRootAddress"] + "app/importExports";

            try
            {
                // ===============================
                // 1. CONSULTAR REGISTROS ENVIADOS
                // ===============================
                string sqlDetalle = $@"
                    SELECT
                        plannerName,
                        supplierCode,
                        supplierName,
                        partNumber,
                        partDescription,
                        inTransitQty
                    FROM DensoStarSheets
                    WHERE Id IN ({ids})
                ";

                var registros = await _hotSheetsDapperRepository.QueryAsync<dynamic>(sqlDetalle);

                // ===============================
                // 2. ARMAR FILAS HTML
                // ===============================
                string rows = "";

                foreach (var item in registros)
                {
                    rows += $@"
                    <tr>
                        <td>{item.plannerName}</td>
                        <td>{item.supplierCode}</td>
                        <td>{item.supplierName}</td>
                        <td>{item.partNumber}</td>
                        <td>{item.partDescription}</td>
                        <td style='text-align:right'>{item.inTransitQty}</td>
                    </tr>";
                }

                // ===============================
                // 3. BODY CORREO
                // ===============================
                string body = $@"
                                    <html>
                                    <body style='font-family:Arial;'>

                                    <p>Estimado equipo de Import / Export,</p>

                                    <p>
                                        Solicitamos su apoyo para dar seguimiento al proceso de importación de los siguientes materiales,
                                        los cuales actualmente se encuentran incluidos en la Hot Sheet para monitoreo prioritario.
                                    </p>

                                    <p>
                                        A continuación se muestra el detalle de los registros asignados:
                                    </p>

                                    <table border='1' cellspacing='0' cellpadding='5'
                                           style='border-collapse:collapse;font-family:Arial;font-size:12px;width:100%;'>

                                        <tr style='background:#d32f2f;color:white;'>
                                            <th>Planner</th>
                                            <th>Supplier Code</th>
                                            <th>Supplier</th>
                                            <th>Part Number</th>
                                            <th>Description</th>
                                                <th>Qty</th>
                                            </tr>

                                            {rows}

                                        </table>

                                        <br/>

                                        <p>
                                            Agradecemos su apoyo para revisar el estatus correspondiente y dar seguimiento oportuno a cada caso.
                                        </p>

                                        <p>
                                            Para consultar el detalle y dar seguimiento a los registros, favor de ingresar al sistema mediante la siguiente liga:
                                        </p>

                                        <p>
                                            <a href='{urlIE}'
                                               style='font-size:14px;font-weight:bold;color:#0d6efd;'>
                                                Acceder al módulo Import / Export
                                            </a>
                                        </p>

                                        <p>Saludos cordiales.</p>

                                        </body>
                                        </html>";

                // ===============================
                // 4. CORREOS ROL IE
                // ===============================
                string sqlCorreos = @"
                    SELECT DISTINCT u.EmailAddress
                    FROM AbpUsers u
                    INNER JOIN AbpUserRoles ur ON ur.UserId = u.Id
                    INNER JOIN AbpRoles r ON r.Id = ur.RoleId
                    WHERE r.Name = 'IE'
                    AND u.IsDeleted = 0
                    AND ISNULL(u.EmailAddress,'') <> ''
                ";

                var correos = await _hotSheetsDapperRepository.QueryAsync<string>(sqlCorreos);

                // ===============================
                // 5. ENVIAR CORREOS
                // ===============================
                foreach (var correo in correos)
                {
                    await _emailSender.SendAsync(
                        correo,                        
                        "I/E HotSheet - Nuevos registros asignados",
                        body,
                        true
                    );
                }
            }
            catch
            {
                // opcional no romper proceso principal
            }
        }

        public async Task<long> UpdateTypeColor(List<HotSheetColorDto> input)
        {
            long RowsUpdated = 0;
            try
            {
                var IdList = string.Join(",", input.Select(i => i.Id));

                DynamicParameters spParams = new DynamicParameters();
                spParams.Add("@UserId", AbpSession.UserId);
                spParams.Add("@Ids", IdList);
                spParams.Add("@TypeRecord", input.FirstOrDefault().TypeRecord);
                spParams.Add("@TypeColor", input.FirstOrDefault().TypeColor);
                

                spParams.Add("@RowsUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

                long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync("UpdateTypeColor",
                    spParams, commandType: CommandType.StoredProcedure);

                RowsUpdated = spParams.Get<long>("@RowsUpdated");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RowsUpdated;
        }

      
            public async Task CreateOrUpdateHotSheet(HotSheetsDto input)
            {
                if (input.Id.HasValue)
                {
                    try
                    {
                        var hotSheet = await _hotSheetsRepository.GetAsync(input.Id.Value);
                        if (hotSheet != null)
                        {
                            if (input.TransportModeId != 0) {
                                hotSheet.TransportModeId = input.TransportModeId;
                            }
              
                            hotSheet.DeliveryOrder = input.DeliveryOrder;
                            hotSheet.TrafficContainerFX = input.TrafficContainerFX;
                            hotSheet.UnitNumber = input.UnitNumber;
                            hotSheet.EtaDNMX = input.EtaDNMX;
                            if (input.ShortageShiftId != 0)
                            {
                                hotSheet.ShortageShiftId = input.ShortageShiftId;
                            }

                            if (input.StatusId != 0)
                            {
                                hotSheet.StatusId = input.StatusId;
                            }

                            hotSheet.PCComments = input.PCComments;
                            hotSheet.RealShortageDate = input.RealShortageDate;
                            hotSheet.Shortage = input.Shortage;

                            hotSheet.TypeColor = input.TypeColor;

                            hotSheet.ShortageShift = null;
                            hotSheet.TransportMode = null;
                            hotSheet.StatusHotSheet = null;

                            if (hotSheet.TypeRecord == "IE")
                            {
                                hotSheet.LocationStatus = input.LocationStatus;
                            }


                            hotSheet.CompletedManually = input.CompletedManually;
                            if (input.CompletedManually == 1 && hotSheet.TypeRecord == "IE") {
                                hotSheet.CompletedManually = 0;
                                hotSheet.TypeRecord = "HS";                            
                            }

                            if (input.CompletedManually == 1)
                            {
                                // enviar correo
                            }


                            await _hotSheetsRepository.UpdateAsync(hotSheet);

                        // ===============================
                        // ENVIAR CORREO A PLANEADORES
                        // ===============================
                        if (input.CompletedManually == 1)
                        {
                            await NotificarPlaneadoresHotSheet(new List<long> { hotSheet.Id });
                        }

                    }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }            
            }

        public async Task NotificarPlaneadoresHotSheet(List<long> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    return;

                var urlReporte = _appConfiguration["App:ClientRootAddress"] +
                                 "app/reports/report-hot-sheets";

                var idsString = string.Join(",", ids);

                // ===============================
                // 1. OBTENER REGISTROS
                // ===============================
                string sql = $@"
                        SELECT
                            hs.Id,
                            hs.PlannerName,
                            hs.SupplierCode,
                            hs.SupplierName,
                            hs.PartNumber,
                            hs.PartDescription,
                            hs.InTransitQty,
                            ISNULL(tm.Description, '') AS TransportModeName,
                            ISNULL(hs.TrafficContainerFX, '') AS TrafficContainerFX,
                            ISNULL(hs.UnitNumber, '') AS UnitNumber,
                            hs.EtaDNMX,
                            hs.RealShortageDate,
                            ISNULL(ss.Description, '') AS ShortageShiftName
                        FROM DensoHotSheets hs
                        LEFT JOIN DensoTransportMode tm
                            ON tm.Id = hs.TransportModeId
                        LEFT JOIN DensoShortageShift ss
                            ON ss.Id = hs.ShortageShiftId
                        WHERE hs.Id IN ({idsString})
                    ";

                var registros = (await _hotSheetsDapperRepository
                    .QueryAsync<dynamic>(sql))
                    .ToList();

                if (!registros.Any())
                    return;

                // ===============================
                // 2. AGRUPAR POR PLANNER
                // ===============================
                var grupos = registros.GroupBy(x => (string)x.PlannerName);

                foreach (var grupo in grupos)
                {
                    var planner = grupo.Key;

                    // ===============================
                    // 3. OBTENER CORREOS DEL PLANEADOR
                    // ===============================
                    string sqlCorreos = @"
                            SELECT Correo
                            FROM PlaneadorCorreos
                            WHERE NombrePlaneador = @Nombre
                        ";

                    var correos = (await _hotSheetsDapperRepository
                        .QueryAsync<string>(
                            sqlCorreos,
                            new { Nombre = planner }))
                        .ToList();


                    // ===============================
                    // 3.1 OBTENER CORREOS ROLES PROD Y PQ
                    // ===============================
                    string sqlCorreosRoles = @"
                                                SELECT DISTINCT u.EmailAddress
                                                FROM AbpUsers u
                                                INNER JOIN AbpUserRoles ur ON ur.UserId = u.Id
                                                INNER JOIN AbpRoles r ON r.Id = ur.RoleId
                                                WHERE r.Name IN ('PROD', 'PQ')
                                                  AND u.IsDeleted = 0
                                                  AND ISNULL(u.EmailAddress,'') <> ''
                                            ";

                    var correosRoles = (await _hotSheetsDapperRepository
                        .QueryAsync<string>(sqlCorreosRoles))
                        .ToList();


                    // ===============================
                    // 3.2 UNIR Y ELIMINAR DUPLICADOS
                    // ===============================
                    correos.AddRange(correosRoles);

                    correos = correos
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    if (!correos.Any())
                        continue;

                    // ===============================
                    // 4. ARMAR FILAS
                    // ===============================
                    string rows = "";

                    foreach (var item in grupo)
                    {
                        rows += $@"
                        <tr>
                            <td>{item.SupplierCode}</td>
                            <td>{item.SupplierName}</td>
                            <td>{item.PartNumber}</td>
                            <td>{item.PartDescription}</td>
                            <td style='text-align:right'>{item.InTransitQty}</td>
                            <td>{item.TransportModeName}</td>
                            <td>{item.TrafficContainerFX}</td>
                            <td>{item.UnitNumber}</td>
                            <td>{item.EtaDNMX}</td>
                            <td>{item.RealShortageDate}</td>
                            <td>{item.ShortageShiftName}</td>
                        </tr>";
                    }

                    // ===============================
                    // 5. ARMAR BODY
                    // ===============================
                    string body = $@"
            <html>
            <body style='font-family:Arial'>

                <p>Estimado Planeador,</p>

                <p>
                    Por medio del presente se informa que los siguientes materiales fueron recibidos en almacén y ya se encuentran reflejados en inventario.
                </p>

                <p>
                    Puede consultar el reporte completo en la siguiente liga:
                </p>

                <p>
                    <a href='{urlReporte}'
                       style='font-size:14px;
                              font-weight:bold;
                              color:#0d6efd;
                              text-decoration:none;'>
                        Consultar Reporte Hot Sheet
                    </a>
                </p>

                <div style='overflow-x:auto;'>

                    <table border='1'
                           cellspacing='0'
                           cellpadding='5'
                           style='border-collapse:collapse;
                                  width:100%;
                                  table-layout:auto;
                                  font-size:12px;'>

                        <tr style='background:#2f75b5;color:white;'>

                            <th style='white-space:nowrap;'>Supplier Code</th>
                            <th style='white-space:nowrap;'>Supplier</th>
                            <th style='white-space:nowrap;'>Part Number</th>
                            <th style='white-space:nowrap;'>Description</th>
                            <th style='white-space:nowrap;'>In Transit Qty</th>
                            <th style='white-space:nowrap;'>Transport Mode</th>
                            <th style='white-space:nowrap;'>Container FX</th>
                            <th style='white-space:nowrap;'>Unit Number</th>
                            <th style='white-space:nowrap;'>ETA DNMX</th>
                            <th style='white-space:nowrap;'>Shortage Date</th>
                            <th style='white-space:nowrap;'>Shift</th>

                        </tr>

                        {rows}

                    </table>

                </div>

                <br/>

                <p>
                    Saludos cordiales.
                </p>

            </body>
            </html>";

                    // ===============================
                    // 6. ENVIAR CORREO
                    // ===============================
                    foreach (var correo in correos)
                    {
                        await _emailSender.SendAsync(
                            correo,
                            "Notificación de Recepción de Material - Hot Sheet",
                            body,
                            true
                        );
                    }
                }

                // ===============================
                // 7. MARCAR COMO ENVIADO
                // ===============================
                await _hotSheetsDapperRepository.ExecuteAsync($@"
            UPDATE DensoHotSheets
            SET EmailSent = 1,
                EmailSentDate = GETDATE(),
                EmailSentBy = @UserId
            WHERE Id IN ({idsString})
        ",
                new
                {
                    UserId = AbpSession.UserId
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Error NotificarPlaneadoresHotSheet", ex);
            }
        }

        //public async Task CreateOrUpdateImportExport(ImportExportDto input)
        //{
        //    if (input.Id.HasValue)
        //    {
        //        try
        //        {
        //            var importExport = await _hotSheetsRepository.GetAsync(input.Id.Value);
        //            if (importExport != null)
        //            {
        //                if (input.TransportModeId != 0)
        //                {
        //                    importExport.TransportModeId = input.TransportModeId;
        //                }

        //                importExport.DeliveryOrder = input.DeliveryOrder;
        //                importExport.TrafficContainerFX = input.TrafficContainerFX;
        //                importExport.UnitNumber = input.UnitNumber;
        //                importExport.EtaDNMX = input.EtaDNMX;
        //                if (input.ShortageShiftId != 0)
        //                {
        //                    importExport.ShortageShiftId = input.ShortageShiftId;
        //                }

        //                if (input.StatusId != 0)
        //                {
        //                    importExport.StatusId = input.StatusId;
        //                }

        //                importExport.PCComments = input.PCComments;
        //                importExport.RealShortageDate = input.RealShortageDate;
        //                importExport.Shortage = input.Shortage;



        //                importExport.ShortageShift = null;
        //                importExport.TransportMode = null;
        //                importExport.StatusHotSheet = null;

        //                importExport.CompletedManually = input.CompletedManually;

        //                await _hotSheetsRepository.UpdateAsync(importExport);
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}

        public async Task CreateOrUpdatePurchaseOrder(PurchaseOrdersDto input)
        {
            if (input.Id.HasValue)
            {
                try
                {
                    var purchaseOrder = await _purchaseOrdersRepository.GetAsync(input.Id.Value);
                    if (purchaseOrder != null)
                    {
                        purchaseOrder.PlannerCode = input.PlannerCode;
                        purchaseOrder.PlannerName = input.PlannerName;
                        purchaseOrder.PurchaseOrder = input.PurchaseOrder;
                        purchaseOrder.Line = input.Line;
                        purchaseOrder.PartNumber = input.PartNumber;
                        purchaseOrder.PartDescription = input.PartDescription;
                        purchaseOrder.SupplierCode = input.SupplierCode;
                        purchaseOrder.SupplierName = input.SupplierName;
                        purchaseOrder.Qty = input.Qty;
                        purchaseOrder.RequiredDate = input.RequiredDate;
                        purchaseOrder.StatusId = input.StatusId;
                        purchaseOrder.Ticket = input.Ticket;

                        await _purchaseOrdersRepository.UpdateAsync(purchaseOrder);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else {

                try
                {
                    var purchaseOrder = new PurchaseOrders();
                    purchaseOrder.PlannerCode = input.PlannerCode;
                    purchaseOrder.PlannerName = input.PlannerName;
                    purchaseOrder.PurchaseOrder = input.PurchaseOrder;
                    purchaseOrder.Line = input.Line;
                    purchaseOrder.PartNumber = input.PartNumber;
                    purchaseOrder.PartDescription = input.PartDescription;
                    purchaseOrder.SupplierCode = input.SupplierCode;
                    purchaseOrder.SupplierName = input.SupplierName;
                    purchaseOrder.Qty = input.Qty;
                    purchaseOrder.RequiredDate = input.RequiredDate;
                    purchaseOrder.StatusId = input.StatusId;
                    purchaseOrder.Ticket = input.Ticket;

                    await _purchaseOrdersRepository.InsertAsync(purchaseOrder);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        public async Task DeletePurchaseOrder(long purchaseOrderId)
        {
            var item = await _purchaseOrdersRepository.GetAsync(purchaseOrderId);
            if (item != null)
            {
                await _purchaseOrdersRepository.DeleteAsync(item);
            }
        }


        public async Task CreateOrUpdateStarSheet(StarSheetsDto input)
        {
            if (input.Id.HasValue)
            {
                try
                {
                    var starSheet = await _hotSheetsRepository.GetAsync(input.Id.Value);
                    if (starSheet != null)
                    {
                        //starSheet.TransportModeId = input.TransportModeId;
                        //starSheet.DeliveryOrder = input.DeliveryOrder;
                        //starSheet.TrafficContainerFX = input.TrafficContainerFX;
                        //starSheet.UnitNumber = input.UnitNumber;
                        //starSheet.EtaDNMX = input.EtaDNMX;
                        //starSheet.ShortageShiftId = input.ShortageShiftId;
                        starSheet.PCComments = input.PCComments;
                        //starSheet.RealShortageDate = input.RealShortageDate;
                        //starSheet.Shortage = input.Shortage;

                        starSheet.ShortageShift = null;
                        starSheet.TransportMode = null;

                        await _hotSheetsRepository.UpdateAsync(starSheet);

                        //await _hotSheetsRepository.InsertAsync(starSheet);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
        }

        public async Task<List<HotSheetsCommetsDto>> GetHotSheetComments(long HotSheetId)
        {

            string sqlQueryFiles = "EXEC GetHotSheetComments @HotSheetId";
            var sqlParamsFiles = new
            {                
                HotSheetId = HotSheetId.ToString(),
            };

            try
            {
                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<HotSheetsCommetsDto>(sqlQueryFiles, sqlParamsFiles);

                return itemsFilesDapper.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<List<ImportExportCommetsDto>> GetImportExportComments(long ImportExportId)
        //{

        //    string sqlQueryFiles = "EXEC GetImportExportComments @ImportExportId";
        //    var sqlParamsFiles = new
        //    {
        //        ImportExportId = ImportExportId.ToString(),
        //    };

        //    try
        //    {
        //        var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<ImportExportCommetsDto>(sqlQueryFiles, sqlParamsFiles);

        //        return itemsFilesDapper.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task<List<StarSheetsCommetsDto>> GetStarSheetComments(long HotSheetId)
        {

            string sqlQueryFiles = "EXEC GetStarSheetComments @StarSheetId";
            var sqlParamsFiles = new
            {
                StarSheetId = HotSheetId.ToString(),
            };

            try
            {
                var itemsFilesDapper = await _hotSheetsDapperRepository.QueryAsync<StarSheetsCommetsDto>(sqlQueryFiles, sqlParamsFiles);

                return itemsFilesDapper.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<HotSheetsCommetsDto> CreateOrUpdateHotSheetComments(HotSheetsCommetsDto input)
        {            
            DynamicParameters spParams = new DynamicParameters();
            spParams.Add("@UserId", AbpSession.UserId);            
            spParams.Add("@HotSheetId", input.HotSheetId);
            spParams.Add("@DepartmentId", input.DepartmentId);
            spParams.Add("@Comments", input.Comments);            

            spParams.Add("@HotSheeCommentIdUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

            long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync("HotSheetCommentCreate",
                spParams, commandType: CommandType.StoredProcedure);

            var HotSheetCommentId = spParams.Get<long>("@HotSheeCommentIdUpdated");

            input.Id = HotSheetCommentId;

            return input;            
        }

        //public async Task<ImportExportCommetsDto> CreateOrUpdateImportExportComments(ImportExportCommetsDto input)
        //{
        //    DynamicParameters spParams = new DynamicParameters();
        //    spParams.Add("@UserId", AbpSession.UserId);
        //    spParams.Add("@ImportExportId", input.ImportExportId);
        //    spParams.Add("@DepartmentId", input.DepartmentId);
        //    spParams.Add("@Comments", input.Comments);

        //    spParams.Add("@ImportExportCommentIdUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

        //    long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync("ImportExportCommentCreate",
        //        spParams, commandType: CommandType.StoredProcedure);

        //    var ImportExportCommentId = spParams.Get<long>("@ImportExportCommentIdUpdated");

        //    input.Id = ImportExportCommentId;

        //    return input;
        //}


        public async Task<StarSheetsCommetsDto> CreateOrUpdateStarSheetComments(StarSheetsCommetsDto input)
        {
            DynamicParameters spParams = new DynamicParameters();
            spParams.Add("@UserId", AbpSession.UserId);
            spParams.Add("@StarSheetId", input.StarSheetId);
            spParams.Add("@DepartmentId", input.DepartmentId);
            spParams.Add("@Comments", input.Comments);

            spParams.Add("@StarSheeCommentIdUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

            long affectedRows = await _hotSheetsDapperRepository.ExecuteAsync("StarSheetCommentCreate",
                spParams, commandType: CommandType.StoredProcedure);

            var StarSheetCommentId = spParams.Get<long>("@StarSheeCommentIdUpdated");

            input.Id = StarSheetCommentId;

            return input;
        }

       

        //[HttpPost]
        //public async Task<List<HotSheetShipItemDto>> GetHotSheetShip(GetHotSheeShiptInput input)
        //{
        //    string sqlQuery = "EXEC GetHotSheet @UserId, @PlantId, @StatusId, @IsTemplate";

        //    int? IsTemplateValue = input.IsTemplate.HasValue ? Convert.ToInt32(input.IsTemplate.Value) : null;

        //    var sqlParams = new { 
        //        UserId = AbpSession.UserId,
        //        PlantId = input.PlantId,
        //        StatusId = input.StatusId,
        //        IsTemplate = IsTemplateValue
        //    };

        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<HotSheetShipItemDto>(sqlQuery, sqlParams);
        //    return itemsDapper.ToList();
        //}

        //[HttpPost]
        //public async Task<List<HotSheetShipItemDto>> GetHotSheetShipReports(GetHotSheeShiptInput input)
        //{
        //    string sqlQuery = @"EXEC GetHotSheetReports
        //        @PlantId, @DepartmentId, @AuthorId, @CarrierId, @ServiceId, @CustomerId, @HotSheetTermId, @StatusId, @StartDate, @EndDate";

        //    var sqlParams = new
        //    {
        //        PlantId = input.PlantId,
        //        DepartmentId = input.DepartmentId,
        //        AuthorId = input.AuthorId,
        //        CarrierId = input.CarrierId,
        //        ServiceId = input.ServiceId,
        //        CustomerId = input.CustomerId,
        //        HotSheetTermId = input.HotSheetTermId,
        //        StatusId = input.StatusId,
        //        StartDate = input.StartDate,
        //        EndDate = input.EndDate,
        //    };

        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<HotSheetShipItemDto>(sqlQuery, sqlParams);
        //    return itemsDapper.ToList();
        //}

        //public async Task<HotSheetShipInfoDto> GetHotSheetShipByIdInfo(long HotSheetShiptId)
        //{
        //    var item = await _HotSheetShipRepository.GetAll()
        //        .Include(c => c.Plant)
        //        .FirstOrDefaultAsync(c => c.Id == HotSheetShiptId);

        //    var itemDto = ObjectMapper.Map<HotSheetShipInfoDto>(item);
        //    return itemDto;
        //}

        //public async Task<HotSheetShipInfoDto> GetHotSheetShipByIdEncrypted(string idEncrypted)
        //{
        //    string HotSheetShiptIdDencrypted = SimpleStringCipher.Instance.Decrypt(idEncrypted);

        //    var item = await _HotSheetShipRepository.GetAll()
        //        .Include(c => c.Plant)                
        //        .FirstOrDefaultAsync(c => c.Id == int.Parse(HotSheetShiptIdDencrypted));

        //    var itemDto = ObjectMapper.Map<HotSheetShipInfoDto>(item);
        //    return itemDto;
        //}

        //public async Task<HotSheetShipItemDto> GetHotSheetShipById(long HotSheetShiptId)
        //{
        //    string sqlQuery = "EXEC GetHotSheetById @HotSheetShiptId, @UserId";            
        //    var sqlParams = new
        //    {
        //        HotSheetShiptId = HotSheetShiptId,
        //        UserId = AbpSession.UserId,
        //    };
        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<HotSheetShipItemDto>(sqlQuery, sqlParams);

        //    var shippingFound = itemsDapper.FirstOrDefault();
        //    if (shippingFound != null)
        //    {
        //        var productsByShipping = _HotSheetShipProductRepository.GetAll()
        //            .Include(i => i.UnitMeasure)
        //            .Include(i2 =>i2.OriginCountry)
        //            //.AsSplitQuery()
        //            .Where(c => c.HotSheetShiptId == HotSheetShiptId)
        //            .ToList();

        //        var partNumberIdsByProds = productsByShipping.Select(s => s.PartNumberId).ToArray();

        //        var partNumbersByProds = await _partNumberRepository.GetAllListAsync(c => partNumberIdsByProds.Contains(c.Id));
        //        var productIds = productsByShipping.Select(s => s.Id).ToList();

        //        var partNumberInternalIdsByProds = productsByShipping.Select(s => s.PartNumberInternalId).ToArray();
        //        var partNumbersInternalByProds = await _partNumberInternalRepository.GetAllListAsync(c => partNumberInternalIdsByProds.Contains(c.Id));

        //        string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
        //        var sqlParamsFiles = new
        //        {
        //            EntityType = "HotSheet",
        //            EntityIds = HotSheetShiptId.ToString(),
        //        };
        //        var itemsFilesDapper = await _HotSheetShipDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);
        //        shippingFound.Files = itemsFilesDapper.ToList();

        //        var sqlParamsFilesProds = new
        //        {
        //            EntityType = "HotSheetShipProducts",
        //            EntityIds = string.Join(",", productIds.ToArray()),
        //        };
        //        var HotSheetShipProductsFiles = await _HotSheetShipDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFilesProds);

        //        shippingFound.Products = ObjectMapper.Map<List<HotSheetShipProductDto>>(productsByShipping);
        //        shippingFound.Products.ForEach(p =>
        //        {
        //            var partNumberInfo = partNumbersByProds.FirstOrDefault(part => part.Id == p.PartNumberId);                    
        //            if (partNumberInfo != null)
        //            {
        //                p.PartNumber = ObjectMapper.Map<PartNumberDto>(partNumberInfo);
        //                p.PartNumberText = partNumberInfo.Number;
        //            }

        //            var partNumberInternalInfo = partNumbersInternalByProds.FirstOrDefault(part => part.Id == p.PartNumberInternalId);
        //            if (partNumberInternalInfo != null)
        //            {
        //                p.PartNumberInternal = ObjectMapper.Map<PartNumberInternalDto>(partNumberInternalInfo);
        //                p.PartNumberText = partNumberInternalInfo.Number;
        //            }

        //            p.Files = ObjectMapper.Map<List<FileDto>>(HotSheetShipProductsFiles.Where(f => f.EntityId == p.Id).ToList());
        //        });

        //        var packingByShipping = await _HotSheetShipPackagingRepository.GetAll()
        //            .Include(i => i.Packaging)
        //            .Where(c => c.HotSheetShiptId == HotSheetShiptId)
        //            .ToListAsync();

        //        shippingFound.Packaging = ObjectMapper.Map<List<HotSheetShipPackagingDto>>(packingByShipping);

        //        EntityDto<long> userInput = new EntityDto<long>(shippingFound.CreatorUserId);
        //        var creatorUserInfo = await _userAppService.GetAsync(userInput);
        //        if(creatorUserInfo != null)
        //        {
        //            shippingFound.Creator = creatorUserInfo;
        //        }

        //        shippingFound.History = await _hotSheetHistoryManager.GetAllAsync(HotSheetShiptId);

        //        //if (shippingFound.CustomerPlantContactId.HasValue)
        //        //{
        //        //    var customerPlantContactInfo = await _customerPlantContactRepository.GetAsync(shippingFound.CustomerPlantContactId.Value);
        //        //    shippingFound.CustomerPlantContact = ObjectMapper.Map<CustomerPlantContactDto>(customerPlantContactInfo);
        //        //}

        //        var mianifestsByShipping = await _HotSheetShipManifestRepository.GetAll()
        //            .Where(c => c.HotSheetShiptId == HotSheetShiptId)
        //            .ToListAsync();
        //        shippingFound.Manifests = ObjectMapper.Map<List<HotSheetShipManifestDto>>(mianifestsByShipping);
        //    }
        //    return shippingFound;
        //}

        //public async Task<List<FileDto>> GetHotSheetShipFiles(long HotSheetShiptId)
        //{
        //    string sqlQueryFiles = "EXEC GetFiles @EntityType, @EntityIds";
        //    var sqlParamsFiles = new
        //    {
        //        EntityType = "HotSheet",
        //        EntityIds = HotSheetShiptId.ToString(),
        //    };
        //    var itemsFilesDapper = await _HotSheetShipDapperRepository.QueryAsync<FileDto>(sqlQueryFiles, sqlParamsFiles);

        //    return itemsFilesDapper.ToList();
        //}

        //public async Task<List<HotSheetShipItemDto>> GetHotSheetShipPendingForApproval()
        //{
        //    string sqlQuery = "EXEC GetHotSheetShipPendingForApproval @UserId";

        //    var sqlParams = new
        //    {
        //        UserId = AbpSession.UserId
        //    };

        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<HotSheetShipItemDto>(sqlQuery, sqlParams);
        //    return itemsDapper.ToList();
        //}

        //public async Task<List<HotSheetShipDto>> GetHotSheetShipOld(GetHotSheeShiptInput input)
        //{
        //    var items = await _HotSheetShipRepository.GetAllListAsync();
        //    var itemsDto = ObjectMapper.Map<List<HotSheetShipDto>>(items);           
        //    return itemsDto;
        //}

        //public async Task<HotSheetShipDto> GetHotSheetShipByIdOld(long HotSheetShiptId)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(HotSheetShiptId);
        //    var itemDto = ObjectMapper.Map<HotSheetShipDto>(item);
        //    return itemDto;
        //}

        //public async Task<HotSheetShipDto> CreateOrUpdateHotSheetShip(HotSheetShipDto input)
        //{
        //    DynamicParameters spParams = new DynamicParameters();
        //    spParams.Add("@UserId", AbpSession.UserId);
        //    spParams.Add("@TenantId", AbpSession.TenantId);
        //    spParams.Add("@HotSheetShiptId", input.Id);
        //    spParams.Add("@DocumentTypeId", input.DocumentTypeId);
        //    spParams.Add("@CarrierId", input.CarrierId);
        //    spParams.Add("@ServiceId", input.ServiceId);
        //    spParams.Add("@HotSheetReasonId", input.HotSheetReasonId);
        //    spParams.Add("@PaymentTermId", input.PaymentTermId);
        //    spParams.Add("@AdditionalExplanation", input.AdditionalExplanation);
        //    spParams.Add("@PlantId", input.PlantId);
        //    spParams.Add("@CustomerId", input.CustomerId);
        //    spParams.Add("@CustomerPlantId", input.CustomerPlantId);
        //    spParams.Add("@CustomerPlantContactId", input.CustomerPlantContactId);
        //    spParams.Add("@HotSheetTermId", input.HotSheetTermId);
        //    spParams.Add("@RmaAssignmentId", input.RmaAssignmentId);
        //    spParams.Add("@OtherBy", input.OtherBy);
        //    spParams.Add("@RmaNumber", input.RMANumber);
        //    spParams.Add("@BNotice", input.BNotice);
        //    spParams.Add("@AccountNumber", input.AccountNumber);
        //    spParams.Add("@CostPaidById", input.CostPaidById);
        //    spParams.Add("@FreightPaidById", input.FreightPaidById);
        //    spParams.Add("@Currency", input.Currency);
        //    spParams.Add("@DepartmentId", input.DepartmentId);                        
        //    spParams.Add("@IEStaffId", input.IEStaffId);
        //    spParams.Add("@ManagerApprovalId", input.ManagerApprovalId);
        //    spParams.Add("@AccountingApprovalId", input.AccountingApprovalId);
        //    spParams.Add("@IsTemplate", input.IsTemplate);
        //    spParams.Add("@TemplateName", input.TemplateName);
        //    spParams.Add("@TemplateDescription", input.TemplateDescription);
        //    spParams.Add("@SpecialExpeditedReasonId", input.SpecialExpeditedReasonId);
        //    spParams.Add("@ShowBehalfFields", input.ShowBehalfFields);
        //    spParams.Add("@TelephoneExt", input.TelephoneExt);
        //    spParams.Add("@FreightPaidByDepartmentId", input.FreightPaidByDepartmentId);
        //    spParams.Add("@FreightPaidByOther", input.FreightPaidByOther);
        //    spParams.Add("@FreightPrePaidExplanation", input.FreightPrePaidExplanation);
        //    spParams.Add("@ContactName", input.CustomerPlantContact?.ContactName);
        //    spParams.Add("@PhoneNumber", input.CustomerPlantContact?.PhoneNumber);
        //    spParams.Add("@DepartmentOrSection", input.CustomerPlantContact?.DepartmentOrSection);
        //    spParams.Add("@NetNumber", input.CustomerPlantContact?.NetNumber);
        //    spParams.Add("@OnBehalfOfUserId", input.OnBehalfOfUserId);
        //    spParams.Add("@OnBehalfOfDeptoId", input.OnBehalfOfDeptoId);
        //    spParams.Add("@OnBehalfOfExt", input.OnBehalfOfExt);

        //    spParams.Add("@GuideReference", input.GuideReference);
        //    spParams.Add("@GuideStatusDetail", input.GuideStatusDetail);
        //    spParams.Add("@GuideStatus", input.GuideStatus);
        //    spParams.Add("@GuideCost", input.GuideCost);
        //    spParams.Add("@GuideCurrency", input.GuideCurrency);

        //    spParams.Add("@HotSheetShiptIdUpdated", dbType: DbType.Int64, direction: ParameterDirection.Output);

        //    long affectedRows = await _HotSheetShipDapperRepository.ExecuteAsync("HotSheetCreateOrUpdate",
        //        spParams, commandType: CommandType.StoredProcedure);

        //    var HotSheetShiptId = spParams.Get<long>("@HotSheetShiptIdUpdated");

        //    if (HotSheetShiptId > 0)
        //    {
        //        if (input.Products.Count > 0)
        //        {
        //            foreach (var product in input.Products)
        //            {
        //                var productToSave = ObjectMapper.Map<HotSheetShipProduct>(product);
        //                if (productToSave.Id > 0)
        //                {
        //                    var productToUpdate = await _HotSheetShipProductRepository.GetAsync(productToSave.Id);
        //                    if (productToUpdate != null)
        //                    {
        //                        productToUpdate.PartNumberId = product.PartNumberId;
        //                        productToUpdate.PartNumberInternalId = product.PartNumberInternalId;
        //                        productToUpdate.UnitMeasureId = product.UnitMeasureId;
        //                        productToUpdate.Description = product.Description;
        //                        productToUpdate.DescriptionSpanish = product.DescriptionSpanish;
        //                        productToUpdate.ProductCodeSATId = product.ProductCodeSATId;
        //                        productToUpdate.Quantity = product.Quantity;
        //                        productToUpdate.UnitPrice = product.UnitPrice;
        //                        productToUpdate.Total = product.Total;
        //                        productToUpdate.Model = product.Model;
        //                        productToUpdate.Serial = product.Serial;
        //                        productToUpdate.Maker = product.Maker;
        //                        productToUpdate.TechInfo = product.TechInfo;
        //                        productToUpdate.PoNumber = product.PoNumber;
        //                        productToUpdate.OriginCountryId = product.OriginCountryId;

        //                        await _HotSheetShipProductRepository.UpdateAsync(productToUpdate);
        //                    }
        //                }
        //                else
        //                {
        //                    productToSave.HotSheetShiptId = HotSheetShiptId;
        //                    productToSave.UnitMeasure = null;
        //                    productToSave.OriginCountry = null;
        //                    productToSave.ProductCodeSAT = null;

        //                    product.Id = await _HotSheetShipProductRepository.InsertAndGetIdAsync(productToSave);
        //                }
        //            };
        //        }

        //        List<HotSheetShipProduct> currentShippingProds = await _HotSheetShipProductRepository.GetAllListAsync(c => c.HotSheetShiptId == input.Id);
        //        List<long> shippingProdIds = input.Products.Select(s => s.Id.Value).ToList();
        //        List<long> shippingProdIdsDeleted = currentShippingProds.Where(prod => !shippingProdIds.Contains(prod.Id)).ToList()
        //                    .Select(s => s.Id).ToList();
        //        _HotSheetShipProductRepository.Delete(c => shippingProdIdsDeleted.Contains(c.Id));

        //        if (input.Packaging.Count > 0)
        //        {
        //            foreach (var packagingItem in input.Packaging)
        //            {
        //                var itemToSave = ObjectMapper.Map<HotSheetShipPackaging>(packagingItem);
        //                if (itemToSave.Id > 0)
        //                {
        //                    var itemToUpdate = await _HotSheetShipPackagingRepository.GetAsync(itemToSave.Id);
        //                    if (itemToUpdate != null)
        //                    {
        //                        itemToUpdate.PackagingId = packagingItem.PackagingId;
        //                        itemToUpdate.DimensionLL = packagingItem.DimensionLL;
        //                        itemToUpdate.DimensionWA = packagingItem.DimensionWA;
        //                        itemToUpdate.DimensionHA = packagingItem.DimensionHA;
        //                        itemToUpdate.WeightPerBox = packagingItem.WeightPerBox;
        //                        itemToUpdate.BoxQuantity = packagingItem.BoxQuantity;
        //                        itemToUpdate.NetWeight = packagingItem.NetWeight;
        //                        itemToUpdate.GrossWeight = packagingItem.GrossWeight;

        //                        await _HotSheetShipPackagingRepository.UpdateAsync(itemToUpdate);
        //                    }
        //                }
        //                else
        //                {
        //                    itemToSave.HotSheetShiptId = HotSheetShiptId;
        //                    itemToSave.Packaging = null;

        //                    await _HotSheetShipPackagingRepository.InsertAsync(itemToSave);
        //                }
        //            }
        //        }
        //    }

        //    if (input.Manifests.Count > 0)
        //    {
        //        foreach (var manifestItem in input.Manifests)
        //        {
        //            var itemToSave = ObjectMapper.Map<HotSheetShipManifest>(manifestItem);
        //            if (itemToSave.Id > 0)
        //            {
        //                if (manifestItem.Update == "1")
        //                {
        //                    var itemToUpdate = await _HotSheetShipManifestRepository.GetAsync(itemToSave.Id);

        //                    if (itemToUpdate != null)
        //                    {
        //                        itemToUpdate.Manifest = manifestItem.Manifest;
        //                        itemToUpdate.HotSheetDate = manifestItem.HotSheetDate;
        //                        itemToUpdate.ReportDateStart = manifestItem.ReportDateStart;
        //                        itemToUpdate.ReportDateEnd = manifestItem.ReportDateEnd;
        //                        itemToUpdate.Comments = manifestItem.Comments;

        //                        await _HotSheetShipManifestRepository.UpdateAsync(itemToUpdate);
        //                    }
        //                }
        //                //else if(manifestItem.Update == "2")
        //                //{
        //                //    var itemToUpdate = await _HotSheetShipManifestRepository.GetAsync(itemToSave.Id);
        //                //    if (itemToUpdate != null)
        //                //    {
        //                //        await _HotSheetShipManifestRepository.DeleteAsync(itemToUpdate);

        //                //    }
        //                //}                   
        //            }
        //            else
        //            {
        //                itemToSave.Id = 0;
        //                itemToSave.HotSheetShiptId = HotSheetShiptId;

        //                //await _HotSheetShipManifestRepository.InsertAsync(itemToSave);

        //                manifestItem.Id = await _HotSheetShipManifestRepository.InsertAndGetIdAsync(itemToSave);

        //            }
        //        }

        //        List<HotSheetShipManifest> currentShippingManifest = await _HotSheetShipManifestRepository.GetAllListAsync(c => c.HotSheetShiptId == input.Id);
        //        List<long> shippingManifestsIds = input.Manifests.Select(s => s.Id.Value).ToList();
        //        List<long> shippingManifestsIdsDeleted = currentShippingManifest.Where(manifest => !shippingManifestsIds.Contains(manifest.Id)).ToList()
        //                    .Select(s => s.Id).ToList();
        //        _HotSheetShipManifestRepository.Delete(c => shippingManifestsIdsDeleted.Contains(c.Id));

        //    }


        //    input.Id = HotSheetShiptId;

        //    return input;
        //}

        //private async Task SendNotificationsToApprovers(long HotSheetShiptId, HotSheetNotificationType notificationType,
        //    HotSheetApprovalType approvalType = HotSheetApprovalType.Manager, string reasonRejection = "")
        //{
        //    var HotSheet = await GetHotSheetShipById(HotSheetShiptId);

        //    var jobArgs = new SendHotSheetEmailArgs
        //    {
        //        HotSheetShiptId = HotSheetShiptId,
        //        Folio = HotSheet.Folio,
        //        CreationDate = HotSheet.CreationDate.Value,
        //        CustomerName = HotSheet.CustomerName,
        //        DocumentTypeId = HotSheet.DocumentTypeId,
        //        CreatorFullName = HotSheet.CreatorFullName,
        //        UsersToNotify = new List<HotSheetEmailItem>()
        //    };

        //    if (notificationType == HotSheetNotificationType.ApprovalRequestRejected)
        //    {
        //        jobArgs.ReasonRejection = reasonRejection;

        //        switch (approvalType)
        //        {
        //            case HotSheetApprovalType.Manager:
        //                jobArgs.RejectedBy = HotSheet.ManagerName;
        //                break;
        //            case HotSheetApprovalType.ImpoExpoStaff:
        //                jobArgs.RejectedBy = HotSheet.IEStaffName;
        //                break;
        //            case HotSheetApprovalType.AccountingStaff:
        //                jobArgs.RejectedBy = HotSheet.AccountingName;
        //                break;
        //        }
        //    }

        //    if (HotSheet.ManagerApprovalId.HasValue && !string.IsNullOrEmpty(HotSheet.ManagerEmailAddress)
        //        && notificationType == HotSheetNotificationType.ApprovalRequestToManager)
        //    {
        //        jobArgs.NotificationType = HotSheetNotificationType.ApprovalRequestToManager;
        //        jobArgs.UsersToNotify = new List<HotSheetEmailItem> {
        //                new HotSheetEmailItem() {
        //                    UserId = HotSheet.ManagerApprovalId,
        //                    FullName = HotSheet.ManagerName,
        //                    EmailAddress = HotSheet.ManagerEmailAddress,
        //                }
        //            };

        //        await _backgroundJobManager.EnqueueAsync<SendHotSheetEmailJob, SendHotSheetEmailArgs>(jobArgs);                
        //    }

        //    if (HotSheet.IEStaffId.HasValue && !string.IsNullOrEmpty(HotSheet.IEStaffEmailAddress)
        //        && notificationType == HotSheetNotificationType.ApprovalRequestToImpoExpoStaff)
        //    {
        //        jobArgs.NotificationType = HotSheetNotificationType.ApprovalRequestToImpoExpoStaff;
        //        jobArgs.UsersToNotify = new List<HotSheetEmailItem> {
        //                new HotSheetEmailItem() {
        //                    UserId = HotSheet.IEStaffApproverUserId,
        //                    FullName = HotSheet.IEStaffName,
        //                    EmailAddress = HotSheet.IEStaffEmailAddress,
        //                }
        //            };

        //        await _backgroundJobManager.EnqueueAsync<SendHotSheetEmailJob, SendHotSheetEmailArgs>(jobArgs);
        //    }

        //    if (HotSheet.AccountingApprovalId.HasValue && !string.IsNullOrEmpty(HotSheet.AccountingEmailAddress)
        //        && notificationType == HotSheetNotificationType.ApprovalRequestToAccountingStaff)
        //    {
        //        jobArgs.NotificationType = HotSheetNotificationType.ApprovalRequestToAccountingStaff;
        //        jobArgs.UsersToNotify = new List<HotSheetEmailItem> {
        //                new HotSheetEmailItem() {
        //                    UserId = HotSheet.AccountingApproverUserId,
        //                    FullName = HotSheet.AccountingName,
        //                    EmailAddress = HotSheet.AccountingEmailAddress,
        //                }
        //            };

        //        await _backgroundJobManager.EnqueueAsync<SendHotSheetEmailJob, SendHotSheetEmailArgs>(jobArgs);
        //    }

        //    if (HotSheet.Creator != null && !string.IsNullOrEmpty(HotSheet.Creator.EmailAddress)
        //        && (notificationType == HotSheetNotificationType.ApprovalRequestRejected 
        //           || notificationType == HotSheetNotificationType.ApprovalRequestApproved))
        //    {
        //        jobArgs.NotificationType = notificationType;
        //        jobArgs.UsersToNotify = new List<HotSheetEmailItem> {
        //                new HotSheetEmailItem() {
        //                    UserId = HotSheet.Creator.Id,
        //                    FullName = HotSheet.Creator.FullName,
        //                    EmailAddress = HotSheet.Creator.EmailAddress,
        //                }
        //            };

        //        await _backgroundJobManager.EnqueueAsync<SendHotSheetEmailJob, SendHotSheetEmailArgs>(jobArgs);
        //    }
        //}

        //public async Task CancelHotSheetShip(long HotSheetShiptId)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(HotSheetShiptId);
        //    if (item != null 
        //        && (item.StatusId == (int)HotSheetStatus.Draft || item.StatusId == (int)HotSheetStatus.Circulating || item.StatusId == (int)HotSheetStatus.PendingForApproval))
        //    {
        //        item.StatusId = (int)HotSheetStatus.Cancelled;

        //        await _HotSheetShipRepository.UpdateAsync(item);

        //        await _hotSheetHistoryManager.AddAsync(HotSheetShiptId, HotSheetHistoryType.StatusUpdated, HotSheetStatus.Cancelled.ToString());
        //    }
        //}

        //public async Task ExportHotSheetShipToAS400(long HotSheetShiptId)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(HotSheetShiptId);
        //    if (item != null && item.StatusId == (int)HotSheetStatus.Approved)
        //    {
        //        bool exported = await _as400Manager.ExportHotSheetToAS400(HotSheetShiptId);
        //        if (exported)
        //        {
        //            item.ExportedCigmaStatus = 1;
        //            item.ExportedCigmaDate = DateTime.Now;

        //            await _HotSheetShipRepository.UpdateAsync(item);
        //        }
        //    }
        //}


        //public async Task<List<ApproverDto>> GetApprovers()
        //{
        //    string sqlQuery = "EXEC GetApproversByUser @UserId";            
        //    var sqlParams = new
        //    {
        //        UserId = AbpSession.UserId
        //    };
        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<ApproverDto>(sqlQuery, sqlParams);
        //    return itemsDapper.ToList();
        //}

        //public async Task ApproveHotSheetShip(ApproveHotSheetShipInput input)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(input.HotSheetShiptId);
        //    if (item != null)
        //    {
        //        var shippingApprovals = _HotSheetApprovalRepository.GetAll()
        //            .Where(c => c.HotSheetShiptId == input.HotSheetShiptId)
        //            .FirstOrDefault();

        //        bool allApproversAproved = true;

        //        if (shippingApprovals != null)
        //        {
        //            switch (input.ApprovalType)
        //            {
        //                case HotSheetApprovalType.Manager:
        //                    if (input.StatusToUpdate == HotSheetStatus.Approved)
        //                    {
        //                        shippingApprovals.ManagerApprovalDate = DateTime.Now;
        //                    }
        //                    shippingApprovals.ManagerIsApproved = input.StatusToUpdate == HotSheetStatus.Approved ? true : false;

        //                    if (shippingApprovals.ManagerIsApproved == true && shippingApprovals.AccountingApprovalId.HasValue)
        //                    {
        //                        await SendNotificationsToApprovers(input.HotSheetShiptId, HotSheetNotificationType.ApprovalRequestToAccountingStaff);
        //                    }

        //                    if (shippingApprovals.ManagerIsApproved == true && !shippingApprovals.AccountingApprovalId.HasValue  && shippingApprovals.IEStaffId.HasValue)
        //                    {
        //                        await SendNotificationsToApprovers(input.HotSheetShiptId, HotSheetNotificationType.ApprovalRequestToImpoExpoStaff);
        //                    }

        //                    break;

        //                case HotSheetApprovalType.AccountingStaff:
        //                    if (input.StatusToUpdate == HotSheetStatus.Approved)
        //                    {
        //                        shippingApprovals.AccountingApprovalDate = DateTime.Now;
        //                    }
        //                    shippingApprovals.AccountingIsApproved = input.StatusToUpdate == HotSheetStatus.Approved ? true : false;

        //                    if (shippingApprovals.AccountingIsApproved == true && shippingApprovals.IEStaffId.HasValue)
        //                    {
        //                        await SendNotificationsToApprovers(input.HotSheetShiptId, HotSheetNotificationType.ApprovalRequestToImpoExpoStaff);
        //                    }
        //                    break;

        //                case HotSheetApprovalType.ImpoExpoStaff:
        //                    if (input.StatusToUpdate == HotSheetStatus.Approved)
        //                    {
        //                        shippingApprovals.IEStaffApprovalDate = DateTime.Now;
        //                    }
        //                    shippingApprovals.IEStaffIsApproved = input.StatusToUpdate == HotSheetStatus.Approved ? true : false;                                                    

        //                    break;
        //            }

        //            if(shippingApprovals.ManagerApprovalId.HasValue && (!shippingApprovals.ManagerIsApproved.HasValue ||
        //                (shippingApprovals.ManagerIsApproved.HasValue && !shippingApprovals.ManagerIsApproved.Value)) )
        //            {
        //                allApproversAproved = false;
        //            }
        //            if (shippingApprovals.IEStaffId.HasValue && (!shippingApprovals.IEStaffIsApproved.HasValue ||
        //                (shippingApprovals.IEStaffIsApproved.HasValue && !shippingApprovals.IEStaffIsApproved.Value)))
        //            {
        //                allApproversAproved = false;
        //            }
        //            if (shippingApprovals.AccountingApprovalId.HasValue && (!shippingApprovals.AccountingIsApproved.HasValue ||
        //                (shippingApprovals.AccountingIsApproved.HasValue && !shippingApprovals.AccountingIsApproved.Value)))
        //            {
        //                allApproversAproved = false;
        //            }

        //            await _HotSheetApprovalRepository.UpdateAsync(shippingApprovals);
        //        }

        //        var historyType = input.StatusToUpdate == HotSheetStatus.Approved ? HotSheetHistoryType.Approved : HotSheetHistoryType.Rejected;
        //        await _hotSheetHistoryManager.AddAsync(input.HotSheetShiptId, historyType, input.Comments);

        //        if(historyType == HotSheetHistoryType.Rejected)
        //        {
        //            item.StatusId = (int)input.StatusToUpdate;
        //            await _HotSheetShipRepository.UpdateAsync(item);

        //            await SendNotificationsToApprovers(input.HotSheetShiptId, HotSheetNotificationType.ApprovalRequestRejected, input.ApprovalType, input.Comments);
        //        }

        //        if (allApproversAproved)
        //        {
        //            item.StatusId = (int)input.StatusToUpdate;
        //            await _HotSheetShipRepository.UpdateAsync(item);

        //            bool exported = await _as400Manager.ExportHotSheetToAS400(input.HotSheetShiptId);

        //            if (exported)
        //            {
        //                item.ExportedCigmaStatus = 1;
        //                item.ExportedCigmaDate = DateTime.Now;

        //                await _HotSheetShipRepository.UpdateAsync(item);
        //            }

        //            await SendNotificationsToApprovers(input.HotSheetShiptId, HotSheetNotificationType.ApprovalRequestApproved);
        //        }
        //    }
        //}

        //public async Task SendForApproval(long HotSheetShiptId, string idEncrypted)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(HotSheetShiptId);
        //    if (item != null)
        //    {
        //        await SendNotificationsToApprovers(HotSheetShiptId, HotSheetNotificationType.ApprovalRequestToManager);

        //        item.StatusId = (int)HotSheetStatus.PendingForApproval;

        //        await _HotSheetShipRepository.UpdateAsync(item);

        //        await _hotSheetHistoryManager.AddAsync(HotSheetShiptId, HotSheetHistoryType.ApprovalRequested);
        //    }
        //}

        //public async Task<DashboardData> GetDashboardData(int? year)
        //{
        //    string sqlQuery = "EXEC GetDashboardData @UserId, @Year";            
        //    var sqlParams = new
        //    {
        //        UserId = AbpSession.UserId,
        //        Year = year.HasValue ? year.Value : DateTime.Now.Year
        //    };
        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<DashboardItemData>(sqlQuery, sqlParams);

        //    var result = new DashboardData();
        //    result.Items = itemsDapper.ToList();

        //    foreach (var item in result.Items)
        //    {
        //        item.IEStaffName = RemoveDiacritics(item.IEStaffName);
        //        item.CreatorUser = RemoveDiacritics(item.CreatorUser);
        //        item.HotSheetReason = RemoveDiacritics(item.HotSheetReason);
        //        item.DepartmentName = RemoveDiacritics(item.DepartmentName);
        //    }

        //    return result;
        //}

        //private string RemoveDiacritics(string text)
        //{
        //    if(text == null)
        //    {
        //        return string.Empty;
        //    }

        //    var normalizedString = text.Normalize(NormalizationForm.FormD);
        //    var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        //    for (int i = 0; i < normalizedString.Length; i++)
        //    {
        //        char c = normalizedString[i];
        //        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
        //        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        //        {
        //            stringBuilder.Append(c);
        //        }
        //    }

        //    return stringBuilder
        //        .ToString()
        //        .Normalize(NormalizationForm.FormC);
        //}

        //public async Task ResetHotSheetShip(long HotSheetShiptId)
        //{
        //    var item = await _HotSheetShipRepository.GetAsync(HotSheetShiptId);
        //    if (item != null)
        //    {
        //        item.StatusId = (int)HotSheetStatus.Draft;

        //        await _HotSheetShipRepository.UpdateAsync(item);

        //        var shippingApprovals = _HotSheetApprovalRepository.GetAll()
        //            .Where(c => c.HotSheetShiptId == HotSheetShiptId)
        //            .FirstOrDefault();

        //        if (shippingApprovals != null)
        //        {
        //            shippingApprovals.ManagerApprovalDate = null;
        //            shippingApprovals.ManagerIsApproved = false;

        //            shippingApprovals.AccountingApprovalDate = null;
        //            shippingApprovals.AccountingIsApproved = false;

        //            shippingApprovals.IEStaffApprovalDate = null;
        //            shippingApprovals.IEStaffIsApproved = false;

        //            await _HotSheetApprovalRepository.UpdateAsync(shippingApprovals);
        //        }

        //        await _hotSheetHistoryManager.AddAsync(HotSheetShiptId, HotSheetHistoryType.Reseted);
        //    }
        //}

        //public async Task ChangeApproversHotSheetShip(ChangeApproversHotSheetInput input)
        //{
        //    var shippingApprovals = _HotSheetApprovalRepository.GetAll()
        //           .Where(c => c.HotSheetShiptId == input.HotSheetShiptId)
        //           .FirstOrDefault();

        //    if (shippingApprovals != null )
        //    {
        //        shippingApprovals.ManagerApprovalId = input.ManagerApprovalId;
        //        shippingApprovals.AccountingApprovalId = input.AccountingApprovalId;
        //        shippingApprovals.IEStaffId = input.IEStaffId;

        //        await _HotSheetApprovalRepository.UpdateAsync(shippingApprovals);

        //        await _hotSheetHistoryManager.AddAsync(input.HotSheetShiptId, HotSheetHistoryType.ApproverChanged, input.Comments);
        //    }
        //}

        //public async Task<List<DBServicesLogsDto>> GetLogsInterfacesServices(string DateStart, string DateEnd)
        //{
        //    var listlogs = new  List<DBServicesLogsDto>();
        //    //var listLogs = await _dBServicesManager.GetLogsInterfaces(DateStart, DateEnd);
        //    //if (listLogs != null)
        //    //{

        //    //}
        //    return listlogs;
        //}

        //public async Task<List<CarrierNonWorkingDayDto>> GetNonWorkDaysToNotify()
        //{
        //    string sqlQuery = "EXEC GetNonWorkingDaysToNotify";                       

        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<CarrierNonWorkingDayDto>(sqlQuery);
        //    return itemsDapper.ToList();
        //}

        public async Task DeleteFile(long fileId)
        {
            var item = await _fileRepository.GetAsync(fileId);
            if (item != null)
            {
                await _fileRepository.DeleteAsync(item);
            }
        }

       

        //[HttpPost]
        //public async Task<TrackingScrapSales> GetTrackingScrapSales(GetTrackingScrapSalesInput input)
        //{
        //    string sqlQuery = "EXEC GetTrackingScrapSales @PlantId, @CustomerId, @PaymentStatusId, @Manifest, @Folio, @Invoice";
        //    var sqlParams = new
        //    {
        //        PlantId = input.PlantId,
        //        CustomerId = input.CustomerId,
        //        PaymentStatusId = input.PaymentStatusId,
        //        Manifest = input.Manifest,
        //        Folio = input.Folio,
        //        Invoice = input.Invoice
        //    };
        //    var itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<TrackingScrapSalesItem>(sqlQuery, sqlParams);

        //    var result = new TrackingScrapSales();
        //    result.Items = itemsDapper.ToList();

        //    return result;
        //}

        //public async Task<List<HotSheetShipGuidesResXlsxDto>> UpdateHotSheetShipGuidesXLSX(List<HotSheetShipGuidesXlsxDto> input)
        //{

        //    IEnumerable<HotSheetShipGuidesResXlsxDto> itemsDapper = null;

        //    int registro = 0;
        //    List<HotSheetShipGuidesXlsxDto> newList = new List<HotSheetShipGuidesXlsxDto>();
        //    HotSheetShipGuidesXlsxDto itemError = new HotSheetShipGuidesXlsxDto(); //Nos ayuda a determinar en que item esta el error.
        //    string sHotSheetGuidesList = string.Empty;
        //    try
        //    {
        //        for (int i = 0; i < input.Count; i++)
        //        {
        //            registro = i;

        //            HotSheetShipGuidesXlsxDto item = new HotSheetShipGuidesXlsxDto();

        //            item.TrackingNumber = Regex.Replace(input[i].TrackingNumber, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        //            item.GuideReference = Regex.Replace(input[i].GuideReference, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        //            item.GuideStatusDetail = Regex.Replace(input[i].GuideStatusDetail, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        //            item.GuideStatus = Regex.Replace(input[i].GuideStatus, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
        //            item.GuideCurrency = Regex.Replace(input[i].GuideCurrency, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");


        //            item.GuideCost = input[i].GuideCost;                                        

        //            newList.Add(item);
        //            itemError = item;
        //        }

        //        XmlSerializer oSerializer = new XmlSerializer(typeof(List<HotSheetShipGuidesXlsxDto>));
        //        StringWriter sWriter = new StringWriter();
        //        XmlWriter writer = XmlWriter.Create(sWriter);
        //        oSerializer.Serialize(writer, newList);
        //        sHotSheetGuidesList = sWriter.ToString();
        //        int iIdx = sHotSheetGuidesList.IndexOf("<HotSheetGuidesXlsxDto>");
        //        sHotSheetGuidesList = "<ArrayOfHotSheetGuidesXlsxDto>" + sHotSheetGuidesList.Substring(iIdx);
        //        sHotSheetGuidesList = sHotSheetGuidesList.Replace(" xmlns=\"http://tempuri.org/\"", "");
        //        sHotSheetGuidesList = sHotSheetGuidesList.Replace("xsi:nil=\"true\"", ""); //evita error en sql por prefijo no declarado

        //        var sqlParams = new { iIdUser = 1, HotSheetGuidesXML = sHotSheetGuidesList };
        //        itemsDapper = await _HotSheetShipDapperRepository.QueryAsync<HotSheetShipGuidesResXlsxDto>("EXEC UpdateHotSheetGuidesXlsx @iIdUser, @HotSheetGuidesXML", sqlParams);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return itemsDapper.ToList();

        //}
    }
}

using IncarichiCUServer.Models.StoredProcedure;
using IncarichiCUServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace IncarichiCUServer.Controllers
{
    public class StoricoCheckupController : ControllerBase
    {
        private readonly IntranetSopranDbContext _context;

        public StoricoCheckupController(IntranetSopranDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetIncarichi")]
        public async Task<IEnumerable<SP_StoricoCheckup_GetIncarichi>> GetListIncarichi(int idsam)
        {
            string StoredProc = "exec Net_ciodueit.dbo.[SP_StoricoCheckup_GetIncarichi] @idsam = 69455";
            var result = await _context.SP_StoricoCheckup_GetIncarichi.FromSqlRaw(StoredProc).ToListAsync();
            return result;
        }
        [HttpGet("GetAllegatiList")]
        public async Task<IEnumerable<SP_StoricoCheckup_GetAllegatiList>> GetListAllegati(string keyord, int haccp)
        {
            string StoredProc = "exec Net_ciodueit.dbo.[SP_StoricoCheckup_GetAllegatiList] @keyord = '"+ keyord +"', @haccp = "+ haccp +"";
            var result = await _context.SP_StoricoCheckup_GetAllegatiList.FromSqlRaw(StoredProc).ToListAsync();
            return result;
        }
        [HttpGet("GetAllegatiData")]
        public async Task<IActionResult> GetListAllegatiData(int rientro)
        {
            string storedProc = $"exec Net_ciodueit.dbo.[SP_StoricoCheckup_GetAllegatiData] @keyord = '201376070010', @haccp = 0, @contatore = 78079, @rientro = " + rientro;
            var result = _context.SP_StoricoCheckup_GetAllegatiData.FromSqlRaw(storedProc).AsEnumerable().FirstOrDefault();

            // Definisci il nome del file
            string fileName = "FileTest.rar";

            // Restituisci i dati binari come file
            return new FileContentResult(result.Doc, "application/x-rar-compressed")
            {
                FileDownloadName = fileName
            };
        }




    }

}

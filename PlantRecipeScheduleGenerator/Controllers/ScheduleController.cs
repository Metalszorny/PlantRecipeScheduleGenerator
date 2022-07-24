using Common;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PlantRecipeScheduleGenerator.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        [HttpPost("generate")]
        public void Post([FromBody]RequestParameters requestParameters)
        {
            var fileNameAndPath = $"{requestParameters.FolderPath.TrimEnd('\\')}\\{requestParameters.FileName}";
            var actualResult = ScheduleGenerator.GenerateSchedule(requestParameters.Input.Input, requestParameters.Recipes.Recipes);
            var jsonResult = JsonConvert.SerializeObject(actualResult, new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffffffZ",
            });

            if (!System.IO.Directory.Exists(requestParameters.FolderPath))
            {
                System.IO.Directory.CreateDirectory(requestParameters.FolderPath);
            }

            if (!System.IO.File.Exists(fileNameAndPath))
            {
                using (var stream = System.IO.File.Create(fileNameAndPath))
                {
                    stream.Flush();
                    stream.Close();
                }
            }

            System.IO.File.WriteAllText(fileNameAndPath, jsonResult);
        }
    }
}

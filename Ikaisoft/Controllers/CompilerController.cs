using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ikaisoft.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CompilerController : ControllerBase
    {
        [HttpPost("{language}")]
        public IActionResult Run(string language, [FromBody] CodeRequest request)
        {
            string code = request.Code;
            string output = "";

            switch (language.ToLower())
            {
                case "python":
                    output = RunProcess("python3", "-c", code);
                    break;

                case "java":
                    System.IO.File.WriteAllText("Program.java", code);
                    RunProcess("javac", "Program.java");
                    output = RunProcess("java", "Program");
                    break;

                case "c":
                    System.IO.File.WriteAllText("program.c", code);
                    RunProcess("gcc", "program.c -o program.out");
                    output = RunProcess("./program.out", "");
                    break;

                default:
                    output = "Language not supported!";
                    break;
            }

            return Ok(new { output });
        }

        private string RunProcess(string fileName, string args, string codeInput = null)
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                if (codeInput != null)
                {
                    process.StartInfo.RedirectStandardInput = true;
                }

                process.Start();

                if (codeInput != null)
                {
                    process.StandardInput.WriteLine(codeInput);
                    process.StandardInput.Close();
                }

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return output + error;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }

    public class CodeRequest
    {
        public string Code { get; set; }
    }
}

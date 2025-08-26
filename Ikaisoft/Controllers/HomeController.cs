using System.Diagnostics;
using Ikaisoft.Models;
using Ikaisoft.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ikaisoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Consultation()
        {
            return View();
        }
        public IActionResult OfflineTraining()
        {
            return View();
        }
        public IActionResult HtmlCourse()
        {
            return View();
        }
        public IActionResult CssCourse()
        {
            return View();
        }
        public IActionResult JsCourse()
        {
            return View();
        }
        public IActionResult PythonCourse()
        {
            return View();
        }
        public IActionResult ReactCourse()
        {
            return View();
        }
        public IActionResult NodejsCourse()
        {
            return View();
        }
        public IActionResult VMS()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendContactMail(model);
                return Json(new { success = true, message = "Request has been submitted successfully. We will connect with you shortly." });
            }
            return Json(new { success = false, message = "Invalid input, please check the form." });
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult TermsAndCondition()
        {
            return View();
        }
        public IActionResult JobProgram()
        {
            return View();
        }
        public IActionResult CerticateCourse()
        {
            return View();
        }
        public IActionResult MockDrives()
        {
            return View();
        }
        public IActionResult PlacementAssistance()
        {
            return View();
        }
        public IActionResult IndustryPatnership()
        {
            return View();
        }
        public IActionResult CareerCounseling()
        {
            return View();
        }
        public IActionResult StudyMaterial()
        {
            return View();
        }
        public IActionResult PracticalTest()
        {
            return View();
        }
        public IActionResult AiQuizzes()
        {
            return View();
        }
        public IActionResult SoftwareDevelopment()
        {
            return View();
        }
        public IActionResult CustomProjects()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Compiler()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Compiler(string language, [FromBody] CodeRequest request)
        {
            string code = request.Code;
            string output = "";

            switch (language.ToLower())
            {
                case "python":
                    output = RunCommand("python3", "script.py", code);
                    break;
                case "java":
                    output = RunCommand("javac", "Program.java", code, "java Program");
                    break;
                case "c":
                    output = RunCommand("gcc", "program.c", code, "./a.out");
                    break;
            }

            return Ok(new { output });
        }

        private string RunCommand(string compiler, string fileName, string code, string runCmd = null)
        {
            System.IO.File.WriteAllText(fileName, code);
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = compiler;
            process.StartInfo.Arguments = fileName;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string result = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(runCmd))
            {
                var run = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = runCmd.Split(' ')[0],
                    Arguments = runCmd.Contains(" ") ? runCmd.Substring(runCmd.IndexOf(" ") + 1) : "",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                result += run.StandardOutput.ReadToEnd() + run.StandardError.ReadToEnd();
                run.WaitForExit();
            }

            return result;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Npgsql;
using OptimaJet.Workflow;
using System.Collections.Specialized;
using System.Text;
using Workflow;

namespace WorkflowDesigner.Controllers
{
    public class DesignerController : Controller
    {
        // var dbContext = AppContext.
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Api()
        {
            Stream? filestream = null;
            var parameters = new NameValueCollection();

            //Defining the request method
            var isPost = Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);

            //Parse the parameters in the query string
            foreach (var q in Request.Query)
            {
                parameters.Add(q.Key, q.Value.First());
            }

            if (isPost)
            {
                //Parsing the parameters passed in the form
                var keys = parameters.AllKeys;

                foreach (var key in Request.Form.Keys)
                {
                    if (!keys.Contains(key))
                    {
                        parameters.Add(key, Request.Form[key]);
                    }
                }

                //If a file is passed
                if (Request.Form.Files.Count > 0)
                {
                    //Save file
                    filestream = Request.Form.Files[0].OpenReadStream();
                }
            }

            //Calling the Designer Api and store answer
            var (result, hasError) = await WorkflowInit.Runtime.DesignerAPIAsync(parameters, filestream);


            //If it returns a file, send the response in a special way
            if (parameters["operation"]?.ToLower() == "downloadscheme" && !hasError)
            {
                //save the byte into column Scheme in WorkflowSheme table
                var con = new NpgsqlConnection(WorkflowInit.ConnectionString);
                con.Open();
                var cmd = new NpgsqlCommand("insert into public.\"WorkflowScheme\" values ('orderProcess6','" + result + "',false,'','')", con);
                try
                {
                    cmd.ExecuteScalar();
                }
                catch (Exception ex) { }


                con.Close();
                return File(Encoding.UTF8.GetBytes(result), "text/xml");
            }

            if (parameters["operation"]?.ToLower() == "downloadschemebpmn" && !hasError)
                return File(Encoding.UTF8.GetBytes(result), "text/xml");

            //response
            return Content(result);
        }
    }
}

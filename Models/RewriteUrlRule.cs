using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;
using System;

namespace aspcore_watchshop.Models
{
    public class RewriteUrlRule : IRule
    {
        public RewriteUrlRule()
        {

        }
        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            // if already redirected, skip  
            if (request.Path.Value.Contains("Index"))
            {
                Match match = Regex.Match(request.QueryString.Value, @"\d+");
                if (match.Success == true)
                {
                    string newLocation = GetUrlName(Int32.Parse(match.Value));
                    var response = context.HttpContext.Response;
                    response.StatusCode = StatusCodes.Status302Found;
                    context.Result = RuleResult.EndResponse;
                    response.Headers[HeaderNames.Location] = newLocation;
                }
            }
        }

        public string GetUrlName(int code)
        {
            switch (code)
            {
                case -2: return "ket-qua-tim-kiem";
                case 0: return "km";
                case 1: return "sp-nam";
                case 2: return "sp-nu";
                case 3: return "sp-doi";
                case 4: return "sp-khac";
                default: return "";
            }
        }
    }
}
using Microsoft.AspNetCore.Razor.TagHelpers;
using Projekt.Models;

namespace Projekt.TagHelpers;

[HtmlTargetElement("reviews")]
public class DateTagHelper : TagHelper{
    
    [HtmlAttributeName("collection")]
    public ICollection<BeerReview> _reviews{ get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output){
        output.TagName = "table";
        output.Attributes.SetAttribute("id", "content-table");
        output.TagMode = TagMode.StartTagAndEndTag;
        var dates = new List<DateTime>();
        foreach (var review in _reviews)
        {
            if (!dates.Contains(review.Date.Date))
            {
                dates.Add(review.Date.Date);
            }
        }
        dates.Sort();
        dates.Reverse();
        foreach (var date in dates)
        {
            output.Content.AppendHtml($"<tr class=\"date-line\"><td colspan=\"4\">{date.ToString("dd.MM.yyyy")}</td></tr>");

            foreach (var review in _reviews)
            {
                if (review.Date.Date == date.Date)
                {
                    output.Content.AppendHtml($"<tr>" +
                                              $"<td>{review.Beer.Name}</td>" +
                                              $"<td>{review.Rating}/10</td>" +
                                              $"<td>{review.Review}</td>" +
                                              $"<td>User: {review.BeerUser?.NickName}</td>" +
                                              $"</tr>");
                    
                    
                }
            }
        }
        // output.Content.AppendHtml("a");
    }
    
    
    

}

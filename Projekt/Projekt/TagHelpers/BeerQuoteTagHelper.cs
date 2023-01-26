using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Projekt.TagHelpers;

[HtmlTargetElement("beerquote")]
public class BeerQuoteTagHelper : TagHelper{
    
    [HtmlAttributeName("path")]
    public string path { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output){
        string[] lines = File.ReadAllLines(path);
        Random rnd = new Random();
        int random = rnd.Next(0, lines.Length);
        while(random%2!=0){
            random = rnd.Next(0, lines.Length);
        }
        
        output.Content.AppendHtml("<p class='quote'>" + lines[random] + "</p>");
        output.Content.AppendHtml("<p class='author'>" + lines[random+1] + "</p>");
        
    }
}
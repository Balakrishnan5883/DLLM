
using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary.Models;

public class PromptData
{
    [Required]
    [StringLength(1)]
    public string UserInput = string.Empty;
    [Required]
    public  string?  SelectedModelName {get;set;}

}
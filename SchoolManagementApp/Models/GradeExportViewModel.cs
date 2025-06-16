using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

public class GradeExportViewModel
{
    public int? ClassId { get; set; }
    public string Subject { get; set; }
    public string ExportFormat { get; set; } // "excel" 或 "csv"

    public List<SelectListItem> Classes { get; set; }
    public List<string> Subjects { get; set; }
}
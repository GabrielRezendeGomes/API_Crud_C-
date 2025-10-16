using ClosedXML.Excel;
using CashFllow.Domain.Reports;
using CashFllow.Domain.Repositories.Expenses;
using DocumentFormat.OpenXml.Spreadsheet;
using CashFllow.Domain.Enums;
namespace CashFlow.Application.UseCases.Expenses.Reports;
public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private const string CURRENCY_SYMBOL = "R$";
    public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }

        using var Workbook = new XLWorkbook();

        Workbook.Author = "Gabriel";
        Workbook.Style.Font.FontSize = 12;
        Workbook.Style.Font.FontName = "Times New Roma";

        var worksheet = Workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var expense in expenses)
        {
            worksheet.Cell($"A{raw}").Value = expense.Title;

            worksheet.Cell($"B{raw}").Value = expense.Date ;
            worksheet.Cell($"B{raw}").Style.DateFormat.Format = "mm/dd/yyyy"  ;

            worksheet.Cell($"C{raw}").Value = ConvertPaymentType(expense.PaymentType);

            worksheet.Cell($"D{raw}").Value = expense.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format =  $"-{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{raw}").Value = expense.Description;

            raw++;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.Rows().AdjustToContents();
        

        

        var file = new MemoryStream();

        Workbook.SaveAs(file);

        return file.ToArray();
    }

    private string ConvertPaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.pix => "Pix",
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITULO;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATA;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.TIPO_PAGAMENTO;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.VALOR;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRICAO;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.AirForceBlue;

        worksheet.Cells("A1,B1,C1,E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            
    }
}

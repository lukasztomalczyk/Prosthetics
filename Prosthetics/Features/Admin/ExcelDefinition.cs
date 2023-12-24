using ClosedXML.Excel;

namespace Prosthetics.Features.Admin
{
    public class ExcelDefinition
    {
        private readonly IXLWorksheet _worksheet;
        private readonly IEnumerable<OrderByRangeDto> _orders;

        public ExcelDefinition(IXLWorksheet worksheet, IEnumerable<OrderByRangeDto> orders)
        {
            _worksheet = worksheet;
            _orders = orders;
        }

        public ExcelDefinition AddHeaders()
        {
            _worksheet.Cell(1, 1).Value = "Imię i nazwisko";
            _worksheet.Cell(1, 2).Value = "Typ zamówienia";

            var maxAdditionalWorks = _orders.Select(_ => _.AdditionalWorks.Count).OrderByDescending(_ => _).FirstOrDefault();

            for (int i = 3; i < maxAdditionalWorks + 3; i++)
            {
                _worksheet.Cell(1, i).Value = $"Dodatek {i - 2}";
            }

            return this;
        }

        public ExcelDefinition AddRows()
        {
            for (int i = 2; i < _orders.Count() + 2; i++)
            {
                _worksheet.Cell(i, 1).Value = _orders.ElementAt(i - 2).PatientFullName;
                _worksheet.Cell(i, 2).Value = _orders.ElementAt(i - 2).Type;

                for (int j = 3; j < _orders.ElementAt(i - 2).AdditionalWorks.Count + 3; j++)
                {
                    _worksheet.Cell(i, j).Value = _orders.ElementAt(i - 2).AdditionalWorks.ElementAt(j - 3).Name;
                }
            }

            return this;
        }
    }
}

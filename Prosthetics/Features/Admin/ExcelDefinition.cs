using ClosedXML.Excel;

namespace Prosthetics.Features.Admin
{
    public class ExcelDefinition
    {
        private readonly IXLWorksheet _worksheet;
        private readonly IEnumerable<ParientOrdersDto> _patientsOrders;
        private readonly IEnumerable<string> _headers;
        private Dictionary<string, int>? _headersWithColNumber;

        public ExcelDefinition(IXLWorksheet worksheet, IEnumerable<ParientOrdersDto> doctorsOrders, 
            IEnumerable<string> headers)
        {
            _worksheet = worksheet;
            _patientsOrders = doctorsOrders;
            _headers = headers;
        }

        public ExcelDefinition AddHeaders()
        {
            _headersWithColNumber = new Dictionary<string, int>();

            for (int i = 0; i < _headers.Count(); i++)
            {
                _headersWithColNumber.Add(_headers.ElementAt(i), i + 3);
            }

            _worksheet.Cell(1, 2).Value = "Imię i nazwisko";

            foreach (var header in _headersWithColNumber) 
            {
                _worksheet.Cell(1, header.Value).Value = header.Key;
            }

            return this;
        }

        public ExcelDefinition AddRows()
        {
            ParientOrdersDto orderByPatient;
            KeyValuePair<string, int> header;

            for (int i = 0; i < _patientsOrders.Count(); i++)
            {
                orderByPatient = _patientsOrders.ElementAt(i);
                _worksheet.Cell(i + 2, 2).Value = orderByPatient.PatientFullName;

                for (int j = 0; j < _headersWithColNumber?.Count(); j++)
                {
                    header = _headersWithColNumber.ElementAt(j);
                    _worksheet.Cell(i + 2, header.Value).Value = orderByPatient.Orders.Any(_ => _.OrderName == header.Key)
                        ? orderByPatient.Orders.First(_ => _.OrderName == header.Key).Count : 0;
                }
            }

            return this;
        }

        public ExcelDefinition AddSummary(IEnumerable<OrderCountDto> summary)
        {
            var summaryRowIndex = _patientsOrders.Count() + 3;
            _worksheet.Cell(summaryRowIndex, 1).Value = "Podsumowanie:";

            foreach (var header in _headersWithColNumber ?? new Dictionary<string, int>())
            {
                _worksheet.Cell(summaryRowIndex, header.Value).Value = 
                    summary.FirstOrDefault(_ => _.OrderName == header.Key)?.Count ?? 0;
            }

            return this;
        }
    }
}

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Prosthetics.Common
{
    public interface IExcelExporter
    {
        IExcelExporter New();
        void Dispose();
        Stream SaveFile(Stream stream);
        IExcelExporter AddWorksheet(string name);
        IExcelExporter SetDataForWorksheet(string name, Action<IXLWorksheet> action);
    }

    public class ExcelExporter : IDisposable, IExcelExporter
    {
        private XLWorkbook _xlWorkbook;

        public IExcelExporter New()
        {
            if (_xlWorkbook != null) _xlWorkbook.Dispose();
                _xlWorkbook = new XLWorkbook();

            return this;
        }

        public IExcelExporter AddWorksheet(string name)
        {
            _xlWorkbook.Worksheets.Add(name);

            return this;
        }

        public IExcelExporter SetDataForWorksheet(string name, Action<IXLWorksheet> action)
        {
            try
            {
                var worksheet = _xlWorkbook.Worksheet(name) 
                    ?? throw new Exception($"Worksheet with name: {name} was not found. Create new AddWorksheet");

                action.Invoke(worksheet);

            }
            catch (Exception ex)
            {

                throw;
            }

            return this;
        }

        public Stream SaveFile(Stream stream)
        {
            _xlWorkbook.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        public void Dispose()
        {
            _xlWorkbook.Dispose();
        }
    }
}

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Prosthetics.Common
{
    public interface IExcelExporter
    {
        void Dispose();
        Stream SaveFile(Stream stream);
        IExcelExporter AddWorksheet(string name);
        IExcelExporter SetData(Action<IXLWorksheet> action);
    }

    public class ExcelExporter : IDisposable, IExcelExporter
    {
        private readonly XLWorkbook _xlWorkbook;
        private IXLWorksheet _worksheet;

        public ExcelExporter()
        {
            _xlWorkbook = new XLWorkbook();
        }

        public IExcelExporter AddWorksheet(string name)
        {
            _worksheet = _xlWorkbook.Worksheets.Add(name);

            return this;
        }

        public IExcelExporter SetData(Action<IXLWorksheet> action)
        {
            try
            {
                action.Invoke(_worksheet);

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

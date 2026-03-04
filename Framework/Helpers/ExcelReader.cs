using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace PlaywrightCSharpFramework.Framework.Helpers
{
    public class ExcelReader
    {
        private readonly string _filePath;

        public ExcelReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Excel file not found at path: {filePath}");

            _filePath = filePath;

            // Required for EPPlus (non-commercial use)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        #region Read Single Cell

        public string ReadCell(string sheetName, int row, int column)
        {
            using var package = new ExcelPackage(new FileInfo(_filePath));
            var worksheet = package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
                throw new Exception($"Sheet '{sheetName}' not found.");

            return worksheet.Cells[row, column].Text;
        }

        #endregion

        #region Read Entire Row

        public Dictionary<string, string> ReadRow(string sheetName, int rowNumber)
        {
            using var package = new ExcelPackage(new FileInfo(_filePath));
            var worksheet = package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
                throw new Exception($"Sheet '{sheetName}' not found.");

            var columnCount = worksheet.Dimension.End.Column;
            var data = new Dictionary<string, string>();

            for (int col = 1; col <= columnCount; col++)
            {
                var header = worksheet.Cells[1, col].Text;
                var value = worksheet.Cells[rowNumber, col].Text;

                if (!string.IsNullOrEmpty(header))
                    data[header] = value;
            }

            return data;
        }

        #endregion

        #region Read Entire Sheet (Dictionary)

        public List<Dictionary<string, string>> ReadSheet(string sheetName)
        {
            using var package = new ExcelPackage(new FileInfo(_filePath));
            var worksheet = package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
                throw new Exception($"Sheet '{sheetName}' not found.");

            var rowCount = worksheet.Dimension.End.Row;
            var columnCount = worksheet.Dimension.End.Column;

            var result = new List<Dictionary<string, string>>();

            for (int row = 2; row <= rowCount; row++)
            {
                var rowData = new Dictionary<string, string>();

                for (int col = 1; col <= columnCount; col++)
                {
                    var header = worksheet.Cells[1, col].Text;
                    var value = worksheet.Cells[row, col].Text;

                    if (!string.IsNullOrEmpty(header))
                        rowData[header] = value;
                }

                result.Add(rowData);
            }

            return result;
        }

        #endregion

        #region Read Sheet as Strongly Typed List

        public List<T> ReadSheetAs<T>(string sheetName) where T : new()
        {
            var data = ReadSheet(sheetName);
            var result = new List<T>();

            foreach (var row in data)
            {
                var obj = new T();
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (row.ContainsKey(property.Name))
                    {
                        var value = row[property.Name];

                        if (!string.IsNullOrEmpty(value))
                        {
                            var convertedValue = Convert.ChangeType(value, property.PropertyType);
                            property.SetValue(obj, convertedValue);
                        }
                    }
                }

                result.Add(obj);
            }

            return result;
        }

        #endregion
    }
}
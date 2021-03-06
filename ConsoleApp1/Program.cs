using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

class ExcelManager : IDisposable
{
    private Application? _application = null;
    private Workbook? _workbook = null;
    private bool disposedValue = false;

    static void Main()
    {
        ExcelManager em = new ExcelManager();
        em.Open();
        em.SaveAsPDF();
    }

    /// <summary>
    /// Excelワークブックを開く
    /// </summary>
    public void Open()
    {
        // Excelアプリケーション生成
        _application = new Application()
        {
            // 非表示
            Visible = false,
        };

        // Bookを開く
        _workbook = _application.Workbooks.Open(@"C:\Users\youba\Desktop\amd-dev\sample\samplePDF.xlsx");
    }

    /// <summary>
    /// Excelワークブックをファイル名を指定してPDF形式で保存する
    /// </summary>
    /// <returns>true:正常終了、false:保存失敗</returns>
    public bool SaveAsPDF()
    {
        try
        {
            // 全シートを選択する
            _workbook.Worksheets.Select();

            // ファイル名を指定してPDF形式で保存する
            // ExportAsFixedFormatメソッド: ブックを PDF または XPS 形式に発行する
            //  Type    : タイプ   : XlFixedFormatType xlTypePDF=PDF, xlTypeXPS=XPS
            //  Filename: 出力ファイル名
            //  Quality : 種t力品質: XlFixedFormatQuality xlQualityStandard=標準品質, xlqualityminimum=最小限の品質

            _workbook.ExportAsFixedFormat(
                Type: XlFixedFormatType.xlTypePDF,
                Filename: @"C:\Users\youba\Desktop\amd-dev\sample\sample.pdf",
                Quality: XlFixedFormatQuality.xlQualityStandard);
        }
        catch
        {
            return false;
        }

        return true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: Managed Objectの破棄
            }

            if (_workbook != null)
            {
                _workbook.Close();
                Marshal.ReleaseComObject(_workbook);
                _workbook = null;
            }

            if (_application != null)
            {
                _application.Quit();
                Marshal.ReleaseComObject(_application);
                _application = null;
            }

            disposedValue = true;
        }
    }

    ~ExcelManager()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Gf.Web.Automation.Framework;
using Gf.Web.Automation.Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Gf.Web.Automation.Page
{
    public class CheckOutPage
    {
        public readonly CheckOutPageMap Map;

        public CheckOutPage()
        {
            Map = new CheckOutPageMap();
        }
        
        /// <summary>
        /// Check Batch Table is sorted by Column
        /// </summary>
        /// <param name = "columnName" > which column name you would like to sort: BatchID, Pouches, Rolls</param>
        public void AssertSortBatchTable(string columnName)
        {
            Map.BatchTable.GetTableCell(0, columnName).ClickByJS();
            List<string> actual = Map.BatchTable.GetTableColumnData(columnName);            
            List<string> expected = Map.BatchTable.GetTableColumnData(columnName);
            var sortType = Map.BatchTable.GetTableCell(0, columnName).GetAttribute("aria-sort");            
            if (columnName == "Rolls" || columnName == "Pouches")
            {
                if (sortType == "ascending")
                {
                    expected = expected.Select(double.Parse)
                            .OrderBy(i => i).ToList()
                            .ConvertAll<string>(i => i.ToString());
                }
                else
                {
                    expected = expected.Select(double.Parse)
                            .OrderByDescending(i => i).ToList()
                            .ConvertAll<string>(i => i.ToString());
                }
            }
            else if (columnName == "Inspection")
            {
                if (sortType == "ascending")
                {
                    expected = expected.Select(DateTime.Parse)
                                .OrderBy(i => i).ToList()
                                .ConvertAll<string>(i => i.ToString());
                }
                else
                {
                    expected = expected.Select(DateTime.Parse)
                                .OrderByDescending(i => i).ToList()
                                .ConvertAll<string>(i => i.ToString());
                }                    
                actual = actual.Select(DateTime.Parse).ToList()
                            .ConvertAll<string>(i => i.ToString());               
            }
            else
            {
                expected = (sortType == "ascending") ? expected.OrderBy(i => i).ToList() : expected.OrderByDescending(i => i).ToList();
                    //expected = expected.OrderBy(i=>i).ToList();
            }
            Console.WriteLine($"Check the table is sorted by {columnName} {sortType}");
            foreach (var temp in actual)
                Console.WriteLine(temp);
            foreach (var temp1 in expected)
                Console.WriteLine(temp1);
            FW.Log.Step($"Check the table is sorted by {columnName} {sortType}");
            actual.Should().Equal(expected);
            
            //Assert.That(actual.SequenceEqual(expected), $"Table is sorted by {columnName} incorrectly");
        }

       /* /// <summary>
        /// Verify the Batch Details data of a specific row
        /// </summary>
        /// <param name="row">The specific row number</param>
        public void AssertBatchDetailsByRow(int row)
        {
            Map.BatchTable.GetTableCell(row, 1).ClickByJS();
            IList<string> expectedResult = new List<string>()
            {
                Map.BatchTable.GetTableCellData(row, "BatchID"),
                Map.BatchTable.GetTableCellData(row, "Location"),
                Map.BatchTable.GetTableCellData(row, "Pouches"),
                Map.BatchTable.GetTableCellData(row, "AlarmRate"),
                Map.BatchTable.GetTableCellData(row, "Rolls"),
                Map.BatchTable.GetTableCellData(row, "Status"),
            };
            Assert.Multiple(() => {
                FW.Log.Step($"Check Batch Number in Details should be {expectedResult[0]}");
                Map.BatchDetailsTable.GetTableCellData(0, 0).Should().Be(expectedResult[0], $"The Batch Number in Details should be {expectedResult[0]} the same with on Batch List");
                FW.Log.Step($"Check Batch Status in Details should be {expectedResult[5]}");
                Map.BatchDetailsTable.GetTableCellData(0, 5).Should().Be(expectedResult[5], $"The Batch Status in Details should be {expectedResult[5]} the same with on Batch List");
                FW.Log.Step($"Check Batch Location in Details should be {expectedResult[1]}");
                Map.BatchDetailsTable.GetTableCellData(1, 1).Should().Be(expectedResult[1], $"The Batch Location in Details should be {expectedResult[1]} the same with on Batch List");
                FW.Log.Step($"Check Batch Rolls in Details should be {expectedResult[4]}");
                Map.BatchDetailsTable.GetTableCellData(4, 1).Should().Be(expectedResult[4], $"The Batch Rolls in Details should be {expectedResult[4]} the same with on Batch List");
                FW.Log.Step($"Check Batch Alarm Rate in Details should be {expectedResult[2]}");
                Map.BatchDetailsTable.GetTableCellData(3, 1).Should().Be(expectedResult[2], $"The Batch Alarm Rate in Details should be {expectedResult[2]} the same with on Batch List");               
            });
            Map.BatchDetailsTable.GetTableCell(0, 1).ClickByJS();
        }

        /// <summary>
        ///  Verify the Batch Details data of all rows in current page
        /// </summary>
        public void AssertBatchDetailsByPage()
        {
            var rows = Map.BatchTable.GetTableRows().Count;
            for (int i = 1; i < rows; i++)
            {
                AssertBatchDetailsByRow(i);
            }
        }*/
    }

    public class CheckOutPageMap
    {
        public MyTable BatchTable => new MyTable(Driver.FindElement(By.CssSelector("table.table--batch"), "Batches Table"));
        public Elements BatchDetails => Driver.FindElements(By.CssSelector(".b-table-details .col"));
        public Element MainContent => Driver.FindElement(By.Id("main-content"), "Main Content Checkout Page");
        public Element ScanBarCodeTextBox => Driver.FindElement(By.CssSelector("input[aria-label='Scan barcode']"), "Scan BarCode TextBox");
        public Element ScanButton => Driver.FindElement(By.CssSelector("button.btn.btn-outline-primary-5"), "Scan BarCode Button");
        public Element SearchBatchTextBox => Driver.FindElement(By.CssSelector("input[placeholder='Search batch']"), "Serach Batch TextBox");
        public Element SearchButton => Driver.FindElement(By.CssSelector("button.btn.btn-primary-2"), "Search Batch Button");
    }


    public class MyTable
    {
        private readonly Element _tableElement;

        public MyTable(Element tableElement)
        {
            _tableElement = tableElement;
        }

        public Element Current => _tableElement ?? throw new NullReferenceException("TableElement is null");

        readonly IDictionary<string, int> dictColumn = new Dictionary<string, int>()
        {
            {"CheckBox", 0 },
            {"BatchID", 1 },
            {"Location", 2 },
            {"Pouches", 3 },
            {"AlarmRate", 4 },
            {"Rolls", 5 },
            {"Inspection", 6 },
            {"Status", 7 }
        };

        public void WaitForTableLoading()
        {
            try
            {
                Driver.Wait.Until(WaitConditions.TableLoadCompletely(Current));
            }
            catch (WebDriverTimeoutException)
            {
                FW.Log.Warning($"{Current.Name} takes long time to load");
                Console.WriteLine($"{Current.Name} takes long time to load");
            }
        }

        public Elements GetTableRows()
        {
            WaitForTableLoading();           
            return new Elements(Current.FindElements(By.TagName("tr")));
        }

        public List<string> GetTableColumnData(string columnName)
        {
            Elements tableRows = GetTableRows();
            List<string> data = new List<string>();
            for (int i = 1; i < tableRows.Count; i++)
            {
                data.Add(GetTableCell(i, columnName).GetAttribute("innerText"));
            }
            return data;
        }

        /// <summary>
        /// Get poistion of the item in the table by row and column name
        /// </summary>
        /// <param name="row"> row of the item, start from 0 </param>
        /// <param name="columnName"> column name of the item: Checkbox, BatchID, Location, Pouches, AlarmRate, Rolls, Inspection, Status</param>
        /// <returns></returns>
        public Element GetTableCell(int row, string columnName)
        {
            Elements tableRows = GetTableRows();            
            if (dictColumn.TryGetValue(columnName, out int column))
            {
                if (tableRows[row].FindElements(By.TagName("td")).Count == 0)
                {
                    return new Element(tableRows[row].FindElements(By.TagName("th"))[column], $"{columnName} Header");
                }
                return new Element(tableRows[row].FindElements(By.TagName("td"))[column], $"Cell({row},{column})");
            }
            else
            {
                FW.Log.Warning($"{columnName} column is not existed");
                Console.WriteLine($"{columnName} column is not existed");
                return null;
            }
        }

        /// <summary>
        /// Get data of the cell on the table by row and column name
        /// </summary>
        /// <param name="row">row of the item, start from 0</param>
        /// <param name="columnName">column name of the item: Checkbox, BatchID, Location, Pouches, AlarmRate, Rolls, Status</param>
        /// <returns></returns>
        public string GetTableCellData(int row, string columnName)
        {
            return GetTableCell(row, columnName).GetAttribute("innerText").Trim();
        }

        /// <summary>
        /// Get poistion of the item in the table by row and column
        /// </summary>
        /// <param name="row">row of the item, start from 0</param>
        /// <param name="column">column of the item, start from 0</param>
        /// <returns>return Element</returns>
        public Element GetTableCell(int row, int column)
        {
            Elements tableRows = GetTableRows();
            if (tableRows[row].FindElements(By.TagName("td")).Count == 0)
            {
                return new Element(tableRows[row].FindElements(By.TagName("th"))[column], $"Header {column}");
            }
            return new Element(tableRows[row].FindElements(By.TagName("td"))[column], $"Cell({row},{column})");
        }

        /// <summary>
        /// Get data of the cell on the table by row and column name
        /// </summary>
        /// <param name="row">row of the item, start from 0</param>
        /// <param name="column">column of the item, start from 0</param>
        /// <returns></returns>
        public string GetTableCellData(int row, int column)
        {
            return GetTableCell(row, column).GetAttribute("innerText").Trim();
        }

        public Element PreJudgeButtonAtRow(int row)
        {
            return new Element(GetTableRows()[row].FindElement(By.CssSelector("button[id^='prejudge']")), $"PreJudge Button row {row}");
        }

        public Element CorrectButtonAtRow(int row)
        {
            return new Element(GetTableRows()[row].FindElement(By.CssSelector("button[id^='button-b-correct']")), $"Correct Button row {row}");
        }
        

    }
}

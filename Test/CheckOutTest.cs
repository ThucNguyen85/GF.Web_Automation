using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gf.Web.Automation.Page;
using NUnit.Framework;
using FluentAssertions;

namespace Gf.Web.Automation.Test
{
    public class CheckOutTest : TestBase
    {
        public override void BeforeEach()
        {
            base.BeforeEach();
            Pages.Home.GoToCheckOutPage();
            //Enter valid UserName and Password
            Pages.Login.SubmitLogin("admin", "Global@123");
        }

        [Test, Category("Sort Table")]
        public void VerifyTableIsSortedProperly()
        {
            Assert.Multiple(() =>
            {   //order by Pouches ASC                
                Pages.CheckOut.AssertSortBatchTable("Pouches");

               //order by Pouches DESC               
                Pages.CheckOut.AssertSortBatchTable("Pouches");

                //order by BatchID ASC
                Pages.CheckOut.AssertSortBatchTable("BatchID");

                //order by BatchID DESC
                Pages.CheckOut.AssertSortBatchTable("BatchID");

                //order by Rolls ASC
                Pages.CheckOut.AssertSortBatchTable("Rolls");

                //order by Rolls DESC
                Pages.CheckOut.AssertSortBatchTable("Rolls");

                //order by Inspection ASC
                Pages.CheckOut.AssertSortBatchTable("Inspection");

                //order by Inspection DESC
                Pages.CheckOut.AssertSortBatchTable("Inspection");                
            });
        }

        /*  [Test, Category("Verify Batch Details Table")]
          public void VerifyBatchDetailsDisplayProperly()
          {
              Pages.CheckOut.AssertBatchDetailsByPage();
          }
        */
        [Test, Category("Verify Buttons on Batch Details Table")]
        public void VerifyPrejudgeButtonNavigateToPrejudgePage()
        {
            Pages.CheckOut.Map.BatchTable.GetTableCell(0, "Inspection").ClickByJS();


            //Get Batch ID at row 1
            string batchID = Pages.CheckOut.Map.BatchTable.GetTableCellData(1, "BatchID");


            //Click on Prejudge button at row 1
            Pages.CheckOut.Map.BatchTable.PreJudgeButtonAtRow(1).ClickByJS();

            //Wait for Prejudge Page is loaded
            Pages.PreJudge.WaitForPreJudgePageIsLoaded();

            //Close the Batch Compeleted Message if it displays
            Pages.PreJudge.CloseTheBatchCompletedMsg();
            
            //Assert that the the Prejudge page of the selected Batch displayed
            Pages.PreJudge.GetBatchID().Should().Be(batchID, $"BatchID label in Prejudge page should be the same with selected batch in Batch List: {batchID}");
        }
    }
}

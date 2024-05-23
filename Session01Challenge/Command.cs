#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion


namespace Session01Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));

            Transaction t = new Transaction(doc);
            t.Start("Fizbuzz challenge");

            string result = "";

            XYZ offset = new XYZ(0, 6, 0);
            XYZ myPoint = new XYZ(0, 0, 0);


            for (int i = 0; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    result = "FIZZBUZZ";
                }
                else if (i % 5 == 0)
                {
                    result = "FIZZ";
                }
                else if (i % 3 == 0)
                {
                    result = "BUZZ";
                }
                else
                {
                    result = i.ToString();
                }
                TextNote MyTexnote = TextNote.Create(doc, doc.ActiveView.Id, myPoint, result, collector.FirstElementId());
                myPoint = myPoint.Subtract(offset);

            }


            t.Commit();

            return Result.Succeeded;
        }
    }
}
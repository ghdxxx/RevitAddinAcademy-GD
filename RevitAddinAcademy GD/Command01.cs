#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 

#endregion

namespace RevitAddinAcademy_GD
{
    [Transaction(TransactionMode.Manual)]
    public class Command01: IExternalCommand
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

           
          

            double offset = 0.05;

            double offsetcalc = offset * doc.ActiveView.Scale;


            XYZ currentpoint = new XYZ(0, 0, 0);
            XYZ offsetpoint = new XYZ(0, offsetcalc, 0); 

          
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));

            Transaction t = new Transaction(doc, "create text note");
            t.Start();

            int range = 100;

            for (int i = 1; i <= range; i++)
            {
                TextNote Enneadnote = TextNote.Create(doc, doc.ActiveView.Id, currentpoint, "This is Line" + i.ToString(), collector.FirstElementId());
                currentpoint = currentpoint.Subtract(offsetpoint); 

            }


            t.Commit();

            t.Dispose();


            return Result.Succeeded;

        }

        internal double Method01(double a, double b)
        {
            double c = a + b;

            Debug.Print("Got here" + c.ToString());

            return c;
        }
    }
}

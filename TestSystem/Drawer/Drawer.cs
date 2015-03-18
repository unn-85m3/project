using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Drawer
{
    class Drawer
    {
        public static void DrawGraphics(List<Tasks.ITaskPackage> Tasks, List<Algorithm.IAlgorithm> Alghoritms, int Algoritmh, int Task)
        {
            System.Collections.Generic.Dictionary<string, System.Windows.Forms.Form> newMDIChild = new Dictionary<string,System.Windows.Forms.Form>();

            string tmp = Alghoritms[Algoritmh].Name + " - " + Tasks[Task].Name;

            //foreach (System.Windows.Forms.Form frm in System.Windows.Forms.Application.OpenForms)
            //    if (frm.Name == tmp)
            //    {
            //        flg = true;
            //    }
            if (!newMDIChild.ContainsKey(tmp))
            {
                newMDIChild[tmp] = new System.Windows.Forms.Form();
                newMDIChild[tmp].Name = tmp;
                newMDIChild[tmp].Text = tmp;
                newMDIChild[tmp].ClientSize = new System.Drawing.Size(500, 400);
                //newMDIChild[tmp].MdiParent = Form1.ActiveForm;
            }
            else
            {
                //перерисовка
            }
                newMDIChild[tmp].Show();
        }
    }
}

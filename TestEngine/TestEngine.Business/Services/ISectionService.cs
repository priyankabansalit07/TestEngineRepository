using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine.Business.Services
{
    interface ISectionService<Model>
    {
        Model ReadXmlFile(Model obj);

        Model LoadXml(Model obj);

        Model Processing(Model obj);

        void UpdateSec(string attemptid, string scurrentsection, string snotime, string stimerem, string sRevType);

        void UpdateRev(string type, string attemptid);

        //string RevAsciiValues(string varCheck);

        void UpdateCodeAccessHist(string strFilename, string newqueshist);

        void CompleteSection(string attemptid, string scurrentsection);

        Model LoadSectionQuestion(Model obj);

        Model LoadMainQuestion(Model obj);

        Model UpdateQuesHist(Model obj);

        void CreateHidden(string strFilename, string shdnsectionid, string shdnvarid);

        string StoreVal(string val, string str);
    }
}

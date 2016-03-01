using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public partial class RequestBus
    {
        #region passengers

        [Serializable]
        public partial class Passengers
        {
            public string passenger0 { get; set; }
            public string passenger1 { get; set; }
            public string passenger2 { get; set; }
            public string passenger3 { get; set; }
            public string passenger4 { get; set; }
            public string passenger5 { get; set; }
            public string passenger6 { get; set; }
            public string passenger7 { get; set; }
            public string passenger8 { get; set; }
            public string passenger9 { get; set; }

            public string passenger10 { get; set; }
            public string passenger11 { get; set; }
            public string passenger12 { get; set; }
            public string passenger13 { get; set; }
            public string passenger14 { get; set; }
            public string passenger15 { get; set; }
            public string passenger16 { get; set; }
            public string passenger17 { get; set; }
            public string passenger18 { get; set; }
            public string passenger19 { get; set; }

            public string passenger20 { get; set; }
            public string passenger21 { get; set; }
            public string passenger22 { get; set; }
            public string passenger23 { get; set; }
            public string passenger24 { get; set; }
            public string passenger25 { get; set; }
            public string passenger26 { get; set; }
            public string passenger27 { get; set; }
            public string passenger28 { get; set; }
            public string passenger29 { get; set; }

            public string passenger30 { get; set; }
            public string passenger31 { get; set; }
            public string passenger32 { get; set; }
            public string passenger33 { get; set; }
            public string passenger34 { get; set; }
            public string passenger35 { get; set; }
            public string passenger36 { get; set; }
            public string passenger37 { get; set; }
            public string passenger38 { get; set; }
            public string passenger39 { get; set; }

        }

        /// <summary>
        /// convert passengers to dss for getting sign name.
        /// </summary>
        /// <param name="p">the passengers object</param>
        /// <param name="openWith">the dictionary</param>
        /// <example>
        /// <code language="C#" description="convert passengers to dss">
        /// RequestBus.PackagePassengersToDSS(p:p, specifiedFields:specifiedFields, openWith: out openWith);
        /// </code>
        /// </example>
        private static void PackagePassengersToDSS(Passengers p, out Dictionary<string, string> openWith, Dictionary<string, string> specifiedFields = null, string SignNameKey = "Sign", Dictionary<string, string> keyIDpair = null)
        {
            Dictionary<string, string> _dss = new Dictionary<string, string>();
            specifiedFields = specifiedFields ?? new Dictionary<string, string>() { };
            keyIDpair = keyIDpair ?? new Dictionary<String, String>();
            RF.GlobalClass.Utils.Do.getPropertyNamesOfObject(p).Select(delegate(string _name)
            {
                string result = RF.GlobalClass.Utils.Do.MagicProperty<string>(p, _name)();
                try
                {
                    string _tmp = String.Empty;
                    if (keyIDpair.TryGetValue(_name, out _tmp))
                    {
                        _name = _tmp;
                    }
                    else { }
                    if ((specifiedFields.Keys.Count == 0 && SignNameKey != _name))
                    {
                        _dss.Add(_name, "" + result + "");
                    }
                    else if ((specifiedFields.TryGetValue(_name, out _tmp)))
                    {
                        if (!String.IsNullOrEmpty(_tmp))
                        {
                            _name = _tmp;
                        }
                        else { }
                        _dss.Add(_name, "" + result + "");
                    }
                    else { }
                    if (null == result)
                    {
                        p.GetType().GetProperty(_name).SetValue(p, "", null);
                    }
                    else { }
                }
                catch (Exception ex) { }
                return result;
            }).ToArray();
            openWith = _dss;
        }

        #endregion
    
    }
}

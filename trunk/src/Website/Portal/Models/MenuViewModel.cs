using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Models
{
    public class MenuViewModel
    {
        private JArray _sb = null;
        public JArray SB
        {
            get
            {
                return _sb;
            }

            set
            {
                _sb = value;
            }
        }

        private JObject _transitions = null;
        public JObject Transitions
        {
            get
            {
                return _transitions;
            }

            set
            {
                _transitions = value;
            }
        }
    }
}
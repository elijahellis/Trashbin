using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;

/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://sam.zoy.org/wtfpl/COPYING for more details. */ 

namespace Trashbin
{
    class PastebinItem
    {
        private NameValueCollection _valsCollection = new NameValueCollection();

        public PastebinItem(string context)
        {
            _valsCollection.Add("paste_code", context);
            _valsCollection.Add("paste_email", "");
            _valsCollection.Add("paste_name","");
            _valsCollection.Add("paste_expire_date", "");
            _valsCollection.Add("paste_format", "");
            _valsCollection.Add("paste_subdomain", "");
        }

        public string Email
        {
            get
            {
                return _valsCollection["paste_email"];
            }
            set
            {
                _valsCollection["paste_email"] = value;
            }
        }

        public string Name
        {
            get
            {
                return _valsCollection["paste_name"];
            }
            set
            {
                _valsCollection["paste_name"] = value;
            }
        }

        public string Expiration
        {
            get
            {
                return _valsCollection["paste_expire_date"];
            }
            set
            {
                _valsCollection["paste_expire_date"] = value;
            }
        }

        public string Subdomain
        {
            get
            {
                return _valsCollection["paste_subdomain"];
            }
            set
            {
                _valsCollection["paste_subdomain"] = value;
            }
        }

        public string Format
        {
            get
            {
                return _valsCollection["paste_format"];
            }
            set
            {
                _valsCollection["paste_format"] = value;
            }
        }

        public bool Private
        {
            get
            {
                return bool.Parse(_valsCollection["paste_private"]);
            }
            set
            {
                if (value == true)
                {
                    _valsCollection["paste_name"] = "1";
                }
                else
                {
                    _valsCollection["paste_name"] = "0";
                }
            }
        }

        public string Paste()
        {
            return new StreamReader(BuildRequest( _valsCollection).GetResponse().GetResponseStream()).ReadToEnd();
        }

        private HttpWebRequest BuildRequest(NameValueCollection content)
        {
            var newHTTP = (HttpWebRequest)HttpWebRequest.Create("http://pastebin.com/api_public.php");
            newHTTP.Method = "POST";
            newHTTP.ContentType = "application/x-www-form-urlencoded";
            
                StringBuilder buildItem = new StringBuilder();
                foreach (string val in content)
                {
                    buildItem.Append(val + "=");
                    buildItem.Append(content[val] + "&");                    
                }

                byte[] encodedString= Encoding.UTF8.GetBytes(buildItem.ToString().TrimEnd("&".ToCharArray()));

                newHTTP.ContentLength = encodedString.Length;
                newHTTP.GetRequestStream().Write(encodedString, 0, (int)encodedString.Length);
                //System.Windows.Forms.MessageBox.Show(Encoding.UTF8.GetString(encodedString));
            
            
            return newHTTP;
        }

    }
}

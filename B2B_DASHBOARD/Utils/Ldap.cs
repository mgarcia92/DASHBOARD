using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Utils
{
    public class Ldap
    {
        private bool _success;

        public bool success
        {
            get { return _success; }
        }

        private DirectoryEntry root { get; set; }
        private DirectorySearcher search { get; set; }
        private string dc { get; set; }

        public Ldap()
        {
            dc = ConfigurationManager.AppSettings["domain"].ToString();
        }

        public void FindOne(string User,string Password)
        {
            using (DirectoryEntry root = ConnectLdap(User,Password))
            {
                _success = false;
                //BUSQUEDA EN ACTIVE DIRECTORY
                string query = $"(sAMAccountName={User})";
                DirectorySearcher search = new DirectorySearcher(root, query);
                try
                {
                    search.FindOne();
                    _success = true;
                }
                catch
                {
                    _success = false;
                }

            }
            
        }

        private DirectoryEntry ConnectLdap(string user,string pass)
        {
            string path = $"LDAP://{dc}";
            using (DirectoryEntry root = new DirectoryEntry(path,user,pass))
            {
                this.root = root;
                return root;
            }
        }

        public Image GetUserPicture(string userName ,string Pass)
        {
            
            using (DirectoryEntry root = ConnectLdap(userName,Pass))
            {
                using (var dsSearcher = new DirectorySearcher(root))
                {

                    dsSearcher.Filter = $"(&(objectClass=user)(objectCategory=Person)(samaccountname={userName}))";
                    dsSearcher.PropertiesToLoad.Add("thumbnailPhoto");
                    SearchResult result = dsSearcher.FindOne();

                    if (result != null)
                    {
                        byte[] data = result.Properties["thumbnailPhoto"][0] as byte[];

                        if (data != null)
                            using (var s = new MemoryStream(data))
                                return Bitmap.FromStream(s);

                    }

                    return null;
                }
            }
             
        }
    }
}
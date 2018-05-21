using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
    public class FileUpload
    {
        public FileUpload()
        {
            Data = string.Empty;
            FileName = string.Empty;
            MD5 = string.Empty;
        }

        public string Data { get; set; }
        public string FileName { get; set; }
        public string MD5 { get; set; }
    }
    public class FileResult
    {
        /// <summary>
        /// Gets or sets the local path of the file saved on the server.
        /// </summary>
        /// <value>
        /// The local path.
        /// </value>
        public IEnumerable<string> FileNames { get; set; }

        /// <summary>
        /// Gets or sets the submitter as indicated in the HTML form used to upload the data.
        /// </summary>
        /// <value>
        /// The submitter.
        /// </value>
        public string Submitter { get; set; }
    }
}

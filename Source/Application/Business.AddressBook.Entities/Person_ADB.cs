using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.AddressBook.Entities
{
    public class Person_ADB
    {
        #region Fields & Properties

        public Guid PersonId { get; set; }
        public bool Archived { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }

        #endregion






        #region Constructors

        public Person_ADB()
        {
            PersonId = Guid.NewGuid();
        }

        #endregion






        #region Validators

        //public override IEnumerable<ChError> GetErrors()
        //{
        //    if ((string.IsNullOrEmpty(this.FirstName)) || (this.FirstName.Trim() == ""))
        //    {
        //        yield return new ChError("FirstName", "1", "Person");
        //    }
        //    else
        //    {
        //        if (this.FirstName.Length > 50)
        //        {
        //            yield return new ChError("FirstName", "2", "Person");
        //        }
        //        string allowedChars = "'";
        //        foreach (char charFirstName in this.FirstName)
        //        {
        //            if (allowedChars.Contains(charFirstName))
        //            {
        //                yield return new ChError("FirstName", "3", "Person");
        //                break;
        //            }
        //        }
        //    }

        //    if ((string.IsNullOrEmpty(this.LastName)) || (this.LastName.Trim() == ""))
        //    {
        //        yield return new ChError("LastName", "1", "Person");
        //    }
        //    else
        //    {
        //        if (this.LastName.Length > 100)
        //        {
        //            yield return new ChError("LastName", "2", "Person");
        //        }
        //        string allowedChars = "'";
        //        foreach (char charLastName in this.LastName)
        //        {
        //            if (allowedChars.Contains(charLastName))
        //            {
        //                yield return new ChError("LastName", "3", "Person");
        //                break;
        //            }
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(this.HomePhone))
        //    {
        //        if (this.HomePhone.Length > 100)
        //        {
        //            yield return new ChError("HomePhone", "1", "Person");
        //        }
        //        string allowedChars = "01234567890+-() ";
        //        foreach (char charPhone in this.HomePhone)
        //        {
        //            if (!allowedChars.Contains(charPhone))
        //            {
        //                yield return new ChError("HomePhone", "2", "Person");
        //                break;
        //            }
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(this.CellPhone))
        //    {
        //        if (this.CellPhone.Length > 50)
        //        {
        //            yield return new ChError("CellPhone", "1", "Person");
        //        }
        //        string allowedChars = "01234567890+-() ";
        //        foreach (char charCellPhone in this.CellPhone)
        //        {
        //            if (!allowedChars.Contains(charCellPhone))
        //            {
        //                yield return new ChError("CellPhone", "2", "Person");
        //                break;
        //            }
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(this.Email))
        //    {
        //        //string expresion = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        //        //string expresion = @"(\w+@[a-zA-Z0-9]+?\.[a-zA-Z]{2,6})";
        //        string expresion = @"(\w+@[a-zA-Z0-9\-_]+?\.[a-zA-Z]{2,6})";
        //        Regex regex = new Regex(expresion);
        //        if (!Regex.IsMatch(this.Email, expresion))
        //        {
        //            yield return new ChError("Email", "1", "Person");
        //        }
        //        if (this.Email.Length > 50)
        //        {
        //            yield return new ChError("Email", "2", "Person");
        //        }
        //    }
        //}

        #endregion
    }
}

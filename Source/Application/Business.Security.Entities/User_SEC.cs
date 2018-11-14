using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.Security.Entities
{
    public class User_SEC
    {
        #region Fields & Properties

        public Guid UserId { get; set; }
        public bool Archived { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public virtual UserState_SEC UserState { get; set; }
        public virtual Guid OwnerId { get; set; }
        public virtual Guid UserTypeId { get; set; }
        public virtual User_SEC Owner { get; set; }
        public virtual UserType_SEC UserType { get; set; }

        #endregion






        #region Constructors

        public User_SEC()
        {
            UserId = Guid.NewGuid();
        }

        #endregion






        #region Validators

        //public override IEnumerable<ChError> GetErrors()
        //{
        //    if ((string.IsNullOrEmpty(this.Password)) || (this.Password.Trim() == ""))
        //    {
        //        yield return new ChError("Password", "1", "User");
        //    }
        //    else
        //    {
        //        if (this.Password.Length < 4)
        //        {
        //            yield return new ChError("Password", "2", "User");
        //        }
        //        foreach (char charPass in this.Password)
        //        {
        //            if (charPass != '1' && charPass != '2' && charPass != '3' && charPass != '4' && charPass != '5' && charPass != '6' && charPass != '7' && charPass != '8' && charPass != '9' && charPass != '0'
        //                && charPass != 'a' && charPass != 'b' && charPass != 'c' && charPass != 'd' && charPass != 'e' && charPass != 'f' && charPass != 'g' && charPass != 'h' && charPass != 'i' && charPass != 'j'
        //                && charPass != 'k' && charPass != 'l' && charPass != 'm' && charPass != 'n' && charPass != 'ñ' && charPass != 'o' && charPass != 'p' && charPass != 'q' && charPass != 'r' && charPass != 's'
        //                && charPass != 't' && charPass != 'u' && charPass != 'v' && charPass != 'w' && charPass != 'x' && charPass != 'y' && charPass != 'z'
        //                && charPass != 'A' && charPass != 'B' && charPass != 'C' && charPass != 'D' && charPass != 'E' && charPass != 'F' && charPass != 'G' && charPass != 'H' && charPass != 'I' && charPass != 'J'
        //                && charPass != 'K' && charPass != 'L' && charPass != 'M' && charPass != 'N' && charPass != 'Ñ' && charPass != 'O' && charPass != 'P' && charPass != 'Q' && charPass != 'R' && charPass != 'S'
        //                && charPass != 'T' && charPass != 'U' && charPass != 'V' && charPass != 'W' && charPass != 'X' && charPass != 'Y' && charPass != 'Z'
        //                && charPass != '!' && charPass != '@' && charPass != '#' && charPass != '$' && charPass != '%' && charPass != '^' && charPass != '&' && charPass != '*' && charPass != '(' && charPass != ')'
        //                && charPass != '-' && charPass != '_' && charPass != '=' && charPass != '+' && charPass != '/' && charPass != '<' && charPass != '>' && charPass != '.' && charPass != ',' && charPass != '"' && charPass != '\'')
        //            {
        //                yield return new ChError("Password", "3", "User");
        //                break;
        //            }
        //        }
        //    }
        //    if ((string.IsNullOrEmpty(this.ConfirmPassword)) || (this.ConfirmPassword.Trim() == ""))
        //    {
        //        yield return new ChError("PasswordConfirm", "1", "User");
        //    }
        //    else
        //    {
        //        if (this.ConfirmPassword.Length < 4)
        //        {
        //            yield return new ChError("PasswordConfirm", "2", "User");
        //        }
        //        foreach (char charPass in this.ConfirmPassword)
        //        {
        //            if (charPass != '1' && charPass != '2' && charPass != '3' && charPass != '4' && charPass != '5' && charPass != '6' && charPass != '7' && charPass != '8' && charPass != '9' && charPass != '0'
        //                && charPass != 'a' && charPass != 'b' && charPass != 'c' && charPass != 'd' && charPass != 'e' && charPass != 'f' && charPass != 'g' && charPass != 'h' && charPass != 'i' && charPass != 'j'
        //                && charPass != 'k' && charPass != 'l' && charPass != 'm' && charPass != 'n' && charPass != 'ñ' && charPass != 'o' && charPass != 'p' && charPass != 'q' && charPass != 'r' && charPass != 's'
        //                && charPass != 't' && charPass != 'u' && charPass != 'v' && charPass != 'w' && charPass != 'x' && charPass != 'y' && charPass != 'z'
        //                && charPass != 'A' && charPass != 'B' && charPass != 'C' && charPass != 'D' && charPass != 'E' && charPass != 'F' && charPass != 'G' && charPass != 'H' && charPass != 'I' && charPass != 'J'
        //                && charPass != 'K' && charPass != 'L' && charPass != 'M' && charPass != 'N' && charPass != 'Ñ' && charPass != 'O' && charPass != 'P' && charPass != 'Q' && charPass != 'R' && charPass != 'S'
        //                && charPass != 'T' && charPass != 'U' && charPass != 'V' && charPass != 'W' && charPass != 'X' && charPass != 'Y' && charPass != 'Z'
        //                && charPass != '!' && charPass != '@' && charPass != '#' && charPass != '$' && charPass != '%' && charPass != '^' && charPass != '&' && charPass != '*' && charPass != '(' && charPass != ')'
        //                && charPass != '-' && charPass != '_' && charPass != '=' && charPass != '+' && charPass != '/' && charPass != '<' && charPass != '>' && charPass != '.' && charPass != ',' && charPass != '"' && charPass != '\'')
        //            {
        //                yield return new ChError("ConfirmPassword", "3", "User");
        //                break;
        //            }
        //        }
        //    }
        //    if (this.ConfirmPassword != this.Password)
        //    {
        //        yield return new ChError("PasswordConfirm", "4", "User");
        //    }
        //    if (UserState <= 0)
        //    {
        //        yield return new ChError("UserState", "1", "User");
        //    }
        //    if (Roles == null || Roles.Count() == 0)
        //    {
        //        yield return new ChError("Roles", "1", "User");
        //    }
        //    else
        //    {
        //        List<Guid> ids = new List<Guid>();
        //        for (int i = 0; i < Roles.Count(); i++)
        //        {
        //            if (Roles[i] == null)
        //            {
        //                yield return new ChError("Roles", "2", "User[" + i + "]");
        //            }
        //            else
        //            {
        //                if (ids.Exists(e => e == Roles[i].EntityId) == false)
        //                {
        //                    ids.Add(Roles[i].EntityId);
        //                }
        //                else
        //                {
        //                    yield return new ChError("Roles", "3", "User[" + i + "]");
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion

    }
}

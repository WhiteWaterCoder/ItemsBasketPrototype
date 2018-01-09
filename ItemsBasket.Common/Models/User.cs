using System.Collections.Generic;

namespace ItemsBasket.Common.Models
{
    public class User
    {
        public static readonly User Empty = new User(-1, "", "", "");

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityToken { get; set; }

        public User(int id, string userName, string password, string securityToken)
        {
            Id = id;
            UserName = userName;
            Password = password;
            SecurityToken = securityToken;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   Id == user.Id &&
                   UserName == user.UserName &&
                   Password == user.Password &&
                   SecurityToken == user.SecurityToken;
        }

        public override int GetHashCode()
        {
            var hashCode = -983945063;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecurityToken);
            return hashCode;
        }

        public override string ToString()
        {
            return $"ID: {Id} - UserName: {UserName}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace vp4_proxy
{
    public enum Role { customer, admin }
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [JsonConverter(typeof(UserRoleConverter))]
        public Role role { get; set; }
        public User()
        {

        }
        public User(string firstName, string lastName, string email, string password, Role role)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.role = role;
        }
        public void ReadJson(ICollection collection)
        {
            collection.ReadJsonFile();
        }
        public void Sort(ICollection collection)
        {

             collection.Sort();
        }
        public void Search(ICollection collection)
        {

           collection.Search();
        }
        public void Add(ICollection collection)
        {
           collection.Append();
        }
        public void Delete(ICollection collection)
        {
            collection.Delete();
        }
        public void Edit(ICollection collection)
        {
            collection.Edit();
        }
        public void WriteInJson(ICollection collection)
        {
            collection.WriteToJson();
        }
        public void Show(ICollection collection)
        {
            collection.Show();
        }
        public void getById(ICollection collection)
        {
            collection.getById();
        }
    }
    public class UserRoleConverter : JsonConverter<Role>
    {
        public override Role Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            if (Enum.TryParse<Role>(value, out Role result))
            {
                return result;
            }
            throw new JsonException($"Unable to parse UserRole value: {value}");
        }
        public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

    }
}
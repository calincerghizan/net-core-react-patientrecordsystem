namespace PatientRecordSystem.BLL.Models
{
    public class MetaData
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}

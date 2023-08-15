using System;
namespace JumioDemo.Models.Retrieval
{
    public class Address
    {
        public string line1 { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string subdivision { get; set; }
        public string city { get; set; }
        public string formattedAddress { get; set; }
    }

    public class Capabilities
    {
        public List<Extraction> extraction { get; set; }
        public List<Similarity> similarity { get; set; }
        public List<Liveness> liveness { get; set; }
        public List<DataCheck> dataChecks { get; set; }
        public List<ImageCheck> imageChecks { get; set; }
        public List<Usability> usability { get; set; }
    }

    public class Credential
    {
        public string id { get; set; }
        public string category { get; set; }
        public List<Part> parts { get; set; }
    }

    public class Data
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string issuingCountry { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string issuingDate { get; set; }
        public string documentNumber { get; set; }
        public string state { get; set; }
        public Address address { get; set; }
        public string gender { get; set; }
        public string currentAge { get; set; }
        public string similarity { get; set; }
        public int predictedAge { get; set; }
        public string ageConfidenceRange { get; set; }
        public FaceSearchFindings faceSearchFindings { get; set; }
    }

    public class DataCheck
    {
        public string id { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
    }

    public class Decision
    {
        public string type { get; set; }
        public Details details { get; set; }
        public Risk risk { get; set; }
    }

    public class Details
    {
        public string label { get; set; }
    }

    public class Extraction
    {
        public string id { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
        public Data data { get; set; }
    }

    public class FaceSearchFindings
    {
        public string status { get; set; }
        public List<string> findings { get; set; }
    }

    public class ImageCheck
    {
        public string id { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
        public Data data { get; set; }
    }

    public class Liveness
    {
        public string id { get; set; }
        public string validFaceMapForAuthentication { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
        public Data data { get; set; }
    }

    public class Part
    {
        public string classifier { get; set; }
        public string href { get; set; }
    }

    public class Risk
    {
        public double score { get; set; }
    }

    public class Root
    {
        public Workflow workflow { get; set; }
        public Account account { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime startedAt { get; set; }
        public DateTime completedAt { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
        public Steps steps { get; set; }
        public Capabilities capabilities { get; set; }
    }

    public class Similarity
    {
        public string id { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
        public Data data { get; set; }
    }

    public class Steps
    {
        public string href { get; set; }
    }

    public class Usability
    {
        public string id { get; set; }
        public List<Credential> credentials { get; set; }
        public Decision decision { get; set; }
    }

    public class Workflow
    {
        public string id { get; set; }
        public string status { get; set; }
        public string definitionKey { get; set; }
        public string userReference { get; set; }
        public string customerInternalReference { get; set; }
    }
}


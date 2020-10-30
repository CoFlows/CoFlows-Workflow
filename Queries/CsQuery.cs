using Python.Runtime;
using JVM;

using System;
using System.Linq;

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;





/// <info version="1.0.100">
///     <title>CsQuery Test API</title>
///     <description>CsQuery Test API</description>
///     <termsOfService url="https://www.coflows.com"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflows.com" email="arturo@coflows.com"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

public class CsQuery
{   
    public class Resource
    {
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public string MeterCategory { get; set; }
        public string MeterSubCategory { get; set; }
        public string Unit { get; set; }
        public Dictionary<double, double> MeterRates { get; set; }
        public string EffectiveDate { get; set; }
        public List<string> MeterTags { get; set; }
        public string MeterRegion { get; set; }
        public double IncludedQuantity { get; set; }

    }

    public class RateCardPayload
    {
        public List<object> OfferTerms { get; set; }
        public List<Resource> Meters { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public string RatingDate { get; set; }
        public bool IsTaxIncluded { get; set; }
    }

    /// <api name="getName">
    ///     <description> Function that returns a name </description>
    ///     <returns> returns an string-- </returns>
    ///     <permissions>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="0.2" currency="USD" type="hourly"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="20" currency="USD" type="percall"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="30" permission="write"/>
    ///     </permissions>
    /// </api>
    public static string getName()
    {
        return "something";
    }

    /// <api name="Add">
    ///     <description> Function that adds two numbers </description>
    ///     <returns> returns an integer </returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    /// </api>
    public static int Add(int x, int y)
    {
        var indicatorInfoTable = QuantApp.Kernel.Database.DB["DefaultStrategy"].GetDataTable("CMIndicatorInfoList", null, null);
        var rows = indicatorInfoTable.Rows;
        foreach (var dr in rows)
        {
        }
        return x + y;
    }

    public static void ResizeVM(string authFile, string rgGroup, string vmName, string size)
        {
            var authObj = JObject.Parse(authFile);

            var t0 = DateTime.Now;
            string res = "";
            Task.Run(async () => {   
                using(HttpClient httpClient = new HttpClient()){
                    httpClient.Timeout = Timeout.InfiniteTimeSpan;
                    
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var nvc = new List<KeyValuePair<string, string>>();
                    nvc.Add(new KeyValuePair<string, string>("resource", "https://management.core.windows.net/"));
                    nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                    
                    nvc.Add(new KeyValuePair<string, string>("client_id", authObj["clientId"].ToString()));
                    nvc.Add(new KeyValuePair<string, string>("client_secret", authObj["clientSecret"].ToString()));
                    var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/" + authObj["tenantId"].ToString() + "/oauth2/token") { Content = new FormUrlEncodedContent(nvc) };
                    
                    var data = await httpClient.SendAsync(req);
                    var dd = await data.Content.ReadAsStringAsync();
                    Console.WriteLine(dd);

                    dynamic d = JObject.Parse(dd.ToString());
                    string access_code = d.access_token;

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                    Console.WriteLine("------- RESIZE: " + "https://management.azure.com/subscriptions/" + authObj["subscriptionId"].ToString() + "/resourceGroups/" + rgGroup + "/providers/Microsoft.Compute/virtualMachines/" + vmName + "?api-version=2019-12-01");

                    req = new HttpRequestMessage(HttpMethod.Patch, "https://management.azure.com/subscriptions/" + authObj["subscriptionId"].ToString() + "/resourceGroups/" + rgGroup + "/providers/Microsoft.Compute/virtualMachines/" + vmName + "?api-version=2019-12-01") {
                        Content = new StringContent(JsonConvert.SerializeObject(new {properties = new {
                            hardwareProfile = new {
                            vmSize = size
                            }
                        }
                        }), System.Text.Encoding.UTF8, "application/json")
                    };
                    data = await httpClient.SendAsync(req);
                    res = await data.Content.ReadAsStringAsync();

                    Console.WriteLine("------- RESULT RESIZE");
                    Console.WriteLine(res);
                }
            }).Wait();
        }

    private static object ManageVM()
    {

        string authFile =
"{" +
"  \"clientId\": \"bcce5da0-f5bf-49f7-8678-c972718aa49c\"," +
"  \"clientSecret\": \"f762ec63-40f9-4146-9c46-5240a0526461\"," +
"  \"subscriptionId\": \"e5aca585-e1c7-461b-897a-ac633074a4a0\"," +
"  \"tenantId\": \"33919ee6-6888-4f32-a73c-878df64055fc\"," +
"  \"activeDirectoryEndpointUrl\": \"https://login.microsoftonline.com\"," +
"  \"resourceManagerEndpointUrl\": \"https://management.azure.com/\"," +
"  \"activeDirectoryGraphResourceId\": \"https://graph.windows.net/\"," +
"  \"sqlManagementEndpointUrl\": \"https://management.core.windows.net:8443/\"," +
"  \"galleryEndpointUrl\": \"https://gallery.azure.com/\"," +
"  \"managementEndpointUrl\": \"https://management.core.windows.net/\"" +
"}";

        ResizeVM(authFile, "MC_coflows-worker-15756_coflows-worker-15756-cluster_australiaeast", "aks-ap-52230401-0", "Standard_D2S_V4");

        return "";

        // var authObj = JObject.Parse(authFile);

        // var t0 = DateTime.Now;
        // string res = "";
        // Task.Run(async () => {   
        //     using(HttpClient httpClient = new HttpClient()){
        //         httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
        //         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        //         var nvc = new List<KeyValuePair<string, string>>();
        //         nvc.Add(new KeyValuePair<string, string>("resource", "https://management.core.windows.net/"));
        //         nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                 
        //         nvc.Add(new KeyValuePair<string, string>("client_id", authObj["clientId"].ToString()));
        //         nvc.Add(new KeyValuePair<string, string>("client_secret", authObj["clientSecret"].ToString()));
        //         var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/" + authObj["tenantId"].ToString() + "/oauth2/token") { Content = new FormUrlEncodedContent(nvc) };
                
        //         var data = await httpClient.SendAsync(req);
        //         var dd = await data.Content.ReadAsStringAsync();
        //         Console.WriteLine(dd);

        //         dynamic d = JObject.Parse(dd.ToString());
        //         string access_code = d.access_token;

        //         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

        //         // req = new HttpRequestMessage(HttpMethod.Get, "https://management.azure.com/subscriptions/e5aca585-e1c7-461b-897a-ac633074a4a0/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003p' and Currency eq '" + ccy + "' and Locale eq 'en-US' and RegionInfo eq 'US'");
        //         // req = new HttpRequestMessage(HttpMethod.Get, "https://management.azure.com/subscriptions/" + authObj["subscriptionId"].ToString() + "/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003p' and Currency eq '" + ccy + "' and Locale eq 'en-US' and RegionInfo eq 'US'");                
        //         req = new HttpRequestMessage(HttpMethod.Patch, "https://management.azure.com/subscriptions/e5aca585-e1c7-461b-897a-ac633074a4a0/resourceGroups/MC_coflows-worker-15756_coflows-worker-15756-cluster_australiaeast/providers/Microsoft.Compute/virtualMachines/aks-ap-52230401-0?api-version=2019-12-01") {
        //                 Content = new StringContent(JsonConvert.SerializeObject(new {properties = new {
        //                     hardwareProfile = new {
        //                        vmSize = "Standard_DS1_V2"
        //                     }
        //                 }
        //                 }), System.Text.Encoding.UTF8, "application/json")
        //             };
        //         // req = new HttpRequestMessage(HttpMethod.Get, "https://management.azure.com/subscriptions/e5aca585-e1c7-461b-897a-ac633074a4a0/resourceGroups/MC_coflows-worker-15756_coflows-worker-15756-cluster_australiaeast/providers/Microsoft.Compute/virtualMachines/aks-ap-52230401-0?api-version=2019-12-01");
        //         data = await httpClient.SendAsync(req);
        //         res = await data.Content.ReadAsStringAsync();
        //     }
        // }).Wait();

        // Console.WriteLine(res);

        // return JsonConvert.SerializeObject(JObject.Parse(res), Formatting.Indented);
    }


    public static object RateCard()
    {

        string authFile =
"{" +
"  \"clientId\": \"bcce5da0-f5bf-49f7-8678-c972718aa49c\"," +
"  \"clientSecret\": \"f762ec63-40f9-4146-9c46-5240a0526461\"," +
"  \"subscriptionId\": \"e5aca585-e1c7-461b-897a-ac633074a4a0\"," +
"  \"tenantId\": \"33919ee6-6888-4f32-a73c-878df64055fc\"," +
"  \"activeDirectoryEndpointUrl\": \"https://login.microsoftonline.com\"," +
"  \"resourceManagerEndpointUrl\": \"https://management.azure.com/\"," +
"  \"activeDirectoryGraphResourceId\": \"https://graph.windows.net/\"," +
"  \"sqlManagementEndpointUrl\": \"https://management.core.windows.net:8443/\"," +
"  \"galleryEndpointUrl\": \"https://gallery.azure.com/\"," +
"  \"managementEndpointUrl\": \"https://management.core.windows.net/\"" +
"}";

        var authObj = JObject.Parse(authFile);

        var regions = new Dictionary<string, string>(){
                {"eastasia", "AP East"},
                {"southeastasia", "AP Southeast"},
                {"centralus", "US Central"},
                {"eastus", "US East"},
                {"eastus2", "US East 2"},
                {"westus", "US West"},
                {"northcentralus", "US North Central"},
                {"southcentralus", "US South Central"},
                {"northeurope", "EU North"},
                {"westeurope", "EU West"},
                {"japanwest", "JA West"},
                {"japaneast", "JA East"},
                {"brazilsouth", "BR South"},
                {"australiaeast", "AU East"},
                {"australiasoutheast", "AU Southeast"},
                {"southindia", "IN South"},
                {"centralindia", "IN Central"},
                {"westindia", "IN West"},
                {"canadacentral", "CA Central"},
                {"canadaeast", "CA East"},
                {"uksouth", "UK South"},
                {"ukwest", "UK West"},
                {"westcentralus", "US West Central"},
                {"westus2", "US West 2"},
                {"koreacentral", "KR Central"},
                {"koreasouth", "KR South"},
                {"francecentral", "FR Central"},
                {"francesouth", "FR South"},
                {"australiacentral", "AU Central"},
                {"australiacentral2", "AU Central 2"},
                {"uaecentral", "AE Central"},
                {"uaenorth", "AE North"},
                {"southafricanorth", "ZA North"},
                {"southafricawest", "ZA West"},
                {"germanynorth", "DE North"},
                {"germanywestcentral", "DE West Central"},
                {"norwaywest", "NO West"},
                {"norwayeast", "NO East"}
            };
        var t0 = DateTime.Now;
        string res = "";
        Task.Run(async () => {   
            using(HttpClient httpClient = new HttpClient()){
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("resource", "https://management.core.windows.net/"));
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                 
                nvc.Add(new KeyValuePair<string, string>("client_id", authObj["clientId"].ToString()));
                nvc.Add(new KeyValuePair<string, string>("client_secret", authObj["clientSecret"].ToString()));
                var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/" + authObj["tenantId"].ToString() + "/oauth2/token") { Content = new FormUrlEncodedContent(nvc) };
                // nvc.Add(new KeyValuePair<string, string>("client_id", "bcce5da0-f5bf-49f7-8678-c972718aa49c"));
                // nvc.Add(new KeyValuePair<string, string>("client_secret", "f762ec63-40f9-4146-9c46-5240a0526461"));
                // var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/33919ee6-6888-4f32-a73c-878df64055fc/oauth2/token") { Content = new FormUrlEncodedContent(nvc) };
                
                var data = await httpClient.SendAsync(req);
                var dd = await data.Content.ReadAsStringAsync();
                Console.WriteLine(dd);

                dynamic d = JObject.Parse(dd.ToString());
                string access_code = d.access_token;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                //var ccy = "AUD";
                //var location = "AU";
                
                var ccy = "USD";
                var location = "AU";

                // req = new HttpRequestMessage(HttpMethod.Get, "https://management.azure.com/subscriptions/e5aca585-e1c7-461b-897a-ac633074a4a0/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003p' and Currency eq '" + ccy + "' and Locale eq 'en-US' and RegionInfo eq 'US'");
                req = new HttpRequestMessage(HttpMethod.Get, "https://management.azure.com/subscriptions/" + authObj["subscriptionId"].ToString() + "/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003p' and Currency eq '" + ccy + "' and Locale eq 'en-US' and RegionInfo eq '" + location + "'");                
                data = await httpClient.SendAsync(req);
                res = await data.Content.ReadAsStringAsync();
            }
        }).Wait();

        // Console.WriteLine(res);

        // var users = JObject.Parse(res);

        var t1 = DateTime.Now;

        
        var rateCard = JsonConvert.DeserializeObject<RateCardPayload>(res);

        var t2 = DateTime.Now;

        Console.WriteLine("-----------: " + (t1 - t0).TotalSeconds + " <-> " + (t2 - t1).TotalSeconds);

        var vms = "Standard_DS3_v2".Replace("Standard_", "").Replace("_", " ").ToLower();
        var reg = "australiaeast";


        
        // return rateCard.Meters.Where(x => x.MeterRegion != "" && x.MeterCategory == "Virtual Machines" && !x.MeterName.Contains("Low Priority") && !x.MeterSubCategory.Contains("Windows")).Select(x => new { Name = x.MeterName, Rate = x.MeterRates[0], Region = x.MeterRegion });
        return rateCard.Meters
                    .Where(x => x.MeterRegion == regions[reg] && x.MeterName.ToLower().Contains(vms))
                    .Where(x => x.MeterCategory == "Virtual Machines" && !x.MeterName.Contains("Low Priority") && !x.MeterName.Contains("- Expired") && !x.MeterSubCategory.Contains("Windows")).Select(x => new { Name = x.MeterName, Rate = x.MeterRates[0], Region = x.MeterRegion });
    }

    public static List<object> GraphUsers()
    {
        string res = "";
        Task.Run(async () => {   
            using(HttpClient httpClient = new HttpClient()){
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"));
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                
                nvc.Add(new KeyValuePair<string, string>("client_id", "ec388b90-777a-4396-afdb-431333ad0bd9"));
                nvc.Add(new KeyValuePair<string, string>("client_secret", "vnFEMw~_X4_b5aa~u9~4V9O1HEtFA3VgyD"));

                var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/coflowsad.onmicrosoft.com/oauth2/v2.0/token") { Content = new FormUrlEncodedContent(nvc) };
                var data = await httpClient.SendAsync(req);
                var dd = await data.Content.ReadAsStringAsync();

                dynamic d = JObject.Parse(dd.ToString());
                string access_code = d.access_token;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users?$select=id,identities,surname,givenName");
                // req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users");
                data = await httpClient.SendAsync(req);
                res = await data.Content.ReadAsStringAsync();
            }
        }).Wait();

        Console.WriteLine(res);

        var users = JObject.Parse(res);

        var result = new List<object>();

        foreach(var user in users["value"])
        {
            var email = "";
            foreach(var identity in user["identities"])
                if(identity["signInType"].ToString() == "emailAddress")
                    email = identity["issuerAssignedId"].ToString();
                
            var firstName = user["givenName"].ToString();
            var lastName = user["surname"].ToString(); 
            var id = user["id"].ToString(); 

            result.Add(new { ID = id, Email = email, FirstName = firstName, LastName = lastName});

            //Sync to CoFlows users.
            if(email != "")
            {
                var qid = "QuantAppSecure_" + email.ToLower().Replace('@', '.').Replace(':', '.');
                var quser = QuantApp.Kernel.User.FindUser(qid);
                
                if(quser == null)
                {
                    Console.WriteLine("--- CREATE NEW USER: " + qid);
                //     var nuser = UserRepository.CreateUser(System.Guid.NewGuid().ToString(), "QuantAppSecure");

                //     var firstName = identity.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.GivenName, StringComparison.OrdinalIgnoreCase));
                //     var lastName = identity.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.Surname, StringComparison.OrdinalIgnoreCase));

                //     nuser.FirstName = firstName != null ? firstName.Value : "No first name";
                //     nuser.LastName = lastName != null ? lastName.Value : "No last name";
                //     nuser.Email = email.Value.ToLower();

                //     nuser.TenantName = id;
                //     nuser.Hash = QuantApp.Kernel.Adapters.SQL.Factories.SQLUserFactory.GetMd5Hash(System.Guid.NewGuid().ToString());

                //     nuser.Secret = QuantApp.Engine.Code.GetMd5Hash(id);

                //     quser = QuantApp.Kernel.User.FindUser(id);
                //     QuantApp.Kernel.Group group = QuantApp.Kernel.Group.FindGroup("Public");
                //     group.Add(quser, typeof(QuantApp.Kernel.User), AccessType.Invited);

                //     // QuantApp.Kernel.Group gp = GroupRepository.FindByProfile(profile);
                //     var defGroupId =  Program.config["Server"]["OAuth"] != null && Program.config["Server"]["OAuth"]["AzureAdB2C"] != null && Program.config["Server"]["OAuth"]["AzureAdB2C"]["DefaultGroupId"] != null ? Program.config["Server"]["OAuth"]["AzureAdB2C"]["DefaultGroupId"].ToString() : "";
                //     QuantApp.Kernel.Group gp = Group.FindGroup(defGroupId);
                //     if (gp != null)
                //         gp.Add(quser, typeof(QuantApp.Kernel.User), AccessType.Invited);
                }
            }
        }
    
        return result;
    }

    public static List<object> GraphGroups()
    {
        string res = "";
        var result = new List<object>();
        Task.Run(async () => {   
            using(HttpClient httpClient = new HttpClient()){
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                nvc.Add(new KeyValuePair<string, string>("client_id", "ec388b90-777a-4396-afdb-431333ad0bd9"));
                nvc.Add(new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"));
                nvc.Add(new KeyValuePair<string, string>("client_secret", "vnFEMw~_X4_b5aa~u9~4V9O1HEtFA3VgyD"));
                
                var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/coflowsad.onmicrosoft.com/oauth2/v2.0/token") { Content = new FormUrlEncodedContent(nvc) };
                var data = await httpClient.SendAsync(req);
                var dd = await data.Content.ReadAsStringAsync();
                
                dynamic d = JObject.Parse(dd.ToString());
                string access_code = d.access_token;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/groups");
                data = await httpClient.SendAsync(req);
                
                res = await data.Content.ReadAsStringAsync();

                var groups = JObject.Parse(res);

                foreach(var group in groups["value"])
                {
                    var id = group["id"].ToString();
                    var name = group["displayName"].ToString(); 

                    req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/groups/" + id + "/members?$select=identities,surname,givenName");
                    data = await httpClient.SendAsync(req);
                    
                    res = await data.Content.ReadAsStringAsync();

                    var members = JObject.Parse(res);

                    var sub_result = new List<object>();

                    foreach(var member in members["value"])
                    {
                        var email = "";
                        foreach(var identity in member["identities"])
                            if(identity["signInType"].ToString() == "emailAddress")
                                email = identity["issuerAssignedId"].ToString();
                            
                        var firstName = member["givenName"].ToString();
                        var lastName = member["surname"].ToString(); 

                        sub_result.Add(new { Email = email, FirstName = firstName, LastName = lastName});
                    }

                    result.Add(new { ID = id, Name = name, Members = sub_result });
                }                
            }
        }).Wait();
        
        return result;
    }

    public static string GraphRoles()
    {
        string res = "";
        Task.Run(async () => {   
            using(HttpClient httpClient = new HttpClient()){
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                nvc.Add(new KeyValuePair<string, string>("client_id", "ec388b90-777a-4396-afdb-431333ad0bd9"));
                nvc.Add(new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"));
                nvc.Add(new KeyValuePair<string, string>("client_secret", "vnFEMw~_X4_b5aa~u9~4V9O1HEtFA3VgyD"));
                
                var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/coflowsad.onmicrosoft.com/oauth2/v2.0/token") { Content = new FormUrlEncodedContent(nvc) };
                var data = await httpClient.SendAsync(req);
                var dd = await data.Content.ReadAsStringAsync();
                
                dynamic d = JObject.Parse(dd.ToString());
                string access_code = d.access_token;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                // req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/directoryRoles");
                req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users/045c9566-b259-4b6e-9342-70135e25f68a/memberOf");
                
                data = await httpClient.SendAsync(req);
                
                res = await data.Content.ReadAsStringAsync();
            }
        }).Wait();
    
        return JsonConvert.SerializeObject(JObject.Parse(res), Formatting.Indented);
    }

    public static string GraphRoleMembers()
    {
        string res = "";
        Task.Run(async () => {   
            using(HttpClient httpClient = new HttpClient()){
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                nvc.Add(new KeyValuePair<string, string>("client_id", "ec388b90-777a-4396-afdb-431333ad0bd9"));
                nvc.Add(new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"));
                nvc.Add(new KeyValuePair<string, string>("client_secret", "vnFEMw~_X4_b5aa~u9~4V9O1HEtFA3VgyD"));
                
                var req = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/coflowsad.onmicrosoft.com/oauth2/v2.0/token") { Content = new FormUrlEncodedContent(nvc) };
                var data = await httpClient.SendAsync(req);
                var dd = await data.Content.ReadAsStringAsync();
                
                dynamic d = JObject.Parse(dd.ToString());
                string access_code = d.access_token;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_code);

                req = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/directoryRoles/34bb84f2-ced4-4d29-9803-a15bef652bec/members");
                data = await httpClient.SendAsync(req);
                
                res = await data.Content.ReadAsStringAsync();
            }
        }).Wait();
    
        return JsonConvert.SerializeObject(JObject.Parse(res), Formatting.Indented);
    }
    
    /// <api name="Permission">
    ///     <description> Function that returns a permission </description>
    ///     <returns> returns an string-- </returns>
    ///     <permissions>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Permission()
    {
        Console.WriteLine("--- CAL P");
        //curl -H "_cokey: 26499e5e555e9957725f51cc4d400384" http://localhost/m/query/06e1da00-4c81-4a35-914b-81c548b07345/CsQuery/Permission
        var user = QuantApp.Kernel.User.ContextUser;
        var permission = QuantApp.Kernel.User.PermissionContext("06e1da00-4c81-4a35-914b-81c548b07345");
        switch(permission)
        {
            case QuantApp.Kernel.AccessType.Write:
                return user.FirstName + " WRITE";
            case QuantApp.Kernel.AccessType.Read:
                return user.FirstName + " READ";
            case QuantApp.Kernel.AccessType.View:
                return user.FirstName + " VIEW";
            default:
                return user.FirstName + " DENIED";
        }
    }

    // F# Interop
    public static string Fs()
    {
        var fsbase = new Fs.Base.FsBase();
        var age = fsbase.getAge;
        var age_in_5_years = fsbase.Add((int)fsbase.getAge, 5);
        
        var result = "F# " + fsbase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in fsbase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }

    //Python Interop
    public static string Python()
    {
        using(Py.GIL())
        {
            dynamic pymodule = Py.Import("Base.pyBase.pybase");
            dynamic pybase = pymodule.pybase();
            var age_in_5_years = pybase.Add((int)pybase.getAge, 5);

            var result = "Python " + pybase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
            foreach(var interest in pybase.getInterests())
                result += System.Environment.NewLine + interest.ToString();

            return result;
        }
    }

    //Java Interop
    public static string Java()
    {
        dynamic javabase = JVM.Runtime.CreateInstance("javabase.javaBase");
        var age_in_5_years = javabase.Add((int)javabase.getAge, 5);
        
        var result = "Java " + javabase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in javabase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }

    //Scala Interop
    public static string Scala()
    {
        dynamic scalabase = JVM.Runtime.CreateInstance("scalabase.scalaBase");
        var age_in_5_years = scalabase.Add((int)scalabase.getAge(), 5);

        var result = "Scala " + scalabase.getName().ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in scalabase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }
}
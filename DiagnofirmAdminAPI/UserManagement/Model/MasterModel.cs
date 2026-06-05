using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;

namespace DiagnofirmAdmin.Model
{
    public class userModel
    {
        public string userid { get; set; }
        public string startdate { set; get; }
        public string enddate { set; get; }
    }

    public class getuserModel
    {
        public string userid { get; set; }
        public string sgid { get; set; }
    }

    public class adduserModel
    {
        public string sgid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string userrole { get; set; }
        public string userdepartment { get; set; }
        public string userprocess { get; set; }
        public string userproduct { get; set; }
        public string userplant { get; set; }
        public string password { get; set; }
        public string userid { get; set; }
        public string status { get; set; }
    }

    public class updateuserModel
    {
        public string usermasterid { get; set; }
        public string sgid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string userrole { get; set; }
        public string userdepartment { get; set; }
        public string userprocess { get; set; }
        public string userproduct { get; set; }
        public string userplant { get; set; }
        public string password { get; set; }
        public string userid { get; set; }
        public string status { get; set; }
    }

    public class UserMasterList
    {
        public string usermasterid { get; set; }
        public string sgid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string designation { get; set; }
        public string userrole { get; set; }
        public string password { get; set; }
        public string userid { get; set; }
        public string status { get; set; }
    }

    public class deluserModel
    {
        public string delid { get; set; }
        public string username { get; set; }
    }


    public class categoryModel
    {
        public string userid { get; set; }
    }

    public class getcategoryModel
    {
        public int categoryid { get; set; }
    }

    public class addcategoryModel
    {
        public string categorycode { get; set; }
        public string categoryname { get; set; }
        public string categoryorder { get; set; }
        public string categorydescription { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class updatecategoryModel
    {
        public int categoryid { get; set; }
        public string categorycode { get; set; }
        public string categoryname { get; set; }
        public string categoryorder { get; set; }
        public string categorydescription { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class delcategoryModel
    {
        public int categoryid { get; set; }
        public string username { get; set; }
    }

    public class imageviewModel
    {
        public string Productid { get; set; }
        public string username { get; set; }
    }

    public class newsletterimageviewModel
    {
        public string newsletterid { get; set; }
        public string username { get; set; }
    }

    public class packageimageviewModel
    {
        public string Packageid { get; set; }
        public string username { get; set; }
    }

    public class ImagesubcatvalModel
    {
        //public string imageid { get; set; }
        public string imagenamevalue { get; set; }
        public string imagepathvalue { get; set; }
        public string subcategoryId { get; set; }
        public string imageBase64value { get; set; }
    }

    public class ImagevalModel
    {
        //public string imageid { get; set; }
        public string imagenamevalue { get; set; }
        public string imagepathvalue { get; set; }
        public string productId { get; set; }
        public string imageBase64value { get; set; }
    }

    public class newsletterImagevalModel
    {
        //public string imageid { get; set; }
        public string imagenamevalue { get; set; }
        public string imagepathvalue { get; set; }
        public string newsletterId { get; set; }
        public string imageBase64value { get; set; }
        public string filename { get; set; }
        public string fileBase64String { get; set; }
    }

    public class packageImagevalModel
    {
        //public string imageid { get; set; }
        public string imagenamevalue { get; set; }
        public string imagepathvalue { get; set; }
        public string packageId { get; set; }
        public string imageBase64value { get; set; }
    }

    public class getlastcodeModel
    {
        public string schemaname { get; set; }
        public string tablename { get; set; }
        public string columnname { get; set; }
    }


    public class subcategoryModel
    {
        public string userid { get; set; }
    }

    public class getsubcategoryModel
    {
        public int subcategoryid { get; set; }
    }

    public class addsubcategoryModel
    {
        public int categoryid { get; set; }
        public string subcategorycode { get; set; }
        public string subcategoryname { get; set; }
        public string subcategorydescription { get; set; }
        public string subcategoryorder { get; set; }
        public string subcategoryimage { get; set; }
        public string subcategoryimagename { get; set; }

        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class updatesubcategoryModel
    {
        public int subcategoryid { get; set; }
        public int categoryid { get; set; }
        public string subcategorycode { get; set; }
        public string subcategoryname { get; set; }
        public string subcategorydescription { get; set; }
        public string subcategoryorder { get; set; }
        public string subcategoryimage { get; set; }
        public string subcategoryimagename { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class delsubcategoryModel
    {
        public int subcategoryid { get; set; }
        public string username { get; set; }
    }

    public class getsubcategorybycategoryModel
    {
        public int categoryid { get; set; }
    }

    public class subcatimageviewModel
    {
        public string subcatid { get; set; }
        public string username { get; set; }
    }

    public class healthconditionModel
    {
        public string userid { get; set; }
    }

    public class gethealthconditionModel
    {
        public string healthconditionid { get; set; }
    }

    public class addhealthconditionModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string healthconditioncode { get; set; }
        public string healthconditionname { get; set; }
        public string healthconditiondescription { get; set; }
        public string healthconditionorder { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class updatehealthconditionModel
    {
        public string healthconditionid { get; set; }
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string healthconditioncode { get; set; }
        public string healthconditionname { get; set; }
        public string healthconditiondescription { get; set; }
        public string healthconditionorder { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class delhealthconditionModel
    {
        public string healthconditionid { get; set; }
        public string username { get; set; }
    }

    public class gethcbycatModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
    }

    public class organModel
    {
        public string userid { get; set; }
    }

    public class getorganModel
    {
        public string organid { get; set; }
    }

    public class addorganModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string organcode { get; set; }
        public string organname { get; set; }
        public string organdescription { get; set; }
        public string organorder { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class updateorganModel
    {
        public string organid { get; set; }
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string organcode { get; set; }
        public string organname { get; set; }
        public string organdescription { get; set; }
        public string organorder { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class delorganModel
    {
        public string organid { get; set; }
        public string username { get; set; }
    }

    public class getorganbycatModel
    {
        public int categoryid { get; set; }
        public string subcategoryid { get; set; }
    }


    public class productModel { }

    public class getproductModel
    {
        public int productid { get; set; }
    }

    public class addproductModel
    {
        public int categoryid { get; set; }
        public int[] subcategoryid { get; set; }
        public string packageid { get; set; }
        public string userid { get; set; }
        public string producthead { get; set; }
        public string productcode { get; set; }
        public string productname { get; set; }
        public string productdesc { get; set; }
        public string productord { get; set; }
        public decimal productprice { get; set; }
        public string productgrpcod { get; set; }
        public string productimage { get; set; }
        public string productimagename { get; set; }
        public string username { get; set; }
        public string status { get; set; }
    }

    public class updateproductModel
    {
        public int productid { get; set; }
        public int categoryid { get; set; }
        public int subcategoryid { get; set; }
        public string packageid { get; set; }
        public string userid { get; set; }
        public string producthead { get; set; }
        public string productcode { get; set; }
        public string productname { get; set; }
        public string productdesc { get; set; }
        public string productord { get; set; }
        public decimal productprice { get; set; }
        public string productgrpcod { get; set; }
        public string productimage { get; set; }
        public string productimagename { get; set; }
        public string username { get; set; }
        public string status { get; set; }
    }

    public class delproductModel
    {
        public int productid { get; set; }
        public string username { get; set; }
    }

    // FILTER MODELS
    public class getproductbycatModel
    {
        public string categoryid { get; set; }
    }
    public class getproductbycatsubModel
    {
        public int categoryid { get; set; }
        public int subcategoryid { get; set; }
    }
    public class getproductbyhcModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string healthconditionid { get; set; }
    }
    public class getproductbyorgModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string organid { get; set; }
    }
    public class getproductbypackModel
    {
        public string categoryid { get; set; }
        public string subcategoryid { get; set; }
        public string packageid { get; set; }
    }

    public class testdirectoryModel { }

    public class gettestdirectoryModel
    {
        public int testdirectoryid { get; set; }
    }

    public class gettestdirectoryIndustryModel
    {
        public int testdirectoryindustryid { get; set; }
    }


    public class addtestdirectoryModel
    {
        public int industryid { get; set; }
        public string testdirectoryhead { get; set; }
        public string testdirectorycode { get; set; }
        public string testdirectoryname { get; set; }
        public string specimen { get; set; }
        public string unit { get; set; }
        public string refrange { get; set; }
        public string testdescription { get; set; }
        public string testorder { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    public class updatetestdirectoryModel : addtestdirectoryModel
    {
        public int testdirectoryid { get; set; }
    }

    public class deltestdirectoryModel
    {
        public int testdirectoryid { get; set; }
        public string username { get; set; }
    }

    public class feedbackModel
    {
    }


    // ================= GET BY ID =================
    public class getfeedbackbyidModel
    {
        public int feedbackid { get; set; }
    }

    // ================= GET BY USER =================
    public class getfeedbackbyuseridModel
    {
        public string userid { get; set; }
    }

    // ================= ADD FEEDBACK =================
    public class addfeedbackModel
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string useremail { get; set; }
        public string userrole { get; set; }

        public string feedbackdesc { get; set; }
        public string starrating { get; set; }
        public string feedbackord { get; set; }

        public string createdby { get; set; }
        public string status { get; set; }
    }

    // ================= UPDATE FEEDBACK =================
    public class updatefeedbackModel
    {
        public int feedbackid { get; set; }

        public string userid { get; set; }
        public string username { get; set; }
        public string useremail { get; set; }
        public string userrole { get; set; }

        public string feedbackdesc { get; set; }
        public string starrating { get; set; }
        public string feedbackord { get; set; }

        public string updatedby { get; set; }
        public string status { get; set; }
    }

    // ================= DELETE FEEDBACK =================
    public class deletefeedbackModel
    {
        public int feedbackid { get; set; }
        public string username { get; set; }
    }


    public class packageModel
    {
    }

    public class getpackagebyidModel
    {
        public int packageid { get; set; }
    }

    public class getpackagebycatsubcatModel
    {
        public int categoryid { get; set; }
        public int subcategoryid { get; set; }
    }

    public class addpackageModel
    {
        public string packagehead { get; set; }
        public string packagecode { get; set; }
        public string packagename { get; set; }
        public string packagesampletype { get; set; }
        public string packagegender { get; set; }
        public decimal packageprice { get; set; }
        public string packagetestparam { get; set; }
        public string packageord { get; set; }
        public string packagedesc { get; set; }
        public string packagefacts { get; set; }
        public string packageimage { get; set; }
        public string packageimagename { get; set; }
        public string username { get; set; }
        public string status { get; set; }
    }

    public class updatepackageModel
    {
        public int packageid { get; set; }
        public string packagehead { get; set; }
        public string packagecode { get; set; }
        public string packagename { get; set; }
        public string packagesampletype { get; set; }
        public string packagegender { get; set; }
        public decimal packageprice { get; set; }
        public string packagetestparam { get; set; }
        public string packageord { get; set; }
        public string packagedesc { get; set; }
        public string packagefacts { get; set; }
        public string packageimage { get; set; }
        public string packageimagename { get; set; }
        public string username { get; set; }
        public string status { get; set; }
    }

    public class deletepackageModel
    {
        public int packageid { get; set; }
    }

    public class ImageJsonModel
    {
        //public string imageid { get; set; }
        public string imagenamevalue { get; set; }
        public string imagepathvalue { get; set; }
        public string imageTypeValue { get; set; }
        public string imageBase64value { get; set; }
    }


    // ================= ADD FAQ =================
    public class addfaqModel
    {
        public int? prodid { get; set; }
        public int? subcatid { get; set; }
        public int? packgid { get; set; }
        public string faqcode { get; set; }
        public string faqname { get; set; }
        public string faqdesc { get; set; }
        public string faqord { get; set; }
        public string faqques { get; set; }
        public string faqans { get; set; }
        public string faqhomecheck { get; set; }
        public string status { get; set; }
        public string username { get; set; }
    }

    // ================= UPDATE FAQ =================
    public class updatefaqModel
    {
        public int faqid { get; set; }
        public int? prodid { get; set; }
        public int? subcatid { get; set; }
        public int? packgid { get; set; }
        public string faqcode { get; set; }
        public string faqname { get; set; }
        public string faqdesc { get; set; }
        public string faqord { get; set; }
        public string faqques { get; set; }
        public string faqans { get; set; }
        public string faqhomecheck { get; set; }
        public string status { get; set; }
        public string username { get; set; }
    }

    // ================= DELETE FAQ =================
    public class deletefaqModel
    {
        public int faqid { get; set; }
        public string username { get; set; }
    }

    // ================= GET BY ID =================
    public class getfaqModel
    {
        public int faqid { get; set; }
    }

    // ================= GET BY Package ID =================
    public class getfaqpackageModel
    {
        public int packgid { get; set; }
    }

    // ================= GET BY Product ID =================
    public class getfaqproductModel
    {
        public int prodid { get; set; }
    }

    // ================= GET BY Subcategory ID =================
    public class getfaqsubcatModel
    {
        public int subcatid { get; set; }
    }

    // =========================
    // ADD NEWSLETTER MODEL
    // =========================
    public class addnewsletterModel
    {
        public string usr_id { get; set; }
        public string version_no { get; set; }
        public string letter_date { get; set; }
        public string letter_image { get; set; }
        public string letter_imgname { get; set; }
        public string letter_file { get; set; }
        public string letter_filename { get; set; }
        public string letter_ord { get; set; }
        public string is_active { get; set; }
        public string username { get; set; }
    }

    // =========================
    // UPDATE NEWSLETTER MODEL
    // =========================
    public class updatenewsletterModel
    {
        public int nid { get; set; }
        public string usrid { get; set; }
        public string versionno { get; set; }
        public string letterdate { get; set; }
        public string letterimage { get; set; }
        public string letterimgname { get; set; }
        public string letterfile { get; set; }
        public string letterfilename { get; set; }
        public string letterord { get; set; }
        public string isactive { get; set; }
        public string username { get; set; }
    }

    // =========================
    // GET BY ID MODEL
    // =========================
    public class getnewsletterbyidModel
    {
        public int Id { get; set; }
    }

    // =========================
    // DELETE MODEL
    // =========================
    public class deletenewsletterModel
    {
        public int nid { get; set; }
        public string username { get; set; }
    }

    public class CartModel
    {
        public int cid { get; set; }
        public string p_usr_id { get; set; }
        public int? p_prod_id { get; set; }
        public int? p_packg_id { get; set; }
        public decimal p_cart_qty { get; set; }
        public decimal p_prod_total { get; set; }
        public decimal p_cart_total { get; set; }
        public string p_cart_desc { get; set; }
        public string p_is_active { get; set; }
        public string p_username { get; set; }
    }

    public class addCartModel
    {
        public string usr_id { get; set; }
        public int? prod_id { get; set; }
        public int? packg_id { get; set; }
        public decimal cart_qty { get; set; }
        public decimal prod_total { get; set; }
        public decimal cart_total { get; set; }
        public string cart_desc { get; set; }
        public string is_active { get; set; }
        public string username { get; set; }
    }

    public class getCartModel
    {
        public int cid { get; set; }
    }

    public class getCartbypackageModel
    {
        public int packg_id { get; set; }
    }

    public class getCartbyproductModel
    {
        public int prod_id { get; set; }
    }

    public class CheckoutModel
    {
        public int order_id { get; set; }
        public string usr_id { get; set; }
        public int? prod_id { get; set; }
        public int? packg_id { get; set; }
        public int? pay_id { get; set; }
        public decimal check_qty { get; set; }
        public decimal prod_total { get; set; }
        public decimal check_total { get; set; }
        public string check_firstname { get; set; }
        public string check_lastname { get; set; }
        public string check_country { get; set; }
        public string check_address1 { get; set; }
        public string check_address2 { get; set; }
        public string check_city { get; set; }
        public string check_state { get; set; }
        public string check_zip { get; set; }
        public string check_phno { get; set; }
        public string check_email { get; set; }
        public string check_addnote { get; set; }
        public string is_active { get; set; }
        public string username { get; set; }
    }

    public class updateCheckoutModel
    {
        public int cid { get; set; }
        public int order_id { get; set; }
        public string usr_id { get; set; }
        public int? prod_id { get; set; }
        public int? packg_id { get; set; }
        public int? pay_id { get; set; }
        public decimal check_qty { get; set; }
        public decimal prod_total { get; set; }
        public decimal check_total { get; set; }
        public string check_firstname { get; set; }
        public string check_lastname { get; set; }
        public string check_country { get; set; }
        public string check_address1 { get; set; }
        public string check_address2 { get; set; }
        public string check_city { get; set; }
        public string check_state { get; set; }
        public string check_zip { get; set; }
        public string check_phno { get; set; }
        public string check_email { get; set; }
        public string check_addnote { get; set; }
        public string is_active { get; set; }
        public string username { get; set; }
    }

    public class getCheckoutModel
    {
        public int cid { get; set; }
    }
    public class getCheckoutbyOrderModel
    {
        public int ordid { get; set; }
    }

    public class getCheckoutByProductModel
    {
        public int prod_id { get; set; }
    }

    public class getCheckoutByPackageModel
    {
        public int packg_id { get; set; }
    }

    public class PaymentModel
    {
        public int id { get; set; }
        public string pay_type { get; set; }
        public string pay_code { get; set; }
        public string pay_name { get; set; }
        public string pay_desc { get; set; }
        public string pay_ord { get; set; }
        public string is_active { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
    }

    public class getPaymentModel
    {
        public int id { get; set; }
    }

    public class SearchRequestModel
    {
        public string Query { get; set; } = string.Empty;
    }

    // ================= GET CONTACT BY ID =================
    public class getcontactModel
    {
        public int contactid { get; set; }
    }

    // ================= GET CONTACT TYPE BY ID =================
    public class getcontacttypeModel
    {
        public int ctid { get; set; }
    }

    // ================= ADD CONTACT =================
    public class addcontactModel
    {
        public string conttype { get; set; }
        public string contname { get; set; }
        public string contaddress { get; set; }
        public string contcity { get; set; }
        public string contstate { get; set; }
        public string contcountry { get; set; }
        public string contphno { get; set; }
        public string contaltphno { get; set; }
        public string contwrkhrs1 { get; set; }
        public string contwrkhrs2 { get; set; }
        public string contwrkhrs3 { get; set; }
        public string contemail { get; set; }
        public string contdircts { get; set; }
        public string contdesc { get; set; }
        public string contord { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    // ================= UPDATE CONTACT =================
    public class updatecontactModel
    {
        public int contactid { get; set; }
        public string conttype { get; set; }
        public string contname { get; set; }
        public string contaddress { get; set; }
        public string contcity { get; set; }
        public string contstate { get; set; }
        public string contcountry { get; set; }
        public string contphno { get; set; }
        public string contaltphno { get; set; }
        public string contwrkhrs1 { get; set; }
        public string contwrkhrs2 { get; set; }
        public string contwrkhrs3 { get; set; }
        public string contemail { get; set; }
        public string contdircts { get; set; }
        public string contdesc { get; set; }
        public string contord { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
    }

    // ================= DELETE CONTACT =================
    public class delcontactModel
    {
        public int contactid { get; set; }
        public string username { get; set; }
    }

    // ================= SEARCH CONTACT =================
    public class SearchContactRequestModel
    {
        public string Query { get; set; }
    }

    public class EmailRequest
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
    }

}

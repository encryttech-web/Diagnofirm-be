
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';

export class GlobalConstants {


  constructor(private _http: HttpClient,) {

  }

  public static Authurl = 'https://localhost:44346/api/';
  public static WebSSoUrl = 'https://uat.websso.saint-gobain.com/cas/?ticket=&service=';

  // START: LOGIN
  public static ApiSsoLoginMethod = 'Login/SSO';
  public static ApiLDAPMethod = 'Login/ValidateLDAP';
  public static ValidateUser = 'Login/ValidateSgid';
  public static Login = 'Login/Login';
  // END: LOGIN

  // START: USER
  public static GetUser = 'Usermaster/getuser';
  public static GetUserbyId = 'Usermaster/getuserbyId';
  public static AddUser = 'Usermaster/adduser';
  public static UpdateUser = 'Usermaster/updateuser';
  public static DeleteUser = 'Usermaster/deluser';
  public static GetUserrole = 'Usermaster/getuserrole';
  public static GetUserdepartment = 'Usermaster/getuserdepartment';
  public static GetUserprocess = 'Usermaster/getuserprocess';
  public static GetUserproduct = 'Usermaster/getuserproduct';
  // END: USER

  // START: CATEGORY
  public static Getcategory = 'Category/getcategory';
  public static GetcategorybyId = 'Category/getcategorybyId';
  public static Addcategory = 'Category/addcategory';
  public static Updatecategory = 'Category/updatecategory';
  public static Deletecategory = 'Category/deletecategory';
  public static Getlastcode = 'Category/getlastcode';
  // END: CATEGORY

  // START: SUBCATEGORY
  public static Getsubcategory = 'SubCategory/getsubcategory';
  public static GetsubcategorybyId = 'SubCategory/getsubcategorybyId';
  public static Addsubcategory = 'SubCategory/addsubcategory';
  public static Updatesubcategory = 'SubCategory/updatesubcategory';
  public static Deletesubcategory = 'SubCategory/delsubcategory';
  public static GetsubcategorybyCategoryId = 'SubCategory/getsubcategorybycategoryid';
  public static GetBySubcatIdviewImage = 'SubCategory/getsubcatImagebyId';
  // END: SUBCATEGORY

  // START: TESTDIRECTORY
  public static Gettestdirectory = 'TestDirectory/gettestdirectory';
  public static GettestdirectorybyId = 'TestDirectory/gettestdirectorybyId';
  public static Addtestdirectory = 'TestDirectory/addtestdirectory';
  public static Updatetestdirectory = 'TestDirectory/updatetestdirectory';
  public static Deletetestdirectory = 'TestDirectory/deltestdirectory';
  public static GettestdirectoryIndustry = 'TestDirectory/gettestdirectoryIndustry';
  public static GettestdirectoryIndustrybyId = 'TestDirectory/gettestdirectorybyIndustryId';
  // END: TESTDIRECTORY

  // START: PRODUCT
  public static Getproduct = 'Product/getproduct';
  public static GetproductbyId = 'Product/getproductbyId';
  public static Addproduct = 'Product/addproduct';
  public static Updateproduct = 'Product/updateproduct';
  public static Deleteproduct = 'Product/delproduct';
  public static GetByIdviewImage = 'Product/getImagebyId';
  public static Getbycatandsubcat = 'Product/getbycatandsubcat';
  // END: PRODUCT

  // START: PACKAGES
  public static Getpackage = 'Package/getpackage';
  public static GetpackagebyId = 'Package/getpackagebyid';
  public static Addpackage = 'Package/addpackage';
  public static Updatepackage = 'Package/updatepackage';
  public static Deletepackage = 'Package/deletepackage';
  public static GetpackageByIdviewImage = 'Package/getpackageImagebyId';

  // END: PACKAGES

  // START: FAQ
  public static Getfaq = 'Faq/getfaq';
  public static GetfaqbyId = 'Faq/getfaqbyId';
  public static Addfaq = 'Faq/addfaq';
  public static Updatefaq = 'Faq/updatefaq';
  public static Deletefaq = 'Faq/deletefaq';
  public static GetfaqbysubcatId = 'Faq/getfaqbysubcatId';
  public static GetfaqbyproductId = 'Faq/getfaqbyproductId';
  public static GetfaqbypackageId = 'Faq/getfaqbypackageId';
  public static Getfaqbyhomecheck = 'Faq/getfaqbyhomecheck';

  // END: FAQ

  // START: FEEDBACK
  public static Getfeedback = 'Feedback/getfeedback';
  public static GetfeedbackbyId = 'Feedback/getfeedbackbyid';
  public static Addfeedback = 'Feedback/addfeedback';
  public static Updatefeedback = 'Feedback/updatefeedback';
  public static Deletefeedback = 'Feedback/deletefeedback';
  public static GetfeedbackbyuserId = 'Feedback/getfeedbackbyuserid';
  // END: FEEDBACK

  // START: NEWSLETTER
  public static Getnewsletter = 'Newsletter/getnewsletter';
  public static Getnewsletterbyid = 'Newsletter/getnewsletterbyid';
  public static Addnewsletter = 'Newsletter/addnewsletter';
  public static Updatenewsletter = 'Newsletter/updatenewsletter';
  public static Deletenewsletter = 'Newsletter/deletenewsletter';
  public static GetnewsletterByIdviewImage = 'Newsletter/getnewsletterImagebyId';
  public static GetnewsletterByIdviewFile = 'Newsletter/getnewsletterImagebyId';
  // END: NEWSLETTER

  // START: CART
  public static Getcart = 'Cart/getcart';
  public static Getcartbyid = 'Cart/getcartbyid';
  public static Addcart = 'Cart/createcart';
  public static Updatecart = 'Cart/updatecart';
  public static Deletecart = 'Cart/deletecart';
  public static GetcartByPackageId = 'Cart/getcartbypackage';
  public static GetcartByProductId = 'Cart/getcartbyproduct';
  // END: CART

  // START: CHECKOUT
  public static Getcheckout = 'Checkout/getcheckout';
  public static Getcheckoutbyid = 'Checkout/getcheckoutbyid';
  public static Addcheckout = 'Checkout/createcheckout';
  public static Updatecheckout = 'Checkout/updatecheckout';
  public static Deletecheckout = 'Checkout/deletecheckout';
  public static GetcheckoutByPackageId = 'Checkout/getcheckoutbypackage';
  public static GetcheckoutByProductId = 'Checkout/getcheckoutbyproduct';
  public static Getallorder = 'Checkout/getallorder';
  public static Getallcount = 'Checkout/getallcount';
  // END: CHECKOUT

  // START: PAYMENT
  public static Getpayment = 'Checkout/getpayment';
  public static Getpaymentbyid = 'Checkout/getpaymentbyid';
  // END: PAYMENT

  // START: CONTACT
  public static Getcontacttype = 'Contact/getcontacttype';
  public static Getcontact = 'Contact/getcontact';
  public static Getcontactbyid = 'Contact/getcontactbyId';
  public static Addcontact = 'Contact/addcontact';
  public static Updatecontact = 'Contact/updatecontact';
  public static Deletecontact = 'Contact/delcontact';
  public static SearchContact = 'Contact/SearchContact';
  // END: CONTACT

}
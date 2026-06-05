import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';

import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ToggleSwitchModule } from 'primeng/toggleswitch';

import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';

import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-editfaq',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgIf,
    NgFor,
    SelectModule,
    InputTextModule,
    ButtonModule,
    DialogModule,
    ToggleSwitchModule
  ],
  templateUrl: './editfaq.html',
  styleUrl: './editfaq.scss'
})

export class Editfaq {

  checked: boolean = true;

  userid: any;
  faq_homecheck: any;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private cdr: ChangeDetectorRef
  ) { }

  // =========================
  // INPUT / OUTPUT
  // =========================
  @Input() editdisplayfaq: boolean = false;

  @Output() editdisplayChange =
    new EventEmitter<boolean>();

  @Output() dataReloaded =
    new EventEmitter<any>();

  @Input() faqdata: any;

  // =========================
  // FAQ FIELDS
  // =========================
  faq_id: number = 0;

  faq_code: string = '';
  faq_name: string = '';
  faq_desc: string = '';
  faq_ord: string = '';

  faq_ques: string = '';
  faq_ans: string = '';

  owner_type: string = '';

  prod_id: number | null = null;

  // =========================
  // SUB CATEGORY
  // =========================
  subcategory_id: number | null = null;

  packg_id: number | null = null;

  // =========================
  // STATUS
  // =========================
  is_active: string = '1';

  // =========================
  // DROPDOWN LISTS
  // =========================
  statusList: any[] = [

    {
      label: 'Active',
      value: '1'
    },

    {
      label: 'Inactive',
      value: '0'
    }

  ];

  ownerTypeList: any[] = [

    {
      label: 'Product',
      value: 'product'
    },

    {
      label: 'Package',
      value: 'package'
    }

  ];

  // PRODUCT LIST
  productList: any[] = [];

  // SUB CATEGORY LIST
  subcategoryList: any[] = [];

  // PACKAGE LIST
  packageList: any[] = [];

  // =========================
  // INIT
  // =========================
  ngOnInit() {

    if (this.faqdata) {

      this.faq_id = this.faqdata.id;

      this.getFaqById();

    }

    this.getProducts();

    // SUB CATEGORY
    this.getSubCategories();

    this.getPackages();

  }

  ngOnChanges() {

    if (this.faqdata) {

      this.faq_id = this.faqdata.id;

      this.getFaqById();

    }

  }

  // =========================
  // GET FAQ BY ID
  // =========================
  getFaqById() {

    const input = {

      faqid: Number(this.faq_id)

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.GetfaqbyId;

    this.dataService
      .addData(url, input)
      .subscribe((res: any) => {

        if (res.status === 'success') {

          const data =
            res.response.ref1[0];

          this.faq_code = data.faqcode;
          this.faq_name = data.faqname;
          this.faq_desc = data.faqdesc;
          this.faq_ord = data.faqord;

          this.faq_ques = data.faqquestion;
          this.faq_ans = data.faqanswer;

          //this.faq_homecheck = data.faqhomecheck;
          this.faq_homecheck = data.faqhomecheck === 'Yes' ? true : false;
          // this.faq_homecheck = data.faqhomecheck === 'Yes' ? '1' : '0';

          this.is_active = data.is_active;

          this.checked =
            data.is_active === '1';

          this.prod_id = data.productid;

          // SUB CATEGORY
          this.subcategory_id =
            data.subcatid;

          this.packg_id = data.packageid;

          this.cdr.detectChanges();

        }

      });

  }

  // =========================
  // GET PRODUCTS
  // =========================
  getProducts() {

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getproduct;

    this.dataService
      .getData(url)
      .subscribe((res: any) => {

        if (res.status === 'success') {

          this.productList =
            res.response.ref1;

        }

      });

  }

  // =========================
  // GET SUB CATEGORY
  // =========================
  getSubCategories() {

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getsubcategory;

    this.dataService
      .getData(url)
      .subscribe((res: any) => {

        if (res.status === 'success') {

          this.subcategoryList =
            res.response.ref1;

          this.cdr.detectChanges();

        }

      });

  }

  // =========================
  // GET PACKAGES
  // =========================
  getPackages() {

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getpackage;

    this.dataService
      .getData(url)
      .subscribe((res: any) => {

        if (res.status === 'success') {

          this.packageList =
            res.response.ref1;

        }

      });

  }

  // =========================
  // CLOSE
  // =========================
  close() {

    this.editdisplayfaq = false;

    this.editdisplayChange.emit(false);

  }

  // =========================
  // CLEAR
  // =========================
  clear() {

    this.getFaqById();

  }

  // =========================
  // UPDATE FAQ
  // =========================
  editbtn(form: NgForm) {

    if (!form.valid) {

      this.notificationService.showMessage(
        'error',
        'Error',
        'Please fill required fields'
      );

      return;

    }

    const input = {

      faqid: this.faq_id,

      prodid: this.prod_id,

      // SUB CATEGORY
      subcatid: this.subcategory_id,

      packgid: this.packg_id,

      faqcode: this.faq_code,
      faqname: this.faq_name,
      faqdesc: this.faq_desc,
      faqord: this.faq_ord,

      faqques: this.faq_ques,
      faqans: this.faq_ans,

      faqhomecheck:this.faq_homecheck,

      status: this.checked ? '1' : '0',

      username: this.userid

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Updatefaq;

    this.dataService
      .addData(url, input)
      .subscribe((res: any) => {

        if (res.status === 'success') {

          this.notificationService.showMessage(
            'success',
            'Success',
            'FAQ updated'
          );

          this.close();

          this.dataReloaded.emit();

        }
        else {

          this.notificationService.showMessage(
            'error',
            'Error',
            'Update failed'
          );

        }

      });

  }

}
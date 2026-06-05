import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';

import { CommonModule } from '@angular/common';

import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

import {
  FormsModule,
  FormGroup,
  FormControl,
  Validators,
  NgForm
} from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { EditorModule } from 'primeng/editor';
import { ToggleSwitchModule } from 'primeng/toggleswitch';

@Component({
  selector: 'app-addfaq',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    SelectModule,
    EditorModule,
    ToggleSwitchModule
  ],
  templateUrl: './addfaq.html',
  styleUrl: './addfaq.scss'
})

export class Addfaq {

  // =========================
  // INPUT / OUTPUT
  // =========================
  @Input() display: boolean = false;

  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded: EventEmitter<any> = new EventEmitter();

  // =========================
  // FAQ FIELDS
  // =========================
  faq_name: string = '';
  faq_code: string = '';
  faq_desc: string = '';
  faq_ord: string = '';

  faq_ques: string = '';
  faq_ans: string = '';

  prod_id: number | null = null;

  // SUB CATEGORY
  subcategory_id: number | null = null;

  packg_id: number | null = null;

  checked: boolean = true;

  userid: string = '';
  username: string = '';

  // =========================
  // DROPDOWN DATA
  // =========================
  productList: any[] = [];

  // SUB CATEGORY LIST
  subcategoryList: any[] = [];

  packageList: any[] = [];

  // =========================
  // FORM
  // =========================
  public faqForm = new FormGroup({

    fcfaqname: new FormControl(
      '',
      [Validators.required]
    ),

    fcfaqcode: new FormControl(
      '',
      [Validators.required]
    ),

    fcfaqquestion: new FormControl(
      '',
      [Validators.required]
    ),

    fcfaqanswer: new FormControl(
      '',
      [Validators.required]
    ),

    fcstatus: new FormControl(
      '1',
      [Validators.required]
    )

  });

  // =========================
  // CODE
  // =========================
  Lastcode: any;

  homechecked:boolean = false;

  statusList!: {
    label: string;
    value: string;
  }[];
  faq_homecheck: any;

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private notificationService: NotificationService
  ) { }

  // =========================
  // INIT
  // =========================
  ngOnInit() {

    this.getProducts();

    // SUB CATEGORY
    this.getSubCategories();

    this.getPackages();

    this.getlastcode(
      'diafrm',
      'faq_tbl',
      'faq_code'
    );

    this.CDR.detectChanges();

  }

  ngOnChanges() {

    this.getProducts();

    // SUB CATEGORY
    this.getSubCategories();

    this.getPackages();

    this.getlastcode(
      'diafrm',
      'faq_tbl',
      'faq_code'
    );

    this.CDR.detectChanges();

  }

  // =========================
  // GENERATE CODE
  // =========================
  generateNextCode(lastCode: string): string {

    const prefix = 'FAQ';

    if (!lastCode) {

      return `${prefix}-0001`;

    }

    const lastNumber = parseInt(
      lastCode.split('-')[1],
      10
    );

    const nextNumber = lastNumber + 1;

    const formatted = nextNumber
      .toString()
      .padStart(4, '0');

    return `${prefix}-${formatted}`;

  }

  // =========================
  // GET LAST CODE
  // =========================
  getlastcode(
    schemaname: any,
    tablename: any,
    columnname: any
  ) {

    const input = {
      schemaname: schemaname,
      tablename: tablename,
      columnname: columnname
    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getlastcode;

    this.dataService
      .addData(url, input)
      .subscribe((response: any) => {

        if (response.status == 'success') {

          const value =
            response?.response?.ref1?.[0]?.ref1 ??
            'FAQ-0000';

          this.Lastcode = value;

          this.faq_code =
            this.generateNextCode(this.Lastcode);

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
      .subscribe((response: any) => {

        if (response.status == 'success') {

          this.productList =
            response['response']['ref1'];

          this.CDR.detectChanges();

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
      .subscribe((response: any) => {

        if (response.status == 'success') {

          this.subcategoryList =
            response['response']['ref1'];

          this.CDR.detectChanges();

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
      .subscribe((response: any) => {

        if (response.status == 'success') {

          this.packageList =
            response['response']['ref1'];

          this.CDR.detectChanges();

        }

      });

  }

  // =========================
  // CLOSE
  // =========================
  close() {

    this.display = false;

    this.displayChange.emit(this.display);

    this.dataReloaded.emit();

  }

  // =========================
  // ADD FAQ
  // =========================
  addbtn(faqForm: NgForm) {

    if (!faqForm.valid) {

      this.notificationService.showMessage(
        'error',
        'Missing Fields',
        'Please fill all required fields.'
      );

      return;

    }

    // validation
    if (
      !this.prod_id &&
      !this.subcategory_id &&
      !this.packg_id &&
      !this.faq_homecheck        
    ) {

      this.notificationService.showMessage(
        'error',
        'Validation',
        'Select Product or Package or Sub Category or home'
      );

      return;

    }

    const input = {

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

      faqhomecheck: this.faq_homecheck ? '1' : '0',

      status: this.checked ? '1' : '0',
      username: this.userid

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Addfaq;

    this.dataService
      .addData(url, input)
      .subscribe((response: any) => {

        if (response.status == 'success') {

          this.close();

          this.notificationService.showMessage(
            'success',
            'FAQ Added',
            'FAQ added successfully'
          );

        }
        else if (
          response.status == 'information'
        ) {

          this.notificationService.showMessage(
            'error',
            'Error',
            'FAQ already exists'
          );

        }
        else {

          this.notificationService.showMessage(
            'error',
            'Error',
            'Failed to add FAQ'
          );

        }

      });

  }

  // =========================
  // CLEAR
  // =========================
  clear() {

    this.prod_id = null;

    // SUB CATEGORY
    this.subcategory_id = null;

    this.packg_id = null;

    this.faq_name = '';
    this.faq_desc = '';
    this.faq_ord = '';

    this.faq_ques = '';
    this.faq_ans = '';

    this.checked = true;

  }

}
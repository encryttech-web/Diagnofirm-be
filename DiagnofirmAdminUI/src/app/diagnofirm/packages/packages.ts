import { Component, ChangeDetectorRef, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { EditorModule } from 'primeng/editor';
import { Addpackages } from './addpackages/addpackages';
import { Editpackages } from './editpackages/editpackages';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';
import { GlobalConstants } from '../services/global.constant';
import { Router } from '@angular/router';
import { ToastvalueService } from '../services/toastvalue.service';
import { ConfigService } from '../services/config.service';
import { DataService } from '../services/data.service';
import { HttpService } from '@/layout/service/http.service';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-packages',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    CheckboxModule,
    EditorModule,
    Addpackages,
    Editpackages,
    FormsModule,
    ToastModule
  ],
  templateUrl: './packages.html',
  styleUrls: ['./packages.scss'],
  providers: [
    ConfirmationService,
    MessageService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Packages {

  addenable: boolean = false;
  display: boolean = false;
  editenable: boolean = false;
  editdisplaypackages: boolean = false;
  packagedata: any;

  loading: boolean = true;

  globalFilter: string = '';
  testdirectorydata: any;

  selectedRange: Date[] = [];
  startdate: any;
  enddate: any;

  currentFilter: any = {};
  currentSort: any = null;

  packages: any[] = [];

  packageForm!: FormGroup;

  editIndex: number | null = null;

  filterApplied: boolean = false;
  clearButtonEnabled: boolean = false;

  isFilterVisible: { [key: string]: boolean } = {};

  packageFields: string[] = [
    'packagehead',
    'packagecode',
    'packagename',
    'packagesampletype',
    'packagegender',
    'packageprice',
    'packagedesc',
    'packagefacts'
  ];

  packageUiFields: string[] = [
    'Package HeadName',
    'Package Code',
    'Package Name',
    'Smaple type',
    'Gender',
    'Price',
    'Description',
    'Facts'
  ];

  constructor(
    private HTTPSERVICE: HttpService,
    private cdr: ChangeDetectorRef,
    private navigation: Router,
    private toasterService: ToastvalueService,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private dataService: DataService,
    private messageService: MessageService
  ) { }

  // constructor(
  //   private fb: FormBuilder,
  //   private cdr: ChangeDetectorRef
  // ) {
  //   this.initForm();
  // }

  ngOnInit() {
    this.loadPackages();
  }

  ngOnChanges() {
    this.loadPackages();
    this.editenable = true;
    this.editdisplaypackages = true;
    this.cdr.detectChanges();
  }

  // ---------------- FORM INIT ----------------
  // initForm() {
  //   this.packageForm = this.fb.group({
  //     packageCode: ['', Validators.required],
  //     packageName: ['', Validators.required],
  //     gender: [false],
  //     packageDescription: [''],
  //     testParameter: [''],
  //     faqs: this.fb.array([])
  //   });
  // }

  // ---------------- FAQ ARRAY ----------------
  get faqs(): FormArray {
    return this.packageForm.get('faqs') as FormArray;
  }


  backtomainChange(event: any) {
    if (event === false) {
      this.editdisplaypackages = true;
      this.editenable = false;
      this.loadPackages();
      this.cdr.detectChanges();
    }
  }

  onGlobalFilter(table: any, event: any) {
    const value = event.target.value;
    this.globalFilter = value;

    table.filterGlobal(value, 'contains');

    this.filterApplied = this.checkInputIsValid(value);
  }

  clearFilter(table: any) {
    this.globalFilter = '';
    table.clear();
    this.filterApplied = false;
  }

  /* =========================
     COLUMN FILTER
  ==========================*/

  onFilter(event: any) {

    const filterEntries: [string, any][] = Object.entries(event.filters);

    const filteredEntries = filterEntries.filter(([key, data]) => {
      return data[0].value !== null && data[0].value !== '';
    });

    if (filteredEntries.length > 0) {
      this.currentFilter = filteredEntries[0][1][0].value;
    } else {
      this.currentFilter = {};
    }

    if (Object.keys(this.currentFilter).length > 0) {
      this.clearButtonEnabled = true;
    } else {
      this.filterApplied = false;
      this.clearButtonEnabled = false;
    }
  }

  /* =========================
     SORTING
  ==========================*/

  onSort(event: any) {

    if (this.checkInputIsValid(event.order)) {
      this.clearButtonEnabled = true;
    } else {
      this.clearButtonEnabled = false;
    }

    const field = event.field;
    const order = event.order;

    this.packages.sort((a: any, b: any) => {

      if (a[field] < b[field]) {
        return order === 1 ? -1 : 1;
      }

      if (a[field] > b[field]) {
        return order === 1 ? 1 : -1;
      }

      return 0;
    });
  }

  /* =========================
     CLEAR TABLE
  ==========================*/

  clear(table: any) {
    table.clear();
    this.clearButtonEnabled = false;
  }

  /* =========================
     VALIDATION
  ==========================*/

  checkInputIsValid(value: any) {
    return value !== undefined && value !== null && value !== '';
  }

  toggleSearchBox(field: string) {
    this.isFilterVisible[field] = !this.isFilterVisible[field];
  }

  openAddDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.cdr.detectChanges();
  }

  closeAddtestdirectoryDialog() {
    this.display = false;
  }

  editscreen(packages: any) {
    this.addenable = false;
    this.editenable = true;
    this.editdisplaypackages = true;
    this.display = false;

    this.packagedata = packages;

    this.cdr.detectChanges();
  }

  onDataReloaded() {
    this.addenable = false;
    this.loadPackages();
    this.cdr.detectChanges();
  }

  // ---------------- RESET ----------------
  resetForm() {
    this.packageForm.reset();
    this.faqs.clear();
    this.editIndex = null;
  }

  // ---------------- LOAD SAMPLE DATA ----------------
  loadPackages() {

    let url = GlobalConstants.Authurl + GlobalConstants.Getpackage;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.packages = response['response']['ref1'];
        this.cdr.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'No product data found');
      }
    });
  }

    deleteaction(delid: any) {
      const input = {
        delid: Number(delid),
        username: ''
      };

      let url = GlobalConstants.Authurl + GlobalConstants.Deletepackage;

      this.dataService.addData(url, input).subscribe((response: any) => {
        if (response.status == 'success') {
          this.loadPackages();
          this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Product deleted successfully',
              life:     5000
            });
          // this.notificationService.showMessage('success', 'Deleted', 'Product deleted successfully');
        } else {
          //this.notificationService.showMessage('error', 'Error', 'There was a dependency on deleting the Packages');
          // this.notificationService.showMessage('error', 'Error', response.message);
          this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Packages',
              life:     5000
            });
        }
      });

      //const url =
      // this.CONFIGSERVICE.getApi('AUTH_URL') +
      // GlobalConstants.Deleletetestdirectory;

      console.log('delete payload', input);
    }
}
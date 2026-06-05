import { ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, ViewChild } from '@angular/core';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ToggleSwitchModule } from 'primeng/toggleswitch';

import { ToastvalueService } from '../services/toastvalue.service';
import { ConfigService } from '../services/config.service';
import { GlobalConstants } from '../services/global.constant';

import { Addtestdirectory } from './addtestdirectory/addtestdirectory';
import { Edittestdirectory } from './edittestdirectory/edittestdirectory';

import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-testdirectory',
  imports: [
    TableModule,
    MultiSelectModule,
    SelectModule,
    InputIconModule,
    TagModule,
    InputTextModule,
    SliderModule,
    ProgressBarModule,
    ToggleButtonModule,
    ToastModule,
    CommonModule,
    FormsModule,
    ButtonModule,
    RatingModule,
    RippleModule,
    IconFieldModule,
    DialogModule,
    FloatLabelModule,
    DatePickerModule,
    ToggleSwitchModule,
    NgIf,
    NgFor,
    Addtestdirectory,
    Edittestdirectory
  ],
  templateUrl: './testdirectory.html',
  styleUrl: './testdirectory.scss',
  providers: [
    ConfirmationService,
    MessageService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Testdirectory {

  constructor(
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private navigation: Router,
    private toasterService: ToastvalueService,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private dataService: DataService,
    private messageService: MessageService
  ) { }

  @ViewChild('dt1') dt1: any;
  @ViewChild('filter') filter!: ElementRef;

  addenable: boolean = false;
  display: boolean = false;
  editenable: boolean = false;
  editdisplaytestdirectory: boolean = false;

  gettestdirectoryist: any;
  loading: boolean = true;

  globalFilter: string = '';
  testdirectorydata: any;

  selectedRange: Date[] = [];
  startdate: any;
  enddate: any;

  currentFilter: any = {};
  currentSort: any = null;

  //categoryDescription:any;

  filterApplied: boolean = false;
  clearButtonEnabled: boolean = false;

  isFilterVisible: { [key: string]: boolean } = {};

  // ================= SAME PATTERN AS CATEGORY =================
  userFields: string[] = [
    'indust_name',
    'testdirectoryheadname',
    'testdirectoryname',
    'testdirectorydescription',
    'statusvalue'
  ];

  userUiFields: string[] = [
    'Industry Name',
    'Test Code',
    'Test Name',
    'Description',
    'Status'
  ];


  ngOnInit() {
    this.gettestdirectory();
    this.loading = false;
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.gettestdirectory();
    this.editenable = true;
    this.editdisplaytestdirectory = true;
    this.CDR.detectChanges();
  }

  /* =========================
     CHILD EVENTS
  ==========================*/

  onDataReloaded() {
    this.addenable = false;
    this.gettestdirectory();
    this.CDR.detectChanges();
  }

  backtomainChange(event: any) {
    if (event === false) {
      this.editdisplaytestdirectory = true;
      this.editenable = false;
      this.gettestdirectory();
      this.CDR.detectChanges();
    }
  }

  /* =========================
     DATE RANGE
  ==========================*/

  formatDateWithCustomTime(date: Date, hours: number, minutes: number, seconds: number): string {
    date.setHours(hours, minutes, seconds, 0);

    const year = date.getFullYear();
    const month = (`0${date.getMonth() + 1}`).slice(-2);
    const day = (`0${date.getDate()}`).slice(-2);
    const h = (`0${date.getHours()}`).slice(-2);
    const m = (`0${date.getMinutes()}`).slice(-2);
    const s = (`0${date.getSeconds()}`).slice(-2);

    return `${year}-${month}-${day} ${h}:${m}:${s}`;
  }

  onDateRangeSelected(range: any) {
    this.selectedRange = range;

    if (this.checkInputIsValid(this.selectedRange)) {
      this.startdate = this.formatDateWithCustomTime(new Date(this.selectedRange[0]), 0, 0, 0);
      this.enddate = this.formatDateWithCustomTime(new Date(this.selectedRange[1]), 23, 59, 59);

      this.onDataReloaded();
    }
  }

  /* =========================
     GLOBAL FILTER
  ==========================*/

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

    this.gettestdirectoryist.sort((a: any, b: any) => {

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

  /* =========================
     CRUD ACTIONS
  ==========================*/

  openAddDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.CDR.detectChanges();
  }

  closeAddtestdirectoryDialog() {
    this.display = false;
  }

  editscreen(testdirectorydata: any) {
    this.addenable = false;
    this.editenable = true;
    this.editdisplaytestdirectory = true;
    this.display = false;

    this.testdirectorydata = testdirectorydata;

    this.CDR.detectChanges();
  }

  /* =========================
     GET DATA
  ==========================*/

  gettestdirectory() {

    //const url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Gettestdirectory;

    const url = GlobalConstants.Authurl + GlobalConstants.Gettestdirectory;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.gettestdirectoryist = response['response']['ref1'];
        this.CDR.detectChanges();
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There is no data .');
      }
    });
  }

  /* =========================
     DELETE
  ==========================*/

  deleteaction(delid: any) {
    const input = {
      testdirectoryid: Number(delid),
      username: ''
    };

    const url = GlobalConstants.Authurl + GlobalConstants.Deletetestdirectory;

    this.dataService.addData(url,input).subscribe((response: any) => {
      if (response.status == 'success') {
        this.gettestdirectory();
        this.CDR.detectChanges();
        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Test Directory deleted successfully',
              life:     5000
            });
      }
      else {
        this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Test Directory',
              life:     5000
            });
        // this.notificationService.showMessage('error', 'Error', 'There is no data.');
      }
    });
  }

  exportToExcel(): void {
    // Get the table instance
    const table = this.dt1; // Make sure dt1 is a ViewChild reference

    // Get filtered data if available, otherwise fallback to all data
    const dataToExport = table.filteredValue ? table.filteredValue : this.gettestdirectoryist;

    // Prepare the data
    const dataWithHeaders = [
      this.userUiFields,  // header row
      ...dataToExport.map((row: any) => this.userFields.map(field => row[field]))
    ];

    // Create worksheet and workbook
    // const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(dataWithHeaders);
    // const wb: XLSX.WorkBook = XLSX.utils.book_new();
    // XLSX.utils.book_append_sheet(wb, ws, 'category');

    // // Export
    // XLSX.writeFile(wb, 'category.xlsx');
  }

}
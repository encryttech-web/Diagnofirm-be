import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  ViewChild
} from '@angular/core';

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

import { ConfigService } from '../services/config.service';
import { GlobalConstants } from '../services/global.constant';
import { ToastvalueService } from '../services/toastvalue.service';

import { ConfirmationService, MessageService, FilterService } from 'primeng/api';

import { NotificationService } from '../services/notification.service';
import { DataService } from '../services/data.service';
import { Addfaq } from './addfaq/addfaq';
import { Editfaq } from './editfaq/editfaq';

@Component({
  selector: 'app-faq',
  standalone: true,
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
    Addfaq,Editfaq
  ],
  templateUrl: './faq.html',
  styleUrl: './faq.scss',
  providers: [
    ConfirmationService,
    MessageService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Faq {

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

  // =========================
  // FLAGS
  // =========================
  loading: boolean = true;

  addenable: boolean = false;
  display: boolean = false;

  editenable: boolean = false;
  editdisplayfaq: boolean = false;

  // =========================
  // DATA
  // =========================
  faqList: any[] = [];
  faqdata: any;

  // =========================
  // FILTER
  // =========================
  globalFilter: string = '';
  filterApplied: boolean = false;
  clearButtonEnabled: boolean = false;

  currentFilter: any = {};
  currentSort: any = null;
  sortApplied: boolean = false;

  selectedRange: Date[] = [];
  startdate: any;
  enddate: any;

  isFilterVisible: { [key: string]: boolean } = {};

  // =========================
  // TABLE FIELDS
  // =========================
  faqFields: string[] = [
    'productname',
    'subcategoryname',
    'packagename',
    'faqquestion',
    'faqanswer',
    'faqhomecheck',
    'statusvalue',
  ];

  faqUiFields: string[] = [
    'Product Name',
    'Subcategory Name',
    'Package Name',
    'Question',
    'Answer',
    'Home Check',
    'Status',
  ];

  // =========================
  // INIT
  // =========================
  ngOnInit() {
    this.getFaqs();
    this.loading = false;
    this.CDR.detectChanges();
  }

  // =========================
  // RELOAD
  // =========================
  onDataReloaded() {
    this.addenable = false;
    this.getFaqs();
  }

  // =========================
  // EDIT CLOSE
  // =========================
  backtomainChange(event: any) {
    if (event === false) {
      this.editdisplayfaq = true;
      this.editenable = false;
      this.getFaqs();
      this.CDR.detectChanges();
    }
  }

  // =========================
  // FILTER
  // =========================
  onGlobalFilter(table: any, event: any) {

    const value = event.target.value;

    this.globalFilter = value;

    table.filterGlobal(value, 'contains');

    if (this.checkInputIsValid(this.globalFilter)) {
      this.filterApplied = true;
    }
    else {
      this.filterApplied = false;
    }
  }

  clearFilter(table: any) {
    this.globalFilter = '';
    table.clear();
    this.filterApplied = false;
  }

  onFilter(event: any) {

    const filterEntries: [
      string,
      [{ value: any; matchMode: string; operator: string }]
    ][] = Object.entries(event.filters);

    const filteredEntries = filterEntries.filter(([key, data]) => {
      return data[0].value !== null && data[0].value.trim() !== '';
    });

    if (filteredEntries.length > 0) {
      this.currentFilter = filteredEntries[0][1][0].value;
    }
    else {
      this.currentFilter = {};
    }

    if (Object.keys(this.currentFilter).length > 0) {
      this.clearButtonEnabled = true;
    }
    else {
      this.filterApplied = false;
      this.clearButtonEnabled = false;
    }
  }

  // =========================
  // SORT
  // =========================
  onSort(event: any) {

    if (this.checkInputIsValid(event.order)) {
      this.clearButtonEnabled = true;
    }
    else {
      this.clearButtonEnabled = false;
    }

    const field = event.field;
    const order = event.order;

    this.faqList.sort((a: any, b: any) => {

      if (a[field] < b[field]) {
        return order === 1 ? -1 : 1;
      }

      if (a[field] > b[field]) {
        return order === 1 ? 1 : -1;
      }

      return 0;
    });
  }

  // =========================
  // VALIDATION
  // =========================
  checkInputIsValid(inputValue: any) {

    if (
      inputValue !== undefined &&
      inputValue !== null &&
      inputValue !== ''
    ) {
      return true;
    }
    else {
      return false;
    }
  }

  // =========================
  // TOGGLE FILTER
  // =========================
  toggleSearchBox(field: string) {
    this.isFilterVisible[field] = !this.isFilterVisible[field];
  }

  // =========================
  // EXPORT
  // =========================
  exportToExcel(): void {

    const table = this.dt1;

    const dataToExport =
      table.filteredValue
        ? table.filteredValue
        : this.faqList;

    const dataWithHeaders = [
      this.faqUiFields,
      ...dataToExport.map((row: any) =>
        this.faqFields.map(field => row[field])
      )
    ];

    console.log(dataWithHeaders);
  }

  // =========================
  // OPEN ADD
  // =========================
  openAddFaqDialog() {

    this.addenable = true;
    this.editenable = false;
    this.display = true;

    this.CDR.detectChanges();
  }

  // =========================
  // CLOSE ADD
  // =========================
  closeAddFaqDialog() {
    this.display = false;
  }

  // =========================
  // GET FAQ
  // =========================
  getFaqs() {

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getfaq;

    this.dataService.getData(url).subscribe((response: any) => {

      if (response.status == 'success') {

        this.faqList = response['response']['ref1'];

        this.CDR.detectChanges();
      }
      else {

        this.notificationService.showMessage(
          'error',
          'Error',
          'No FAQ data found'
        );
      }

    });
  }

  // =========================
  // EDIT FAQ
  // =========================
  editFaq(faq: any) {

    this.addenable = false;

    this.editenable = true;

    this.editdisplayfaq = true;

    this.display = false;

    this.faqdata = faq;

    this.CDR.detectChanges();
  }

  // =========================
  // DELETE FAQ
  // =========================
  deleteFaq(id: any) {

    const input = {
      faqid: Number(id),
      username: ''
    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Deletefaq;

    this.dataService.addData(url, input).subscribe((response: any) => {

      if (response.status == 'success') {

        this.getFaqs();
        this.CDR.detectChanges();

        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'FAQ deleted successfully',
              life:     5000
            });

        // this.notificationService.showMessage(
        //   'success',
        //   'Deleted',
        //   'FAQ deleted successfully'
        // );
      }
      else {

      //  this.notificationService.showMessage('error', 'Error', 'There was a dependency on deleting the Faq');

      this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Faq',
              life:     5000
            });

        // this.notificationService.showMessage(
        //   'error',
        //   'Error',
        //   response.message
        // );
      }

    });
  }

  // =========================
  // CLEAR TABLE
  // =========================
  clear(table: any) {

    table.clear();

    this.filterApplied = false;
  }

}
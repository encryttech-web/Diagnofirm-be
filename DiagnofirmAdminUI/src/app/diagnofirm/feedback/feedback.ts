import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, ViewChild } from '@angular/core';
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
import { DataService } from '../services/data.service';
import { GlobalConstants } from '../services/global.constant';
import { NotificationService } from '../services/notification.service';
import { ToastvalueService } from '../services/toastvalue.service';
import { HttpService } from '@/layout/service/http.service';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { Addfeedback } from './addfeedback/addfeedback';
import { Editfeedback } from './editfeedback/editfeedback';

@Component({
  selector: 'app-feedback',
  imports: [TableModule,
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
    NgFor,Addfeedback,Editfeedback],
  templateUrl: './feedback.html',
  styleUrl: './feedback.scss',
  providers: [
    ConfirmationService,
    MessageService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Feedback {

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

  @ViewChild('dt1') dt1: any;
  @ViewChild('filter') filter!: ElementRef;

  // ================= FLAGS =================
  addenable: boolean = false;
  display: boolean = false;

  editenable: boolean = false;
  editdisplayfeedback: boolean = false;

  loading: boolean = true;

  // ================= DATA =================
  feedbackList: any;
  feedbackData: any;

  globalFilter: string = '';
  filterApplied: boolean = false;

  // ================= TABLE FIELDS =================
  userFields: string[] = [
    'username',
    'useremail',
    'userrole',
    'feedbackdesc',
    'starrating',
    'statusvalue'
  ];

  userUiFields: string[] = [
    'User Name',
    'User Email',
    'Role',
    'Description',
    'Rating',
    'Status'
  ];

  // ================= INIT =================
  ngOnInit() {
    this.getFeedback();
    this.loading = false;
    this.cdr.detectChanges();
  }

  ngOnChanges() {
    this.getFeedback();
    this.cdr.detectChanges();
  }

  // ================= GET ALL =================
  getFeedback() {

    const url = GlobalConstants.Authurl + GlobalConstants.Getfeedback;

    this.dataService.getData(url).subscribe((response: any) => {

      if (response.status == 'success') {
        this.feedbackList = response.response.ref1;
        this.cdr.detectChanges();
      }
      else {
        this.notificationService.showMessage(
          'error',
          'Error',
          'No feedback data found'
        );
      }

    });

  }

  // ================= GLOBAL FILTER =================
  onGlobalFilter(table: any, event: any) {
    const value = event.target.value;
    this.globalFilter = value;
    table.filterGlobal(value, 'contains');
    this.filterApplied = value?.length > 0;
  }

  clearFilter(table: any) {
    this.globalFilter = '';
    table.clear();
    this.filterApplied = false;
  }

  // ================= TABLE EVENTS =================
  onFilter(event: any) { }

  onSort(event: any) {
    const field = event.field;
    const order = event.order;

    this.feedbackList.sort((a: any, b: any) => {

      if (a[field] < b[field]) return order === 1 ? -1 : 1;
      if (a[field] > b[field]) return order === 1 ? 1 : -1;

      return 0;
    });

  }

  // ================= CRUD =================
  openAddDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.cdr.detectChanges();
  }

  editscreen(row: any) {
    this.addenable = false;
    this.editenable = true;
    this.editdisplayfeedback = true;
    this.feedbackData = row;
    this.cdr.detectChanges();
  }

  deleteaction(id: any) {

    const input = {
      feedbackid: Number(id),
      username: ''
    };

    const url = GlobalConstants.Authurl + GlobalConstants.Deletefeedback;

    this.dataService.addData(url, input).subscribe((res: any) => {

      if (res.status == 'success') {
        this.getFeedback();
        this.cdr.detectChanges();
        // this.notificationService.showMessage('success', 'Deleted', 'Feedback deleted');
        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Feedback deleted successfully',
              life:     5000
            });
      }
      else {
        // this.notificationService.showMessage('error', 'Error', 'Delete failed');
        this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Feedback',
              life:     5000
            });
      }

    });

  }

  // ================= CHILD EVENTS =================
  onDataReloaded() {
    this.addenable = false;
    this.getFeedback();
    this.cdr.detectChanges();
  }

  backToMain(event: any) {
    if (event === false) {
      this.editdisplayfeedback = false;
      this.editenable = false;
      this.getFeedback();
      this.cdr.detectChanges();
    }
  }

  // ================= CLEAR =================
  clear(table: any) {
    table.clear();
  }

  exportToExcel(): void {
    // Get the table instance
    const table = this.dt1; // Make sure dt1 is a ViewChild reference

    // Get filtered data if available, otherwise fallback to all data
    const dataToExport = table.filteredValue ? table.filteredValue : this.feedbackList;

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

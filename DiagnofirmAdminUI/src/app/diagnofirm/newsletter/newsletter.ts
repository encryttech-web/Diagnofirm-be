import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';

import { DataService } from '../services/data.service';
import { GlobalConstants } from '../services/global.constant';
import { NotificationService } from '../services/notification.service';
import { ConfigService } from '../services/config.service';

import { Addnewsletter } from './addnewsletter/addnewsletter';
import { Editnewsletter } from './editnewsletter/editnewsletter';
import { CustomerService } from '@/pages/service/customer.service';
import { ProductService } from '@/pages/service/product.service';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';

@Component({
  selector: 'app-newsletter',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    IconFieldModule,
    InputIconModule,
    NgIf,
    NgFor,
    Addnewsletter,
    Editnewsletter
  ],
  templateUrl: './newsletter.html',
  styleUrl: './newsletter.scss',
  providers: [
    ConfirmationService,
    MessageService,
    CustomerService,
    ProductService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Newsletter {

  constructor(
    private dataService: DataService,
    private CDR: ChangeDetectorRef,
    private router: Router,
    private notificationService: NotificationService,
    private CONFIGSERVICE: ConfigService,
    private messageService: MessageService
  ) { }

  @ViewChild('dt1') dt1: any;

  newsletterList: any[] = [];
  newsletterdata: any;

  addenable: boolean = false;
  display: boolean = false;

  editenable: boolean = false;
  editdisplaynewsletter: boolean = false;

  loading: boolean = true;

  globalFilter: string = '';
  filterApplied: boolean = false;

  newsletterFields: string[] = [
    'version_no',
    'letter_date',
    'letter_filename',
    'letter_imgname',
    'statusvalue'
  ];

  newsletterUiFields: string[] = [
    'Version',
    'Date',
    'File Name',
    'Image Name',
    'Status'
  ];

  ngOnInit() {
    this.getNewsletter();
    this.loading = false;
  }

  // ================= GET ALL =================
  getNewsletter() {

    // this.newsletterList = [
    //   {
    //     version_no: 10,
    //     letter_date: 'Mar-2026',
    //     letter_filename: 'New File',
    //     letter_imagename: 'New Image',
    //     statusvalue: 'Active',
    //     is_active:'1'
    //   }
    // ];

    let url = GlobalConstants.Authurl + GlobalConstants.Getnewsletter;

    this.dataService.getData(url).subscribe((response: any) => {

      if (response.status == 'success') {
        this.newsletterList = response['response']['ref1'];
        this.CDR.detectChanges();
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'No newsletter data found');
      }

    });
  }

  // ================= ADD =================
  openAddNewsletterDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.CDR.detectChanges();
  }

  onDataReloaded() {
    this.addenable = false;
    this.getNewsletter();
  }

  // ================= EDIT =================
  editNewsletter(data: any) {
    this.editenable = true;
    this.addenable = false;
    this.editdisplaynewsletter = true;
    this.newsletterdata = data;
    this.CDR.detectChanges();
  }

  backToMain(event: any) {
    if (event === false) {
      this.editdisplaynewsletter = false;
      this.editenable = false;
      this.getNewsletter();
      this.CDR.detectChanges();
    }
  }

  // ================= DELETE =================
  deleteNewsletter(id: any) {

    const input = {
      nid: Number(id),
      username: ''
    };

    let url = GlobalConstants.Authurl + GlobalConstants.Deletenewsletter;

    this.dataService.addData(url, input).subscribe((response: any) => {

      if (response.status == 'success') {
        this.getNewsletter();
        this.CDR.detectChanges();
        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Newsletter deleted successfully',
              life:     5000
            });
        // this.notificationService.showMessage('success', 'Deleted', 'Newsletter deleted successfully');
      }
      else {
        this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Newsletter',
              life:     5000
            });
        // this.notificationService.showMessage('error', 'Error', response.message);
      }

    });
  }

  // ================= FILTER =================
  onGlobalFilter(table: any, event: any) {
    const value = event.target.value;
    this.globalFilter = value;
    table.filterGlobal(value, 'contains');

    this.filterApplied = value ? true : false;
  }

  clear(table: any) {
    table.clear();
    this.filterApplied = false;
    this.globalFilter = '';
  }

  clearFilter(table: any) {
    this.globalFilter = '';
    table.clear();
    this.filterApplied = false;
  }

  exportToExcel(): void {
    // Get the table instance
    const table = this.dt1; // Make sure dt1 is a ViewChild reference

    // Get filtered data if available, otherwise fallback to all data
    const dataToExport = table.filteredValue ? table.filteredValue : this.newsletterList;

    // Prepare the data
    const dataWithHeaders = [
      this.newsletterUiFields,   // header row
      ...dataToExport.map((row: any) => this.newsletterFields.map(field => row[field]))
    ];

    // Create worksheet and workbook
    // const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(dataWithHeaders);
    // const wb: XLSX.WorkBook = XLSX.utils.book_new();
    // XLSX.utils.book_append_sheet(wb, ws, 'subcategory');

    // // Export
    // XLSX.writeFile(wb, 'subcategory.xlsx');
  }
}
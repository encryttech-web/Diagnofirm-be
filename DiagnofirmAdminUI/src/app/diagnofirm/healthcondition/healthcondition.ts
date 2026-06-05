import { Component, ChangeDetectorRef, ViewChild, CUSTOM_ELEMENTS_SCHEMA, ElementRef } from '@angular/core';
import { HttpService } from '@/layout/service/http.service';
import { ConfigService } from '../services/config.service';
import { GlobalConstants } from '../services/global.constant';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
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
import { Addsubcategory } from '../subcategory/addsubcategory/addsubcategory';
import { Editsubcategory } from '../subcategory/editsubcategory/editsubcategory';
import { CustomerService } from '@/pages/service/customer.service';
import { ProductService } from '@/pages/service/product.service';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-healthcondition',
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
    ToggleSwitchModule, NgIf, NgFor],
  templateUrl: './healthcondition.html',
  styleUrl: './healthcondition.scss',
  providers: [ConfirmationService, MessageService, CustomerService, ProductService, NotificationService, FilterService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Healthcondition {

  constructor(
    private http: HttpService,
    private cdr: ChangeDetectorRef,
    private config: ConfigService
  ) { }

  @ViewChild('dt1') dt1: any;

  addenable = false;
  editenable = false;

  gethealthconditionlist: any;
  healthdata: any;

  floatValue: any = null;
  getsubcategoryist: any;
  loading: boolean = true;
  selectedProduct: any = null;

  clearButtonEnabled: boolean = false;
  currentFilter: any = {};
  currentSort: any = null;
  filterApplied: boolean = false;
  sortApplied: boolean = false;

  globalFilter: string = '';
  selectedRange: Date[] = [];
  startdate: any;
  enddate: any;

  @ViewChild('filter') filter!: ElementRef;
  data: any;
  subcategorydata: any;
  isFilterVisible: { [key: string]: boolean } = {};

  userdatavalue: any[] = [];
  userid: string = '';

  // 👉 TABLE COLUMNS
  healthFields: string[] = [
    'categoryid',
    'subcategoryid',
    'healthconditioncode',
    'healthconditionname',
    'healthconditiondesc',
    'healthconditionord',
    'status'
  ];

  healthUiFields: string[] = [
    'Category',
    'Subcategory',
    'Healthcondition Code',
    'Healthcondition Name',
    'Healthcondition Description',
    'Healthcondition Order',
    'Status'
  ];

  ngOnInit() {
    this.getHealthCondition();
    this.loading = false;
  }

  // ================= GET =================
  getHealthCondition() {

    const input = {
      userid: '',
    };

    //const url = this.config.getApi('AUTH_URL') + GlobalConstants.GetHealthCondition;

    // demo data
    this.gethealthconditionlist = [
      {
        id: 1,
        categoryid:'1',
        category:'Healthcondition',
        subcategoryid:'Blood',
        healthconditioncode: 'HC001',
        healthconditionname: 'Diabetes',
        healthconditiondesc: 'Blood sugar condition',
        healthconditionord: '1',
        username:'Anitha',
        status:'Active'
      }
    ];

    this.cdr.detectChanges();
  }

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

    const filterEntries: [string, [{ value: any; matchMode: string; operator: string }]][] = Object.entries(event.filters);
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
      //this.filterApplied = true;
      this.clearButtonEnabled = true;
    }
    else {
      this.filterApplied = false;
      this.clearButtonEnabled = false;
    }



  }

  onSort(event: any) {
    if (this.checkInputIsValid(event.order)) {
      this.clearButtonEnabled = true;
    }
    else {
      this.clearButtonEnabled = false;
    }

    const field = event.field;  // The field that is being sorted
    const order = event.order;

    this.getsubcategoryist.sort((a: any, b: any) => {
      if (a[field] < b[field]) {
        return order === 1 ? -1 : 1; // Ascending or descending
      }
      if (a[field] > b[field]) {
        return order === 1 ? 1 : -1; // Ascending or descending
      }
      return 0;
    });
  }

  clear(table: any) {
    table.clear();
    this.clearButtonEnabled = false;
  }

  checkInputIsValid(inputValue: any) {
    if (inputValue !== undefined && inputValue !== null && inputValue !== '')
      return true;
    else
      return false;
  }

  toggleSearchBox(field: string) {
    this.isFilterVisible[field] = !this.isFilterVisible[field];
  }
  exportToExcel(): void {
    // Get the table instance
    const table = this.dt1; // Make sure dt1 is a ViewChild reference

    // Get filtered data if available, otherwise fallback to all data
    const dataToExport = table.filteredValue ? table.filteredValue : this.getsubcategoryist;

    // Prepare the data
    const dataWithHeaders = [
      this.healthUiFields,  // header row
      ...dataToExport.map((row: any) => this.healthFields.map(field => row[field]))
    ];

    // Create worksheet and workbook
    // const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(dataWithHeaders);
    // const wb: XLSX.WorkBook = XLSX.utils.book_new();
    // XLSX.utils.book_append_sheet(wb, ws, 'subcategory');

    // // Export
    // XLSX.writeFile(wb, 'subcategory.xlsx');
  }

  // ================= ADD =================
  openAdd() {
    this.addenable = true;
    this.editenable = false;
    this.cdr.detectChanges();
  }

  // ================= EDIT =================
  editHealth(row: any) {
    this.editenable = true;
    this.addenable = false;
    this.healthdata = row;
    this.cdr.detectChanges();
  }

  backToMain(event: any) {
    if (event === false) {
      this.editenable = false;
      this.getHealthCondition();
      this.cdr.detectChanges();
    }
  }

  // ================= DELETE =================
  deleteHealth(id: any) {

    const input = {
      delid: id
    };

    //const url = this.config.getApi('AUTH_URL') + GlobalConstants.DeleteHealthCondition;

    // this.http.post(url, input).subscribe(() => {
    //   this.getHealthCondition();
    // });

    this.getHealthCondition(); // demo refresh
  }

  onDataReloaded() {
    this.addenable = false;
    this.getHealthCondition();
  }

}
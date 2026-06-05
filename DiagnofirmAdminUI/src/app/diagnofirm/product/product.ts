import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
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
import { CustomerService } from '@/pages/service/customer.service';
import { ProductService } from '@/pages/service/product.service';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';
import { DataService } from '../services/data.service';
import { Addproduct } from './addproduct/addproduct';
import { Editproduct } from './editproduct/editproduct';

@Component({
  selector: 'app-product',
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
    Addproduct,
    Editproduct
  ],
  templateUrl: './product.html',
  styleUrl: './product.scss',
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
export class Product {

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

  addenable: boolean = false;
  display: boolean = false;
  editenable: boolean = false;
  editdisplayproduct: boolean = false;

  loading: boolean = true;

  productList: any[] = [];
  productdata: any;

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

  productFields: string[] = [
    'categoryname',
    'subcategoryname',
    'productheadname',
    'productcode',
    'productname',
    'productprice',
    'productdescription',
    'statusvalue'
  ];

  productUiFields: string[] = [
    'Category',
    'Subcategory',
    'Product Head',
    'Product Code',
    'Product Name',
    'Price',
    'Product Description',
    'Status'
  ];

  ngOnInit() {
    this.getProducts();
    this.loading = false;
    this.CDR.detectChanges();
  }

  backtomainChange(event: any) {
    if (event === false) {
      this.editdisplayproduct = true;
      this.editenable = false;
      this.getProducts();
      this.CDR.detectChanges();
    }
  }

  onDataReloaded() {
    this.addenable = false;
    this.getProducts();
  }
  formatDateWithCustomTime(date: Date, hours: number, minutes: number, seconds: number): string {
    date.setHours(hours, minutes, seconds, 0); // set time
    const year = date.getFullYear();
    const month = (`0${date.getMonth() + 1}`).slice(-2);
    const day = (`0${date.getDate()}`).slice(-2);
    const h = (`0${date.getHours()}`).slice(-2);
    const m = (`0${date.getMinutes()}`).slice(-2);
    const s = (`0${date.getSeconds()}`).slice(-2);

    return `${year}-${month}-${day} ${h}:${m}:${s}`;
  }

  // onDateRangeSelected(range: any) {

  //   this.selectedRange = range;

  //   if (this.checkInputIsValid(this.selectedRange)) {
  //     this.startdate = this.formatDateWithCustomTime(new Date(this.selectedRange[0]), 0, 0, 0);
  //     this.enddate = this.formatDateWithCustomTime(new Date(this.selectedRange[1]), 23, 59, 59);

  //     this.onDataReloaded();
  //   }
  // }

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

    this.productList.sort((a: any, b: any) => {
      if (a[field] < b[field]) {
        return order === 1 ? -1 : 1; // Ascending or descending
      }
      if (a[field] > b[field]) {
        return order === 1 ? 1 : -1; // Ascending or descending
      }
      return 0;
    });
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
    const dataToExport = table.filteredValue ? table.filteredValue : this.productList;

    // Prepare the data
    const dataWithHeaders = [
      this.productUiFields,   // header row
      ...dataToExport.map((row: any) => this.productFields.map(field => row[field]))
    ];

    // Create worksheet and workbook
    // const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(dataWithHeaders);
    // const wb: XLSX.WorkBook = XLSX.utils.book_new();
    // XLSX.utils.book_append_sheet(wb, ws, 'subcategory');

    // // Export
    // XLSX.writeFile(wb, 'subcategory.xlsx');
  }


  openAddproductDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.CDR.detectChanges();
  }

  closeAddproductDialog() {
    this.display = false;
  }


  getProducts() {

    let url = GlobalConstants.Authurl + GlobalConstants.Getproduct;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.productList = response['response']['ref1'];
        this.CDR.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'No product data found');
      }
    });
  }

  openAddProductDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.CDR.detectChanges();
  }

  editProduct(product: any) {
    this.addenable = false;
    this.editenable = true;
    this.editdisplayproduct = true;
    this.display = false;
    this.productdata = product;
    this.CDR.detectChanges();
  }

  deleteProduct(id: any) {
    const input = {
      productid: Number(id),
      username:''
    };

    let url = GlobalConstants.Authurl + GlobalConstants.Deleteproduct;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {
        this.getProducts();
        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Product deleted successfully',
              life:     5000
            });
        // this.notificationService.showMessage('success', 'Deleted', 'Product deleted successfully');
      } else {
        this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the Product',
              life:     5000
            });
         //this.notificationService.showMessage('error', 'Error', 'There was a dependency on deleting the Product');

        //  this.notificationService.showMessage('error', 'Error', response.message); --
      }
    });
  }

  // onGlobalFilter(table: any, event: any) {
  //   const value = event.target.value;
  //   this.globalFilter = value;
  //   table.filterGlobal(value, 'contains');

  //   this.filterApplied = value ? true : false;
  // }

  clear(table: any) {
    table.clear();
    this.filterApplied = false;
  }

  // onSort(event: any) {
  //   const field = event.field;
  //   const order = event.order;

  //   this.productList.sort((a: any, b: any) => {
  //     if (a[field] < b[field]) return order === 1 ? -1 : 1;
  //     if (a[field] > b[field]) return order === 1 ? 1 : -1;
  //     return 0;
  //   });
  // }
}
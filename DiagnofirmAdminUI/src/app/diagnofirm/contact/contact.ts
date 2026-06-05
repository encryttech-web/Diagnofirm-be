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
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';
import { Addcontact } from './addcontact/addcontact';
import { Editcontact } from './editcontact/editcontact';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-contact',
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
    Addcontact,
    Editcontact,
  ],
  templateUrl: './contact.html',
  styleUrl: './contact.scss',
  providers: [ConfirmationService, MessageService, NotificationService, FilterService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class Contact {
  constructor(
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private navigation: Router,
    private toasterService: ToastvalueService,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private dataService: DataService,
    private messageService: MessageService
  ) {}

  @ViewChild('dt1') dt1: any;

  addenable: boolean = false;
  display: boolean = false;
  editenable: boolean = false;
  editdisplaycontact: boolean = false;

  getcontactlist: any;
  loading: boolean = true;

  clearButtonEnabled: boolean = false;
  currentFilter: any = {};
  filterApplied: boolean = false;

  globalFilter: string = '';

  @ViewChild('filter') filter!: ElementRef;
  contactdata: any;
  isFilterVisible: { [key: string]: boolean } = {};

  userdatavalue: any[] = [];
  userid: string = '';

  userFields: string[] = [
    'conttyp_name',
    'cont_name',
    'cont_city',
    'cont_state',
    'cont_country',
    'cont_phno',
    'cont_email',
    'statusvalue',
  ];
  userUiFields: string[] = [
    'Contact Type',
    'Contact Name',
    'City',
    'State',
    'Country',
    'Phone No',
    'Email',
    'Head office',
  ];

  ngOnInit() {
    this.getcontact();
    this.loading = false;
    this.clearButtonEnabled = false;
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getcontact();
    this.loading = false;
    this.editenable = true;
    this.editdisplaycontact = true;
    this.clearButtonEnabled = false;
    this.CDR.detectChanges();
  }

  backtomainChange(event: any) {
    if (event === false) {
      this.editdisplaycontact = true;
      this.editenable = false;
      this.getcontact();
      this.CDR.detectChanges();
    }
  }

  onDataReloaded() {
    this.addenable = false;
    this.getcontact();
  }

  onGlobalFilter(table: any, event: any) {
    const value = event.target.value;
    this.globalFilter = value;
    table.filterGlobal(value, 'contains');
    this.filterApplied = this.checkInputIsValid(this.globalFilter);
  }

  clearFilter(table: any) {
    this.globalFilter = '';
    table.clear();
    this.filterApplied = false;
  }

  onFilter(event: any) {
    const filterEntries: [string, [{ value: any; matchMode: string; operator: string }]][] =
      Object.entries(event.filters);
    const filteredEntries = filterEntries.filter(([key, data]) => {
      return data[0].value !== null && data[0].value.trim() !== '';
    });

    if (filteredEntries.length > 0) {
      this.currentFilter = filteredEntries[0][1][0].value;
    } else {
      this.currentFilter = {};
    }

    this.clearButtonEnabled = Object.keys(this.currentFilter).length > 0;
    if (!this.clearButtonEnabled) {
      this.filterApplied = false;
    }
  }

  onSort(event: any) {
    this.clearButtonEnabled = this.checkInputIsValid(event.order);

    const field = event.field;
    const order = event.order;

    this.getcontactlist.sort((a: any, b: any) => {
      if (a[field] < b[field]) return order === 1 ? -1 : 1;
      if (a[field] > b[field]) return order === 1 ? 1 : -1;
      return 0;
    });
  }

  clear(table: any) {
    table.clear();
    this.clearButtonEnabled = false;
  }

  checkInputIsValid(inputValue: any) {
    return inputValue !== undefined && inputValue !== null && inputValue !== '';
  }

  toggleSearchBox(field: string) {
    this.isFilterVisible[field] = !this.isFilterVisible[field];
  }

  exportToExcel(): void {
    const table = this.dt1;
    const dataToExport = table.filteredValue ? table.filteredValue : this.getcontactlist;
    const dataWithHeaders = [
      this.userUiFields,
      ...dataToExport.map((row: any) => this.userFields.map((field) => row[field])),
    ];
    // Implement XLSX export here if needed
  }

  openAddContactDialog() {
    this.addenable = true;
    this.editenable = false;
    this.display = true;
    this.CDR.detectChanges();
  }

  closeAddContactDialog() {
    this.display = false;
  }

  editscreen(contactdata: any) {
    this.addenable = false;
    this.editenable = true;
    this.editdisplaycontact = true;
    this.display = false;
    this.contactdata = contactdata;
    this.CDR.detectChanges();
  }

  getcontact() {
    let url = GlobalConstants.Authurl + GlobalConstants.Getcontact;

    this.dataService.getData(url).subscribe((response: any) => {
      console.log(response);
      if (response.status == 'success') {
        this.getcontactlist = response['response']['ref1'];
        this.CDR.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'There is no data.');
      }
    });
  }

  deleteaction(delid: any) {
    const input = {
      contactid: Number(delid),
      username: this.userid,
    };

    let url = GlobalConstants.Authurl + GlobalConstants.Deletecontact;

    this.dataService.addData(url, input).subscribe((response: any) => {
      console.log(response);
      if (response.status == 'success') {
        this.getcontact();
        this.messageService.add({
              severity: 'success',
              summary:  'Thank You!',
              detail:   'Contact deleted successfully',
              life:     5000
            });
        // this.notificationService.showMessage(
        //   'success',
        //   'Contact Deleted',
        //   'The contact has been successfully deleted!',
        // );
        this.CDR.detectChanges();
      } else {
        this.messageService.add({
              severity: 'error',
              summary:  'Thank You!',
              detail:   'There was a dependency on deleting the contact.',
              life:     5000
            });
        // this.notificationService.showMessage(
        //   'error',
        //   'Error',
        //   'There was a dependency on deleting the contact.',
        // );
      }
    });
  }
}
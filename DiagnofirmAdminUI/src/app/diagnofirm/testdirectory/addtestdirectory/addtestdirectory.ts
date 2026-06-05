import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
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
import { EditorModule } from 'primeng/editor';

@Component({
  selector: 'app-addtestdirectory',
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
    EditorModule
  ],
  templateUrl: './addtestdirectory.html',
  styleUrl: './addtestdirectory.scss'
})
export class Addtestdirectory {

  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded: EventEmitter<any> = new EventEmitter();

  checked: boolean = true;
  calendarValue: any = null;

  userdatavalue: any[] = [];
  userid: string = '';

  testdirectoryhead: string = '';
  testdirectorycode: string = '';
  testdirectoryname: string = '';
  specimen: string = '';
  unit: string = '';
  refrange: string = '';
  testdescription: string = '';
  testorder: string = '';

  public userForm = new FormGroup({
    fctetdir_name: new FormControl("", [Validators.required]),
    fctetdir_code: new FormControl("", [Validators.required]),
    fcstatus: new FormControl("1", [Validators.required]),
  });
  Lastcode: any;
  gettestdirectoryindustryist: any[] = [];
  //testdirectorycode: string;
  industry: any = null;

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
  ) {}

  ngOnInit() {
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;

    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }

    //this.testdirectorycode = this.generateNextCode('TD-0000');
    
    this.getlastcode('diafrm', 'testdirectory_tbl', 'tetdir_code');
    this.gettestdirectoryindustry(); // ✅ Call on init
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getlastcode('diafrm', 'testdirectory_tbl', 'tetdir_code');
    this.gettestdirectoryindustry(); // ✅ Call on init
    this.CDR.detectChanges();
  }

  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

   generateNextCode(lastCode: string): string {
    const prefix = 'TES';

    if (!lastCode) {
      return `${prefix}-0001`;
    }

    // Step 1: Extract numeric part
    const lastNumber = parseInt(lastCode.split('-')[1], 10);

    // Step 2: Increment
    const nextNumber = lastNumber + 1;

    // Step 3: Format with leading zeros (4 digits)
    const formatted = nextNumber.toString().padStart(4, '0');

    return `${prefix}-${formatted}`;
  }


  getlastcode(schemaname: any, tablename: any, columnname: any) {

    const input = {
      schemaname: schemaname,
      tablename: tablename,
      columnname: columnname
    }

    let url = GlobalConstants.Authurl + GlobalConstants.Getlastcode;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {

        const value = response?.response?.ref1?.[0]?.ref1 ?? 'TES-0000';
        this.Lastcode = value;
        this.testdirectorycode = this.generateNextCode(this.Lastcode);

      }
    });
  }

  gettestdirectoryindustry() {

    //const url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Gettestdirectory;

    const url = GlobalConstants.Authurl + GlobalConstants.GettestdirectoryIndustry;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.gettestdirectoryindustryist = response['response']['ref1'];
        this.CDR.detectChanges();
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There is no data .');
      }
    });
  }

  addbtn(categoryForm: NgForm) {

  if (!categoryForm.valid) {
    this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
    return;
  }

  // ✅ Validate industry separately (p-select doesn't hook into NgForm)
  if (!this.industry) {
    this.notificationService.showMessage('error', 'Validation', 'Please select an Industry.');
    return;
  }

  const input = {
    industryid: this.industry,   
    testdirectoryhead: this.testdirectoryhead,
    testdirectorycode: this.testdirectorycode,
    testdirectoryname: this.testdirectoryname,
    specimen: this.specimen,
    unit: this.unit,
    refrange: this.refrange,
    testdescription: this.testdescription,
    testorder: this.testorder,
    createdby: this.userid,
    status: this.checked ? '1' : '0',
  };

  let url = GlobalConstants.Authurl + GlobalConstants.Addtestdirectory;

  this.dataService.addData(url, input).subscribe((response: any) => {
    if (response.status == 'success') {
      this.close();
      this.notificationService.showMessage('success', 'Added', 'The test directory data has been successfully added!');
    }
    else if (response.status == 'information') {
      this.notificationService.showMessage('error', 'Error', 'The data already exists.');
    }
    else {
      this.notificationService.showMessage('error', 'Error', 'There was an issue adding the data.');
    }
  });
}

 clear() {
  this.testdirectoryhead = '';
  this.testdirectorycode = '';
  this.testdirectoryname = '';
  this.industry = null;              // ✅ Clear industry
  this.specimen = '';
  this.unit = '';
  this.refrange = '';
  this.testdescription = '';
  this.testorder = '';
  this.checked = false;
  this.calendarValue = null;
}
}